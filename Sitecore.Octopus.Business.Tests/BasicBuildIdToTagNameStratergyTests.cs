using NUnit.Framework;
using Sitecore.Octopus.Business.Stratergies;

namespace Sitecore.Octopus.Business.Tests
{
    [TestFixture]
    public class BasicBuildIdToTagNameStratergyTests
    {
        [Test]
        public void BasicBuildIdToTagNameStratergy_GetTagName_Returns_VInFrontOfBuildId()
        {
            var stratergy = new BasicBuildIdToTagNameStratergy();

            var tagName = stratergy.GetTagName("1");

            Assert.AreEqual("V1", tagName);
        }
    }
}
