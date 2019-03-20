using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace FinanceBuddyWPF.Controllers {
    class DatabaseActions {

        public List<string> GetPersonList() {
            List<string> persons = new List<string>();
            try {

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "samsamjon.database.windows.net";
                builder.UserID = "samsamjon";
                builder.Password = "Test1234";
                builder.InitialCatalog = "samjonDB";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT * FROM Person");
                    //sb.Append("INSERT INTO Person ([LastName], [FirstName], [UserName], [Password])");
                    //sb.Append("VALUES ('Testson', 'Test', 'test', 'test')");
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                int id = reader.GetInt32(0);
                                string LastName = reader.GetString(1);
                                string FirstName = reader.GetString(2);
                                string UserName = reader.GetString(3);

                                string item = id.ToString() + " " + LastName + " " + FirstName + " " + UserName;
                                persons.Add(item);
                            }
                        }
                    }
                }
            }
            catch (SqlException exception) {
                Console.WriteLine(exception.ToString());
            }

            return persons;
        }

        public string GetPerson(string username) {
            string person = "";
            try {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "samsamjon.database.windows.net";
                builder.UserID = "samsamjon";
                builder.Password = "Test1234";
                builder.InitialCatalog = "samjonDB";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT * FROM Person WHERE UserName = '" + username + "'");
                    //sb.Append("INSERT INTO Person ([LastName], [FirstName], [UserName], [Password])");
                    //sb.Append("VALUES ('Testson', 'Test', 'test', 'test')");
                    string sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                string LastName = reader.GetString(1);
                                string FirstName = reader.GetString(2);
                                string UserName = reader.GetString(3);

                                person = LastName + " " + FirstName + " " + UserName;
                            }
                        }
                    }
                }
            }
            catch (SqlException exception) {
                Console.WriteLine(exception.ToString());
            }

            return person;
        }

        public bool createUser(string lastName, string firstName, string userName, string password) {
            try {
                SqlConnectionStringBuilder builder =
                    new SqlConnectionStringBuilder
                    {
                        DataSource = "samsamjon.database.windows.net",
                        UserID = "samsamjon",
                        Password = "Test1234",
                        InitialCatalog = "samjonDB"
                    };

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO Person ([LastName], [FirstName], [UserName], [Password])");
                    sb.Append("VALUES ('" + lastName + "', '" + firstName + "', '" + userName + "', '" + password + "')");
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

        public bool UserLogin(string userName, string password)
        {
            try {
                SqlConnectionStringBuilder builder =
                    new SqlConnectionStringBuilder
                    {
                        DataSource = "samsamjon.database.windows.net",
                        UserID = "samsamjon",
                        Password = "Test1234",
                        InitialCatalog = "samjonDB"
                    };

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select COUNT(1) from Person where UserName ='" + userName + "' AND Password= '" + password + "'");
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
            catch (SqlException exception) {
                Console.WriteLine(exception.ToString());
            }
            return false;
        }
    }
}
