using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ReadData
    {
        public static void ReadDataFromTable()
        {
            SqlConnection sqlConnection = new SqlConnection(@"../../../Database/CustomersDB.sql");
            sqlConnection.Open();
            //sqlConnection.Beg
            sqlConnection.Close();
        }
    }
}
