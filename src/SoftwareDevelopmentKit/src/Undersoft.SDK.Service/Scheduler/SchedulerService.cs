using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl;

namespace Undersoft.SDK.Service.Scheduler;

public static class SchedulerService
{
    public static IServiceSetup AddSchedulerService(this IServiceSetup setup)
    {
        var options = new SchedulerOptions();

        setup.Manager.Configuration.Bind(options);

        setup.Manager.Registry.AddQuartz(options.Properties, build =>
        {
            if (options.Properties[StdSchedulerFactory.PropertySchedulerTypeLoadHelperType] == null)
                build.UseSimpleTypeLoader();


            if (options.Properties[StdSchedulerFactory.PropertyJobStoreType] == null)
                build.UseInMemoryStore();

            if (options.Properties[StdSchedulerFactory.PropertyThreadPoolType] == null)
                build.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 10;
                });

            if (options.Properties["quartz.plugin.timeZoneConverter.type"] == null)
                build.UseTimeZoneConverter();

            //build.UseXmlSchedulingConfiguration(x =>
            //{
            //    x.Files = new[] { "./data/us-gcc-scheduler.config" };
            //    x.ScanInterval = TimeSpan.FromSeconds(5);
            //    x.FailOnFileNotFound = true;
            //    x.FailOnSchedulingError = true;
            //});

            options.Configurator?.Invoke(build);
        });

        setup.Manager.Registry.AddSingleton(serviceProvider =>
        {
            var task = serviceProvider.GetRequiredService<ISchedulerFactory>().GetScheduler();
            task.Wait();
            return task.Result;
        });

        options.Properties = options.Properties;
        options.StartDelay = options.StartDelay;

        return setup;
    }

    public static async Task StartSchedulerServcice(this IServicer servicer)
    {
        var options = servicer.Registry.GetRequiredService<IOptions<SchedulerOptions>>().Value;

        var _scheduler = servicer.Registry.GetRequiredService<IScheduler>();

        await options.StartSchedulerFactory.Invoke(_scheduler);
    }
}
