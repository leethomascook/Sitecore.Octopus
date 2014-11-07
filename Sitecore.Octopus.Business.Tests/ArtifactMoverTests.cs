using System;
using System.IO;
using NUnit.Framework;
using Sitecore.Octopus.Business.Domain;

namespace Sitecore.Octopus.Business.Tests
{
    [TestFixture]
    public class ArtifactMoverTests
    {
        [Test]
        public void ArtifactMover_MovesContentPackageCorrectly()
        {
            var randomFileName = new Random().Next(1000000) + ".txt";
            System.IO.File.Create(randomFileName).Close();
            Directory.CreateDirectory("Artifacts");
            ArtifactMover.Move(new ArtifactDetails() { ContentPackageFilePath = randomFileName }, "Artifacts");
        }

        [Test]
        public void ArtifactMover_MovesItemToPublishCorrectly()
        {
            var randomFileName = new Random().Next(1000000) + ".txt";
            System.IO.File.Create(randomFileName).Close(); 
            Directory.CreateDirectory("Artifacts");
            ArtifactMover.Move(new ArtifactDetails() { ItemsToPublishFilePath = randomFileName }, "Artifacts");
        }

        [Test]
        public void ArtifactMover_MovesReleaseNotesCorrectly()
        {
            var randomFileName = new Random().Next(1000000) + ".txt";
            System.IO.File.Create(randomFileName).Close();
            Directory.CreateDirectory("Artifacts");
            ArtifactMover.Move(new ArtifactDetails() { ReleaseNotesFilePath = randomFileName }, "Artifacts");
        }
    }
}
