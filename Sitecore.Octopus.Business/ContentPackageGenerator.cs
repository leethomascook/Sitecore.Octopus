using System.IO;
using System.IO.Compression;
using Sitecore.Octopus.Business.Contracts;
using Sitecore.Octopus.Business.Domain;
using Sitecore.Octopus.Business.PackageGenerator;
using Sitecore.Octopus.Business.Settings;
using Sitecore.Octopus.Business.Stratergies;

namespace Sitecore.Octopus.Business
{
    public class ContentPackageGenerator
    {
        private readonly IOctopusDeployService _octopusDeployService;
        private readonly IOctopusDeploySettings _octopusDeploySettings;
        private readonly IArtifactRepository _artifactRepository;


        public ContentPackageGenerator(IOctopusDeployService octopusDeployService, IOctopusDeploySettings octopusDeploySettings, IArtifactRepository artifactRepository)
        {
            _octopusDeployService = octopusDeployService;
            _octopusDeploySettings = octopusDeploySettings;
            _artifactRepository = artifactRepository;
        }

        public ArtifactDetails CreatePackage(string currentSerializationFolder, string currentBuildNumber, string sourcePath = null)
        {
            //Step 1. Get Current Production Release Number from OD
            var version = _octopusDeployService.FindCurrentlyDeployedProductionVersion(_octopusDeploySettings.ProjectName, _octopusDeploySettings.EnvironmentName);

            //Step 2. Get Production Build Number From TC
            var buildNumber = new BasicOctopusToTeamcityMappingStrategy().GetTeamCityBuildNumberFromOctopusReleaseNumber(version.VersionNumber);

            //Step 3. Get Serlization folder that you have stored as an artifact
            var sourceZip = _artifactRepository.DownloadSerializationAsset("v" + buildNumber);
            if (string.IsNullOrEmpty(sourcePath))
                sourcePath = Directory.GetCurrentDirectory() + "\\ExtractedZip";

            if (Directory.Exists(sourcePath))
            {
                Directory.Delete(sourcePath, true);
            }

            Directory.CreateDirectory(sourcePath);

            ZipFile.ExtractToDirectory(sourceZip, sourcePath);

            //Step 4. Generate content package via Diff based on the old serlization  compared to new one (Courier!)
            //Step 5. Generate ItemsToPublish.json for Sitecore.Ship

            var packageGenerator = new SitecoreContentPackageGenerator(new SitecoreSerializationDiffGenerator(new ItemsToDeleteSettings()));
            var artifactDetails = packageGenerator.CreateArtifacts(sourcePath + "serialization\\", currentSerializationFolder);

            _artifactRepository.CreateSerializationAsset("v" + currentBuildNumber, currentSerializationFolder);

            return artifactDetails;
        }


    }
}
