using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data.Models;

namespace Data
{
    public class Customer : IPerson<Customer>
    {
        public static List <Customer> customers = new List<Customer>();
        string _firstName, _lastName, _email, _username, _password;
        int _id;
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
      
        public static Customer GetCustomer(int id)
        {
           
            return customers.First ( x => x.id == id ); // Exception if not found : InvalidOperationException
        }
    }
}
