namespace Sitecore.Octopus.Business.Stratergies
{
    public interface IOctopusToTeamcityMappingStratergy
    {
        string GetTeamCityBuildNumberFromOctopusReleaseNumber(string releaseNumber);
    }
}
