using Undersoft.GCC.Infrastructure.Currencies;
using Undersoft.SDK.Logging;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Command;
using Undersoft.SDK.Service.Server.Hosting;
using Undersoft.SDK.Updating;

namespace Undersoft.GCC.Service.API.Extensions
{
    public static class ServerHostSetupExtensions
    {
        public static IServerHostSetup UseCurrenciesFeed(this IServerHostSetup setup)
        {
            using (IServiceScope scope = setup.Manager.CreateSession())
            {
                try
                {
                    var servicer = scope.ServiceProvider.GetRequiredService<IServicer>();
                    if (
                        !servicer
                            .StoreSet<IReportStore, Domain.Entities.CurrencyProvider>()
                            .Query.Any()
                    )
                    {
                        var providerEntries = servicer.GetService<CurrenciesOptions>().Providers;
                        if (providerEntries != null)
                        {
                            providerEntries
                                .ForEach(provider =>
                                    provider.Value.BaseCurrencyId = provider.Value.BaseCurrency!.Id
                                )
                                .Commit();
                            var currencies = providerEntries
                                .Select(provider => provider.Value.BaseCurrency)
                                .GroupBy(c => c.CurrencyCode)
                                .ForEach(h => h.FirstOrDefault())
                                .Commit();

                            servicer
                                .Send(
                                    new CreateSet<
                                        IEntryStore,
                                        Domain.Entities.Currency,
                                        Contracts.Currency
                                    >(
                                        EventPublishMode.PropagateCommand,
                                        currencies
                                            .ForEach(c => c.PatchTo<Contracts.Currency>())
                                            .Commit()
                                    )
                                )
                                .Wait();

                            servicer
                                .Send(
                                    new CreateSet<
                                        IEntryStore,
                                        Domain.Entities.CurrencyProvider,
                                        Contracts.CurrencyProvider
                                    >(
                                        EventPublishMode.PropagateCommand,
                                        providerEntries
                                            .ForEach(c =>
                                                c.Value.PatchTo<Contracts.CurrencyProvider>()
                                            )
                                            .Commit()
                                    )
                                )
                                .Wait();
                        }
                    }
                }
                catch (Exception ex)
                {
                    "Currencies initial feed - unable to connect the database engine".Error<Applog>(
                        null,
                        ex
                    );
                }
            }
            return setup;
        }
    }
}
