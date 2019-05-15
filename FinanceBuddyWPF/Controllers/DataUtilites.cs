using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace FinanceBuddyWPF.Controllers
{
    public class DataUtilites
    {
        /// <summary>
        /// Method for refactoring the format of dates. Used to fit into database and charts.
        /// </summary>
        /// <param name="datefrom"></param> Start date.
        /// <param name="dateto"></param> End date.
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

        /// <summary>
        /// Method for retreiving name of the month in Danish. Used for charts.
        /// </summary>
        /// <param name="firstDate"></param> Start date.
        /// <param name="lastDate"></param> End date.
        public string GetMonth(DateTime? firstDate, DateTime? lastDate) {
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

        /// <summary>
        /// Method for getting the first and last day of current month. Used for charts.
        /// </summary>
        public string GetCurrentMonth()
        {
            var date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var firstDay = firstDayOfMonth.ToString("yyyy-MM-dd");
            var lastDay = lastDayOfMonth.ToString("yyyy-MM-dd");

            return firstDay + " " + lastDay;
        }


        /// <summary>
        /// Method for retreiving first and last day of specific month. Used for charts.
        /// </summary>
        /// <param name="month"></param> Name of the month
        /// Start date.
        public string GetMonthByName(string month)
        {
            switch (month)
            {
                case "Januar":
                    return "2019-01-01 2019-01-31";

                case "Februar":
                    if(DateTime.IsLeapYear(DateTime.Now.Year))
                        return "2019-02-01 2019-02-29";
                    return "2019-02-01 2019-02-28";

                case "Marts":
                    return "2019-03-01 2019-03-31";

                case "April":
                    return "2019-04-01 2019-04-30";

                case "Maj":
                    return "2019-05-01 2019-05-31";

                case "Juni":
                    return "2019-06-01 2019-06-30";

                case "Juli":
                    return "2019-07-01 2019-07-31";

                case "August":
                    return "2019-08-01 2019-08-31";

                case "September":
                    return "2019-09-01 2019-09-30";

                case "Oktober":
                    return "2019-10-01 2019-10-31";

                case "November":
                    return "2019-11-01 2019-03-30";

                case "December":
                    return "2019-12-01 2019-12-31";
            }

            return "";
        }

        /// <summary>
        /// Method for hasing user passwords.
        /// </summary>
        /// <param name="input"></param> user password.
        public string HashPassword(string input)
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
