namespace Sitecore.Octopus.Business.Contracts
{
    public interface IOctopusDeploySettings
    {
        string SevrerUrl { get; }
        string ApiKey { get; }
        string ProjectName { get; }
        string EnvironmentName { get; }
    }
}