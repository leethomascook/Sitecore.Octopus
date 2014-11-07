using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sitecore.Octopus.Business.Domain;

namespace Sitecore.Octopus.Business.Tests
{
    [TestFixture]
    public class ReleaseNoteFileCreatorTests
    {
        [Test]
        public void ReleaseNoteFileCreator_CreatesFileWithHeadings()
        {
            var creator = new ReleaseNoteFileCreator();
            var filePath = creator.CreateFile(new List<Commit>(), new List<Issue>());

            var copy = System.IO.File.ReadAllText(filePath);
            Assert.IsTrue(copy.Contains("Jira Issues Resolved since last release:"));
        }

        [Test]
        public void ReleaseNoteFileCreator_CreatesFileWithMultipleCommitsIn()
        {
            var creator = new ReleaseNoteFileCreator();
            var filePath = creator.CreateFile(new List<Commit>()
            {
                new Commit(){Authour = "Lee Cook", CommitId = "1", Message = "Message1"},
                new Commit(){Authour = "Lee Cook", CommitId = "2", Message = "Message2"}
            }, new List<Issue>());

            var copy = System.IO.File.ReadAllText(filePath);
            Assert.IsTrue(copy.Contains("Message1"));
            Assert.IsTrue(copy.Contains("Message2"));
        }
        [Test]
        public void ReleaseNoteFileCreator_CreatesFileWithMultipleIssuesIn()
        {
            var creator = new ReleaseNoteFileCreator();
            var filePath = creator.CreateFile(new List<Commit>(), new List<Issue>()
            {
                new Issue {DateResolved = DateTime.Now, Description = "Issue 1 description", IssueNumber = "1", Reporter = "Lee Reporter 1"},
               new Issue {DateResolved = DateTime.Now, Description = "Issue 2 description", IssueNumber = "2", Reporter = "Lee Reporter 2"}

            });

            var copy = System.IO.File.ReadAllText(filePath);
            Assert.IsTrue(copy.Contains("Issue 1 description"));
            Assert.IsTrue(copy.Contains("Issue 2 description"));
        }
    }
}
