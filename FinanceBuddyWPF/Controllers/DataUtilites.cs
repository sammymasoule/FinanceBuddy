using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
            var monthfrom = new DateTime(2015, firstDate.Value.Month, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("da"));
            var monthto = new DateTime(2015, lastDate.Value.Month, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("da"));
            if (monthfrom.Equals(monthto))
            {
                string final = monthfrom.Substring(0, 1).ToUpper() + monthfrom.Substring(1);
                return final;

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
    }
}
