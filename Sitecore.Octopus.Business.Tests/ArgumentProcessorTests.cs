using NUnit.Framework;

namespace Sitecore.Octopus.Business.Tests
{
    [TestFixture]
    public class ArgumentProcessorTests
    {
        [Test]
        public void ArgumentProcessor_CorrectlySetsSeralizationFolder()
        {
            var processor = new ArgumentProcessor(new[] { "Data", "", "", "" });
            
            Assert.AreEqual("Data", processor.SerilizationFolder);
        }


        [Test]
        public void ArgumentProcessor_CorrectlySetsDestinationFolder()
        {
            var processor = new ArgumentProcessor(new[] { "", "Destination", "", "" });

            Assert.AreEqual("Destination", processor.PackageDestinationFolder);
        }

        [Test]
        public void ArgumentProcessor_CorrectlySetsCurrentGitCommitId()
        {
            var processor = new ArgumentProcessor(new[] { "", "", "12345", "" });

            Assert.AreEqual("12345", processor.CurrentCommitId);
        }
    }
}
