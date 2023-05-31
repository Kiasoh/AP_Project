using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data
{
    public class Employee
    {
        string _firstName, _lastName , _email , _username , _password;
        int _id;
        public string FirstName
        {
            get { return _firstName; }
            set { Regex regex = new Regex ( "^.{3,32}$" ); if ( !regex.IsMatch ( value ) ) throw new Exception ( "Error" ); _firstName = value; } 
        }
        public string LastName 
        {
            get { return _lastName; }
            set { Regex regex = new Regex ( "^.{3,32}$" ); if ( !regex.IsMatch ( value ) ) throw new Exception ( "Error" ); _lastName = value; }
        }

        

    }
}
