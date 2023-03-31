using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Account_Manager
{
    internal class CONTACT
    {
        My_DB db = new My_DB();

        // insert new contact
        public bool insertContact(string fname, string lname, int userid, int groupid, string phone, string email, string address, MemoryStream picture)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `mycontacts`(`fname`, `lname`, `group_id`, `phone`, `email`, `address`, `pic`, `userid`) VALUES (@fn,@ln,@gid,@phn,@mail,@adrs,@pic,@uid)", db.getConnection);

            //@fn,@ln,@gid,@phn,@mail,@adrs,@pic,@uid
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@gid", MySqlDbType.Int32).Value = groupid; 
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = email;
            command.Parameters.Add("@adrs", MySqlDbType.VarChar).Value = address;
            command.Parameters.Add("@pic", MySqlDbType.Blob).Value = picture.ToArray();
            command.Parameters.Add("@uid", MySqlDbType.Int32).Value = userid;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
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
