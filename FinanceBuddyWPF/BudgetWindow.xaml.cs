using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using FinanceBuddyWPF.Controllers;

namespace FinanceBuddyWPF {
    /// <summary>
    /// Interaction logic for BudgetWindow.xaml
    /// </summary>
    public partial class BudgetWindow : Window {

        private DatabaseActions dbActions = new DatabaseActions();
        private DataUtilites DataU = new DataUtilites();

        public BudgetWindow() {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            string userName = MainWindow.username;
            LoadBudgetValues(userName);
        }

        private void LoadBudgetValues(string userName)
        {
            DateTime dateNow = DateTime.Now;
            var startDate = new DateTime(dateNow.Year, dateNow.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var month = DataU.getMonth(startDate, endDate);
            monthComboBox.Text = month;

            List<float> limits = dbActions.GetBudgetLimits(userName);
            List<KeyValuePair<string, float>> expenses = dbActions.GetExpenses(userName, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
            List<KeyValuePair<string, float>> valuelist = new List<KeyValuePair<string, float>>();
            var myResults = expenses.GroupBy(p => p.Key)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.Value));

            var loanMaxValue = 0.0;
            var indMaxValue = 0.0;
            var householdMaxValue = 0.0;
            var consmpMaxValue = 0.0;
            var transMaxValue = 0.0;
            var savingsMaxValue = 0.0;

            if (limits != null)
            {
                loanMaxValue = limits[0];
                indMaxValue = limits[1];
                householdMaxValue = limits[2];
                consmpMaxValue = limits[3];
                transMaxValue= limits[4];
                savingsMaxValue= limits[5];
            }
            
            myResults.TryGetValue("Lån & Regninger", out var currentLoan);
            myResults.TryGetValue("Indkøb", out var currentInd);
            myResults.TryGetValue("Husholdning", out var currentHouseHold);
            myResults.TryGetValue("Forbrug", out var currentConsmp);
            myResults.TryGetValue("Transport", out var currentTransport);
            myResults.TryGetValue("Opsparing", out var currentSavings);

            loanMax.Text =loanMaxValue.ToString();
            indMax.Text = indMaxValue.ToString();
            householdMax.Text = householdMaxValue.ToString();
            consmpMax.Text = consmpMaxValue.ToString();
            transMax.Text = transMaxValue.ToString();
            opsMax.Text = savingsMaxValue.ToString();

            curLoan.Text = currentLoan.ToString();
            curInd.Text = currentInd.ToString();
            curHousehold.Text = currentHouseHold.ToString();
            curConsmp.Text = currentConsmp.ToString();
            curTrans.Text = currentTransport.ToString();
            curOps.Text = currentSavings.ToString();

            diffLoan.Text = (loanMaxValue - currentLoan).ToString();
            diffInd.Text = (indMaxValue - currentInd).ToString();
            diffHousehold.Text = (householdMaxValue - currentHouseHold).ToString();
            diffConsmp.Text = (consmpMaxValue - currentConsmp).ToString();
            diffTrans.Text = (transMaxValue - currentTransport).ToString();
            diffOps.Text = (savingsMaxValue - currentSavings).ToString();
        }

        private void LogOutMenuItemClick(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void CreateBudget_Click(object sender, RoutedEventArgs e) {
           CreateBudgetWindow window = new CreateBudgetWindow();
            window.Show();
            Close();
        }

        private void UpdateBudget_Click(object sender, RoutedEventArgs e) {
            UpdateBudgetWindow window = new UpdateBudgetWindow();
            window.Show();
            Close();
        }

        private void BudgetMonth_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Overview_Click(object sender, RoutedEventArgs e) {
            OverviewWindow window = new OverviewWindow();
            window.Show();
            Close();
        }
    }
}
