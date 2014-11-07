using System;
using System.IO;
using System.IO.Compression;
using DropNet;
using Sitecore.Octopus.Business.Contracts;

namespace Sitecore.Octopus.Business.Services
{
    public class DropBoxService : IArtifactRepository
    {
        private readonly IDropBoxSettings _dropBoxSettings;

        public DropBoxService(IDropBoxSettings dropBoxSettings)
        {
            _dropBoxSettings = dropBoxSettings;
        }

        public const string ZIP_FILE_NAME = "Serilization.zip";

        public string DownloadSerilizationAsset(string tagName)
        {
            var client = new DropNetClient(_dropBoxSettings.ApiKey, _dropBoxSettings.ApiSecret, _dropBoxSettings.UserToken, _dropBoxSettings.UserSecret);
            client.UseSandbox = true;
            string artifactPath = String.Format("/Artifacts/{0}/{1}", tagName, ZIP_FILE_NAME);
            var file = client.GetFile(artifactPath);
            string tempFile = Directory.GetCurrentDirectory() + "\\" + ZIP_FILE_NAME;
            File.WriteAllBytes(tempFile, file);

            return tempFile;
        }

        public void CreateSerilizationAsset(string tagName, string folderPath)
        {
            var client = new DropNetClient(_dropBoxSettings.ApiKey, _dropBoxSettings.ApiSecret, _dropBoxSettings.UserToken, _dropBoxSettings.UserSecret);
            client.UseSandbox = true;
           File.Delete(ZIP_FILE_NAME);
           ZipFile.CreateFromDirectory(folderPath, ZIP_FILE_NAME, CompressionLevel.Fastest, true);

           string artifactPath = String.Format("Artifacts/{0}", tagName);
           var fileSTream = File.ReadAllBytes(string.Concat(Directory.GetCurrentDirectory(), "\\", ZIP_FILE_NAME));

           client.UploadFile(artifactPath, ZIP_FILE_NAME, fileSTream);
        
        }
    }
}
