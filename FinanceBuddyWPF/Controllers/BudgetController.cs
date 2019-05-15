using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace FinanceBuddyWPF.Controllers {
    class BudgetController {


        public async Task<List<float>> GetBudget(string username)
        {
            List<float> limits = new List<float>();
            var uri = "https://financebuddyapi20190515092853.azurewebsites.net/api/budget/" + username;
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(uri);
            var tmp = response.Replace("[", "").Replace("]", "").Split(',');
            
            foreach (var element in tmp) {
                limits.Add(float.Parse(element));
            }
            return limits;
        }

        public async Task<bool> CreateBudget(string username, float loanLimit, float groceryLimit, float houseHoldLimit, float consumptionLimit, float transportLimit, float savingsLimit) {
            var uri = "https://financebuddyapi20190515092853.azurewebsites.net/api/budget/" + username;
            HttpClient client = new HttpClient();
            JObject body = new JObject
            {
                {"Username", username},
                {"LoanLimit", loanLimit},
                {"GroceryLimit", groceryLimit},
                {"HouseholdLimit", houseHoldLimit},
                {"ConsumptionLimit", consumptionLimit},
                {"TransportLimit", transportLimit},
                {"SavingsLimit", savingsLimit}
            };

            var content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Method for changing/updating a current budget for a specific user.
        /// </summary>
        /// <param name="username"></param> User name of current logged in user.
        /// <param name="loanLimit"></param> Budget limit that the user wishes to have for loan expenses.
        /// <param name="groceryLimit"></param> Budget limit that the user wishes to have for grocery expenses.
        /// <param name="houseHoldLimit"></param> Budget limit that the user wishes to have for household expenses.
        /// <param name="consumptionLimit"></param> Budget limit that the user wishes to have for consumption expenses.
        /// <param name="transportLimit"></param> Budget limit that the user wishes to have for transportation expenses.
        /// <param name="savingsLimit"></param> Budget limit that the user wishes to have for savings expenses.
        public async Task<bool> UpdateBudget(string username, float loanLimit, float groceryLimit, float houseHoldLimit, float consumptionLimit, float transportLimit, float savingsLimit) {
            var uri = "https://financebuddyapi20190515092853.azurewebsites.net/api/budget/" + username;
            HttpClient client = new HttpClient();
            JObject body = new JObject
            {
                {"Username", username},
                {"LoanLimit", loanLimit},
                {"GroceryLimit", groceryLimit},
                {"HouseholdLimit", houseHoldLimit},
                {"ConsumptionLimit", consumptionLimit},
                {"TransportLimit", transportLimit},
                {"SavingsLimit", savingsLimit}
            };

            var content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode) {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteBudget(string username) {
            var uri = "https://financebuddyapi20190515092853.azurewebsites.net/api/budget/" + username;
            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<List<float>> GetBudgetLimits(string username)
        {
            List<float> limits = new List<float>();
            var uri = "https://financebuddyapi20190515092853.azurewebsites.net/api/budget/" + username;
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(uri);
            var tmp = response.Replace("[", "").Replace("]", "").Split(',');

            foreach (var element in tmp) {
                limits.Add(float.Parse(element));
            }
            return limits;
        }
    }
}
