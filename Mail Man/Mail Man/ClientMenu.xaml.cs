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
//using ceTe.DynamicPDF;
using Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
namespace Mail_Man
{
    /// <summary>
    /// Interaction logic for ClientMenu.xaml
    /// </summary>
    public partial class ClientMenu : Window
    {
        Package? package = null;
        public Customer? customer = null;
        public ClientMenu(Customer customer)
        {
            this.customer = customer;
            InitializeComponent();
            RoutedEventArgs? e = null;
            object? sender = null;
            btn_home_Click ( sender, e );
        }

        // start of show information
        private void btn_show_information_Click ( object sender, RoutedEventArgs e )
        {
            grid_changeinformation.Visibility = Visibility.Collapsed;
            grid_showinformation.Visibility = Visibility.Visible;
            grid_showPackage.Visibility = Visibility.Collapsed;
            grid_reportOfOrders.Visibility = Visibility.Collapsed;
            grid_wallet.Visibility = Visibility.Collapsed;
            
        }
        private void btnSearch_package_Click ( object sender, RoutedEventArgs e )
        {
            try
            {
                regsitered_checkbox.IsChecked = false; sending_checkbox.IsChecked = false; expensive_checkbox_showpack.IsChecked = false; ready__checkbox.IsChecked = false; delivered_checkbox.IsChecked = false; comment_tb.IsEnabled = false; Usual_checkbox_showpack.IsChecked = false; Vip_checkbox_showpack.IsChecked = false; checkbox_object.IsChecked = false; checkbox_doc.IsChecked = false; checkbox_breack.IsChecked = false;
                lblError_findpackage.Visibility = Visibility.Collapsed; if ( Package.packages[ int.Parse ( tbPackageID.Text )].customer != customer  ) throw new Exception ( "*No Package Found!*" ); grid_showinformation.Visibility = Visibility.Collapsed; grid_showPackage.Visibility = Visibility.Visible;
                Package a = Package.packages[int.Parse ( tbPackageID.Text )]; package = a; sender_lbl.Content = a.addressSender; reciever_lbl.Content = a.addressReciever; weigh_lbl.Content = a.weight.ToString (); comment_tb.Text = a.comment;
                if ( a.typeOfDelivery == TypeOfDelivery.Normal ) Usual_checkbox_showpack.IsChecked = true;
                else Vip_checkbox_showpack.IsChecked = true;
                if ( a.typeOfPackage == TypeOfPackage.Object ) checkbox_object.IsChecked = true;
                else if ( a.typeOfPackage == TypeOfPackage.Document ) checkbox_doc.IsChecked = true;
                else checkbox_breack.IsChecked = true;
                if ( a.IsExpensive ) expensive_checkbox_showpack.IsChecked = true;
                if ( a.status == Status.Registered ) regsitered_checkbox.IsChecked = true;
                else if ( a.status == Status.OnTheWay ) sending_checkbox.IsChecked = true;
                else if ( a.status == Status.ReadyToGO ) ready__checkbox.IsChecked = true;
                else
                {
                    delivered_checkbox.IsChecked = true;
                    comment_tb.IsEnabled = true;
                }
                if (a.status != Status.Delivered ) comment_tb.IsEnabled = false;
            }
            catch ( FormatException ex ) { MessageBox.Show ( "Invalid Foramt!", "Error", MessageBoxButton.OK, MessageBoxImage.Error ); }
            catch ( Exception ex ) { lblError_findpackage.Content = ex.Message; lblError_findpackage.Visibility = Visibility.Visible; }
        }
        
        private void comment_tb_TextChanged ( object sender, TextChangedEventArgs e )
        {
            package.comment = comment_tb.Text;
            try { SaveAndRead.WriteData (); }
            catch { }
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
                    SaveAndRead.WriteData ();
                    if ( customer.money.ToString ().Length > 15 ) totalMoney_txtblock.Text = "Unable to show the amount!";
                    else
                    {
                        totalMoney_txtblock.Text = Return_Money ( customer.money );
                    }
                    if ( ( MessageBox.Show ( "Do you want to save the reciept?", "Reciept", MessageBoxButton.YesNo, MessageBoxImage.Question ) ) == MessageBoxResult.Yes )
                    {
                        
                        SaveFileDialog saveFileDialog = new SaveFileDialog ();
                        string s = "Your Reciept";
                        saveFileDialog.FileName = "Reciept"; // Default file name
                        saveFileDialog.DefaultExt = ".pdf"; // Default file extension
                        saveFileDialog.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension
                        if ( saveFileDialog.ShowDialog () == true )

                        {
                            /*
                            Document document = new Document ();

                            ceTe.DynamicPDF.Page page = new ceTe.DynamicPDF.Page ( PageSize.Letter, PageOrientation.Portrait, 54.0f );

                            document.Pages.Add ( page );

                            string labelText = "Hello World...\nFrom DynamicPDF Generator for .NET\nDynamicPDF.com";
                            Label label = new Label ( labelText, 0, 0, 504, 100, Font.Helvetica, 18, TextAlign.Center );
                            page.Elements.Add ( label );

                            document.Draw ( "Output.pdf" );
                            */
                            Document doctum = new Document ( iTextSharp.text.PageSize.A6, 10, 10, 42, 35 );
                            PdfWriter writer = PdfWriter.GetInstance ( doctum, new FileStream ( saveFileDialog.FileName, FileMode.Create ) );
                            doctum.Open ();
                            
                            doctum.SetMargins ( 10, 10, 10, 10 );
                            iTextSharp.text.Paragraph paragraph2 = new iTextSharp.text.Paragraph ( $"Your reciept : \n\n{customer.FirstName} {customer.LastName} with Username {customer.username}\n{DateTime.Now}\ncharged account for {a}$\n" +
                            $"Your balance : {customer.money}" );
                            doctum.Add ( paragraph2 );
                            doctum.Close ();
                        }

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
                SaveAndRead.WriteData ();
                btn_home_Click ( sender, e );
                
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
            StreamWriter? file = null;
            try
            {
                try { file = new StreamWriter ( "SearchResultC.csv" ); }
                catch { File.Create ( "SearchResultC.csv" ); file = new StreamWriter ( "SearchResultC.csv" ); }
                IEnumerable<Package> finalResult = Package.packages.Where( k => k.customer == customer);
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
        //end of report

        

        private void btn_home_Click ( object sender, RoutedEventArgs e )
        {
            grid_changeinformation.Visibility = Visibility.Collapsed;
            grid_showinformation.Visibility = Visibility.Collapsed;
            grid_reportOfOrders.Visibility = Visibility.Collapsed;
            grid_wallet.Visibility = Visibility.Collapsed;
            grid_showPackage.Visibility = Visibility.Collapsed;
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

        private void vip_checkbox_Checked ( object sender, RoutedEventArgs e )
        {

        }

        private void usuall_checkbox_Checked ( object sender, RoutedEventArgs e )
        {

        }

        private void breack_checkbox_Checked ( object sender, RoutedEventArgs e )
        {

        }

        private void doc_checkbox_Checked ( object sender, RoutedEventArgs e )
        {

        }

        private void object_checkbox_Checked ( object sender, RoutedEventArgs e )
        {

        }

        
    }
}
