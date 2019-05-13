using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FinanceBuddyWPF.Controllers;

namespace FinanceBuddyWPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }
       
        private readonly DatabaseActions dbActions = new DatabaseActions();
        public static string username;
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            username = UsernameTXT.Text;

            DataUtilites dataUtil = new DataUtilites();
            string hashedPassword = dataUtil.Sha255Hash(PasswordTXT.Password);
            
            if (dbActions.UserLogin(UsernameTXT.Text, hashedPassword))
            {
                username = UsernameTXT.Text;
                UsernameTXT.BorderBrush = new SolidColorBrush(Colors.Gray);
                PasswordTXT.BorderBrush = new SolidColorBrush(Colors.Gray);
                OverviewWindow ow = new OverviewWindow();
                ow.Show();
                Close();
            }
            else
            { 
                Fejl.Content = "Brugernavn eller password er forkert";
                Fejl.Visibility = Visibility.Visible;
                UsernameTXT.BorderBrush = new SolidColorBrush(Colors.Red);
                PasswordTXT.BorderBrush = new SolidColorBrush(Colors.Red);
              
                
            }
            
        }

        private void Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CreateUserWindow createUser = new CreateUserWindow();
            createUser.Show();
            Close();
        }

        private void PasswordTXT_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }
        
    }
}
