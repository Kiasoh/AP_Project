﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SaveAndRead
    {
        public static void ReadData()
        {
            StreamReader? file = null;
            try 
            {
                file = new StreamReader ( "Customer.txt" );
                string? line = null;
                while ( (line = file.ReadLine()) != null )
                {
                    string[] comp = line.Split( " ; " );
                    try {  new Customer ( comp[0], comp[1], comp[2], comp[3], comp[4] , comp[5] , comp[6] ) ; }
                    catch { }
                }
                file.Close ();
            }
            catch { }
            
            try
            {
                file = new StreamReader ( "Employee.txt" );
                string? line = null;
                while ( ( line = file.ReadLine () ) != null )
                {
                    string[] comp = line.Split ( " ; " );
                    try { new Employee ( comp[0], comp[1], comp[2], comp[4], comp[5], comp[3] ) ; }
                    catch { }
                }
                file.Close ();
            }
            catch { }
            

        }
        public static void WriteData ()
        {
            StreamWriter? file = null;
            try { file = new StreamWriter ( "Customer.txt" ); }
            catch { File.Create ( "Customer.txt" ); file = new StreamWriter ( "Customer.txt" ); }
            foreach ( var item in Customer.customers)
            {
                file.WriteLine(item.ToString ());
            }
            file.Close ();

            try { file = new StreamWriter ( "Employee.txt" ); }
            catch { File.Create ( "Employee.txt" ); file = new StreamWriter ( "Employee.txt" ); }
            foreach ( var item in Employee.employees )
            {
                file.WriteLine ( item.ToString () );
            }
            file.Close ();

        }
    }
}
