using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FinanceBuddyWPF.Controllers;

namespace FinanceBuddyWPF {
    /// <summary>
    /// Interaction logic for OverviewWindow.xaml
    /// </summary>
    public partial class OverviewWindow : Window {
        public OverviewWindow() {
            InitializeComponent();
            LoadChart();
        }

        private readonly DatabaseActions dbActions = new DatabaseActions();
        private void LoadChart()
        {
            var userName = ((MainWindow) Application.Current.MainWindow)?.UsernameTXT.Text;
            var income = dbActions.GetIncome(userName);
            var expenses = dbActions.GetExpenses(userName);
            var totalExpenses = expenses.Sum(x => x);

            List<KeyValuePair<string, float>> valueList = new List<KeyValuePair<string, float>>
            {
                new KeyValuePair<string, float>("Indkomst", income),
                new KeyValuePair<string, float>("Udgift", totalExpenses),
            };
            pieChart.DataContext = valueList;
        }
        private void LogOutMenuItemClick(object sender, RoutedEventArgs e) {

            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }
        
    }

}
