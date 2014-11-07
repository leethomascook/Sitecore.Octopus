using System;
using System.IO;
using Moq;
using NUnit.Framework;
using Sitecore.Octopus.Business.Contracts;
using Sitecore.Octopus.Business.Services;

namespace Sitecore.Octopus.Business.Tests
{
    [TestFixture]
    public class DropBoxTests
    {
        [Test]
        public void DropBoxService_CanUploadAFile()
        {
            var settings = new Mock<IDropBoxSettings>();
            settings.SetupGet(x => x.ApiKey).Returns("xx");
            settings.SetupGet(x => x.ApiSecret).Returns("xx");
            settings.SetupGet(x => x.UserSecret).Returns("xx");
            settings.SetupGet(x => x.UserToken).Returns("xxx");
            var service = new DropBoxService(settings.Object);

            service.CreateSerilizationAsset("tag-name", "SerliazedItems\\Added");
        }

        [Test]
        public void DropBoxService_DownloadSerilizationAsset_()
        {
            var settings = new Mock<IDropBoxSettings>();
            settings.SetupGet(x => x.ApiKey).Returns("xx");
            settings.SetupGet(x => x.ApiSecret).Returns("xx");
            settings.SetupGet(x => x.UserSecret).Returns("xx");
            settings.SetupGet(x => x.UserToken).Returns("xx");
            var service = new DropBoxService(settings.Object);

            service.DownloadSerilizationAsset("v2770");

            Assert.IsTrue(System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\" + DropBoxService.ZIP_FILE_NAME));
        }
    }
}
