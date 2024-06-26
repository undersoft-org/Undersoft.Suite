﻿using System.Globalization;
using System.Text.RegularExpressions;
using Undersoft.SDK.Service.Data.Blob;

namespace Undersoft.SDK.Service.Infrastructure.FileSystem
{
    public class FileSystemBlobNamingNormalizer : IBlobNamingNormalizer
    {
        public FileSystemBlobNamingNormalizer()
        {
        }

        public virtual string NormalizeContainerName(string containerName)
        {
            return Normalize(containerName);
        }

        public virtual string NormalizeBlobName(string blobName)
        {
            return Normalize(blobName);
        }

        protected virtual string Normalize(string fileName)
        {
            CultureHelper.Use(CultureInfo.InvariantCulture);
            {
                var os = Environment.OSVersion;
                if (os.Platform == PlatformID.Win32NT)
                {
                    // A filename cannot contain any of the following characters: \ / : * ? " < > |
                    // In order to support the directory included in the blob name, remove / and \
                    fileName = Regex.Replace(fileName, "[:\\*\\?\"<>\\|]", string.Empty);
                }

                return fileName;
            }
        }
    }
}