﻿namespace Undersoft.GCC.Infrastructure.DataServices.Currencies
{
    public class CurrenciesContextFactory<T> : ICurrenciesContextFactory<T> where T : CurrenciesContext
    {
        CurrenciesContextOptions<T> _options;

        public CurrenciesContextFactory(CurrenciesContextOptions<T> options)
        {
            _options = options;
        }

        public T CreateContext()
        {
            return typeof(T).New<T>(_options);
        }
    }
}
