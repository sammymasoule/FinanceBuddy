using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
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
            DateTime date = DateTime.Now;
            string month = new DateTime(2015, date.Month, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("dk"));
            LoadPieChart();
            LoadBarChart(expenses, month);
        }


        private readonly DatabaseActions dbActions = new DatabaseActions();
        List<KeyValuePair<string, float>> expenses = new List<KeyValuePair<string, float>>();
        private readonly string userName = MainWindow.username;
        private readonly DataUtilites DataU = new DataUtilites();

        private void LoadPieChart()
        {
            var tmpdate = DataU.GetCurrentMonth();
            var stringdate = tmpdate.Split(' ');

            var income = dbActions.GetIncome(userName, stringdate[0], stringdate[1]);
            expenses = dbActions.GetExpenses(userName, stringdate[0], stringdate[1]);

            var totalExpenses = expenses.Sum(x => x.Value);


            List<KeyValuePair<string, float>> valueList = new List<KeyValuePair<string, float>>
            {
                new KeyValuePair<string, float>("Indkomst", income),
                new KeyValuePair<string, float>("Udgift", totalExpenses),
            };

            pieChart.DataContext = valueList;
        }

        private void LoadBarChart(List<KeyValuePair<string, float>> list, string month)
        {
            List<KeyValuePair<string, float>> valuelist = new List<KeyValuePair<string, float>>();
            var myResults = list.GroupBy(p => p.Key)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.Value));

            Series.Title = month;
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

        private void LoadBarChartByDate(string firstDay, string lastDay, string month)
        {
            var list = dbActions.GetExpenses(userName, firstDay, lastDay);
            List<KeyValuePair<string, float>> valuelist = new List<KeyValuePair<string, float>>();
            var myResults = list.GroupBy(p => p.Key)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.Value));

            Series.Title = month;
            BarChart.DataContext = myResults;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime? datefrom = DateFrom.SelectedDate;
            DateTime? dateto = DateTo.SelectedDate;

            if (datefrom != null && dateto !=null)
            {
                var tmpdate = DataU.GetDateFormat(datefrom, dateto);
                var stringdatefrom = tmpdate.Split(' ');

                var month = DataU.getMonth(datefrom, dateto);
                LoadBarChartByDate(stringdatefrom[0], stringdatefrom[1], month);
            }
            else
            {
                MessageBox.Show("Indtast venligst en dato");
            }
        }

        private void OutcomeWindow(object sender, RoutedEventArgs e) {
            OutcomeWindow window = new OutcomeWindow();
            window.Show();
            Close();
        }

        private void Budget_click(object sender, RoutedEventArgs e) {
            BudgetWindow window = new BudgetWindow();
            window.Show();
            Close();
        }
    }
    }

