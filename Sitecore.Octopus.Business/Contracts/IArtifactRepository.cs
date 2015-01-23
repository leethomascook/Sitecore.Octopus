namespace Sitecore.Octopus.Business.Contracts
{
    public interface IArtifactRepository
    {
        string DownloadSerializationAsset(string tagName);
        void CreateSerializationAsset(string tagName, string folderPath);
    }
}
