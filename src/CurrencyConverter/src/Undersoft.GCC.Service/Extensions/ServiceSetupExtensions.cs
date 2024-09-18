using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Undersoft.GCC.Infrastructure.Currencies;
using Undersoft.GCC.Infrastructure.Currencies.ECB;
using Undersoft.GCC.Infrastructure.Currencies.Frankfurter;
using Undersoft.GCC.Infrastructure.Currencies.NBP;
using Undersoft.GCC.Service.API.Workflows;
using Undersoft.SDK.Logging;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service;
using Undersoft.SDK.Utilities;

namespace Undersoft.GCC.Service.Extensions
{
    public static class ServiceSetupExtensions
    {
        public static IServiceSetup AddCurrencyContexts(this IServiceSetup services)
        {
            var options = new CurrenciesOptions();
            services.Manager.Configuration.Bind("Currencies", options);
            if (options.Providers == null || !options.Providers.Any())
            {
                options.Info<Servicelog>("Configuration doesn't contain any currency providers");
                return services;
            }

            services.Manager.Registry.AddObject(options);

            var contextRegistry = new Registry<CurrenciesContext>();

            foreach (var providerEntry in options.Providers)
            {
                var provider = providerEntry.Value;
                var providerKey = providerEntry.Key;
                provider.BaseCurrencyId = provider.BaseCurrency!.Id;
                var contextTypeName = $"{providerKey}.{provider.Name}{nameof(CurrenciesContext)}";
                var contextType = AssemblyUtilities.FindTypeByFullName(contextTypeName);

                if (contextType == null)
                {
                    provider.Info<Servicelog>("Unable to find provider _context type", contextTypeName);
                    continue;
                }

                var contextOptionsType = typeof(CurrenciesContextOptions<>).MakeGenericType(contextType);
                var contextOptions = contextOptionsType.New<CurrenciesContextOptions>(provider);

                var contextFactoryType = typeof(CurrenciesContextFactory<>).MakeGenericType(contextType);
                var contextFactory = contextFactoryType.New(contextOptions);

                var context = contextType.New<CurrenciesContext>(contextOptions);

                contextRegistry.Add(context.GetProvider().Name, context);

                services.Manager.Registry.AddObject(contextType, context);
                services.Manager.Registry.AddObject(contextOptionsType, contextOptions);
                services.Manager.Registry.AddObject(contextFactoryType, contextFactory);
            }

            services.Manager.Registry.AddObject<ISeries<CurrenciesContext>>(contextRegistry);

            return services;
        }

        public static IServiceSetup AddCurrencyWorkflows(this IServiceSetup services)
        {
            services.Manager.Registry.AddObject<UpsertWorkflow<NBPCurrenciesContext>>();
            services.Manager.Registry.AddObject<UpsertWorkflow<ECBCurrenciesContext>>();
            services.Manager.Registry.AddObject<UpsertWorkflow<FrankfurterCurrenciesContext>>();

            return services;
        }

        public static IHostBuilder AddWorkflowSchedule(this IHostBuilder builder)
        {
            builder.ConfigureServices((buildContext, services) =>
            {
                var options = new CurrenciesOptions();
                buildContext.Configuration.Bind("Currencies", options);

                var scheduleRegistry = new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>();

                foreach (var providerEntry in options.Providers!)
                {

                    var triggerSet = new HashSet<ITrigger>();
                    var provider = providerEntry.Value;
                    provider.BaseCurrencyId = provider.BaseCurrency!.Id;
                    var contextTypeName = $"{providerEntry.Key}.{provider.Name}{nameof(CurrenciesContext)}";
                    var contextType = AssemblyUtilities.FindTypeByFullName(contextTypeName);
                    var contextOptionsType = typeof(CurrenciesContextOptions<>).MakeGenericType(contextType);
                    var contextOptions = contextOptionsType.New<CurrenciesContextOptions>(provider);
                    var context = contextType.New<CurrenciesContext>(contextOptions);
                    services.AddSingleton(contextType, context);

                    Type workfowType = typeof(UpsertWorkflow<>).MakeGenericType(contextType);

                    var job = JobBuilder
                        .Create(workfowType)
                        .WithIdentity(provider.Name!, provider.BaseCurrency!.CurrencyCode!)
                        .Build();

                    triggerSet.Add(TriggerBuilder.Create().ForJob(job).StartNow().Build());

                    triggerSet.Add(
                        TriggerBuilder
                            .Create()
                            .ForJob(job)
                            .WithSimpleSchedule(a => a.WithIntervalInMinutes(30))
                            .Build()
                    );

                    triggerSet.Add(
                        TriggerBuilder
                            .Create()
                            .ForJob(job)
                            .WithDailyTimeIntervalSchedule(
                                a =>
                                    a.OnMondayThroughFriday()
                                        .StartingDailyAt(
                                            new TimeOfDay(provider.UpdateHour, provider.UpdateMinute)
                                        )
                                        .WithInterval(15, IntervalUnit.Minute)
                                        .EndingDailyAfterCount(5)
                            )
                            .Build()
                    );
                    scheduleRegistry.Add(job, triggerSet);
                }

                services.AddSingleton(scheduleRegistry);
            });

            return builder;
        }
    }
}
