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

namespace FinanceBuddyWPF
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
            var loanlimit = float.Parse(NewLoanTxt.Text);
            var householdLimit = float.Parse(NewHouseholdTxt.Text);
            var consumptionLimit = float.Parse(NewConsumptionTxt.Text);
            var transportLimit = float.Parse(NewTransportTxt.Text);
            var savingsLimit = float.Parse(NewSavingsTxt.Text);

            dbActions.UpdateBudget(username, loanlimit, householdLimit, consumptionLimit, transportLimit, savingsLimit);
            OldConsumptionTxt.Text = consumptionLimit.ToString();
            OldHouseholdTxt.Text = householdLimit.ToString();
            OldSavingsTxt.Text = savingsLimit.ToString();
            OldLoanTxt.Text = loanlimit.ToString();
            OldTransportTxt.Text = transportLimit.ToString();



        }
    }
}
