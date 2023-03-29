using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            panel2.BackgroundImage = Image.FromFile("../../images/1.png");
        }

        // login button
        private void button_Login_Click(object sender, EventArgs e)
        {

        }

        // register button
        private void button_Register_Click(object sender, EventArgs e)
        {

        }

        // browse button
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

        // When this timer start, Show register part
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (panel1.Location.X > -418)
            {
                panel1.Location = new Point(panel1.Location.X - 20, panel1.Location.Y);
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
                panel1.Location = new Point(panel1.Location.X + 20, panel1.Location.Y);
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
    