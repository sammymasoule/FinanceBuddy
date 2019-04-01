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

        //DatabaseActions dbActions = new DatabaseActions();
        private void LoadChart() {
            List<KeyValuePair<string, int>> valueList = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("Indkomst", 25000),
                new KeyValuePair<string, int>("Udgift", 20000)
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
