using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FinanceBuddyWPF.Controllers
{

    /// <summary>
    /// Class that handles all actions connected to the database.
    /// </summary>
    public class DatabaseActions
    {
        readonly SqlConnectionStringBuilder builder =
            new SqlConnectionStringBuilder
            {
                DataSource = "samsamjon.database.windows.net",
                UserID = "samsamjon",
                Password = "Test1234",
                InitialCatalog = "samjonDB"
            };

        /// <summary>
        /// Method that creates an income for a specific user.
        /// </summary>
        /// <param name="amount"></param> Amount of money of the income.
        /// <param name="date"></param> When the income was received.
        /// <param name="userName"></param> The user name of the user logged in.
        /// <param name="description"></param> A short description of the income.
        public bool CreateIncome(float amount, string date, string userName, string description)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO Income ([Amount], [Date], [userName], [Description])");
                    sb.Append("VALUES ('" + amount + "', '" + date + "', '" + userName +"', '" + description + "')");
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        if (command.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.ToString());
            }
            return false;
        }

        /// <summary>
        /// Method for creating a user.
        /// </summary>
        /// <param name="lastName"></param> Last name of user.
        /// <param name="firstName"></param> First name of the user.
        /// <param name="userName"></param> Chosen user name for the user.
        /// <param name="password"></param> Chosen password for the user.
        public bool CreateUser(string lastName, string firstName, string userName, string password) {
            try {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO Person ([LastName], [FirstName], [UserName], [Password])");
                    sb.Append(
                        "VALUES ('" + lastName + "', '" + firstName + "', '" + userName + "', '" + password + "')");
                    string sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        if (command.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.ToString());
            }
            return false;
        }

        /// <summary>
        /// Method for logging into the application.
        /// </summary>
        /// <param name="userName"></param> User name of the user.
        /// <param name="password"></param> Password related to the specific user.
        public bool UserLogin(string userName, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select COUNT(1) from Person where UserName ='" + userName + "' AND Password= '" +
                              password + "'");
                    string sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        if (count == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.ToString());
            }
            return false;
        }

        /// <summary>
        /// Method for getting the total income of a user from and to a specific date.
        /// </summary>
        /// <param name="userName"></param> Chosen user name for the user.
        /// <param name="firstDay"></param> Chosen start date.
        /// <param name="lastDay"></param> Chosen end date.
        public float GetIncome(string userName, string firstDay, string lastDay)
        {
            try
            {
                float amount;
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select SUM(Amount) from Income where userName = '" + userName + "'" +
                              " AND Date between '" +firstDay +"' AND '" + lastDay +  "'");
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        var value = command.ExecuteScalar();
                        if (!String.IsNullOrEmpty(value.ToString()))
                        {
                            amount = float.Parse(value.ToString());
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                return amount;
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.ToString());
            }
            return -1;
        }

        /// <summary>
        /// Method for getting the average expenses of all users not including the user currently logged in.
        /// </summary>
        /// <param name="userName"></param> User name of current logged in user.
        /// <param name="cat"></param> Chosen category to compare expenses.
        /// <param name="firstDay"></param> Chosen start date.
        /// <param name="lastDay"></param> Chosen end date.
        public float GetAvgExpensesOthers(string userName, string cat, string firstDay, string lastDay)
        {
            try
            {
                float amount;
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select SUM(Amount) / COUNT(Distinct userName) from TransItem where userName NOT IN ('" + userName + "')" +
                              " AND Category = '" + cat + "' AND Date between '" + firstDay + "' AND '" + lastDay + "'");
                    string sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        var value = command.ExecuteScalar();
                        if (!string.IsNullOrEmpty(value.ToString()))
                        {
                            amount = float.Parse(value.ToString());
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                return amount;
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.ToString());
            }
            return -1;
        }

        /// <summary>
        /// Method for getting the sum of the user's expenses from a single category in a specific time frame.
        /// </summary>
        /// <param name="userName"></param> User name of current logged in user.
        /// <param name="cat"></param> Chosen category to compare expenses.
        /// <param name="firstDay"></param> Chosen start date.
        /// <param name="lastDay"></param> Chosen end date.
        public float GetAvgExpenses(string userName, string cat, string firstDay, string lastDay)
        {
            try
            {
                float amount;
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select SUM(Amount) from TransItem where userName = '" + userName + "'" +
                              " AND Category = '" + cat + "' AND Date between '" + firstDay + "' AND '" + lastDay + "'");
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        var value = command.ExecuteScalar();
                        if (!String.IsNullOrEmpty(value.ToString()))
                        {
                            amount = float.Parse(value.ToString());
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                return amount;
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.ToString());
            }
            return -1;
        }

        /// <summary>
        /// Method for getting the sum of the user's expenses in categorized order.
        /// </summary>
        /// <param name="userName"></param> User name of current logged in user.
        /// <param name="firstDay"></param> Chosen start date.
        /// <param name="lastDay"></param> Chosen end date.
        public List<KeyValuePair<string, float>> GetExpenses(string userName, string firstDay, string lastDay)
        {
            try
            {
                List<KeyValuePair<string, float>> expenses = new List<KeyValuePair<string, float>>();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select Category, Amount from TransItem where userName = '" + userName + "' " +
                    " AND Date between '" + firstDay +"' AND '" +lastDay + "'");
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                expenses.Add(new KeyValuePair<string, float>(reader.GetString(0), (float)reader.GetDouble(1)));
                            }
                        }
                    }
                }
                return expenses;
            }
            catch (SqlException exception) {
                Console.WriteLine(exception.ToString());
            }
            return new List<KeyValuePair<string, float>>();
        }

        /// <summary>
        /// Method for creating an expense related to a specific user.
        /// </summary>
        /// <param name="category"></param> Which category the expense falls under.
        /// <param name="description"></param> Short description of the expense.
        /// <param name="date"></param> Which date the expense occured.
        /// <param name="userName"></param> User name of current logged in user.
        /// <param name="amount"></param> Cost of the expense.
        public bool CreateExpense(string category, string description, string date, string userName, float amount) {
            try {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO TransItem ([Category], [Description], [Date], [userName], [Amount])");
                    sb.Append("VALUES ('" + category + "', '" + description+ "', '" + date + "', '" + userName+ "', '" + amount + "')");
                    //sb.Append("VALUES ('{0}', '{1}', '{2}', '{3}')", lastName, firstName, userName, password
                    string sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        if (command.ExecuteNonQuery() > 0) {
                            return true;
                        }
                    }
                }
            }
            catch (SqlException exception) {
                Console.WriteLine(exception.ToString());
            }
            return false;
        }

        /// <summary>
        /// Method for creating a budget for a specific user. The budget helps one keep track of how much money they are allowed to spend.
        /// </summary>
        /// <param name="username"></param> User name of current logged in user.
        /// <param name="loanLimit"></param> Budget limit that the user wishes to have for loan expenses.
        /// <param name="houseHoldLimit"></param> Budget limit that the user wishes to have for household expenses.
        /// <param name="consumptionLimit"></param> Budget limit that the user wishes to have for consumption expenses.
        /// <param name="transportLimit"></param> Budget limit that the user wishes to have for transportation expenses.
        /// <param name="savingsLimit"></param> Budget limit that the user wishes to have for savings expenses.
        public bool CreateBudget(string username, float loanLimit, float houseHoldLimit, float consumptionLimit, float transportLimit, float savingsLimit) {
            try {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO Budget ([userName], [LoanLimit], [HouseHoldLimit], [ConsumptionLimit], [TransportationLimit], [SavingsLimit])");
                    sb.Append(" VALUES ('"  + username + "', '" + loanLimit + "', '" + houseHoldLimit+ "', '" + consumptionLimit+ "', '" + transportLimit+ "', '" + savingsLimit+ "')");
                    //sb.Append("VALUES ('{0}', '{1}', '{2}', '{3}')", lastName, firstName, userName, password);
                    string sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        if (command.ExecuteNonQuery() > 0) {
                            return true;
                        }
                    }
                }
            }
            catch (SqlException exception) {
                Console.WriteLine(exception.ToString());
            }
            return false;
        }

        /// <summary>
        /// Method for retreiving the budget limits for each category.
        /// </summary>
        /// <param name="userName"></param> User name of current logged in user.
        public List<float> GetBudgetLimits(string userName) {
            try {
                List<float> limits = new List<float>();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select LoanLimit, GroceryLimit, HouseHoldLimit, ConsumptionLimit, TransportationLimit, SavingsLimit from Budget where userName = '" + userName +"'");
                    string sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                limits.Add(float.Parse(reader["LoanLimit"].ToString()));
                                limits.Add(float.Parse(reader["GroceryLimit"].ToString()));
                                limits.Add(float.Parse(reader["HouseHoldLimit"].ToString()));
                                limits.Add(float.Parse(reader["ConsumptionLimit"].ToString()));
                                limits.Add(float.Parse(reader["TransportationLimit"].ToString()));
                                limits.Add(float.Parse(reader["SavingsLimit"].ToString()));
                            }
                        }
                    }
                }

                return limits;
            }
            catch (SqlException exception) {
                Console.WriteLine(exception.ToString());
            }
            return null;
        }

        /// <summary>
        /// Method for changing/updating a current budget for a specific user.
        /// </summary>
        /// <param name="username"></param> User name of current logged in user.
        /// <param name="loanLimit"></param> Budget limit that the user wishes to have for loan expenses.
        /// <param name="houseHoldLimit"></param> Budget limit that the user wishes to have for household expenses.
        /// <param name="consumptionLimit"></param> Budget limit that the user wishes to have for consumption expenses.
        /// <param name="transportLimit"></param> Budget limit that the user wishes to have for transportation expenses.
        /// <param name="savingsLimit"></param> Budget limit that the user wishes to have for savings expenses.
        public bool UpdateBudget(string username, float loanLimit, float groceryLimit, float houseHoldLimit, float consumptionLimit, float transportLimit, float savingsLimit) {
            try {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Update Budget set LoanLimit =" + loanLimit + ", GroceryLimit = " + groceryLimit + ", HouseHoldLimit = " + houseHoldLimit + ", ConsumptionLimit= " + consumptionLimit + ", TransportationLimit= "+ transportLimit + ", SavingsLimit=" + savingsLimit + " where username='" + username + "'");
                    //sb.Append("VALUES ('{0}', '{1}', '{2}', '{3}')", lastName, firstName, userName, password);

                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        if (command.ExecuteNonQuery() > 0) {
                            return true;
                        }
                    }
                }
            }
            catch (SqlException exception) {
                Console.WriteLine(exception.ToString());
            }
            return false;
        }
    }
}
