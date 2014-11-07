namespace Sitecore.Octopus.Business.Contracts
{
    public interface IDropBoxSettings
    {
        string ApiKey { get; }
        string ApiSecret { get; }
        string UserSecret { get; }
        string UserToken { get; }
    }
}