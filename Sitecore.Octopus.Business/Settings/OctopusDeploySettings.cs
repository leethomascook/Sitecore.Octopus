using System.Configuration;
using Sitecore.Octopus.Business.Contracts;

namespace Sitecore.Octopus.Business.Settings
{
    public class OctopusDeploySettings : IOctopusDeploySettings
    {
        public string SevrerUrl
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("OctopusDeploy.Url");
            }
        }

        public string ApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("OctopusDeploy.ApiKey");
            }
        }

        public string ProjectName
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("OctopusDeploy.ProjectName");
            }
        }

        public string EnvironmentName
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("OctopusDeploy.EnvironmentName");
            }
        }
    }
}