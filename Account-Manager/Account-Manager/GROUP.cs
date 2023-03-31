using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Account_Manager
{
    internal class GROUP
    {
        My_DB mydb = new My_DB();

        // function to add a group to the logged in user 
        public bool insertGroup(string gname, int userid)
        {
            // create a group table first
            MySqlCommand command = new MySqlCommand("INSERT INTO `mygroups`(`name`, `userid`) VALUES (@gn,@uid)", mydb.getConnection);

            command.Parameters.Add("@gn", MySqlDbType.VarChar).Value = gname;
            command.Parameters.Add("@uid", MySqlDbType.Int32).Value = userid;

            mydb.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }

        // check if the group name already exists
        public bool groupExits(string gname, string operation, int userid = 0, int gid = 0)
        {
            string query = "";

            MySqlCommand command = new MySqlCommand();

            if (operation == "add")
            {
                // check if the group name already exists
                query = "SELECT * FROM `mygroups` WHERE `name`=@gn AND `userid`=@uid";
                command.Parameters.Add("@gn", MySqlDbType.VarChar).Value = gname;
                command.Parameters.Add("@uid", MySqlDbType.Int32).Value = userid;

            }
            else if (operation == "edit")
            {
                // check if the user enter a group name that already exist and it's not (not including group name)
                query = "SELECT * FROM `mygroups` WHERE `name`=@gn AND `userid`=@uid And `id` <> @gid";
                command.Parameters.Add("@gn", MySqlDbType.VarChar).Value = gname;
                command.Parameters.Add("@uid", MySqlDbType.Int32).Value = userid;
                command.Parameters.Add("@gid", MySqlDbType.Int32).Value = gid;
            }

            command.Connection = mydb.getConnection;
            command.CommandText = query;

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            // if the group name exists return true
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // create a function to get all user groups 
        public DataTable getGroup(int userid)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `mygroups` WHERE `userid`=@uid", mydb.getConnection);

            command.Parameters.Add("@uid", MySqlDbType.Int32).Value=userid;

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            return table;
        }

        //  create a function to edit a group name 
        public bool updateGroup(int gid, string gname)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `mygroups` SET `name`=@name WHERE `id`=@id", mydb.getConnection);

            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = gname;
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = gid;

            mydb.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }

        // create a function to remove group
        public bool deleteGroup(int groupId)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `mygroups` WHERE `id`=@id", mydb.getConnection);

            command.Parameters.Add("@id", MySqlDbType.Int32).Value = groupId;

            mydb.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }
    }
}
