using System.Diagnostics.CodeAnalysis;
using Undersoft.SDK.Service.Data.Blob.Container;

namespace Undersoft.SDK.Service.Data.Blob
{
    public class BlobProviderSaveArgs : BlobProviderArgs
    {
        [DisallowNull]
        public Stream BlobStream { get; }

        public bool OverrideExisting { get; }

        public BlobProviderSaveArgs(
            [DisallowNull] string containerName,
            [DisallowNull] BlobContainerConfiguration configuration,
            [DisallowNull] string blobName,
            [DisallowNull] Stream blobStream,
            bool overrideExisting = false,
            CancellationToken cancellationToken = default)
            : base(
                containerName,
                configuration,
                blobName,
                cancellationToken)
        {
            if (blobStream == null) { this.Warning<Runlog, Exception>("stream is null"); }
            BlobStream = blobStream;
            OverrideExisting = overrideExisting;
        }
    }
}
