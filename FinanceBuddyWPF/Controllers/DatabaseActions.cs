using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FinanceBuddyWPF.Controllers
{
    class DatabaseActions
    {
        readonly SqlConnectionStringBuilder builder =
            new SqlConnectionStringBuilder
            {
                DataSource = "samsamjon.database.windows.net",
                UserID = "samsamjon",
                Password = "Test1234",
                InitialCatalog = "samjonDB"
            };

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


        public bool CreateUser(string lastName, string firstName, string userName, string password) {
            try {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO Person ([LastName], [FirstName], [UserName], [Password])");
                    sb.Append(
                        "VALUES ('" + lastName + "', '" + firstName + "', '" + userName + "', '" + password + "')");
                    //sb.Append("VALUES ('{0}', '{1}', '{2}', '{3}')", lastName, firstName, userName, password);

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
        public float GetAvgExpensesOthers(string userName, string cat, String firstDay, String lastDay)
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


        public bool CreateExpense(string category, string description, string date, string userName, float amount) {
            try {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO TransItem ([Category], [Description], [Date], [userName], [Amount])");
                    sb.Append("VALUES ('" + category + "', '" + description+ "', '" + date + "', '" + userName+ "', '" + amount + "')");
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
        public bool UpdateBudget(string username, float loanLimit, float houseHoldLimit, float consumptionLimit, float transportLimit, float savingsLimit) {
            try {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Update Budget set LoanLimit =" + loanLimit + ", HouseHoldLimit = " + houseHoldLimit + ", ConsumptionLimit= " + consumptionLimit + ", TransportationLimit= "+ transportLimit + ", SavingsLimit=" + savingsLimit + " where username='" + username + "'");
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
