using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Sitecore.Octopus.Business.Contracts;

namespace Sitecore.Octopus.Business.Settings
{
    public class ItemsToDeleteSettings : IItemsToDeleteSettings
    {
        public List<string> PathsToRemove
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings.Get("ItemsToDelete.Paths").Split(',').ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}