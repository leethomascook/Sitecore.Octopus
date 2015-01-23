using System.IO;
using NUnit.Framework;
using Sitecore.Octopus.Business.Services;
using Sitecore.Octopus.Business.Settings;
using Sitecore.Octopus.Business.Stratergies;

namespace Sitecore.Octopus.Business.Tests
{
    [TestFixture]
    public class ReleaseNotesGeneratorTests
    {
        [Test]
        public void ReleaseNotesGenerator_CreatesAFileContaingReleaseNotes()
        {
            var generator = new ReleaseNotesGenerator(new BasicOctopusToTeamcityMappingStrategy(), new OctopusDeployService(new OctopusDeploySettings()), new OctopusDeploySettings(), new BasicBuildIdToTagNameStratergy(), new GitHubService(new GitSettings()), new JiraService(new JiraSettings())  );
            var releaseNotesFilePath = generator.CreateReleaseNotes("3dd8fb2");
            Assert.IsTrue(File.Exists(releaseNotesFilePath)); 
        }
    }
}
