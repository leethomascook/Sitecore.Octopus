using System;
using System.Collections.Generic;
using System.IO;
using Sitecore.Octopus.Business.Contracts;
using Sitecore.Update.Configuration;
using Sitecore.Update.Data;
using Sitecore.Update.Interfaces;

namespace Sitecore.Octopus.Business.PackageGenerator
{
    /* This is stolen from sitecore Courier.  */
    public class SitecoreSerilizationDiffGenerator : ISitecoreSerilizationDiffGenerator
    {
        private readonly IItemsToDeleteSettings _itemsToDeleteSettings;

        public SitecoreSerilizationDiffGenerator(IItemsToDeleteSettings itemsToDeleteSettings)
        {
            _itemsToDeleteSettings = itemsToDeleteSettings;
        }

        private void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        private void RemoveUnWantedItems(string sourcePath)
        {
            if (_itemsToDeleteSettings.PathsToRemove != null)
            {
                foreach (var path in _itemsToDeleteSettings.PathsToRemove)
                {
                    try
                    {
                        Console.WriteLine("About to delete: " + string.Concat(sourcePath, path));
                        DeleteDirectory(string.Concat(sourcePath, path));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error delete this folder" + string.Concat(sourcePath, path) + " ------" + ex);
                    }
                }
            }
        }

        public List<ICommand> GetDiffCommands(string sourcePath, string targetPath)
        {
            Console.WriteLine("The source path is: " + sourcePath); 
            Console.WriteLine("The Target path is: " + targetPath);
            RemoveUnWantedItems(sourcePath);
            RemoveUnWantedItems(targetPath);

            var targetManager = Factory.Instance.GetSourceDataManager();
            var sourceManager = Factory.Instance.GetTargetDataManager();

            sourceManager.SerializationPath = sourcePath;
            targetManager.SerializationPath = targetPath;
            IDataIterator sourceDataIterator = sourceManager.ItemIterator;
            IDataIterator targetDataIterator = targetManager.ItemIterator;

            var engine = new DataEngine();

            var commands = new List<ICommand>();
            commands.AddRange(GenerateDiff(sourceDataIterator, targetDataIterator));
            engine.ProcessCommands(ref commands);
            return commands;
        }

        private IEnumerable<ICommand> GenerateDiff(IDataIterator sourceIterator, IDataIterator targetIterator)
        {
            var commands = new List<ICommand>();
            IDataItem sourceDataItem = sourceIterator.Next();
            IDataItem targetDataItem = targetIterator.Next();

            while (sourceDataItem != null || targetDataItem != null)
            {
                int compareResult = Compare(sourceDataItem, targetDataItem);
                commands.AddRange((sourceDataItem ?? targetDataItem).GenerateDiff(sourceDataItem, targetDataItem, compareResult));
                if (compareResult < 0)
                {
                    sourceDataItem = sourceIterator.Next();
                }
                else
                {
                    if (compareResult > 0)
                    {
                        targetDataItem = targetIterator.Next();
                    }
                    else
                    {
                        if (compareResult == 0)
                        {
                            targetDataItem = targetIterator.Next();
                            sourceDataItem = sourceIterator.Next();
                        }
                    }
                }
            }

            return commands;
        }

        private int Compare(IDataItem sourceItem, IDataItem targetItem)
        {
            if (sourceItem == null)
            {
                return 1;
            }

            if (targetItem == null)
            {
                return -1;
            }

            return sourceItem.CompareTo(targetItem);
        }
    }
}
