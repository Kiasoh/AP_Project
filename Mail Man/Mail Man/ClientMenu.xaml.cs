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
    /// Interaction logic for ClientMenu.xaml
    /// </summary>
    public partial class ClientMenu : Window
    {
        public Customer? customer = null;
        public ClientMenu(Customer customer)
        {
            this.customer = customer;
            InitializeComponent();

        }

        
        private void btn_show_information_Click ( object sender, RoutedEventArgs e )
        {
            grid_changeinformation.Visibility = Visibility.Collapsed;
            grid_showinformation.Visibility = Visibility.Visible;
            grid_reportOfOrders.Visibility = Visibility.Collapsed;
            grid_wallet.Visibility = Visibility.Collapsed;
        }

        private void btn_wallert_Click ( object sender, RoutedEventArgs e )
        {
            grid_changeinformation.Visibility = Visibility.Collapsed;
            grid_showinformation.Visibility = Visibility.Collapsed;
            grid_reportOfOrders.Visibility = Visibility.Collapsed;
            grid_wallet.Visibility = Visibility.Visible;
            totalMoney_txtblock.Text = Return_Money ( customer.money );
        }



        private void btn_change_UserPass_Click ( object sender, RoutedEventArgs e )
        {
            grid_changeinformation.Visibility = Visibility.Visible;
            grid_showinformation.Visibility = Visibility.Collapsed;
            grid_reportOfOrders.Visibility = Visibility.Collapsed;
            grid_wallet.Visibility = Visibility.Collapsed;
        }

        private void btn_report_Click ( object sender, RoutedEventArgs e )
        {
            grid_changeinformation.Visibility = Visibility.Collapsed;
            grid_showinformation.Visibility = Visibility.Collapsed;
            grid_reportOfOrders.Visibility = Visibility.Visible;
            grid_wallet.Visibility = Visibility.Collapsed;
        }

        private void cardNumber_TextChanged ( object sender, TextChangedEventArgs e )
        {

        }
        private void cvv2_TextChanged ( object sender, TextChangedEventArgs e )
        {

        }

        private void amount_TextChanged ( object sender, TextChangedEventArgs e )
        {

        }
        private string Return_Money(int a)
        {
            string s = "";
            for ( int i = 0; i < a.ToString ().Length; i++ )
            {
                if ( i % 3 == 0 && i != 0 ) s += ",";
                s += ( a % 10 ).ToString ();
            }
            return s;
        }
        private void pay_btn_Click ( object sender, RoutedEventArgs e )
        {
            int a;
            if (cardNumber.Text.IsCardValid() && cvv2.Text.IsThisCVVValid() && int.TryParse(amount.Text , out a)) //add expiration valodation
            {
                customer.money += a;
                if ( customer.money.ToString ().Length > 15 ) totalMoney_txtblock.Text = "Unable to show the amount!";
                else
                {
                    totalMoney_txtblock.Text = Return_Money ( customer.money );
                }
            }
            else
            {
                if ( !cardNumber.Text.IsCardValid () ) MessageBox.Show("Invalid card number." , "Error" , MessageBoxButton.OK, MessageBoxImage.Error );
                if (!cvv2.Text.IsThisCVVValid () ) MessageBox.Show ( "Invalid cvv.", "Error", MessageBoxButton.OK, MessageBoxImage.Error );
                if (!int.TryParse ( amount.Text, out a ) ) MessageBox.Show ( "Invalid amount.", "Error", MessageBoxButton.OK, MessageBoxImage.Error );
                else if ( a  < 0) MessageBox.Show ( "Invalid amount.", "Error", MessageBoxButton.OK, MessageBoxImage.Error );
            }
        }

        private void btn_home_Click ( object sender, RoutedEventArgs e )
        {
            grid_changeinformation.Visibility = Visibility.Collapsed;
            grid_showinformation.Visibility = Visibility.Collapsed;
            grid_reportOfOrders.Visibility = Visibility.Collapsed;
            grid_wallet.Visibility = Visibility.Collapsed;
        }
        private void tbusername_TextChanged ( object sender, TextChangedEventArgs e )
        {
            try { tbUsername.Text.IsThisUsernameValid (); lblUsername.Content = $""; }
            catch ( Exception ex ) { lblUsername.Visibility = Visibility.Visible; lblUsername.Content = $"*{ex.Message}*"; }
        }
        private void Password_TextChanged ( object sender, TextChangedEventArgs e )
        {
            if( Password.Text.IsThisPasswordValid()) {lblPassword.Content = $""; }
            else { lblPassword.Visibility = Visibility.Visible; lblPassword.Content = $"*Password invalid!*"; }
        }
        private void Save_Click ( object sender, RoutedEventArgs e )
        {
            if (lblPassword.Content == lblUsername.Content)
            {
                customer.ChangePU(Password.Text , tbUsername.Text);
                btn_home_Click (sender , e);
                SaveAndRead.WriteData ();
            }
            
        }

    }
}
