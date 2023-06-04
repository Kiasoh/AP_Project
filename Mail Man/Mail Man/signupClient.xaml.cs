﻿using System;
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

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if ( lblID.Content == lblName.Content && lblName.Content == lblEmail.Content )
            {
                string[] s = tbName.Text.Split ( ' ' );
                new Customer ( s[0], s[1], tbEmail.Text, tb_phonenumber.Text );
                var logInFrm = new Login ();
                logInFrm.Show ();
                this.Close ();
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
    }
}
