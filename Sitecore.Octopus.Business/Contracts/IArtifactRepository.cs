namespace Sitecore.Octopus.Business.Contracts
{
    public interface IArtifactRepository
    {
        string DownloadSerilizationAsset(string tagName);
        void CreateSerilizationAsset(string tagName, string folderPath);
    }
}
