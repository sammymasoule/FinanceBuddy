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
        String userName = MainWindow.username;
        DatabaseActions dbActions = new DatabaseActions();
        private void LoadChart()
        {

            
           
            var amount = dbActions.GetIncome(userName);

            List<KeyValuePair<string, float>> valueList = new List<KeyValuePair<string, float>>
            {
                new KeyValuePair<string, float>("Indkomst", amount),
                new KeyValuePair<string, float>("Udgift", 20000),
            };
            pieChart.DataContext = valueList;
        }
        private void LogOutMenuItemClick(object sender, RoutedEventArgs e) {
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
    }

}
