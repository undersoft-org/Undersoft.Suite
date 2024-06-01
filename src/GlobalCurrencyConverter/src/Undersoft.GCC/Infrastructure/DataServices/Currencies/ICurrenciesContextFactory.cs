namespace Undersoft.GCC.Infrastructure.DataServices.Currencies
{
    public interface ICurrenciesContextFactory<T> where T : CurrenciesContext
    {
        T CreateContext();
    }
}