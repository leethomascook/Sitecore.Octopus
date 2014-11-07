namespace Sitecore.Octopus.Business.Contracts
{
    public interface IJiraSettings
    {
        string Url { get; }
        string UserName { get; }
        string Passsword { get; }
        string ProjectName { get; }
    }
}
