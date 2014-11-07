using NUnit.Framework;
using Sitecore.Octopus.Business.Stratergies;

namespace Sitecore.Octopus.Business.Tests
{
    [TestFixture]
    public class BasicOctopusToTeamcityMappingStratergyTests
    {
        [Test]
        public void BasicOctopusToTeamcityMappingStratergy_RemovesFirstTwoCharactersofReleaseNumber()
        {
            var stratergy = new BasicOctopusToTeamcityMappingStratergy();

            var buildId = stratergy.GetTeamCityBuildNumberFromOctopusReleaseNumber("1.1");

            Assert.AreEqual("1", buildId);
        }

        [Test]
        public void BasicOctopusToTeamcityMappingStratergy_RemovesFirstTwoCharactersofReleaseNumber_WhenMultipleDeciamlPlaces()
        {
            var stratergy = new BasicOctopusToTeamcityMappingStratergy();

            var buildId = stratergy.GetTeamCityBuildNumberFromOctopusReleaseNumber("1.1.2.3");

            Assert.AreEqual("1.2.3", buildId);
        }
    }
}
