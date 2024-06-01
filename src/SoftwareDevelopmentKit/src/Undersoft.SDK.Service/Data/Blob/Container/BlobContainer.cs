using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Undersoft.SDK.Service.Data.Blob;

namespace Undersoft.SDK.Service.Data.Blob.Container
{
    public class BlobContainer<TContainer> : IBlobContainer<TContainer>
        where TContainer : class
    {
        private readonly IBlobContainer _container;

        public BlobContainer(IBlobContainerFactory blobContainerFactory)
        {
            _container = blobContainerFactory.Create<TContainer>();
        }

        public Task SaveAsync(
            string name,
            Stream stream,
            bool overrideExisting = false,
            CancellationToken cancellationToken = default)
        {
            return _container.SaveAsync(
                name,
                stream,
                overrideExisting,
                cancellationToken
            );
        }

        public Task<bool> DeleteAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return _container.DeleteAsync(
                name,
                cancellationToken
            );
        }

        public Task<bool> ExistsAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return _container.ExistsAsync(
                name,
                cancellationToken
            );
        }

        public Task<Stream> GetAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return _container.GetAsync(
                name,
                cancellationToken
            );
        }

        public Task<Stream> GetOrNullAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return _container.GetOrNullAsync(
                name,
                cancellationToken
            );
        }
    }

    public class BlobContainer : IBlobContainer
    {
        public string ContainerName { get; set; }

        public BlobContainerConfiguration Configuration { get; }

        public IBlobProvider Provider { get; }

        public CancellationToken CancellationTokenProvider { get; }

        public IBlobNormalizeNamingService BlobNormalizeNamingService { get; }

        public BlobContainer(
            string containerName,
            BlobContainerConfiguration configuration,
            IBlobProvider provider,
            IBlobNormalizeNamingService blobNormalizeNamingService = null)
        {
            ContainerName = containerName;
            Configuration = configuration;
            Provider = provider;
            BlobNormalizeNamingService = blobNormalizeNamingService;

        }

        public virtual async Task SaveAsync(
            string name,
            Stream stream,
            bool overrideExisting = false,
            CancellationToken cancellationToken = default)
        {
            var blobNormalizeNaming = BlobNormalizeNamingService?.NormalizeNaming(Configuration, ContainerName, name);
            if (blobNormalizeNaming != null)
            {
                name = blobNormalizeNaming.BlobName;
                ContainerName = blobNormalizeNaming.ContainerName;
            }
            await Provider.SaveAsync(
                new BlobProviderSaveArgs(
                    name,
                    Configuration,
                    ContainerName,
                    stream,
                    overrideExisting,
                    cancellationToken
                )
            );
        }

        public virtual async Task<bool> DeleteAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            var blobNormalizeNaming = BlobNormalizeNamingService?.NormalizeNaming(Configuration, ContainerName, name);
            if (blobNormalizeNaming != null)
            {
                name = blobNormalizeNaming.BlobName;
                ContainerName = blobNormalizeNaming.ContainerName;
            }

            return await Provider.DeleteAsync(
                    new BlobProviderArgs(
                        blobNormalizeNaming.ContainerName,
                        Configuration,
                        blobNormalizeNaming.BlobName,
                          cancellationToken
                    )
                );

        }

        public virtual async Task<bool> ExistsAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            var blobNormalizeNaming = BlobNormalizeNamingService?.NormalizeNaming(Configuration, ContainerName, name);
            if (blobNormalizeNaming != null)
            {
                name = blobNormalizeNaming.BlobName;
                ContainerName = blobNormalizeNaming.ContainerName;
            }

            return await Provider.ExistsAsync(
                    new BlobProviderArgs(
                        blobNormalizeNaming.ContainerName,
                        Configuration,
                        blobNormalizeNaming.BlobName,
                          cancellationToken
                    )
                );

        }

        public virtual async Task<Stream> GetAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            var stream = await GetOrNullAsync(name, cancellationToken);

            if (stream == null)
            {
                //TODO: Consider to throw some type of "not found" exception and handle on the HTTP status side
                throw new Exception(
                    $"Could not found the requested BLOB '{name}' in the container '{ContainerName}'!");
            }

            return stream;
        }

        public virtual async Task<Stream> GetOrNullAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            var blobNormalizeNaming = BlobNormalizeNamingService?.NormalizeNaming(Configuration, ContainerName, name);
            if (blobNormalizeNaming != null)
            {
                name = blobNormalizeNaming.BlobName;
                ContainerName = blobNormalizeNaming.ContainerName;
            }

            return await Provider.GetOrNullAsync(
                new BlobProviderArgs(
                    blobNormalizeNaming.ContainerName,
                    Configuration,
                    blobNormalizeNaming.BlobName,
                      cancellationToken
                    ));
        }
    }
}