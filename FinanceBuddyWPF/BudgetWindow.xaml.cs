using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using FinanceBuddyWPF.Controllers;

namespace FinanceBuddyWPF {
    /// <summary>
    /// Interaction logic for BudgetWindow.xaml
    /// </summary>
    public partial class BudgetWindow : Window {

        private DatabaseActions dbActions = new DatabaseActions();

        public BudgetWindow() {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            string userName = MainWindow.username;
            LoadBudgetValues(userName);
        }

        private void LoadBudgetValues(string userName)
        {
            List<float> limits = dbActions.GetBudgetLimits(userName);
            //var month = comboBox.Index || comboBox.Text
            List<KeyValuePair<string, float>> expenses = dbActions.GetExpenses(userName, "2019/04/01", "2019/04/30");
            
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
    }
}
