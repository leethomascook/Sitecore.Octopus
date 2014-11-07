using NUnit.Framework;
using Sitecore.Octopus.Business.PackageGenerator;
using Sitecore.Octopus.Business.Settings;

namespace Sitecore.Octopus.Business.Tests
{
    [TestFixture]
    public class SitecoreSerilizationDiffGeneratorTests
    {
        [Test]
        public void SitecoreSerilizationDiffGenerator_GetDiffCommands_Returns_CorrectNumberOfAddedItems()
        {
            var generator = new SitecoreSerilizationDiffGenerator(new ItemsToDeleteSettings());
            var commands = generator.GetDiffCommands("C:\\Projects\\Sitecore.Octopus\\Sitecore.Octopus.Business.Tests\\SerliazedItems\\Added", "C:\\Projects\\Sitecore.Octopus\\Sitecore.Octopus.Business.Tests\\SerliazedItems\\EmptyTarget");

            Assert.AreEqual(1, commands.Count);
        }

        [Test]
        public void SitecoreSerilizationDiffGenerator_GetDiffCommands_Returns_NoItemsWhenSrcAndDestinationTheSame()
        {
            var generator = new SitecoreSerilizationDiffGenerator(new ItemsToDeleteSettings());
            var commands = generator.GetDiffCommands("C:\\Projects\\Sitecore.Octopus\\Sitecore.Octopus.Business.Tests\\SerliazedItems\\Added", "C:\\Projects\\Sitecore.Octopus\\Sitecore.Octopus.Business.Tests\\SerliazedItems\\Added");

            Assert.AreEqual(0, commands.Count);
        }

        [Test]
        public void SitecoreSerilizationDiffGenerator_GetDiffCommands_Returns_CorrectNumberOfDeletedItems()
        {
            var generator = new SitecoreSerilizationDiffGenerator(new ItemsToDeleteSettings());
            var commands = generator.GetDiffCommands("C:\\Projects\\Sitecore.Octopus\\Sitecore.Octopus.Business.Tests\\SerliazedItems\\Deleted", "C:\\Projects\\Sitecore.Octopus\\Sitecore.Octopus.Business.Tests\\SerliazedItems\\Added");

            Assert.AreEqual(1, commands.Count);
        }
    }
}
