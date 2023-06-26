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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Data;

namespace Mail_Man
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Employee? employee = null;
        public MainWindow(Employee employee)
        {
            this.employee = employee;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRegistration(object sender, RoutedEventArgs e)
        {
            var SignupFrm = new signupClient ();
            SignupFrm.Show ();
            this.Close ();
        }
        
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        //start of order
        private void btn_order_Click ( object sender, RoutedEventArgs e )
        {
            grid_checkuser.Visibility = Visibility.Collapsed;
            grid_reportOfOrders.Visibility = Visibility.Collapsed;
            grid_ordering.Visibility = Visibility.Visible;
        }
        private void btnOrder_Click ( object sender, RoutedEventArgs e )
        {

        }
        //end of order

        private void btn_report_Click ( object sender, RoutedEventArgs e )
        {
            grid_checkuser.Visibility = Visibility.Collapsed;
            grid_reportOfOrders.Visibility = Visibility.Collapsed;
            grid_ordering.Visibility = Visibility.Collapsed;
        }

        private void btn_show_information_Click ( object sender, RoutedEventArgs e )
        {
            grid_checkuser.Visibility = Visibility.Collapsed;
            grid_reportOfOrders.Visibility = Visibility.Collapsed;
            grid_ordering.Visibility = Visibility.Collapsed;
        }

        private void weight_txt_TextChanged ( object sender, TextChangedEventArgs e )
        {

        }

        private void btn_home_Click ( object sender, RoutedEventArgs e )
        {

        }

        
    }
}
