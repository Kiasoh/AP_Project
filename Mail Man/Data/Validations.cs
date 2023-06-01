using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace Data
{
    public static class Validations
    {
        public static bool IsThisNameValid(this string name)
        {
            Regex regex = new Regex ( "^[A-Za-z]{3,32}$" ); 
            if ( regex.IsMatch ( name ) ) return true;
            return false;
        }
        public static bool IsThisEmailValid ( this string name )
        {
            Regex regex = new Regex ( "^[A-Za-z0-9]{3,32}@[A-Za-z]{3,32}\\.[A-Za-z]{2,3}$" );
            if ( regex.IsMatch ( name ) ) return true;
            return false;
        }
        public static bool IsThisPasswordValid ( this string name )
        {
            Regex regex = new Regex ( " ^(?=.*[a - z])(?=.*[A - Z] )(?=.*\\d )[a - zA - Z\\d]{ 8, 32}$" );
            if ( regex.IsMatch ( name ) ) return true;
            return false;
        }
        public static void IsThisSSNValid ( this string id )
        {
            if ( id[0]!=0 || id[1]!=0 || id.Length !=10 ) throw new Exception ( "Invalid SSN!" );
            foreach (var cust in Customer.customers) if (cust.ssn == id) throw new Exception ( "This SSN is already in use!" );
            
        }
        public static void IsThisIDValid (this string id)
        {
            Regex regex = new Regex ( "^[0-9]{2}9[0-9]{2}$" );
            if ( !regex.IsMatch ( id ) ) throw new Exception ( "Invalid ID!" );
            foreach ( var emp in Employee.employees ) if ( emp.id == id ) throw new Exception ( "This ID is already in use!" );
            
        }
        public static bool IsThisPhoneNumberValid(this string s)
        {
            Regex regex = new Regex ( "^09[0-9]{9}$" );
            if ( regex.IsMatch ( s ) ) return true;
            return false;
        }
        public static bool IsThisCVVValid ( this string s )
        {
            Regex regex = new Regex ( "^[0-9]{3,4}$" );
            if ( regex.IsMatch ( s ) ) return true;
            return false;
        }
        public static bool IsExpired (this DateTime date)
        {
            DateTime now = DateTime.Now;
            if(date.CompareTo ( now ) < 0) return false;
            return true;
        }
        public static bool IsCardValid ( this string code )
        {
            int sum = 0;

            for (int i = 0; i < code.Length; i++)
            {
                if (i%2 == 0)
                {
                    sum += int.Parse ( code[i].ToString() );
                }
                else
                {
                    sum += int.Parse ( code[i].ToString () ) * 2;
                }
            }
            if (sum %10 == 0) return true;
            return false;
        }
    }
}
