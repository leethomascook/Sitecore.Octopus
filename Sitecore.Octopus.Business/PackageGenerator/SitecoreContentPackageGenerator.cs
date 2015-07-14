using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Sitecore.Octopus.Business.Contracts;
using Sitecore.Octopus.Business.Domain;
using Sitecore.Update;
using Sitecore.Update.Interfaces;

namespace Sitecore.Octopus.Business.PackageGenerator
{
    public class SitecoreContentPackageGenerator
    {
        private readonly ISitecoreSerilizationDiffGenerator _sitecoreSerilizationDiffGenerator;
        private readonly string PACKAGE_NAME = "GeneratedContentPackage.update";
   

        public SitecoreContentPackageGenerator(ISitecoreSerilizationDiffGenerator sitecoreSerilizationDiffGenerator)
        {
            _sitecoreSerilizationDiffGenerator = sitecoreSerilizationDiffGenerator;
        }

        public ArtifactDetails CreateArtifacts(string sourcePath, string targetPath)
        {
            var commands = _sitecoreSerilizationDiffGenerator.GetDiffCommands(sourcePath, targetPath);
            var diff = new DiffInfo(commands, "Sitecore Courier Package", string.Empty,  string.Format("Diff between folders '{0}' and '{1}'", sourcePath, targetPath));
            Update.Engine.PackageGenerator.GeneratePackage(diff, string.Empty, PACKAGE_NAME);

            var jsonFile = new ItemsToPublishGenerator().CreateItemsToPublishFile(commands,new string[]{"web"}, new string[]{"en"});

            var artifactDetails = new ArtifactDetails()
            {
                ContentPackageFilePath = PACKAGE_NAME,
                ItemsToPublishFilePath = jsonFile
            };

            return artifactDetails;
        }

        public string CreateItemsToPublishFile(string sourcePath, string targetPath, string outputPath, string[] targetDatabases, string[] targetLanguages)
        {
            var commands = _sitecoreSerilizationDiffGenerator.GetDiffCommands(sourcePath, targetPath);
            return new ItemsToPublishGenerator().CreateItemsToPublishFile(commands, targetDatabases, targetLanguages, outputPath);
        }

    }

    public class ItemsToPublishGenerator
    {
      
        public string CreateItemsToPublishFile(IEnumerable<ICommand> commands, string[] targetDatabases, string[] targetLanguages, string filePath = "ItemsToPublish.json")
        {
            if (commands != null && commands.Any())
            {
                var publishableItemsGenerator = new PublishableItemsGenerator();
                var itemsToPublish = publishableItemsGenerator.FindItemsToPublishFromCommands(commands);

                var model = new ItemsToPublishModel();
                model.Items = itemsToPublish.Select(x => x.Id).ToList();
                model.TargetDatabases = targetDatabases;
                model.TargetLanguages = targetLanguages;
                var json = JsonConvert.SerializeObject(model);
                File.WriteAllText(filePath, json);
                return filePath;
            }

            return "";
        }


    }

    public class ItemsToPublishModel
    {
        public List<Guid> Items { get; set; }
        public string[] TargetDatabases { get; set; }
        public string[] TargetLanguages { get; set; }
    }
}
