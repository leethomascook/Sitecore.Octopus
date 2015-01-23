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
            var contentPackageGenerator = new Business.ContentPackageGenerator(
                new OctopusDeployService(new OctopusDeploySettings()), new OctopusDeploySettings(),
                new DropBoxService(new DropBoxSettings()));
            var artifactDetails = contentPackageGenerator.CreatePackage(argumentProcessor.SerializationFolder,
                argumentProcessor.CurrentBuildId, argumentProcessor.OutputPath);

            var releaseNotesGenereator = new ReleaseNotesGenerator(new BasicOctopusToTeamcityMappingStrategy(), new OctopusDeployService(new OctopusDeploySettings()), new OctopusDeploySettings(), new BasicBuildIdToTagNameStratergy(), new GitHubService(new GitSettings()), new JiraService(new JiraSettings()));
            var releaseNotesFilePath = releaseNotesGenereator.CreateReleaseNotes(argumentProcessor.CurrentCommitId);
            artifactDetails.ReleaseNotesFilePath = releaseNotesFilePath;

            ArtifactMover.Move(artifactDetails, argumentProcessor.PackageDestinationFolder);
        }
    }
}
