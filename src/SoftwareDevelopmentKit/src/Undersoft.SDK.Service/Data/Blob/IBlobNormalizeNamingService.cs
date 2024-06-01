using Undersoft.SDK.Service.Data.Blob.Container;

namespace Undersoft.SDK.Service.Data.Blob
{
    public interface IBlobNormalizeNamingService
    {
        BlobNormalizeNaming NormalizeNaming(BlobContainerConfiguration configuration, string containerName, string blobName);

        string NormalizeContainerName(BlobContainerConfiguration configuration, string containerName);

        string NormalizeBlobName(BlobContainerConfiguration configuration, string blobName);
    }
}
