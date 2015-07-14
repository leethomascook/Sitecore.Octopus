using NUnit.Framework;
using Sitecore.Octopus.Business.Services;
using Sitecore.Octopus.Business.Settings;

namespace Sitecore.Octopus.Business.Tests
{
    [TestFixture]
    public class OctopusDeployServiceTests
    {
        [Test]
        public void OctopusDeployService_FindCurrentlyDeployedProductionVersionNumber_ReturnsABUildNumber()
        {
            var octopusSettings = new OctopusDeploySettings();
        
            var octopusDeployService = new OctopusDeployService(octopusSettings);
            
            var octopusDeployReleaseNumber = octopusDeployService.FindCurrentlyDeployedVersion(octopusSettings.ProjectName, octopusSettings.EnvironmentName);
            
            Assert.AreNotEqual(octopusDeployReleaseNumber.VersionNumber, "1.1.1652");
        }
    }
}
