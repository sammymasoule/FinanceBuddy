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
    }
}
