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
    public class SitecoreSerializationDiffGenerator : ISitecoreSerializationDiffGenerator
    {
        private readonly IItemsToDeleteSettings _itemsToDeleteSettings;

        public SitecoreSerializationDiffGenerator(IItemsToDeleteSettings itemsToDeleteSettings)
        {
            _itemsToDeleteSettings = itemsToDeleteSettings;
        }

        private void RemoveUnWantedItems(string sourcePath)
        {
            foreach (var path in _itemsToDeleteSettings.PathsToRemove)
            {
                var targetDir = string.Concat(sourcePath, path);
                try
                {
                    if (Directory.Exists(targetDir))
                    {
                        Console.WriteLine("About to delete: " + targetDir);
                        Directory.Delete(targetDir, true);
                    }
                    else
                    {
                        Console.WriteLine("Skipping: " + targetDir);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting: " + targetDir + "\r\n ------" + ex);
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
            var sourceDataIterator = sourceManager.ItemIterator;
            var targetDataIterator = targetManager.ItemIterator;

            var engine = new DataEngine();

            var commands = new List<ICommand>();
            commands.AddRange(GenerateDiff(sourceDataIterator, targetDataIterator));
            engine.ProcessCommands(ref commands);
            return commands;
        }

        private IEnumerable<ICommand> GenerateDiff(IDataIterator sourceIterator, IDataIterator targetIterator)
        {
            var commands = new List<ICommand>();
            var sourceDataItem = sourceIterator.Next();
            var targetDataItem = targetIterator.Next();

            while (sourceDataItem != null || targetDataItem != null)
            {
                var compareResult = Compare(sourceDataItem, targetDataItem);
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
