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
using System.Windows.Shapes;
using Data;

namespace Mail_Man
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        static bool firsttime = true;
        public Login()
        {
            InitializeComponent();
            if (firsttime)
            {
                SaveAndRead.ReadData ();
                firsttime = false;
            }
            
        }

        private void btnLog_In(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    Employee employee = Employee.GetEmployee ( tbUsername.Text, tbPassword.Password );
                    
                    lblError.Visibility = Visibility.Collapsed;
                    // go to Employee Page
                    var clientMenu = new MainWindow ( employee );
                    clientMenu.Show ();
                    this.Close ();
                }
                catch ( InvalidOperationException ex )
                {
                    Customer customer = Customer.GetCustomer ( tbUsername.Text, tbPassword.Password );
                    lblError.Visibility = Visibility.Collapsed;
                    // go to Customer Page
                    var clientMenu = new ClientMenu ( customer );
                    clientMenu.Show ();
                    this.Close ();
                }
            }
            catch
            {
                lblError.Visibility = Visibility.Visible;
            }
        }
        private void btn_Signup ( object sender , RoutedEventArgs e)
        {
            var SignupFrm = new SignUp ();
            SignupFrm.Show ();
            this.Close ();
        }

        private void tbUsername_Watermark_GotFocus ( object sender, RoutedEventArgs e )
        {
            tbUsername_Watermark.Visibility = Visibility.Collapsed;
            tbUsername.Visibility = Visibility.Visible;
            tbUsername.Focus ();
        }

        private void tbUsername_LostFocus ( object sender, RoutedEventArgs e )
        {
            if ( string.IsNullOrEmpty ( tbUsername.Text ) )
            {
                tbUsername.Visibility = Visibility.Collapsed;
                tbUsername_Watermark.Visibility = Visibility.Visible;
            }
        }

        private void tbUsername_Watermark_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
