using Undersoft.SDK.Service.Data.Blob;
using Undersoft.SDK.Service.Data.Blob.Container;
using Undersoft.SDK.Service.Infrastructure.FileSystem;
using Undersoft.SDK.Service.Server.Resource;

namespace Undersoft.SDK.Service.Server.Resource.Container;

public class ResourceFileContainer : BlobContainer
{
    public ResourceFileContainer(string containerName)
        : this(containerName,
        new BlobContainerConfiguration(),
        new FileSystemBlobProvider(
            new DefaultBlobFilePathCalculator()))
    {
    }

    public ResourceFileContainer(
        string containerName,
        BlobContainerConfiguration configuration,
        IBlobProvider provider,
        IBlobNormalizeNamingService blobNormalizeNamingService = null)
        : base(containerName, configuration, provider, blobNormalizeNamingService)
    {
        configuration.UseFileSystem(fsc => { fsc.BasePath = ".resources"; fsc.AppendContainerNameToBasePath = true; });
    }

    public ResourceFile Get(string filename) => new ResourceFile(this, filename);
}
