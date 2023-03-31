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
    public partial class Login_Register_Form : Form
    {
        public Login_Register_Form()
        {
            InitializeComponent();
        }

        private void Login_Register_Form_Load(object sender, EventArgs e)
        {
            // display image on the panel (menu bar)
            /*panel2.BackgroundImage = Image.FromFile("../../images/1.png");*/
        }

        // button login  
        private void button_Login_Click(object sender, EventArgs e)
        {
            My_DB db = new My_DB();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            DataTable table = new DataTable(); 

            MySqlCommand command = new MySqlCommand("SELECT * FROM `user` WHERE `username`=@un and `password`=@pass", db.getConnection);

            command.Parameters.Add("@un",MySqlDbType.VarChar).Value = textBoxUsername.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = textBoxPassword.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(veriffields("login")) // check for empty fields
            {
                if(table.Rows.Count > 0) // check if this user exists
                {
                    // display username and image in the main form
                    // global id
                    int userid = Convert.ToInt32(table.Rows[0][0].ToString());
                    Globals.setGlobalUserId(userid);

                    // show the main form
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    // show an error message
                    MessageBox.Show("Invalid Username or Password", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Username or Password", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // button register  
        private void button_Register_Click(object sender, EventArgs e)
        {
            string fname = textBoxFName.Text;
            string lname = textBoxFName.Text;
            string username = textBoxUsernameRegister.Text;
            string password = textBoxPasswordRegister.Text;

            USER user = new USER();

            if(veriffields("register"))
            {
                MemoryStream pic = new MemoryStream();
                pictureBoxProfile.Image.Save(pic, pictureBoxProfile.Image.RawFormat);

                // we need to check if the username already exists
                // insert the new user in the database 
                // we will create that in class USER
                if(!user.usernameExists(username,"register"))
                {
                    if(user.insertUser(fname,lname,username,password,pic))
                    {
                        MessageBox.Show("Registration Completed Successfully", "Register",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Something Wrong", "Register", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("This Username Already Exists Try Another One", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Required Fields - Username / Password / Image", "Register", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // create a function to check empty fields
        public bool veriffields(string operation)
        {
            bool check = false;
            
            // if the operation is to register 
            if(operation == "register")
            {
                // can  use fname, lname to the verification 
                if(textBoxUsernameRegister.Text.Trim().Equals("") || textBoxPasswordRegister.Text.Trim().Equals("") || pictureBoxProfile.Image == null)
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
            }
            //  if the opereation is to login 
            else if(operation == "login")
            {
                if(textBoxUsername.Text.Trim().Equals("") || textBoxPassword.Text.Trim().Equals(""))
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
            }
            return check;
        }

        // button browse  
        private void button_Browse_Click(object sender, EventArgs e)
        {
            // seclect and display image in the picturebox
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if(opf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxProfile.Image = Image.FromFile(opf.FileName);
            }
        }

        // link go to register
        private void labelGoToRegister_Click(object sender, EventArgs e)
        {
            timer1.Start();
            labelGoToRegister.Enabled = false;
            labelGoToLogin.Enabled = false;
        }

        // link go to login
        private void labelGoToLogin_Click(object sender, EventArgs e)
        {
            timer2.Start();
            labelGoToRegister.Enabled = false;
            labelGoToLogin.Enabled = false;
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

        // When this timer start, Show register part
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (panel1.Location.X > -418)
            {
                panel1.Location = new Point(panel1.Location.X - 418, panel1.Location.Y);
            }
            else
            {
                timer1.Stop();
                labelGoToLogin.Enabled = true;
                labelGoToRegister.Enabled = true;
            }
        }

        // When this timer start, Show login part
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (panel1.Location.X < 0)
            {
                panel1.Location = new Point(panel1.Location.X + 418, panel1.Location.Y);
            }
            else
            {
                timer2.Stop();
                labelGoToLogin.Enabled = true;
                labelGoToRegister.Enabled = true;
            }
        }
    }
}
    