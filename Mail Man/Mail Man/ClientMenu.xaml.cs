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
using ceTe.DynamicPDF;
using Data;
using Microsoft.Win32;
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
        // start of wallet
        private void btn_wallert_Click ( object sender, RoutedEventArgs e )
        {
            grid_changeinformation.Visibility = Visibility.Collapsed;
            grid_showinformation.Visibility = Visibility.Collapsed;
            grid_reportOfOrders.Visibility = Visibility.Collapsed;
            grid_wallet.Visibility = Visibility.Visible;
            totalMoney_txtblock.Text = Return_Money ( customer.money );
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

        private void pay_btn_Click ( object sender, RoutedEventArgs e )
        {
            
            try
            {
                int a;
                if ( cardNumber.Text.IsCardValid () && cvv2.Text.IsThisCVVValid () && int.TryParse ( amount.Text, out a ) && !( Convert.ToDateTime ( $"{int.Parse ( expiration_month.Text )}/{1}/{int.Parse(expiration_year.Text)}" ).IsExpired () )  ) //add expiration valodation
                {
                    customer.money += a;
                    if ( customer.money.ToString ().Length > 15 ) totalMoney_txtblock.Text = "Unable to show the amount!";
                    else
                    {
                        totalMoney_txtblock.Text = Return_Money ( customer.money );
                    }
                    if ( ( MessageBox.Show ( "Do you want to save the reciept?", "Reciept", MessageBoxButton.YesNo, MessageBoxImage.Question ) ) == MessageBoxResult.Yes )
                    {
                        /*
                        SaveFileDialog saveFileDialog = new SaveFileDialog ();
                        string s = "Your Reciept";
                        saveFileDialog.FileName = "Reciept"; // Default file name
                        saveFileDialog.DefaultExt = ".pdf"; // Default file extension
                        saveFileDialog.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension
                        if ( saveFileDialog.ShowDialog () == true )

                        {
                            Document document = new Document ();

                            ceTe.DynamicPDF.Page page = new ceTe.DynamicPDF.Page ( PageSize.Letter, PageOrientation.Portrait, 54.0f );

                            document.Pages.Add ( page );

                            string labelText = "Hello World...\nFrom DynamicPDF Generator for .NET\nDynamicPDF.com";
                            Label label = new Label ( labelText, 0, 0, 504, 100, Font.Helvetica, 18, TextAlign.Center );
                            page.Elements.Add ( label );

                            document.Draw ( "Output.pdf" );
                        }
                        */
                    }
                }
                else
                {
                    if ( !cardNumber.Text.IsCardValid () ) MessageBox.Show ( "Invalid card number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error );
                    if ( !cvv2.Text.IsThisCVVValid () ) MessageBox.Show ( "Invalid cvv.", "Error", MessageBoxButton.OK, MessageBoxImage.Error );
                    if ( !int.TryParse ( amount.Text, out a ) ) MessageBox.Show ( "Invalid amount.", "Error", MessageBoxButton.OK, MessageBoxImage.Error );
                    if ( Convert.ToDateTime ( $"{int.Parse ( expiration_month.Text )}/{1}/{int.Parse ( expiration_year.Text )}" ).IsExpired () ) MessageBox.Show ( "Expired.", "Error", MessageBoxButton.OK, MessageBoxImage.Error );
                    else if ( a < 0 ) MessageBox.Show ( "Invalid amount.", "Error", MessageBoxButton.OK, MessageBoxImage.Error );
                }
            }
            catch { }
        }
        //end of wallet
        //start of change username and password
        private void btn_change_UserPass_Click ( object sender, RoutedEventArgs e )
        {
            grid_changeinformation.Visibility = Visibility.Visible;
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
            if ( Password.Text.IsThisPasswordValid () ) { lblPassword.Content = $""; }
            else { lblPassword.Visibility = Visibility.Visible; lblPassword.Content = $"*Password invalid!*"; }
        }
        private void Save_Click ( object sender, RoutedEventArgs e )
        {
            if ( lblPassword.Content == lblUsername.Content )
            {
                customer.ChangePU ( Password.Text, tbUsername.Text );
                btn_home_Click ( sender, e );
                SaveAndRead.WriteData ();
            }

        }
        //end of change username and password
        //start of report
        private void btn_report_Click ( object sender, RoutedEventArgs e )
        {
            grid_changeinformation.Visibility = Visibility.Collapsed;
            grid_showinformation.Visibility = Visibility.Collapsed;
            grid_reportOfOrders.Visibility = Visibility.Visible;
            grid_wallet.Visibility = Visibility.Collapsed;
        }
        private void Search_Click ( object sender, RoutedEventArgs e )
        {
            
        }
        //end of report

        

        private void btn_home_Click ( object sender, RoutedEventArgs e )
        {
            grid_changeinformation.Visibility = Visibility.Collapsed;
            grid_showinformation.Visibility = Visibility.Collapsed;
            grid_reportOfOrders.Visibility = Visibility.Collapsed;
            grid_wallet.Visibility = Visibility.Collapsed;
        }

        private void logout_btn_Click ( object sender, RoutedEventArgs e )
        {
            var login = new Login ();
            login.Show ();
            this.Close ();
        }

        private void packagecontent_dropbox_information_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
