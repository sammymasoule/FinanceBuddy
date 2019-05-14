using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FinanceBuddyWPF.Controllers;

namespace FinanceBuddyWPF.View {
    /// <summary>
    /// Interaction logic for BudgetWindow.xaml
    /// </summary>
    public partial class BudgetWindow : Window {

        private DatabaseActions dbActions = new DatabaseActions();
        private DataUtilites DataU = new DataUtilites();
        string userName = MainWindow.username;
        public BudgetWindow() {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            LoadBudgetValues(userName, null, null);
        }

        /// <summary>
        /// Method for retrieving budget data from the user and displaying it.
        /// </summary>
        /// <param name="username"></param> User name of currently logged in user.
        /// <param name="datefrom"></param> Start date.
        /// <param name="dateto"></param> End date.
        /// the users username.
        private void LoadBudgetValues(string username, string datefrom, string dateto)
        {
            List<KeyValuePair<string, float>> expenses;
            if (datefrom != null && dateto != null)
            {
                expenses = dbActions.GetExpenses(username, datefrom, dateto);
            }
            else
            {
                DateTime dateNow = DateTime.Now;
                var startDate = new DateTime(dateNow.Year, dateNow.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                var month = DataU.GetMonth(startDate, endDate);
                monthComboBox.Text = month;
                expenses = dbActions.GetExpenses(username, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
            }


            List<float> limits = dbActions.GetBudgetLimits(username);

            var myResults = expenses.GroupBy(p => p.Key)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.Value));

            var loanMaxValue = 0.0;
            var indMaxValue = 0.0;
            var householdMaxValue = 0.0;
            var consmpMaxValue = 0.0;
            var transMaxValue = 0.0;
            var savingsMaxValue = 0.0;

            if (limits != null && limits.Count !=0)
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

            List<float> limits = dbActions.GetBudgetLimits(userName);
            UpdateBudgetWindow window = new UpdateBudgetWindow();
            if (limits != null && limits.Count != 0)
            {
         
                window.Show();
                Close();
            } else
            {
                MessageBox.Show("Lav venligst et budget først");
            }
           
        }

        private void BudgetMonth_Click(object sender, RoutedEventArgs e)
        {
            var month = monthComboBox.Text;
            var tmpmonth = DataU.GetMonthByName(month).Split(' ');
            LoadBudgetValues(userName, tmpmonth[0], tmpmonth[1]);

        }

        private void Overview_Click(object sender, RoutedEventArgs e) {
            OverviewWindow window = new OverviewWindow();
            window.Show();
            Close();
        }
    }
}
