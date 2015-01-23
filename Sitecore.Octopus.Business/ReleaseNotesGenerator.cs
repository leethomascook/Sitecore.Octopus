using Sitecore.Octopus.Business.Contracts;
using Sitecore.Octopus.Business.Services;
using Sitecore.Octopus.Business.Settings;
using Sitecore.Octopus.Business.Stratergies;

namespace Sitecore.Octopus.Business
{
    public class ReleaseNotesGenerator
    {
        private readonly IOctopusToTeamcityMappingStrategy _octopusToTeamcityMappingStrategy;
        private readonly IOctopusDeployService _octopusDeployService;
        private readonly IOctopusDeploySettings _octopusDeploySettings;
        private readonly IBuildIdToTagNameStratergy _buildIdToTagNameStratergy;
        private readonly ISourceControlService _sourceControlService;
        private readonly IBugTrackingService _bugTrackingService;

        public ReleaseNotesGenerator(IOctopusToTeamcityMappingStrategy octopusToTeamcityMappingStrategy, IOctopusDeployService octopusDeployService, IOctopusDeploySettings octopusDeploySettings, IBuildIdToTagNameStratergy buildIdToTagNameStratergy, ISourceControlService sourceControlService, IBugTrackingService bugTrackingService)
        {
            _octopusToTeamcityMappingStrategy = octopusToTeamcityMappingStrategy;
            _octopusDeployService = octopusDeployService;
            _octopusDeploySettings = octopusDeploySettings;
            _buildIdToTagNameStratergy = buildIdToTagNameStratergy;
            _sourceControlService = sourceControlService;
            _bugTrackingService = bugTrackingService;
        }

        public string CreateReleaseNotes(string currentCommitId)
        {
            //Step 1. Get Current Production Release Number from OD
            var octopusDeployVersion = _octopusDeployService.FindCurrentlyDeployedProductionVersion(_octopusDeploySettings.ProjectName, _octopusDeploySettings.EnvironmentName);

            //Step 2. Get Build Number From TC
            var buildNumber = _octopusToTeamcityMappingStrategy.GetTeamCityBuildNumberFromOctopusReleaseNumber(octopusDeployVersion.VersionNumber);

            // Step 3. Get tag label
            var tagLabel = _buildIdToTagNameStratergy.GetTagName(buildNumber);

            //Step 4. Get Commits since last deploy.
            var commits = _sourceControlService.GetCommitsBetweenTag_AndCommit(tagLabel, currentCommitId);

            //Step 5. Get Rsolved Jira issues since last deploy

            var issues = _bugTrackingService.GetIssuesResolvedSinceDate(octopusDeployVersion.DateReleaseCreated);
            //Step 6. Collate commits and issues into text document and save to disk.

            return new ReleaseNoteFileCreator().CreateFile(commits, issues);
        }
    }
}
