using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinanceBuddyWPF.Controllers
{
    class DataUtilites
    {
        public string GetDateFormat(DateTime? datefrom, DateTime? dateto)
        {
                if (datefrom.Value > dateto.Value) {
                    DateTime? tmp = datefrom;
                    datefrom = dateto;
                    dateto = tmp;
                }
                string from = datefrom.Value.ToString("yyyy-MM-dd");
                string to = dateto.Value.ToString("yyyy-MM-dd");

                return from + " " + to;
        }

        public string getMonth(DateTime? firstDate, DateTime? lastDate) {
            var monthfrom = new DateTime(2015, firstDate.Value.Month, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("da")).ToUpper();
            var monthto = new DateTime(2015, lastDate.Value.Month, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("da")).ToUpper();
            if (monthfrom.Equals(monthto)) {
                return monthfrom;

            }
            string month = monthfrom + "-" + monthto;
            return month;
        }

        public string GetCurrentMonth()
        {
            var date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var firstDay = firstDayOfMonth.ToString("yyyy-MM-dd");
            var lastDay = lastDayOfMonth.ToString("yyyy-MM-dd");

            return firstDay + " " + lastDay;
        }
        public string Sha255Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
