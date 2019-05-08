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
    /// Interaction logic for CreateBudgetWindow.xaml
    /// </summary>
    public partial class CreateBudgetWindow : Window {
        public CreateBudgetWindow() {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }

        DatabaseActions dbActions = new DatabaseActions();
        string userName = MainWindow.username;

        private void Logout_click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void BudgetWindow_click(object sender, RoutedEventArgs e)
        {
            BudgetWindow window = new BudgetWindow();
            window.Show();
            Close();
        }

        private void TilfoejButton_Click(object sender, RoutedEventArgs e)
        {
            bool checkParsing = true;
            if (!float.TryParse(LoanTxt.Text, out var loan))
            {
                LoanError.Visibility = Visibility.Visible;
                checkParsing = false;
            }

            if (!float.TryParse(HouseholdTxt.Text, out var houseHold))
            {
                HouseholdError.Visibility = Visibility.Visible;
                checkParsing = false;

            }

            if (!float.TryParse(ConsumptionTxt.Text, out var consumption)) 
            {
                ConsumpError.Visibility = Visibility.Visible;
                checkParsing = false;

            }

            if (!float.TryParse(TransportTxt.Text, out var transport))
            {
                TransportError.Visibility = Visibility.Visible;
                checkParsing = false;

            }

            if (!float.TryParse(SavingsTxt.Text, out var savings))
            {
                SavingsError.Visibility = Visibility.Visible;
                checkParsing = false;

            }

            if (checkParsing)
            {
                if (dbActions.CreateBudget(userName, loan, houseHold, consumption, transport, savings))
                {
                    LoanError.Visibility = Visibility.Hidden;
                    HouseholdError.Visibility = Visibility.Hidden;
                    ConsumpError.Visibility = Visibility.Hidden;
                    TransportError.Visibility = Visibility.Hidden;
                    SavingsError.Visibility = Visibility.Hidden;
                    LoanTxt.Text = String.Empty;
                    HouseholdTxt.Text = String.Empty;
                    ConsumptionTxt.Text = String.Empty;
                    TransportTxt.Text = String.Empty;
                    SavingsTxt.Text = String.Empty;
                }
            }
        }

        private void UpdateBudget_Click(object sender, RoutedEventArgs e) {
            UpdateBudgetWindow window = new UpdateBudgetWindow();
            window.Show();
            Close();
        }

        private void Overview_Click(object sender, RoutedEventArgs e) {
            OverviewWindow window = new OverviewWindow();
            window.Show();
            Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            BudgetWindow_click(sender, e);
        }
    }
}
