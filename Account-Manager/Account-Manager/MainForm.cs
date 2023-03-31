using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Account_Manager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        My_DB mydb = new My_DB();
        GROUP group = new GROUP();

        // form load
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //display the user image and username
            getImageAndUsername();

            // populate the combobox with group name
            getGroups();
        }

        // button close 
        private void button_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        // button minimize 
        private void button_Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // create a function to display the logged in image and username
        public void getImageAndUsername()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            DataTable table = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `user` WHERE `id`=@uid", mydb.getConnection);

            command.Parameters.Add("@uid", MySqlDbType.Int32).Value = Globals.GlobalUserId;

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if(table.Rows.Count > 0 )
            {
                // display the user image
                byte[] pic = (byte[])table.Rows[0]["picture"];
                MemoryStream picture1 = new MemoryStream(pic);
                pictureBox1.Image = Image.FromStream(picture1 );

                // display the username
                labelUsername.Text = "Welcome Back ("+ table.Rows[0]["username"] +")"; 
            }
        }

        // edit user data
        private void labelEditUserData_Click(object sender, EventArgs e)
        {
            Edit_User_Data_Form editUserForm = new Edit_User_Data_Form();
            editUserForm.Show(this);
        }

        // button add new group
        private void buttonAddGroup_Click(object sender, EventArgs e)
        {
            string groupName = textBoxAddGroupName.Text;

            if (!groupName.Trim().Equals(""))
            {
                // check if the group name already exists for the logged in user 
                if(!group.groupExits(groupName,"add",Globals.GlobalUserId))
                {
                    if(group.insertGroup(groupName,Globals.GlobalUserId))
                    {
                    MessageBox.Show("New Group Inserted", "Add Group", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Group Not Inserted", "Add Group", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    getGroups(); // populate the combobox again to display the new data
                }
                else
                {
                    MessageBox.Show("This Group Name Already Exists", "Add Group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Enter a Group Name Before Inserting", "Add Group", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // populate the combobox
        public void getGroups()
        {
            //populate edit combobox 
            comboBoxEditGroupId.DataSource = group.getGroup(Globals.GlobalUserId);
            comboBoxEditGroupId.DisplayMember = "name";
            comboBoxEditGroupId.ValueMember = "id";

            //populate remove combobox 
            comboBoxRemoveGroupId.DataSource = group.getGroup(Globals.GlobalUserId);
            comboBoxRemoveGroupId.DisplayMember = "name";
            comboBoxRemoveGroupId.ValueMember = "id";
        }

        // button edit group name
        private void buttonEditGroup_Click(object sender, EventArgs e)
        {
            try
            {
                string newGroupName = textBoxEditGroupName.Text;
                int groupId = Convert.ToInt32(comboBoxEditGroupId.SelectedValue.ToString());

                if (!newGroupName.Trim().Equals(""))
                {
                    if(!group.groupExits(newGroupName,"edit",Globals.GlobalUserId,groupId))
                    {
                        if(group.updateGroup(groupId, newGroupName))
                        {
                            MessageBox.Show("Group Name Updated", "Edit Group", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Group Name Not Updated", "Edit Group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        getGroups();
                    }
                    else
                    {
                        MessageBox.Show("This Group Name Already Exists", "Edit Group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Enter Group Name Before Editing", "Edit Group", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Select A Group Before Editing", "Edit Group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // button remove selected group 
        private void buttonRemoveGroup_Click(object sender, EventArgs e)
        {
            int groupId = Convert.ToInt32(comboBoxRemoveGroupId.SelectedValue.ToString());

            if(MessageBox.Show("Are You Sure You Want To Delete This Group", "Remove Group", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (group.deleteGroup(groupId))
                    {
                        MessageBox.Show("Group Deleted", "Remove Group", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Group Not Deleted", "Remove Group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    getGroups();
                }
                catch
                {
                    MessageBox.Show("Select A Group Before Deleting", "Remove Group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }                
            }
        }

        // refresh the user image and username
        private void labelRefresh_Click(object sender, EventArgs e)
        {
            getImageAndUsername();
        }

        // button add contact
        private void buttonAddContact_Click(object sender, EventArgs e)
        {
            // show add contact form
            Add_Contact_Form addContacttF = new Add_Contact_Form();
            addContacttF.Show(this);
        }
    }
}
