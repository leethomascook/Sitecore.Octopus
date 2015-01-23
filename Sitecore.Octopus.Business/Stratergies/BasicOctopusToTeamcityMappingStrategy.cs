namespace Sitecore.Octopus.Business.Stratergies
{
    public class BasicOctopusToTeamcityMappingStrategy : IOctopusToTeamcityMappingStrategy
    {
        public string GetTeamCityBuildNumberFromOctopusReleaseNumber(string releaseNumber)
        {
            return releaseNumber.Remove(0, 2);
        }
    }
}