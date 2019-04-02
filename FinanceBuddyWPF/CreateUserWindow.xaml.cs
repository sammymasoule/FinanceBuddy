using System;
using System.Collections.Generic;
using System.Linq;
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
        }
        DatabaseActions dbActions = new DatabaseActions();
        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            if (PasswordTXT.Password.Equals(PasswordConfTXT.Password))
            {
                if (dbActions.CreateUser(LastNameTXT.Text, FirstNameTXT.Text, UserNameTXT.Text, PasswordTXT.Password)) 
                {
                    //dbActions.UserLogin(UserNameTXT.Text, PasswordTXT.Password)
                    MainWindow main = new MainWindow();
                    main.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Brugernavn er allerede i brug.");
                }
                
            }
            else
            {
                MessageBox.Show("Kodeordene stemmer ikke overens.");
            }

            
        }
    }
}
