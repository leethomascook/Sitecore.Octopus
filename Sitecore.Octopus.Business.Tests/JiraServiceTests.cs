using System;
using NUnit.Framework;
using Sitecore.Octopus.Business.Services;
using Sitecore.Octopus.Business.Settings;

namespace Sitecore.Octopus.Business.Tests
{
    [TestFixture]
    public class JiraServiceTests
    {
        [Test]
        public void JiraService_GetIssuesResolvedSinceDate_Returns_MultipleResults()
        {
            var service = new JiraService(new JiraSettings());

            var issues = service.GetIssuesResolvedSinceDate(DateTime.Now.AddMonths(12));

            Assert.Greater(issues.Count, 0);
        }
    }
}
