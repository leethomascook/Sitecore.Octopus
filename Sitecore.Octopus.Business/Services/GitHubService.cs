using System.Collections.Generic;
using System.Linq;
using Octokit;
using Sitecore.Octopus.Business.Contracts;
using Commit = Sitecore.Octopus.Business.Domain.Commit;
using ProductHeaderValue = Octokit.ProductHeaderValue;

namespace Sitecore.Octopus.Business.Services
{
    public class GitHubService : ISourceControlService
    {
        private readonly IGitSettings _settings;

        public GitHubService(IGitSettings settings)
        {
            _settings = settings;
        }

        public List<Commit>  GetCommitsBetweenTag_AndCommit(string tagName, string commitId)
        {
            var credentials = new Credentials(_settings.GithubToken);
            var connection = new Connection(new ProductHeaderValue(_settings.RepositoryName))
            {
                Credentials = credentials
            };

            var client = new GitHubClient(connection);
           
            var results = client.Repository.Commits.Compare(_settings.OrganisationName, _settings.RepositoryName, tagName, commitId);

            var result = results.GetAwaiter().GetResult();

            return result.Commits.Select(commit =>
            {
                var comm = new Commit();

                comm.Message = commit.Commit.Message;
                comm.Authour = commit.Author != null ? commit.Author.Login : "User not found";
                comm.CommitId = commit.Commit.Sha;
                return comm;
            }).ToList();
        }
    }
}
