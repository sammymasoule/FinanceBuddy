using System;
using System.Collections.Generic;
using System.Globalization;
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
        static DateTime date = DateTime.Now;
        string month = new DateTime(2015, date.Month, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("dk"));
        //private readonly DataUtilites DataU = new DataUtilites();

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
            /*var tmpdate = DataU.GetCurrentMonth();
            var stringdate = tmpdate.Split(' '); */
            var yourAmount = dbActions.GetAvgExpenses(userName, katSelected, "fix", "fix");
            var othersAmount = dbActions.GetAvgExpensesOthers(userName, katSelected, "fix", "fix");
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
            DateTime? datefrom = DateFromSidePie.SelectedDate;
            DateTime? dateto = DateToSidePie.SelectedDate;

            LoadSidePieChart();
        }
    }

}
