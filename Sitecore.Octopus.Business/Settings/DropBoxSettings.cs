using System.Configuration;
using Sitecore.Octopus.Business.Contracts;

namespace Sitecore.Octopus.Business.Settings
{
    public class DropBoxSettings : IDropBoxSettings
    {
        public string ApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("DropBox.ApiKey");
            }
        }

        public string ApiSecret
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("DropBox.ApiSecret");
            }
        }

        public string AccessToken
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("DropBox.AccessToken");
            }
        }
    }
}