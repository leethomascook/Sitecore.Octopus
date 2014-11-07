namespace Sitecore.Octopus.Business.Stratergies
{
    public class BasicOctopusToTeamcityMappingStratergy : IOctopusToTeamcityMappingStratergy
    {
        public string GetTeamCityBuildNumberFromOctopusReleaseNumber(string releaseNumber)
        {
            return releaseNumber.Remove(0, 2);
        }
    }
}