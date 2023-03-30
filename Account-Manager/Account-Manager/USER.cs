using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Account_Manager
{
    internal class USER
    {

        My_DB db = new My_DB();

        //function to check the username 
        public bool usernameExists(string username)
        {
            string query = "SELECT * FROM `user` WHERE `username`=@un";
            MySqlCommand command = new MySqlCommand(query, db.getConnection);

            command.Parameters.Add("@un",MySqlDbType.VarChar).Value = username;

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            // if the user exists return true
            if(table.Rows.Count > 0 ) 
            {
                return true;
            }
            else { 
                return false; 
            }
        }

        // insert new user
        public bool insertUser(string fname, string lname, string username, string password, MemoryStream picture) 
        { 
            MySqlCommand command = new MySqlCommand("INSERT INTO `user`(`fname`, `lname`, `username`, `password`, `picture`) VALUES (@fn,@ln,@un,@pass,@pic)", db.getConnection );

            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@un", MySqlDbType.VarChar).Value = username;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
            command.Parameters.Add("@pic", MySqlDbType.Blob).Value = picture.ToArray();

            db.openConnection();

            if(command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }
    }
}
