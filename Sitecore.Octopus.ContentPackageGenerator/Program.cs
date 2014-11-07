using Sitecore.Octopus.Business;
using Sitecore.Octopus.Business.Services;
using Sitecore.Octopus.Business.Settings;
using Sitecore.Octopus.Business.Stratergies;

namespace Sitecore.Octopus.ContentPackageGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var argumentProcessor = new ArgumentProcessor(args);
           /* This is usefull for testing*/
            //argumentProcessor.CurrentCommitId = "33348fd";
            //argumentProcessor.PackageDestinationFolder = "c:\\Packages";
            //argumentProcessor.SerilizationFolder = "C:\\Projects\\Trafalgar\\src\\Data\\serialization";

            var contentPackageGenerator = new Business.ContentPackageGenerator(new GitHubService(new GitSettings()), new OctopusDeployService(new OctopusDeploySettings()), new OctopusDeploySettings(), new DropBoxService(new DropBoxSettings()));
            var artifactDetails = contentPackageGenerator.CreatePackage(argumentProcessor.SerilizationFolder, argumentProcessor.CurrentBuildId);
           
            var releaseNotesGenereator = new ReleaseNotesGenerator(new BasicOctopusToTeamcityMappingStratergy(), new OctopusDeployService(new OctopusDeploySettings()), new OctopusDeploySettings(), new BasicBuildIdToTagNameStratergy(), new GitHubService(new GitSettings()), new JiraService(new JiraSettings())  );
            var releaseNotesFilePath = releaseNotesGenereator.CreateReleaseNotes(argumentProcessor.CurrentCommitId);
            artifactDetails.ReleaseNotesFilePath = releaseNotesFilePath;

            ArtifactMover.Move(artifactDetails, argumentProcessor.PackageDestinationFolder);
        }
    } 
}
