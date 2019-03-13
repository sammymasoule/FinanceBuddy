using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FinanceBuddyWPF.Controllers;

namespace FinanceBuddyWPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        DatabaseActions dbActions = new DatabaseActions();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tbSettingText.Items.Clear();
            var persons = dbActions.GetPersonList();
            foreach (var person in persons)
            {
                tbSettingText.Items.Add(person);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tbSettingText.Items.Clear();
            tbSettingText.Items.Add(dbActions.GetPerson("nijo"));
        }
    }
}
