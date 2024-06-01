using Microsoft.Extensions.DependencyInjection;

namespace Undersoft.SDK.Service.Data.Blob;

using Configuration;
using Undersoft.SDK.Service.Data.Blob.Container;

public class BlobStoringModule
{
    public void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient(
            typeof(IBlobContainer<>),
            typeof(BlobContainer<>)
        );

        context.Services.AddTransient(
            typeof(IBlobContainer),
            serviceProvider => serviceProvider
                .GetRequiredService<IBlobContainer<DefaultContainer>>()
        );
    }
}