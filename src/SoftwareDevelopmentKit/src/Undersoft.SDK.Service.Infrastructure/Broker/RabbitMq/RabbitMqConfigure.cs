using Microsoft.Extensions.DependencyInjection;

namespace Undersoft.SDK.Service.Data.Event.Provider.RabbitMq;

using Configuration;

public class RabbitMqConfigure
{
    public void ConfigureServices(ServiceConfiguration configuration)
    {
        configuration.Services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));
        configuration.Services.Configure<RabbitMqOptions>(options =>
        {
            foreach (var connectionFactory in options.Connections.AsValues())
            {
                connectionFactory.DispatchConsumersAsync = true;
            }
        });
    }
}
