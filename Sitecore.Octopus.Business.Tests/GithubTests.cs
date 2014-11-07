using System;
using System.IO;
using Moq;
using NUnit.Framework;
using Sitecore.Octopus.Business.Contracts;
using Sitecore.Octopus.Business.Services;

namespace Sitecore.Octopus.Business.Tests
{
    [TestFixture]
    public class GithubTests
    {
        [Test]
        public void GithubService_GetCommitsBetweenTag_AndHead()
        {
            var tag = "V1686";
            var commitId = "521c58b6d0b877e189f0fa1ac2ad20ac18bc60d4";
            var settings = new Mock<IGitSettings>();
            settings.SetupGet(x => x.GithubToken).Returns("xx");
            settings.SetupGet(x => x.RepositoryName).Returns("xx");
            settings.SetupGet(x => x.OrganisationName).Returns("Aqueduct");
            var service = new GitHubService(settings.Object);

            var commits = service.GetCommitsBetweenTag_AndCommit(tag, commitId);

            Assert.Greater(commits.Count, 0);
        }
    }
}
