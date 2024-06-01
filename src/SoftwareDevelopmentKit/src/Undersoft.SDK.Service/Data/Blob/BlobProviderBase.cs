using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Undersoft.SDK.Service.Data.Blob
{
    public abstract class BlobProviderBase : IBlobProvider
    {
        public abstract Task SaveAsync(BlobProviderSaveArgs args);

        public abstract Task<bool> DeleteAsync(BlobProviderArgs args);

        public abstract Task<bool> ExistsAsync(BlobProviderArgs args);

        public abstract Task<Stream> GetOrNullAsync(BlobProviderArgs args);

        protected virtual async Task<Stream> TryCopyToMemoryStreamAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream == null)
            {
                return null;
            }

            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }
    }
}