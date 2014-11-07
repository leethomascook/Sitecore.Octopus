using System.Configuration;
using Sitecore.Octopus.Business.Contracts;

namespace Sitecore.Octopus.Business.Settings
{
    public class JiraSettings : IJiraSettings
    {
        public string Url
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("Jira.Url");
            }
        }
        public string UserName
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("Jira.Username");
            }
        }

        public string Passsword
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("Jira.Password");
            }
        }

        public string ProjectName
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("Jira.ProjectName");
            }
        }
    }
}