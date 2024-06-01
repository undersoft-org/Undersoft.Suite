namespace Undersoft.SDK.Tests.Workflows.Features
{
    using System;
    using System.Diagnostics;
    using Undersoft.SDK.Workflows;

    public class PresentResult
    {
        public object Present(double rate1, double rate2, double result)
        {
            try
            {
                string present =
                    "Rate USD : "
                    + rate1.ToString()
                    + " EUR : "
                    + rate2.ToString()
                    + " EUR->USD : "
                    + result.ToString();
                Debug.WriteLine(present);
                return present;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw new InvalidWorkException(ex.ToString());
            }
        }
    }
}
