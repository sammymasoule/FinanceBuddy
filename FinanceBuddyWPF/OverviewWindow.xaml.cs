using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            ShowChart();
        }

        //DatabaseActions dbActions = new DatabaseActions();

        private void ShowChart() {
            List<KeyValuePair<string, int>> valuelist = new List<KeyValuePair<string, int>>();
            valuelist.Add(new KeyValuePair<string, int>("Sammy", 60));
            valuelist.Add(new KeyValuePair<string, int>("Fugl", 40));
            valuelist.Add(new KeyValuePair<string, int>("Jens", 80));
            valuelist.Add(new KeyValuePair<string, int>("Dennis", 100));
            valuelist.Add(new KeyValuePair<string, int>("Flemming", 60));


            BarChart.DataContext = valuelist;
        }

        private void LogOutMenuItemClick(object sender, RoutedEventArgs e) {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }
    }
}
