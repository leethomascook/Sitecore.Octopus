using Sitecore.Octopus.Business.Domain;
using Sitecore.Octopus.Business.Services;

namespace Sitecore.Octopus.Business.Contracts
{
    public interface IOctopusDeployService
    {
        OctopusDeployVersion FindCurrentlyDeployedVersion(string projectName, string environmentName);
    }
}
