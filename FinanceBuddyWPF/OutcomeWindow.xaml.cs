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
    /// Interaction logic for OutcomeWindow.xaml
    /// </summary>
    public partial class OutcomeWindow : Window {
        public OutcomeWindow() {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }

        DatabaseActions dbActions = new DatabaseActions();
        string userName = MainWindow.username;
        string date;
        DataUtilites DataU = new DataUtilites();


        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;


            if (date != null) {
                this.date = date.Value.ToString("yyyy-MM-dd");
            }
        }

        private void Expense_Click(object sender, RoutedEventArgs e)
        {
            float amount = 0;
            bool checkAmount = true;
            bool checkDate = true;
            bool checkCategory = true;

            if (!float.TryParse(ExpenseTxt.Text, out amount)) {
                checkAmount = false;
                ExpenseError.Visibility = Visibility.Visible;
                ExpenseTxt.BorderBrush = new SolidColorBrush(Colors.Red);

            }
            if (String.IsNullOrEmpty(date)) {
                checkDate = false;
                DateError.Visibility = Visibility.Visible;
            }

            if (CategoryComboBox.SelectedItem == null)
            {
                checkCategory = false;
                CategoryError.Visibility = Visibility.Visible;
            }
            if (checkAmount && checkDate && checkCategory) {
                if (dbActions.CreateExpense(CategoryComboBox.Text, DescriptionTxt.Text, date, userName, amount)) {
                    MessageBox.Show("Din udgift er oprettet");
                    CategoryComboBox.Text = null;
                    ExpenseError.Visibility = Visibility.Hidden;
                    DateError.Visibility = Visibility.Hidden;

                    ExpenseTxt.Text = null;
                    date = null;
                    DateTimePicker.SelectedDate = null;
                    DescriptionTxt.Text = "";
                    ExpenseTxt.BorderBrush = new SolidColorBrush(Colors.Gray);

                }

            }
        }

        private void Overview_click(object sender, RoutedEventArgs e) {
            OverviewWindow window = new OverviewWindow();
            window.Show();
            Close();
        }

        private void LogOutMenuItemClick(object sender, RoutedEventArgs e) {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void Income_Click(object sender, RoutedEventArgs e) {
            IncomeWindow window = new IncomeWindow();
            window.Show();
            Close();
        }

        private void OverviewRedirect_Click(object sender, RoutedEventArgs e) {
            Overview_click(sender, e);
        }

        private void Budget_Click(object sender, RoutedEventArgs e) {
            BudgetWindow window = new BudgetWindow();
            window.Show();
            Close();
        }
    }
}
