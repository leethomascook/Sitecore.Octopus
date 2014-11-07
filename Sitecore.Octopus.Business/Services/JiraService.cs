using System;
using System.Collections.Generic;
using System.Linq;
using Atlassian.Jira;
using Sitecore.Octopus.Business.Contracts;
using Issue = Sitecore.Octopus.Business.Domain.Issue;

namespace Sitecore.Octopus.Business.Services
{
    public class JiraService : IBugTrackingService
    {
        private readonly IJiraSettings _jiraSettings;

        public JiraService(IJiraSettings jiraSettings)
        {
            _jiraSettings = jiraSettings;
        }

        public List<Issue> GetIssuesResolvedSinceDate(DateTime dateTime)
        {
            var jira = new Jira(_jiraSettings.Url, _jiraSettings.UserName, _jiraSettings.Passsword);

            IssueStatus status = jira.GetIssueStatuses().FirstOrDefault(x => x.Name == "Resolved");
            var jiraIssues = from i in jira.Issues
                             where i.Status == status && i.Project == _jiraSettings.ProjectName
                orderby i.Created
                select i;

            var issues = new List<Issue>();

            foreach (var jiraIssue in jiraIssues.ToList())
            {
                issues.Add(new Issue()
                {
                     DateResolved = jiraIssue.Updated.Value,
                     Description = jiraIssue.Description,
                     IssueNumber = jiraIssue.Key.Value,
                     Reporter = jiraIssue.Reporter

                });
            }

            return issues;
        }
    }
}