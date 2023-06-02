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
                new Employee ( s[0], s[1], tbEmail.Text, tbUsername.Text, Passbox.Password, tbID.Text );
                var logInFrm = new Login ();
                logInFrm.Show();
                this.Close();
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
    }
}
