using System.Configuration;
using Sitecore.Octopus.Business.Contracts;

namespace Sitecore.Octopus.Business.Settings
{
    public class GitSettings : IGitSettings
    {
        public string RepositoryName
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("Git.RepositoryName");
            }
        }

        public string GithubToken
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("Git.GithubToken");
            }
        }

        public string OrganisationName
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("Git.OrganisationName");
            }
        }
    }
}