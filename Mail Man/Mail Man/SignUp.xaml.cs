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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }
        private void tbName_TextChanged ( object sender, TextChangedEventArgs e)
        {
            string[] s = tbName.Text.Split(' ');
            bool flag = true;
            foreach (string ss in s)
            {
                if (!ss.IsThisNameValid())
                {
                    flag = false;
                    break;
                }
            }
            try { s[1] = s[1]; } catch { flag = false; }
            if (!flag)
            {
                lblName.Content = "*Name is Invalid!*";
                lblName.Visibility = Visibility.Visible;
            }
            else
            {
                lblName.Content = "";
            }
        }
        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (!Passbox.Password.IsThisPasswordValid())
            {
                lblPassword.Content = "*Invalid Password!*";
                flag = false;
            }
            else lblPassword.Content = flag.ToString();
            if (Passbox.Password != AgainPassbox.Password)
            {
                lblPasswordRe.Content = "*Password doesn't match!*";
                flag = false;
            }
            else lblPasswordRe.Content = "";
            //lblPasswordRe.Content =  ( flag && lblID.Content == lblUsername.Content && lblUsername.Content == lblName.Content && lblUsername.Content == lblEmail.Content).ToString();
            if (flag && lblID.Content == lblUsername.Content && lblUsername.Content == lblName.Content && lblUsername.Content == lblEmail.Content )
            {
                string[] s = tbName.Text.Split ( ' ' );
                try
                {
                    new Employee ( s[0], s[1], tbEmail.Text, tbUsername.Text, Passbox.Password, tbID.Text );
                    var logInFrm = new Login ();
                    SaveAndRead.WriteData ();
                    logInFrm.Show ();
                    this.Close ();
                }
                catch { }
                
            }

        }

        private void tbID_TextChanged ( object sender, TextChangedEventArgs e )
        {
            try { tbID.Text.IsThisIDValid (); lblID.Content = ""; }
            catch (Exception ex) { lblID.Visibility = Visibility.Visible; lblID.Content = $"*{ex.Message}*"; }
        }

        private void tbusername_TextChanged ( object sender, TextChangedEventArgs e )
        {
            try { tbUsername.Text.IsThisUsernameValid (); lblUsername.Content = $""; }
            catch (Exception ex) { lblUsername.Visibility = Visibility.Visible; lblUsername.Content = $"*{ex.Message}*"; }
        }

        private void tbEmail_TextChanged ( object sender, TextChangedEventArgs e )
        {
            if ( !tbEmail.Text.IsThisEmailValid() )
            {
                lblEmail.Visibility = Visibility.Visible;
                lblEmail.Content = "*Email is Invalid!*";
            }
            else
            {
                lblEmail.Content = "";
            }
        }

        private void Name_Watermark_GotFocus ( object sender, RoutedEventArgs e )
        {
            Name_Watermark.Visibility = Visibility.Collapsed;
            tbName.Visibility = Visibility.Visible;
            tbName.Focus ();
        }

        private void tbName_LostFocus ( object sender, RoutedEventArgs e )
        {
            if ( string.IsNullOrEmpty ( tbName.Text ))
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

        private void tbEmail_Watermark_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Name_Watermark_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void leftarrow_btn_Click ( object sender, RoutedEventArgs e )
        {
            var login = new Login ();
            login.Show ();
            this.Close ();
        }
    }
}
