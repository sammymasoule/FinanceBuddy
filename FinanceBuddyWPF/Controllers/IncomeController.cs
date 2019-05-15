using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace FinanceBuddyWPF.Controllers {
    class IncomeController {

        /// <summary>
        /// Method for getting the total income of a user from and to a specific date.
        /// </summary>
        /// <param name="username"></param> Chosen user name for the user.
        /// <param name="firstday"></param> Chosen start date.
        /// <param name="lastday"></param> Chosen end date
        public async Task<float> GetIncome(string username, string firstday, string lastday) {
            var uri = "https://financebuddyapi20190515092853.azurewebsites.net/api/income/" + username +"?firstday=" + firstday +"&lastday=" + lastday;
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(uri);
            if(response.Equals("0") || string.IsNullOrEmpty(response)) return 0;

            float income = float.Parse(response);
            return income;
        }

        /// <summary>
        /// Method that creates an income for a specific user.
        /// </summary>
        /// <param name="amount"></param> Amount of money of the income.
        /// <param name="date"></param> When the income was received.
        /// <param name="username"></param> The user name of the user logged in.
        /// <param name="description"></param> A short description of the income.
        public async Task<bool> CreateIncome(float amount, string date, string username, string description) {
            var uri = "https://financebuddyapi20190515092853.azurewebsites.net/api/income/" + username;
            HttpClient client = new HttpClient();
            JObject body = new JObject
            {
                {"Amount", amount},
                {"Date", date},
                {"Username", username},
                {"Description", description}
            };

            var content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode) {
                return true;
            }
            return false;
        }
    }
}
