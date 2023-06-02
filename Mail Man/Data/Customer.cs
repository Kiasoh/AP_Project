using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data.Models;
using System.Net.Mail;
namespace Data
{
    public class Customer : IPerson<Customer>
    {
        public static List <Customer> customers = new List<Customer>();
        string _firstName, _lastName, _email, username, password , _phoneNumber;
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
            set { if ( !value.IsThisEmailValid () ) throw new Exception ( "Email Error" ); _lastName = value; }
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
            chars[rand.Next ( 0, validChars.Length  )] = validChars[rand.Next ( 0, 26 )];
            chars[rand.Next ( 0, validChars.Length  )] = validChars[rand.Next ( 26, 52 )];
            chars[rand.Next ( 0, validChars.Length  )] = validChars[rand.Next ( 52, validChars.Length )];
            for ( int i = 0; i < chars.Length; i++ )
            {
                if ( chars[i] =='-') chars[i] = validChars[rand.Next ( 0, validChars.Length )];
            }
            password = new string( chars );
            
            SmtpClient smtpClient = new SmtpClient();
            var mailMessage = new MailMessage
            {
                From = new MailAddress ( "kiarashs37@gmail.com" ),
                Subject = "Your MAIL MAN Username and Password",
                Body = $"<h1>Hello</h1><h2>Username :{username}</h2><h3>Password :{password}</h3>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add ( email );

            smtpClient.Send ( mailMessage );
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
