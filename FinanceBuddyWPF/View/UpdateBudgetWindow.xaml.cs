using System.Collections.Generic;
using System.Windows;
using FinanceBuddyWPF.Controllers;

namespace FinanceBuddyWPF.View
{
    /// <summary>
    /// Interaction logic for UpdateBudgetWindow.xaml
    /// </summary>
    public partial class UpdateBudgetWindow : Window
    {
        DatabaseActions dbActions = new DatabaseActions();
        string username = MainWindow.username;
        public UpdateBudgetWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            List<float> limits = dbActions.GetBudgetLimits(username);
            OldLoanTxt.Text = limits[0] + " kr.";
            OldHouseholdTxt.Text = limits[1] + " kr.";
            OldConsumptionTxt.Text = limits[2] + " kr.";
            OldTransportTxt.Text = limits[3] + " kr.";
            OldSavingsTxt.Text = limits[4] + " kr.";


        }



        private void TilfoejButton_Click(object sender, RoutedEventArgs e)
        {
            bool checkParsing = true;
            if (!float.TryParse(NewLoanTxt.Text, out var loan))
            {
                loanCheckError.Visibility = Visibility.Visible;
                checkParsing = false;
            }

            if (!float.TryParse(NewHouseholdTxt.Text, out var houseHold))
            {
                householdCheckError.Visibility = Visibility.Visible;
                checkParsing = false;

            }

            if (!float.TryParse(NewConsumptionTxt.Text, out var consumption))
            {
                consumpCheckError.Visibility = Visibility.Visible;
                checkParsing = false;

            }

            if (!float.TryParse(NewTransportTxt.Text, out var transport))
            {
                transportCheckError.Visibility = Visibility.Visible;
                checkParsing = false;

            }

            if (!float.TryParse(NewSavingsTxt.Text, out var savings))
            {
                savingsCheckError.Visibility = Visibility.Visible;
                checkParsing = false;

            }

            if (checkParsing)
            {

                loanCheckError.Visibility = Visibility.Hidden;
                householdCheckError.Visibility = Visibility.Hidden;
                consumpCheckError.Visibility = Visibility.Hidden;
                transportCheckError.Visibility = Visibility.Hidden;
                savingsCheckError.Visibility = Visibility.Hidden;
               

                var loanlimit = loan;
                var householdLimit = houseHold;
                var consumptionLimit = consumption;
                var transportLimit = transport;
                var savingsLimit = savings;

                dbActions.UpdateBudget(username, loanlimit, householdLimit, consumptionLimit, transportLimit, savingsLimit);
                OldConsumptionTxt.Text = consumptionLimit.ToString();
                OldHouseholdTxt.Text = householdLimit.ToString();
                OldSavingsTxt.Text = savingsLimit.ToString();
                OldLoanTxt.Text = loanlimit.ToString();
                OldTransportTxt.Text = transportLimit.ToString();
            }


        }

        private void Logout_click(object sender, RoutedEventArgs e) {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void BudgetWindow_click(object sender, RoutedEventArgs e) {
            BudgetWindow window = new BudgetWindow();
            window.Show();
            Close();
        }

        private void CreateBudget_Click(object sender, RoutedEventArgs e) {
            CreateBudgetWindow window = new CreateBudgetWindow();
            window.Show();
            Close();
        }

        private void BudgetWindowRedirect_Click(object sender, RoutedEventArgs e) {
            BudgetWindow_click(sender, e);
        }

        private void Overview_Click(object sender, RoutedEventArgs e) {
            OverviewWindow window = new OverviewWindow();
            window.Show();
            Close();
        }
    }
}
