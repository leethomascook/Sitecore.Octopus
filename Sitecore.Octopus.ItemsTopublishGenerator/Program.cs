using Sitecore.Octopus.Business.PackageGenerator;
using Sitecore.Octopus.Business.Settings;

namespace Sitecore.Octopus.ItemsTopublishGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            new SitecoreContentPackageGenerator(new SitecoreSerilizationDiffGenerator(new ItemsToDeleteSettings())).CreateItemsToPublishFile(args[0], args[1], args[2]);
        }
    }
}
