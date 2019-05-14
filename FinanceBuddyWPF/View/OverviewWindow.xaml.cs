using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using FinanceBuddyWPF.Controllers;

namespace FinanceBuddyWPF.View
{
    /// <summary>
    /// Interaction logic for OverviewWindow.xaml
    /// </summary>
    public partial class OverviewWindow : Window
    {
        public OverviewWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            string month = new DateTime(2015, DateTime.Now.Month, 1)
                .ToString("MMMM", CultureInfo.CreateSpecificCulture("dk"));
            LoadPieChart();
            LoadBarChart(expenses, month);
        }


        private readonly DatabaseActions dbActions = new DatabaseActions();
        List<KeyValuePair<string, float>> expenses = new List<KeyValuePair<string, float>>();
        private readonly string userName = MainWindow.username;
        private readonly DataUtilites DataU = new DataUtilites();
        private static DateTime date = DateTime.Now;
        /// <summary>
        /// Method for loading the data into the pieChart in the center region.
        /// </summary>
        private void LoadPieChart()
        {
            var tmpdate = DataU.GetCurrentMonth();
            var stringdate = tmpdate.Split(' ');

            var income = dbActions.GetIncome(userName, stringdate[0], stringdate[1]);
            expenses = dbActions.GetExpenses(userName, stringdate[0], stringdate[1]);
            
            var totalExpenses = expenses.Sum(x => x.Value);
            if (income == 0 && totalExpenses == 0)
            {
                pieChart.Title = "Ingen data fundet";
            } else
            {
                pieChart.Title = "Indkomst/Udgift";
            }
               
            List<KeyValuePair<string, float>> valueList = new List<KeyValuePair<string, float>>
            {
                new KeyValuePair<string, float>("Indkomst", income),
                new KeyValuePair<string, float>("Udgift", totalExpenses),
            };

            pieChart.DataContext = valueList;
        }
        /// <summary>
        /// Method for loading the data into the barChart in the center region upon initialize.
        /// </summary>
        /// <param name="list"></param> list for adding data to the barchart.
        /// <param name="month"></param> the month picked by the user.
        private void LoadBarChart(List<KeyValuePair<string, float>> list, string month)
            {
                List<KeyValuePair<string, float>> valuelist = new List<KeyValuePair<string, float>>();
                var myResults = list.GroupBy(p => p.Key)
                    .ToDictionary(g => g.Key, g => g.Sum(p => p.Value));
            if (myResults.Count == 0)
            {
                BarChart.Title = "Ingen data fundet";
            }
            else
            {
                pieChart.Title = "Udgifter";
            }
            Series.Title = month;
                BarChart.DataContext = myResults;
            }
        /// <summary>
        /// Method for loading the data into the pieChart in the center region and updating it when the user enters dates.
        /// </summary>
        /// <param name="firstDay"></param> First date picked by the user in the datePicker.
        /// <param name="lastDay"></param> Last date picked by the user in the datePicker.
        /// <param name="month"></param> the month picked by the user.
        private void LoadBarChartByDate(string firstDay, string lastDay, string month)
            {
                var list = dbActions.GetExpenses(userName, firstDay, lastDay);
                List<KeyValuePair<string, float>> valuelist = new List<KeyValuePair<string, float>>();
                var myResults = list.GroupBy(p => p.Key)
                    .ToDictionary(g => g.Key, g => g.Sum(p => p.Value));
            if (myResults.Count == 0)
            {
                BarChart.Title = "Ingen data fundet";
            }
            else
            {
                pieChart.Title = "Udgifter";
            }
            Series.Title = month;
                BarChart.DataContext = myResults;

            }
        /// <summary>
        /// Method for the buttonclick that updates the bar chart with the new dates.
        /// </summary>
        private void LoadChart_Click(object sender, RoutedEventArgs e)
            {
                DateTime? datefrom = DateFrom.SelectedDate;
                DateTime? dateto = DateTo.SelectedDate;

                if (datefrom != null && dateto != null)
                {
                    var tmpdate = DataU.GetDateFormat(datefrom, dateto);
                    var stringdatefrom = tmpdate.Split(' ');

                    var month = DataU.GetMonth(datefrom, dateto);
                    LoadBarChartByDate(stringdatefrom[0], stringdatefrom[1], month);
                }
                else
                {
                    MessageBox.Show("Indtast venligst en dato");
                }
            }
        /// <summary>
        /// Method for the buttonclick that updates the sidePieChart with the new dates.
        /// </summary>

        private void SidePieButton_Click(object sender, RoutedEventArgs e)
            {
                DateTime? datefrom = DateFromSidePie.SelectedDate;
                DateTime? dateto = DateToSidePie.SelectedDate;

                LoadSidePieChart(datefrom, dateto);

            }
        /// <summary>
        /// Method for loading the data into the pieChart in the right sidebar.
        /// </summary>
        /// <param name="firstDay"></param> First date picked by the user in the datePicker.
        /// <param name="lastDay"></param> Last date picked by the user in the datePicker.
        private void LoadSidePieChart(DateTime? firstDay, DateTime? lastDay)
            {

                if (firstDay != null && lastDay != null)
                {
                    var tmpdate = DataU.GetDateFormat(firstDay, lastDay);
                    var date = tmpdate.Split(' ');
                    var yourAmount = dbActions.GetAvgExpenses(userName, catComboBox.Text, date[0], date[1]);
                    var othersAmount = dbActions.GetAvgExpensesOthers(userName, catComboBox.Text, date[0], date[1]);
                    List<KeyValuePair<string, float>> valueList = new List<KeyValuePair<string, float>>
                    {
                        new KeyValuePair<string, float>("Dine udgifter", yourAmount),
                        new KeyValuePair<string, float>("Andres udgifter", othersAmount),
                    };


                    if (Math.Abs(yourAmount) < 0.1 && Math.Abs(othersAmount) < 0.1) {
                        pieChart2.Title = "Ingen data fundet";
                    }
                    else {
                        pieChart2.Title = "Sammenligning:";
                    }
                    pieChart2.DataContext = valueList;
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

        private void Budget_click(object sender, RoutedEventArgs e)
        {
            BudgetWindow window = new BudgetWindow();
            window.Show();
            Close();
        }

        private void LogOutMenuItemClick(object sender, RoutedEventArgs e) {
            MainWindow.username = null;
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void IndkomstItemClick(object sender, RoutedEventArgs e) {

            IncomeWindow window = new IncomeWindow();
            window.Show();
            Close();
        }
    }
}
   


