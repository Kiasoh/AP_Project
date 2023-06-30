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
        string _id;
        public string FirstName
        {
            get { return _firstName; }
            set { if ( !value.IsThisNameValid () ) throw new Exception ( "Firstname Error" ); _firstName = value; }
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
        public string username
        {
            get { return _username; }
            set { foreach ( var employee in employees ) if ( employee.username == value ) throw new Exception ( "username ALready in use" ); _username = value; }
        }
        public string password
        {
            get { return _password; }
            set { if ( !value.IsThisPasswordValid () ) throw new Exception ( "password Error" ); _password = value; }
        }
        public string id
        {
            get { return _id; }
            set { value.IsThisIDValid (); _id = value; }
        }
        public static Employee GetEmployee ( string username , string password )
        {
            return employees.First ( x => x.username == username && x.password == password); // Exception if not found : InvalidOperationException
        }
        public Employee(string FirstName , string LastName , string email , string username , string password , string id)
        {
            this.id = id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.email = email;
            this.username = username;
            this.password = password;
            employees.Add ( this );
        }
        public override string ToString ()
        {
            return $"{FirstName} ; {LastName} ; {email} ; {id} ; {username} ; {password}";
        }

    }
}
