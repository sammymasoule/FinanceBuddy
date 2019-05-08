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

namespace FinanceBuddyWPF {
    /// <summary>
    /// Interaction logic for BudgetWindow.xaml
    /// </summary>
    public partial class BudgetWindow : Window {
        public BudgetWindow() {
            InitializeComponent();
            WindowState = WindowState.Maximized;    
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

        private void BudgetMonth_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
