﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (dbActions.UserLogin(UsernameTXT.Text, PasswordTXT.Password))
            {
                
                OverviewWindow ow = new OverviewWindow();
                ow.Show();
                Close();
            }
            else
            {
                
                MessageBox.Show("Forkert brugernavn eller kodeord, prøv igen");
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
