using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data.Models;
using System.Net.Mail;
using System.Net;

namespace Data
{
    public class Customer : IPerson<Customer>
    {
        public static List <Customer> customers = new List<Customer>();
        string _firstName, _lastName, _email, password , _phoneNumber;
        public int money = 0;
        public string username;
        string _ssn;
        public string FirstName
        {
            get { return _firstName; }
            set { if ( !value.IsThisNameValid() ) throw new Exception ( "Firstname Error" ); _firstName = value; }
        }
        public string LastName
        {
            get { return _lastName; }
            set { if ( !value.IsThisNameValid () ) throw new Exception ( "Lastname Error" ); _lastName = value; }
        }
        public string email
        {
            get { return _email; }
            set { if ( !value.IsThisEmailValid () ) throw new Exception ( "Email Error" ); _email = value; }
        }
        public string ssn
        {
            get { return _ssn;}
            set { value.IsThisSSNValid (); _ssn = value; }
        }
        public string phoneNumber
        {
            get { return _phoneNumber; }
            set { if ( !value.IsThisPhoneNumberValid () ) throw new Exception ( "Invalid PhoneNumber" );  _phoneNumber = value; }
        }
        public Customer (string FirstName, string LastName , string email , string ssn , string phoneNumber , string? username = null , string? password = null)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.email= email;
            this.phoneNumber = phoneNumber;
            this.ssn = ssn;
            if (password == null && username == null)
                Generate_UsernamePassword();
            else
            {
                this.password = password;
                this.username = username;
            }
            customers.Add ( this );
        }
        public void ChangePU ( string p, string u )
        {
            password = p;
            username = u;
        }

        public void Generate_UsernamePassword()
        {
            Random rand = new Random (); int size = 8;
            int randomInt = 0;
            bool flag = true;
            while (flag)
            {
                randomInt = rand.Next ( 0, 10000 );
                flag = false;
                foreach ( Customer customer in customers ) if ( customer.username.Substring(4) == ( randomInt.ToString () )) flag = true;
            }
            username = $"user{randomInt}";
            string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] chars = new char[size]; for ( int i = 0; i < size; i++ ) chars[i] = '-';
            chars[rand.Next ( 0, size  )] = validChars[rand.Next ( 0, 26 )];
            chars[rand.Next ( 0, size  )] = validChars[rand.Next ( 26, 52 )];
            chars[rand.Next ( 0, size  )] = validChars[rand.Next ( 52, validChars.Length )];
            for ( int i = 0; i < chars.Length; i++ )
            {
                if ( chars[i] =='-') chars[i] = validChars[rand.Next ( 0, validChars.Length - 1 )];
            }
            password = new string( chars );

            string fromMail = "KSPostmailProject@gmail.com";
            string fromPassword = "yqgexaoctofxhorv";

            MailMessage message = new MailMessage ();
            message.From = new MailAddress ( fromMail );
            message.Subject = "Your MAIL MAN Username and Password";
            message.To.Add ( new MailAddress ( email ) );
            message.Body = $"<html><body>Welcome to our service {FirstName} {LastName}!\n Username :{username} Password :{password} </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient ( "smtp.gmail.com" )
            {
                Port = 587,
                Credentials = new NetworkCredential ( fromMail, fromPassword ),
                EnableSsl = true,
            };

            smtpClient.Send ( message );
        }
        public override string ToString ()
        {
            return $"{FirstName} ; {LastName} ; {email} ; {ssn} ; {phoneNumber} ; {username} ; {password} ; {money}";
        }
        public static Customer GetCustomer(string id)
        {
            return customers.First ( x => x.ssn == id ); // Exception if not found : InvalidOperationException
        }
        public static Customer GetCustomer ( string username, string password )
        {
            return customers.First ( x => x.username == username && x.password == password ); // Exception if not found : InvalidOperationException
        }
    }
}
