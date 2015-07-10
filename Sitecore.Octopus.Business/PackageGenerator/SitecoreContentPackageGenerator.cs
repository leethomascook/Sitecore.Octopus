using System.Collections.Generic;
using System.IO;
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

            var jsonFile = new ItemsToPublishGenerator().CreateItemsToPublishFile(commands);

            var artifactDetails = new ArtifactDetails()
            {
                ContentPackageFilePath = PACKAGE_NAME,
                ItemsToPublishFilePath = jsonFile
            };

            return artifactDetails;
        }

        public string CreateItemsToPublishFile(string sourcePath, string targetPath, string outputPath)
        {
            var commands = _sitecoreSerilizationDiffGenerator.GetDiffCommands(sourcePath, targetPath);
            return new ItemsToPublishGenerator().CreateItemsToPublishFile(commands);
        }

    }

    public class ItemsToPublishGenerator
    {
      
        public string CreateItemsToPublishFile(IEnumerable<ICommand> commands, string filePath = "ItemsToPublish.json")
        {
            var publishableItemsGenerator = new PublishableItemsGenerator();
            var itemsToPublish = publishableItemsGenerator.FindItemsToPublishFromCommands(commands);
            var json = JsonConvert.SerializeObject(itemsToPublish);
            File.WriteAllText(filePath, json);
            return filePath;
        }


    }
}
