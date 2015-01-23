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
        private readonly string ITEM_TO_PUBLISH_FILE = "ItemsToPublish.json";
        private readonly string PACKAGE_NAME = "GeneratedContentPackage.update";
        private readonly ISitecoreSerializationDiffGenerator _sitecoreSerializationDiffGenerator;

        public SitecoreContentPackageGenerator(ISitecoreSerializationDiffGenerator sitecoreSerializationDiffGenerator)
        {
            _sitecoreSerializationDiffGenerator = sitecoreSerializationDiffGenerator;
        }

        public ArtifactDetails CreateArtifacts(string sourcePath, string targetPath)
        {
            var commands = _sitecoreSerializationDiffGenerator.GetDiffCommands(sourcePath, targetPath);
            var diff = new DiffInfo(commands, "Sitecore Courier Package", string.Empty,
                string.Format("Diff between folders '{0}' and '{1}'", sourcePath, targetPath));
            Update.Engine.PackageGenerator.GeneratePackage(diff, string.Empty, PACKAGE_NAME);

            CreateItemsToPublishFile(commands);

            var artifactDetails = new ArtifactDetails
                                  {
                                      ContentPackageFilePath = PACKAGE_NAME,
                                      ItemsToPublishFilePath = ITEM_TO_PUBLISH_FILE
                                  };

            return artifactDetails;
        }

        private void CreateItemsToPublishFile(IEnumerable<ICommand> commands)
        {
            var publishableItemsGenerator = new PublishableItemsGenerator();
            var itemsToPublish = publishableItemsGenerator.FindItemsToPublishFromCommands(commands);
            var json = JsonConvert.SerializeObject(itemsToPublish, Formatting.Indented);
            File.WriteAllText(ITEM_TO_PUBLISH_FILE, json);
        }
    }
}