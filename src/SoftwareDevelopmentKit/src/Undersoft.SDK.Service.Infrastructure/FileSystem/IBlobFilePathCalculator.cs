using Undersoft.SDK.Service.Data.Blob;

namespace Undersoft.SDK.Service.Infrastructure.FileSystem
{
    public interface IBlobFilePathCalculator
    {
        string Calculate(BlobProviderArgs args);
    }
}