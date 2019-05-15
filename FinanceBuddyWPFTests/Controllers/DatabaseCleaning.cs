using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceBuddyWPFTests.Controllers {
    class DatabaseCleaning {
        readonly SqlConnectionStringBuilder builder =
            new SqlConnectionStringBuilder
            {
                DataSource = "samsamjon.database.windows.net",
                UserID = "samsamjon",
                Password = "Test1234",
                InitialCatalog = "samjonDB"
            };

        /// <summary>
        /// Method to delete all income test data in database.
        /// </summary>
        public bool DeleteIncomeTests() {
            try {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("DELETE FROM INCOME WHERE Description = UnitTesting");
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
        public bool DeleteExpensesTests()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("DELETE FROM TRANSITEM WHERE UserName = UnitTesting");
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
        /// Method to delete all user test data in database.
        /// </summary>
        public bool DeleteUserTests(string username) {
            try {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("DELETE FROM Person WHERE userName ='" + username + "'");
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
        /// Method to delete all user test data in database.
        /// </summary>
        public bool DeleteBudgetTests(string username) {
            try {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("DELETE FROM Budget WHERE userName ='" + username +"'");
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
