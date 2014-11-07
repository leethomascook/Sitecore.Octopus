using System;
using System.IO;
using System.Security.Cryptography;
using Sitecore.Octopus.Business.Domain;

namespace Sitecore.Octopus.Business
{
    public static class ArtifactMover
    {
        public static void Move(ArtifactDetails artifactDetails, string destination)
        {

            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            if (!string.IsNullOrEmpty(artifactDetails.ContentPackageFilePath ))
            {
                DeleteDestinationFileIfExsistsAlready(artifactDetails.ContentPackageFilePath, destination);
                File.Move(artifactDetails.ContentPackageFilePath, String.Concat(destination, "\\", artifactDetails.ContentPackageFilePath));
            }

            if (!string.IsNullOrEmpty(artifactDetails.ItemsToPublishFilePath))
            {
                DeleteDestinationFileIfExsistsAlready(artifactDetails.ItemsToPublishFilePath, destination);
                File.Move(artifactDetails.ItemsToPublishFilePath, String.Concat(destination, "\\", artifactDetails.ItemsToPublishFilePath));
            }

            if (!string.IsNullOrEmpty(artifactDetails.ReleaseNotesFilePath))
            {
                DeleteDestinationFileIfExsistsAlready(artifactDetails.ReleaseNotesFilePath, destination);
                File.Move(artifactDetails.ReleaseNotesFilePath, String.Concat(destination, "\\", artifactDetails.ReleaseNotesFilePath));
            }
        }

        private static void DeleteDestinationFileIfExsistsAlready(string artifactPath, string destination)
        {
            if (File.Exists(String.Concat(destination, "\\", artifactPath)))
            {
                File.Delete(String.Concat(destination, "\\", artifactPath));
            }
        }
    }
}
