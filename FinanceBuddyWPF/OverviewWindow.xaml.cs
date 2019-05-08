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
            LoadSidePieChart();
        }

        private readonly DatabaseActions dbActions = new DatabaseActions();
        string userName = MainWindow.username;
        string katSelected = "Husholdning";
        bool handle = true;

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
        private void LoadSidePieChart()
        {
            var yourAmount = dbActions.GetTest2(userName, katSelected);
            var othersAmount = dbActions.GetTest(userName, katSelected);
            List<KeyValuePair<string, float>> valueList = new List<KeyValuePair<string, float>>
            {
                new KeyValuePair<string, float>("Dine udgifter", yourAmount),
                new KeyValuePair<string, float>("Andres udgifter", othersAmount),
            };

            pieChart2.DataContext = valueList;
        }

        private void LoadBarChart(List<KeyValuePair<string, float>> list)
        {
            List<KeyValuePair<string, float>> valuelist = new List<KeyValuePair<string, float>>();
            var myResults = list.GroupBy(p => p.Key)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.Value));

            BarChart.DataContext = myResults;
        }

        private void LogOutMenuItemClick(object sender, RoutedEventArgs e)
        {
            MainWindow.username = null;
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void IndkomstItemClick(object sender, RoutedEventArgs e)
        {

            IncomeWindow window = new IncomeWindow();
            window.Show();
            Close();
        }
       
        private void SidePieButton_Click(object sender, RoutedEventArgs e)
        {
            katSelected = katComboBox.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last();
            LoadSidePieChart();
        }
    }

}
