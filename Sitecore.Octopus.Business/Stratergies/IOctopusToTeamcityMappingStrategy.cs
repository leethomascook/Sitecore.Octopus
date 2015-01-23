namespace Sitecore.Octopus.Business.Stratergies
{
    public interface IOctopusToTeamcityMappingStrategy
    {
        string GetTeamCityBuildNumberFromOctopusReleaseNumber(string releaseNumber);
    }
}
