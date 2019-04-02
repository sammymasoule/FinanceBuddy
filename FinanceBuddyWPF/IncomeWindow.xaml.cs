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
        string userName = MainWindow.username;
        
        //string userName = ((MainWindow)Application.Current.MainWindow)?.UsernameTXT.Text;

        private void Income_Click(object sender, RoutedEventArgs e)
        {
            float amount = 0;
            bool check_amount = true;
            bool check_dato = true;
            if(!float.TryParse(IndtjeningTXT.Text, out amount))
            {
                check_amount = false;
                IndtejningFejl.Visibility = Visibility.Visible;
                
            }
            if (dato == null)
            {
                check_dato = false;
                DatoFejl.Visibility = Visibility.Visible;

            }
            if(check_amount && check_dato)
            {
                if (dbActions.CreateIncome(amount, dato, userName, BeskrivelseTXT.Text))
                {
                    MessageBox.Show("Sucess");
                    IndtejningFejl.Visibility = Visibility.Hidden;
                    DatoFejl.Visibility = Visibility.Hidden;

                    IndtjeningTXT.Text = null;
                    dato = null;
                    dateTimePicker.SelectedDate = null;
                    BeskrivelseTXT.Text = "";
                }

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


            if (date != null)
            {
                dato = date.Value.ToString("yyyy-MM-dd");
            }
            
                
          
        }

        private void Overblik_click(object sender, RoutedEventArgs e)
        {
            OverviewWindow window = new OverviewWindow();
            window.Show();
            Close();
        }
    }
}
