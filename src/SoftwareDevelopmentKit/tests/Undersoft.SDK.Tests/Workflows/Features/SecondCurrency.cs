namespace Undersoft.SDK.Tests.Workflows.Features
{
    using System;
    using System.Diagnostics;
    using Undersoft.SDK.Workflows;

    public class SecondCurrency
    {
        public object GetCurrency(string currency, int days)
        {
            BankCurrencyService kurKraju = new BankCurrencyService(days);

            try
            {
                double rate = kurKraju.LoadRate(currency);
                Debug.WriteLine(
                    "Rate 2 : "
                        + currency
                        + " days past : "
                        + days.ToString()
                        + " amount : "
                        + rate.ToString("#.####")
                );

                return new object[] { currency, rate };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw new InvalidWorkException(ex.ToString());
            }
        }
    }
}
