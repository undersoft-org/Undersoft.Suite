namespace Undersoft.GCC.Infrastructure.Currencies
{
    public interface ICurrenciesContextFactory<T> where T : CurrenciesContext
    {
        T CreateContext();
    }
}