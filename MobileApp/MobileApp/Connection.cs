using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace connectionDB
{
    public class Connection
    {
        public SqlConnection _sqlconn;

        public Connection()
        {
           

            string dbname = "DeliciousAppdb";
            string servername = "192.168.1.101";
            string serveruser = "Jordan";
            string serverpassword = "jocejoce2";



            string sqlconnection = $"Data Source={servername};Initial Catalog={dbname}; User Id={serveruser};Password={serverpassword}";
            _sqlconn = new SqlConnection(sqlconnection);
        }

        public SqlConnection GetDBConnection()
        {

            return _sqlconn;
        }
    }

}
        
