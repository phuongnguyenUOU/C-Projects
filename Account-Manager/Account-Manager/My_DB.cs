using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
// add mysql connector to connect with myspl database
using MySql.Data.MySqlClient;

namespace Account_Manager
{
    internal class My_DB
    {
        // connection
        MySqlConnection con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=account_manager_db");

        // return the connection
        public MySqlConnection getConnection
        {
            get
            {
                return con;
            }
        }

        // open the connection
        public void openConnection()
        {
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        // close the connection
        public void closeConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
