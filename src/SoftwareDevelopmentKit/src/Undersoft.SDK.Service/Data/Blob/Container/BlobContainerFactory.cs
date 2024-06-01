using System;
using Undersoft.SDK.Service.Data.Blob;

namespace Undersoft.SDK.Service.Data.Blob.Container
{
    public class BlobContainerFactory : IBlobContainerFactory
    {
        protected IBlobProviderSelector ProviderSelector { get; }

        protected IBlobContainerConfigurationProvider ConfigurationProvider { get; }

        protected IServiceProvider ServiceProvider { get; }

        protected IBlobNormalizeNamingService BlobNormalizeNamingService { get; }

        public BlobContainerFactory(
            IBlobContainerConfigurationProvider configurationProvider,
            IBlobProviderSelector providerSelector,
            IServiceProvider serviceProvider,
            IBlobNormalizeNamingService blobNormalizeNamingService)
        {
            ConfigurationProvider = configurationProvider;
            ProviderSelector = providerSelector;
            ServiceProvider = serviceProvider;
            BlobNormalizeNamingService = blobNormalizeNamingService;
        }

        public virtual IBlobContainer Create(string name)
        {
            var configuration = ConfigurationProvider.Get(name);

            return new BlobContainer(
                name,
                configuration,
                ProviderSelector.Get(name),
                BlobNormalizeNamingService
            );
        }
    }
}
