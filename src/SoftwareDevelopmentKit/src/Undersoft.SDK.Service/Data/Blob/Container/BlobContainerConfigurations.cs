using System.Diagnostics.CodeAnalysis;

namespace Undersoft.SDK.Service.Data.Blob.Container
{
    public class BlobContainerConfigurations
    {
        private BlobContainerConfiguration Default => GetConfiguration<DefaultContainer>();

        private readonly Registry<BlobContainerConfiguration> _containers;

        public BlobContainerConfigurations()
        {
            _containers = new Registry<BlobContainerConfiguration>(true)
            {
                //Add default container
                [BlobContainerNameAttribute.GetContainerName<DefaultContainer>()] = new BlobContainerConfiguration()
            };
        }

        public BlobContainerConfigurations Configure<TContainer>(
            Action<BlobContainerConfiguration> configureAction)
        {
            return Configure(
                BlobContainerNameAttribute.GetContainerName<TContainer>(),
                configureAction
            );
        }

        public BlobContainerConfigurations Configure(
            [DisallowNull] string name,
            [DisallowNull] Action<BlobContainerConfiguration> configureAction)
        {

            configureAction(
                _containers.EnsureGet(
                    name.UniqueKey(),
                    new BlobContainerConfiguration(Default)
                ).Value
            );

            return this;
        }

        public BlobContainerConfigurations ConfigureDefault(Action<BlobContainerConfiguration> configureAction)
        {
            configureAction(Default);
            return this;
        }

        public BlobContainerConfigurations ConfigureAll(Action<long, BlobContainerConfiguration> configureAction)
        {
            foreach (var container in _containers)
            {
                configureAction(container.Id, container.Value);
            }

            return this;
        }

        public BlobContainerConfiguration GetConfiguration<TContainer>()
        {
            return GetConfiguration(BlobContainerNameAttribute.GetContainerName<TContainer>());
        }

        public BlobContainerConfiguration GetConfiguration([DisallowNull] string name)
        {

            return _containers.Get(name) ??
                   Default;
        }
    }
}