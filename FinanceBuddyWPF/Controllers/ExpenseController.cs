using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace FinanceBuddyWPF.Controllers {
    class ExpenseController
    {

        /// <summary>
        /// Method for getting the sum of the user's expenses in categorized order.
        /// </summary>
        /// <param name="username"></param> User name of current logged in user.
        /// <param name="firstday"></param> Chosen start date.
        /// <param name="lastday"></param> Chosen end date.
        public async Task<float> GetExpenses(string username, string firstday, string lastday)
        {
            var uri = "https://financebuddyapi20190515092853.azurewebsites.net/api/expenses/sum/" + username +
                      "?firstday=" + firstday + "&lastday=" + lastday;
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(uri);
            if (response.Equals("0") || string.IsNullOrEmpty(response)) return 0;

            var expenses = float.Parse(response);
            return expenses;
        }

        /// <summary>
        /// Method for creating an expense related to a specific user.
        /// </summary>
        /// <param name="category"></param> Which category the expense falls under.
        /// <param name="description"></param> Short description of the expense.
        /// <param name="date"></param> Which date the expense occured.
        /// <param name="username"></param> User name of current logged in user.
        /// <param name="amount"></param> Cost of the expense.
        /// 
        public async Task<bool> CreateExpense(string category, string description, string date, string username,
            float amount)
        {
            var uri = "https://financebuddyapi20190515092853.azurewebsites.net/api/expenses/" + username;
            HttpClient client = new HttpClient();
            JObject body = new JObject
            {
                {"Category", category},
                {"Description", description},
                {"Date", date},
                {"Username", username},
                {"Amount", amount}

            };

            var content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
