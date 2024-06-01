namespace Undersoft.SDK.Tests.Workflows.Features
{
    using System;
    using System.Diagnostics;
    using Undersoft.SDK.Workflows;

    public class ComputeCurrency
    {
        public object Compute(string currency1, double rate1, string currency2, double rate2)
        {
            try
            {
                double _rate1 = rate1;
                double _rate2 = rate2;
                double result = _rate2 / _rate1;
                Debug.WriteLine("Result : " + result.ToString());

                return new object[] { _rate1, _rate2, result };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw new InvalidWorkException(ex.ToString());
            }
        }
    }
}
