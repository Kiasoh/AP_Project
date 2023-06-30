using System;
using System.Collections.Generic;
using System.IO;
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
        Customer? customer = null;
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
            grid_checkuser.Visibility = Visibility.Visible;
            grid_reportOfOrders.Visibility = Visibility.Collapsed;
            grid_ordering.Visibility = Visibility.Collapsed;
            grid_checkpackage.Visibility = Visibility.Collapsed;
            grid_showPackage.Visibility = Visibility.Collapsed;
        }
        private void tbUsername_LostFocus ( object sender, RoutedEventArgs e )
        {
            if ( string.IsNullOrEmpty ( tbUsername.Text ) )
            {
                tbUsername.Visibility = Visibility.Collapsed;
                tbUsername_Watermark.Visibility = Visibility.Visible;
            }
        }

        private void tbUsername_Watermark_GotFocus ( object sender, RoutedEventArgs e )
        {
            tbUsername_Watermark.Visibility = Visibility.Collapsed;
            tbUsername.Visibility = Visibility.Visible;
            tbUsername.Focus ();
        }
        private void tbUsername_TextChanged ( object sender, TextChangedEventArgs e )
        {
            try
            {
                tbUsername.Text.IsThisSSNValid ();
                lblError.Content = "";
            }
            catch ( Exception ex )
            {
                if ( ex.Message == "Invalid SSN!" ) lblError.Content = "*Invalid SSN!*";
                else lblError.Content = "";
            }
        }
        private void btnSearch_user ( object sender, RoutedEventArgs e )
        {
            try 
            { 
                tbUsername.Text.IsThisSSNValid ();
                if (MessageBox.Show("This Customer is not registered. Do you want to register a new customer?" , "Add Customer" , MessageBoxButton.YesNo , MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var SignupFrm = new signupClient ();
                    SignupFrm.Show ();
                    SignupFrm.tbID.Text = tbUsername.Text;
                    this.Close ();
                }
            }
            catch (Exception ex)
            {
                if ( ex.Message == "This SSN is already in use!" )
                {
                    lblError.Content = "";
                    grid_checkuser.Visibility = Visibility.Collapsed;
                    grid_ordering.Visibility = Visibility.Visible;
                    customer = Customer.GetCustomer (tbUsername.Text);
                }
            }
        }
        private void vip_checkbox_Checked ( object sender, RoutedEventArgs e )
        {
            if (usuall_checkbox.IsChecked == true) usuall_checkbox.IsChecked = false;
        }

        private void usuall_checkbox_Checked ( object sender, RoutedEventArgs e )
        {
            if (vip_checkbox.IsChecked == true) vip_checkbox.IsChecked = false;
        }

        private void breack_checkbox_Checked ( object sender, RoutedEventArgs e )
        {
            if (doc_checkbox.IsChecked == true) doc_checkbox.IsChecked = false;
            if (object_checkbox.IsChecked == true) object_checkbox.IsChecked = false;
            
        }

        private void doc_checkbox_Checked ( object sender, RoutedEventArgs e )
        {
            if ( object_checkbox.IsChecked == true ) object_checkbox.IsChecked = false;
            if ( breack_checkbox.IsChecked == true ) breack_checkbox.IsChecked = false;
            
        }

        private void object_checkbox_Checked ( object sender, RoutedEventArgs e )
        {
            if ( doc_checkbox.IsChecked == true ) doc_checkbox.IsChecked = false;
            
            if ( breack_checkbox.IsChecked == true ) breack_checkbox.IsChecked = false;
        }
        
        private void submit_bin_Click ( object sender, RoutedEventArgs e )
        {
            if ( ( object_checkbox.IsChecked == false && doc_checkbox.IsChecked == false && breack_checkbox.IsChecked == false ) ) MessageBox.Show ( "You must choose one checkbox in \"Package Content\" section.", "Error", MessageBoxButton.OK, MessageBoxImage.Error );
            else if ( usuall_checkbox.IsChecked == false && vip_checkbox.IsChecked == false ) MessageBox.Show ( "You must choose one checkbox in \"Type\" section.", "Error", MessageBoxButton.OK, MessageBoxImage.Error );
            else
            {
                TypeOfPackage t1 = breack_checkbox.IsChecked == true ? TypeOfPackage.Breakable : doc_checkbox.IsChecked==true ? TypeOfPackage.Document : TypeOfPackage.Object;
                TypeOfDelivery t2 = vip_checkbox.IsChecked==true ? TypeOfDelivery.Special : TypeOfDelivery.Normal;
                
                try 
                {
                    if ( customer.money < calculation () ) throw new Exception ();
                    new Package ( customer, t1, t2, Status.Registered, recieveadress_txt.Text, sendaddress_txt.Text, expensive_checkbox.IsChecked == true ? true : false, double.Parse ( weight_txt.Text ) );
                    SaveAndRead.WriteData ();
                }
                catch (FormatException ex) { MessageBox.Show ( "Invalid weight!", "Format Error", MessageBoxButton.OK, MessageBoxImage.Error ); }
                catch (Exception ex) { MessageBox.Show ( "Customer doesn't have enough money!", "Balance", MessageBoxButton.OK ); }
            }

        }
        public string Return_Money ( int a )
        {
            string s = "";
            string ss = a.ToString ();
            for ( int i = 0; i < ss.Length; i++ )
            {
                if ( i % 3 == 0 && i != 0 ) s = "," + s;
                s = Char.ToString ( ss[ss.Length - i - 1] ) + s;
            }
            return s;
        }
        private double calculation ()
        {
            double cost = 10000;
            if ( breack_checkbox.IsChecked == true ) cost *= 2;
            else if ( doc_checkbox.IsChecked == true ) cost *= 1.5;
            if ( expensive_checkbox.IsChecked == true ) cost *= 2;
            double w = double.Parse ( weight_txt.Text );
            for ( w -= 0.5; w > 0; w -= 0.5 ) cost *= 1.2;
            if ( vip_checkbox.IsChecked == true ) cost *= 1.5;
            return cost;
        }
        private void calculation_btn_Click ( object sender, RoutedEventArgs e )
        {
           
            try
            {
                showtotal_txtblock.Text = Return_Money((int)(calculation ()));
            }
            catch { MessageBox.Show ( "Invalid weight!", "Format Error", MessageBoxButton.OK, MessageBoxImage.Error ); }
        }
        //end of order
        //start of advance search
        private void btn_report_Click ( object sender, RoutedEventArgs e )
        {
            grid_checkuser.Visibility = Visibility.Collapsed;
            grid_reportOfOrders.Visibility = Visibility.Visible;
            grid_ordering.Visibility = Visibility.Collapsed;
            grid_checkpackage.Visibility = Visibility.Collapsed;
            grid_showPackage.Visibility = Visibility.Collapsed;
        }
        private void btnSearch_order ( object sender, RoutedEventArgs e )
        {
            StreamWriter? file = null;
            try
            {
                try { file = new StreamWriter ( "SearchResult.csv" ); }
                catch { File.Create ( "SearchResult.csv" ); file = new StreamWriter ( "Customer.csv" ); }
                IEnumerable<Package> finalResult = Package.packages;
                if ( personalID_txt_as.Text != String.Empty ) finalResult = finalResult.Where ( k => k.customer.ssn == personalID_txt_as.Text );
                if ( pricePaid_txt_as.Text != String.Empty ) finalResult = finalResult.Where ( k => k.CalculateCost () == double.Parse ( pricePaid_txt_as.Text ) );
                if ( weight_txt_as.Text != String.Empty ) finalResult = finalResult.Where ( k => k.weight == double.Parse ( weight_txt_as.Text ) );
                finalResult = finalResult.Where ( k => 
                ( k.typeOfPackage == TypeOfPackage.Breakable && (bool) breack_checkbox_as.IsChecked )
                 || ( k.typeOfPackage == TypeOfPackage.Document && (bool) doc_checkbox_as.IsChecked )
                 || ( k.typeOfPackage == TypeOfPackage.Object && (bool) object_checkbox_as.IsChecked )
                 || ( k.typeOfDelivery == TypeOfDelivery.Special && (bool) vip_checkbox_as.IsChecked )
                 || ( k.typeOfDelivery == TypeOfDelivery.Normal && (bool) usuall_checkbox_as.IsChecked ) );
                
                if ( finalResult.ToList ().Count == 0 ) MessageBox.Show ( "Found no package with these properties.", "Result", MessageBoxButton.OK, MessageBoxImage.None );
                else
                {
                    foreach ( var item in finalResult ) file.WriteLine ( item.ToString () );
                    
                    MessageBox.Show ( "Results are saved!", "Result", MessageBoxButton.OK, MessageBoxImage.None );
                }
                

            }
            catch { MessageBox.Show ( "An error accured!", "Error", MessageBoxButton.OK, MessageBoxImage.Error ); }
            finally { file.Close (); }
        }
        //end of advance search
        // start of show package
        private void btn_show_information_Click ( object sender, RoutedEventArgs e )
        {
            grid_checkuser.Visibility = Visibility.Collapsed;
            grid_reportOfOrders.Visibility = Visibility.Collapsed;
            grid_ordering.Visibility = Visibility.Collapsed;
            grid_checkpackage.Visibility = Visibility.Collapsed;
            grid_showPackage.Visibility = Visibility.Collapsed;
        }

        private void weight_txt_TextChanged ( object sender, TextChangedEventArgs e )
        {

        }

        private void btn_home_Click ( object sender, RoutedEventArgs e )
        {
            grid_checkuser.Visibility = Visibility.Visible;
            grid_reportOfOrders.Visibility = Visibility.Collapsed;
            grid_ordering.Visibility = Visibility.Collapsed;
            grid_checkpackage.Visibility = Visibility.Collapsed;
            grid_showPackage.Visibility = Visibility.Collapsed;
        }

        private void logout_btn_Click ( object sender, RoutedEventArgs e )
        {
            var login = new Login();
            login.Show ();
            this.Close ();
        }

        
        
    }
}
