using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FinanceBuddyWPF.Controllers;

namespace FinanceBuddyWPF {
    /// <summary>
    /// Interaction logic for CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window {
        public CreateUserWindow() {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }
        Boolean firstName = false;
        Boolean lastName = false;
        Boolean userNameBool = false;
        Boolean passwordBool = false;
        DatabaseActions dbActions = new DatabaseActions();
        /// <summary>
        /// Method for checking if inputs are valid and then createing the user if so.
        /// </summary>
        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {   
            FirstNameTXT.BorderBrush = new SolidColorBrush(Colors.Gray);
            LastNameTXT.BorderBrush = new SolidColorBrush(Colors.Gray);
            UserNameTXT.BorderBrush = new SolidColorBrush(Colors.Gray);
            PasswordTXT.BorderBrush = new SolidColorBrush(Colors.Gray);
            PasswordConfTXT.BorderBrush = new SolidColorBrush(Colors.Gray);

            if (!String.IsNullOrEmpty(LastNameTXT.Text))
            {
                firstName = true;
            }
            else
            {
                FirstNameError.Content = "Indtast venligst dit fornavn";
                FirstNameError.Visibility = Visibility.Visible;
                FirstNameTXT.BorderBrush = new SolidColorBrush(Colors.Red);
               
            }
            if (!String.IsNullOrEmpty(FirstNameTXT.Text))
            {
                lastName = true;
            }
            else
            {
                LastNameError.Content = "Indtast venligst dit efternavn";
                LastNameError.Visibility = Visibility.Visible;
                LastNameTXT.BorderBrush = new SolidColorBrush(Colors.Red);
                
            }
            if (!String.IsNullOrEmpty(UserNameTXT.Text))
            {
                userNameBool = true;
            }
            else
            {
                userNameError.Content = "Indtast venligst et brugernavn";
                userNameError.Visibility = Visibility.Visible;
                UserNameTXT.BorderBrush = new SolidColorBrush(Colors.Red);
                
            }
            if (!String.IsNullOrEmpty(PasswordTXT.Password))
            {
                passwordBool = true;
            }
            else
            {
                passwordError.Content = "Indtast venligst et password";
                passwordError.Visibility = Visibility.Visible;
                PasswordTXT.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            if (PasswordTXT.Password.Equals(PasswordConfTXT.Password))
            {
                if (firstName && lastName && userNameBool && passwordBool)
                {
                    DataUtilites dataUtil = new DataUtilites();

                    string hashedPassword = dataUtil.HashPassword(PasswordTXT.Password); 
                    if (dbActions.CreateUser(LastNameTXT.Text, FirstNameTXT.Text, UserNameTXT.Text, hashedPassword))
                    {
                        userNameError.Visibility = Visibility.Hidden;
                        passwordError.Visibility = Visibility.Hidden;
                        MainWindow main = new MainWindow();
                        main.Show();
                        Close();
                    }
                    else
                    {
                        userNameError.Content = "Brugernavnet eksisterer allerede";
                        userNameError.Visibility = Visibility.Visible;
                        UserNameTXT.BorderBrush = new SolidColorBrush(Colors.Red);

                    }
                }

            }
            else
            {
                passwordCheckError.Content = "Kodeordene stemmer ikke overens";
                passwordCheckError.Visibility = Visibility.Visible;
                PasswordConfTXT.BorderBrush = new SolidColorBrush(Colors.Red);
                
            }
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
        
    }
}
