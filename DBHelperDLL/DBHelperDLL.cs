using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelperDLL
{
    public class DBHelper
    {
        public SqlConnection conn;

        //method to open the connection to the database
        public SqlConnection openConn(string connString)
        {

            try
            {

                conn = new SqlConnection(connString);

                conn.Open();

            }

            catch (Exception e)
            {

                Debug.WriteLine(e);
            }

            return conn;

        }

        //method to close the connection to the database
        public void closeConn()
        {

            try
            {

                conn.Close();

            }

            catch (Exception e)
            {

                Debug.WriteLine(e);
            }

        }
    }
}
