using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Sitecore.Update.Interfaces;

namespace Sitecore.Octopus.Business.Tests
{
    [TestFixture]
    public class PublishableItemsGeneratorTests
    {
        [Test]
        public void PublishableItemsGenerator_FindItemsToPublishFromCommands()
        {
            var generator = new PublishableItemsGenerator();
            var mockCommand = new Mock<ICommand>();
            mockCommand.Setup(x => x.EntityID).Returns(Guid.NewGuid().ToString);
            mockCommand.Setup(x => x.Parent).Returns(Guid.NewGuid().ToString);
            var itemsToPublish = generator.FindItemsToPublishFromCommands(new List<ICommand>() { mockCommand.Object});

            Assert.AreEqual(1, itemsToPublish.Count);
        }
    }
}
