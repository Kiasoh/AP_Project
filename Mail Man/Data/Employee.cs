using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data.Models;

namespace Data
{
    public class Employee : IPerson <Employee>
    {
        public static List<Employee> employees = new List<Employee> ();
        string _firstName, _lastName, _email, _username , _password;
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
        public string username
        {
            get { return _username; }
            set { foreach ( var employee in employees ) if ( employee.username == value ) throw new Exception ( "username ALready in use" ); _username = value; }
        }
        public string password
        {
            get { return _password; }
            set { Regex regex = new Regex ( " ^(?=.*[a - z])(?=.*[A - Z] )(?=.*\\d )[a - zA - Z\\d]{ 8, 32}$" ); if ( !regex.IsMatch ( value ) ) throw new Exception ( "password Error" ); _password = value; }
        }
        public int id
        {
            get { return _id; }
            set { foreach ( var employee in employees ) if ( employee.id == value ) throw new Exception ( "SSN ALready in use" ); _id = value; }
        }
        public static Employee GetEmployee ( string username , string password )
        {
            return employees.First ( x => x.username == username && x.password == password); // Exception if not found : InvalidOperationException
        }

    }
}
