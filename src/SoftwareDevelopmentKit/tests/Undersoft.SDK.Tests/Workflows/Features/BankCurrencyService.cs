namespace Undersoft.SDK.Tests.Workflows.Features
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Undersoft.SDK.Workflows;

    public class BankCurrencyService
    {
        private const string file_dir = "http://www.nbp.pl/Kursy/xml/dir.txt";
        private const string xml_url = "http://www.nbp.pl/kursy/xml/";

        public string file_name;
        public DateTime rate_date;
        private int start_int = 1;
        private int _daysbefore;

        public BankCurrencyService(int daysbefore)
        {
            _daysbefore = daysbefore;
        }

        public Dictionary<string, double> GetCurrencyRates(List<string> currency_names)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();

            foreach (var item in currency_names)
            {
                result.Add(item, LoadRate(item));
            }
            return result;
        }

        public double LoadRate(string currency_name)
        {
            try
            {
                var filenane = GetFileName(_daysbefore).GetAwaiter().GetResult();
                string file = xml_url + filenane + ".xml";
                DataSet ds = new DataSet();
                ds.ReadXml(file);
                var tabledate = ds.Tables["tabela_kursow"].Rows.Cast<DataRow>().AsEnumerable();
                var before_rate_date = (
                    from k in tabledate
                    select new { Data = k["data_publikacji"].ToString() }
                ).First();
                var tabela = ds.Tables["pozycja"].Rows.Cast<DataRow>().AsEnumerable();
                var rate = (
                    from k in tabela
                    where k["kod_waluty"].ToString() == currency_name
                    select new { Kurs = k["kurs_sredni"].ToString() }
                ).First();
                rate_date = Convert.ToDateTime(before_rate_date.Data);
                return double.Parse(rate.Kurs);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw new InvalidWorkException(ex.ToString());
            }
        }

        private async Task<string> GetFileName(int daysbefore)
        {
            try
            {
                int minusdays = daysbefore * -1;
                HttpClient client = new HttpClient();
                string file_list = await client.GetStringAsync(file_dir);
                string date_str = string.Empty;
                DateTime date_of_rate = DateTime.Now.AddDays(minusdays);
                int maxdaysbackward = -3;
                while (true)
                {
                    date_str =
                        "a"
                        + start_int.ToString().PadLeft(3, '0')
                        + "z"
                        + date_of_rate.ToString("yyMMdd");
                    if (file_list.Contains(date_str))
                        break;

                    start_int++;

                    if (start_int > 365)
                    {
                        if (--maxdaysbackward < -5)
                            throw new ArgumentOutOfRangeException();

                        start_int = 1;
                        date_of_rate = date_of_rate.AddDays(maxdaysbackward);
                    }
                }
                file_name = date_str;
                rate_date = date_of_rate;
                return file_name;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw new InvalidWorkException(ex.ToString());
            }
        }
    }
}
