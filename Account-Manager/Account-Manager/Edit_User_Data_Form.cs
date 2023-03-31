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

namespace Account_Manager
{
    public partial class Edit_User_Data_Form : Form
    {
        public Edit_User_Data_Form()
        {
            InitializeComponent();
        }

        USER user = new USER();

        private void Edit_User_Data_Form_Load(object sender, EventArgs e)
        {
            // display the user data
            DataTable table = user.getUserById(Globals.GlobalUserId);
            textBoxFName.Text = table.Rows[0][1].ToString();
            textBoxLName.Text = table.Rows[0][2].ToString();
            textBoxUsername.Text = table.Rows[0][3].ToString();
            textBoxPassword.Text = table.Rows[0][4].ToString();

            byte[] pic = (byte[])table.Rows[0]["picture"];
            MemoryStream picture = new MemoryStream(pic);
            pictureBoxUserImage.Image = Image.FromStream(picture);
        }

        // close button
        private void button_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        // minimize button
        private void button_Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // browse button
        private void button_browse_Click(object sender, EventArgs e)
        {
            // seclect and display image in the picturebox
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxUserImage.Image = Image.FromFile(opf.FileName);
            }
        }

        // edit user data button
        private void button_Edit_User_Click(object sender, EventArgs e)
        {
            My_DB db = new My_DB();

            int userid = Globals.GlobalUserId; // get the logged in user id
            string fname = textBoxFName.Text;
            string lname = textBoxLName.Text;
            string uname = textBoxUsername.Text;
            string pass = textBoxPassword.Text;

            MemoryStream pic = new MemoryStream();
            pictureBoxUserImage.Image.Save(pic, pictureBoxUserImage.Image.RawFormat);

            if(uname.Trim().Equals("") || pass.Trim().Equals("")) // check empty field
            {
                MessageBox.Show("Required Fields: Username and Password", "Edit info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {   
                if(user.usernameExists(uname,"edit",userid)) // check if the username already exists
                {
                    MessageBox.Show("This Username Already Exists Try Another One", "Invalid Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(user.updateUser(userid,fname,lname,uname,pass,pic))
                {
                    MessageBox.Show("Your Information Has Been Updated", "Edit Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error", "Edit Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
