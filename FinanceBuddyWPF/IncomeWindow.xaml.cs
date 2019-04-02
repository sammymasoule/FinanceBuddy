using FinanceBuddyWPF.Controllers;
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

namespace FinanceBuddyWPF
{
    /// <summary>
    /// Interaction logic for IncomeWindow.xaml
    /// </summary>
    public partial class IncomeWindow : Window
    {
        public IncomeWindow()
        {
            InitializeComponent();
            
           
        }
        string dato;
        
        DatabaseActions dbActions = new DatabaseActions();
        string tmp = MainWindow.username;
        
        //string userName = ((MainWindow)Application.Current.MainWindow)?.UsernameTXT.Text;

        private void Income_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(tmp);
            if (IndtjeningTXT != null && dato != null) {
                dbActions.CreateIncome(float.Parse(IndtjeningTXT.Text), dato, tmp, BeskrivelseTXT.Text);
            }

        }

        private void LogOutMenuItemClick(object sender, RoutedEventArgs e)
        {

            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;


            if (date == null)
            {
                MessageBox.Show("Ingen dato valgt");
            }
            else
                dato = date.Value.ToString("yyyy-MM-dd");
          
        }
    }
}
