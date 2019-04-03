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
            WindowState = WindowState.Maximized;
            LoadPieChart();
        }

        private readonly DatabaseActions dbActions = new DatabaseActions();
        string userName = MainWindow.username;

        private void LoadPieChart()
        {
            var income = dbActions.GetIncome(userName);
            var expenses = dbActions.GetExpenses(userName);
            var totalExpenses = expenses.Sum(x => x.Value);

            List<KeyValuePair<string, float>> valueList = new List<KeyValuePair<string, float>>
            {
                new KeyValuePair<string, float>("Indkomst", income),
                new KeyValuePair<string, float>("Udgift", totalExpenses),
            };

            pieChart.DataContext = valueList;
            LoadBarChart(expenses);
        }

        private void LoadBarChart(List<KeyValuePair<int, float>> list)
        {
            List<KeyValuePair<string, float>> valuelist = new List<KeyValuePair<string, float>>();
            var myResults = list.GroupBy(p => p.Key)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.Value));

            foreach (var element in myResults)
            {
                if (element.Key == 1)
                {

                }
            }

            BarChart.DataContext = myResults;
        }

        private void LogOutMenuItemClick(object sender, RoutedEventArgs e)
        {
            MainWindow.username = null;
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }
        
    }

}
