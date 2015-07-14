using System;
using Sitecore.Octopus.Business.PackageGenerator;
using Sitecore.Octopus.Business.Settings;

namespace Sitecore.Octopus.ItemsTopublishGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running the ItemsTopublishGenerator. Arguments were: " + string.Concat(args[0], " ", args[1], " ", args[2] + string.Format("There were {0} arguments",args.Length)));
            
            
            new SitecoreContentPackageGenerator(new SitecoreSerilizationDiffGenerator(new ItemsToDeleteSettings())).CreateItemsToPublishFile(args[0], args[1], args[2], args[3].Split(','), args[4].Split(','));
        }
    }
}
