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
using Data;

namespace Mail_Man
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLog_In(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee employee = Employee.GetEmployee ( tbUsername.Text, tbPassword.Password );
                lblError.Visibility = Visibility.Collapsed;
                // go to Employee Page
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                Customer customer = Customer.GetCustomer ( tbUsername.Text, tbPassword.Password );
                lblError.Visibility = Visibility.Collapsed;
                // go to Customer Page
                this.Close ();
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
    }
}
