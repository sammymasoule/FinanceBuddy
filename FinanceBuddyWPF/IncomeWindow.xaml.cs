using FinanceBuddyWPF.Controllers;
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

namespace FinanceBuddyWPF
{
    /// <summary>
    /// Interaction logic for IncomeWindow.xaml
    /// </summary>
    public partial class IncomeWindow : Window
    {
        public IncomeWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;


        }
        string dato;
        
        DatabaseActions dbActions = new DatabaseActions();
        string userName = MainWindow.username;



        /// <summary>
        ///  Method for validating income variables and if validated create new income for the user.
        /// </summary>

        private void Income_Click(object sender, RoutedEventArgs e)
        {
            float amount = 0;
            bool check_amount = true;
            bool check_dato = true;
            if(!float.TryParse(IndtjeningTXT.Text, out amount))
            {
                check_amount = false;
                IndtejningFejl.Visibility = Visibility.Visible;
                IndtjeningTXT.BorderBrush = new SolidColorBrush(Colors.Red);
                
            }
            if (String.IsNullOrEmpty(dato))
            {
                check_dato = false;
                DatoFejl.Visibility = Visibility.Visible;
               

            }
            if(check_amount && check_dato)
            {
                if (dbActions.CreateIncome(amount, dato, userName, BeskrivelseTXT.Text))
                {
                    MessageBox.Show("Din indkomst er blevet oprettet");
                    IndtejningFejl.Visibility = Visibility.Hidden;
                    DatoFejl.Visibility = Visibility.Hidden;

                    IndtjeningTXT.Text = null;
                    dato = null;
                    dateTimePicker.SelectedDate = null;
                    BeskrivelseTXT.Text = "";
                    IndtjeningTXT.BorderBrush = new SolidColorBrush(Colors.Gray);
                    
                }

            }



        }

        private void LogOutMenuItemClick(object sender, RoutedEventArgs e)
        {

            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;


            if (date != null)
            {
                dato = date.Value.ToString("yyyy-MM-dd");
            }
            
                
          
        }

        private void Overview_Click(object sender, RoutedEventArgs e)
        {
            OverviewWindow window = new OverviewWindow();
            window.Show();
            Close();
        }

        private void OverviewRedirect_Click(object sender, RoutedEventArgs e) {
            Overview_Click(sender, e);
        }

        private void Expenses_Click(object sender, RoutedEventArgs e) {
            OutcomeWindow window = new OutcomeWindow();
            window.Show();
            Close();
        }

        private void BudgetOverview_Click(object sender, RoutedEventArgs e) {
            BudgetWindow window = new BudgetWindow();
            window.Show();
            Close();
        }
    }
}
