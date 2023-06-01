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
        string _firstName, _lastName, _email, username, password;
        static int counter = 0;
        int _id;
        public Customer()
        {
            counter++;
        }
        public string FirstName
        {
            get { return _firstName; }
            set { Regex regex = new Regex ( "^[A-Za-z]{3,32}$" ); if ( !regex.IsMatch ( value ) ) throw new Exception ( "Firstname Error" ); _firstName = value; }
        }
        public string LastName
        {
            get { return _lastName; }
            set { Regex regex = new Regex ( "^[A-Za-z]{3,32}$" ); if ( !regex.IsMatch ( value ) ) throw new Exception ( "Lastname Error" ); _lastName = value; }
        }
        public string email
        {
            get { return _email; }
            set { Regex regex = new Regex ( "^[A-Za-z0-9]{3,32}@[A-Za-z]{3,32}\\.[A-Za-z]{2,3}$" ); if ( !regex.IsMatch ( value ) ) throw new Exception ( "Email Error" ); _lastName = value; }
        }
        public int id
        {
            get { return _id;}
            set { foreach ( var customer in customers ) if ( customer.id == value ) throw new Exception ( "SSN ALready in use" ); _id = value; }
        }
        public void Generate_UsernamePassword()
        {
            username = $"customer#{counter}";
            Random rand = new Random(); int size = rand.Next ( 8, 32 );
            string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] chars = new char[size]; for ( int i = 0; i < size; i++ ) chars[i] = '-';
            chars[rand.Next ( 0, validChars.Length - 1 )] = validChars[rand.Next ( 0, 25 )];
            chars[rand.Next ( 0, validChars.Length - 1 )] = validChars[rand.Next ( 26, 51 )];
            chars[rand.Next ( 0, validChars.Length - 1 )] = validChars[rand.Next ( 51, validChars.Length - 1 )];
            for ( int i = 0; i < chars.Length; i++ )
            {
                if ( chars[i] =='-') chars[i] = validChars[rand.Next ( 0, validChars.Length - 1 )];
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
        public static Customer GetCustomer(int id)
        {
            return customers.First ( x => x.id == id ); // Exception if not found : InvalidOperationException
        }
        public static Customer GetCustomer ( string username, string password )
        {
            return customers.First ( x => x.username == username && x.password == password ); // Exception if not found : InvalidOperationException
        }
    }
}
