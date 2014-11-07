namespace Sitecore.Octopus.Business.Contracts
{
    public interface IGitSettings
    {
        string RepositoryName { get;  }
        string GithubToken { get;  }
        string OrganisationName { get; }

    }
}