namespace Sitecore.Octopus.Business.Stratergies
{
    public class BasicBuildIdToTagNameStratergy : IBuildIdToTagNameStratergy
    {
        public string GetTagName(string buildId)
        {
            return string.Concat("V", buildId);
        }
    }
}