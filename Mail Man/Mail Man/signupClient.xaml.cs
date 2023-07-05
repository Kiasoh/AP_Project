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
    /// Interaction logic for signupClient.xaml
    /// </summary>
    public partial class signupClient : Window
    {
        public signupClient()
        {
            InitializeComponent();
        }
        Employee? employee1 = null;
        public signupClient (Employee employee)
        {
            employee1 = employee;
            InitializeComponent ();
        }
        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if ( lblID.Content == lblName.Content && lblName.Content == lblEmail.Content )
            {
                try
                {
                    string[] s = tbName.Text.Split ( ' ' );
                    new Customer ( s[0], s[1], tbEmail.Text, tbID.Text, tb_phonenumber.Text );
                    var logInFrm = new MainWindow (employee1);
                    SaveAndRead.WriteData ();
                    logInFrm.Show ();
                    this.Close ();
                }
                catch {  }
            }
        }

        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string[] s = tbName.Text.Split ( ' ' );
            bool flag = true;
            foreach ( string ss in s )
            {
                if ( !ss.IsThisNameValid () )
                {
                    flag = false;
                    break;
                }
            }
            try { s[1] = s[1]; } catch { flag = false; }
            if ( !flag )
            {
                lblName.Content = "*Name is Invalid!*";
                lblName.Visibility = Visibility.Visible;
            }
            else
            {
                lblName.Content = "";
            }
        }

        private void tbID_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { tbID.Text.IsThisSSNValid (); lblID.Content = ""; }
            catch ( Exception ex ) { lblID.Visibility = Visibility.Visible; lblID.Content = $"*{ex.Message}*"; }
        }

        private void tbEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ( !tbEmail.Text.IsThisEmailValid () )
            {
                lblEmail.Visibility = Visibility.Visible;
                lblEmail.Content = "*Email is Invalid!*";
            }
            else
            {
                lblEmail.Content = "";
            }
        }

        private void tb_phonenumber_TextChanged ( object sender, TextChangedEventArgs e )
        {
            if ( !tb_phonenumber.Text.IsThisPhoneNumberValid () )
            {
                lblPhonenumber.Content = "*Phone number is Invalid!*";
            }
            else lblPhonenumber.Content = "";
        }

        private void Name_Watermark_GotFocus ( object sender, RoutedEventArgs e )
        {
            Name_Watermark.Visibility = Visibility.Collapsed;
            tbName.Visibility = Visibility.Visible;
            tbName.Focus ();
        }

        private void tbName_LostFocus ( object sender, RoutedEventArgs e )
        {
            if ( string.IsNullOrEmpty ( tbName.Text ) )
            {
                tbName.Visibility = Visibility.Collapsed;
                Name_Watermark.Visibility = Visibility.Visible;
            }
        }

        private void tbID_Watermark_GotFocus ( object sender, RoutedEventArgs e )
        {
            tbID_Watermark.Visibility = Visibility.Collapsed;
            tbID.Visibility = Visibility.Visible;
            tbID.Focus ();
        }

        private void tbID_LostFocus ( object sender, RoutedEventArgs e )
        {
            if ( string.IsNullOrEmpty ( tbID.Text ) )
            {
                tbID.Visibility = Visibility.Collapsed;
                tbID_Watermark.Visibility = Visibility.Visible;
            }
        }

        private void tbEmail_Watermark_GotFocus ( object sender, RoutedEventArgs e )
        {
            tbEmail_Watermark.Visibility = Visibility.Collapsed;
            tbEmail.Visibility = Visibility.Visible;
            tbEmail.Focus ();
        }

        private void tbEmail_LostFocus ( object sender, RoutedEventArgs e )
        {
            if ( string.IsNullOrEmpty ( tbEmail.Text ) )
            {
                tbEmail.Visibility = Visibility.Collapsed;
                tbEmail_Watermark.Visibility = Visibility.Visible;
            }
        }

        private void tb_phonenumber_Watermark_GotFocus ( object sender, RoutedEventArgs e )
        {
            tb_phonenumber_Watermark.Visibility = Visibility.Collapsed;
            tb_phonenumber.Visibility = Visibility.Visible;
            tb_phonenumber.Focus ();
        }

        private void tb_phonenumber_LostFocus ( object sender, RoutedEventArgs e )
        {
            if ( string.IsNullOrEmpty ( tb_phonenumber.Text ) )
            {
                tb_phonenumber.Visibility = Visibility.Collapsed;
                tb_phonenumber_Watermark.Visibility = Visibility.Visible;
            }
        }

        private void leftarrow_btn_Click ( object sender, RoutedEventArgs e )
        {

            var login = new MainWindow (employee1);
            login.Show ();
            this.Close ();

        }
    }
}
