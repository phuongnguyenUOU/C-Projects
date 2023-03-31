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
    public partial class Add_Contact_Form : Form
    {
        public Add_Contact_Form()
        {
            InitializeComponent();
        }

        // 
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //populate combobox 
            GROUP group = new GROUP();
            comboBoxGroup.DataSource = group.getGroup(Globals.GlobalUserId);
            comboBoxGroup.DisplayMember = "name";
            comboBoxGroup.ValueMember = "id";
        }

        // button close
        private void button_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        //button minimize
        private void button_Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // button add contact 
        private void buttonAddContact_Click(object sender, EventArgs e)
        {
            CONTACT contact = new CONTACT();

            string fname = textBoxFName.Text;
            string lname = textBoxLName.Text;
            string phone = textBoxPhone.Text;
            string email = textBoxEmail.Text;
            string address = textBoxAddress.Text;
            int userid = Globals.GlobalUserId;

            try
            {
                // get group id 
                int groupid = (int)comboBoxGroup.SelectedValue;

                //get image 
                MemoryStream pic = new MemoryStream();
                pictureBoxContactImage.Image.Save(pic, pictureBoxContactImage.Image.RawFormat);

                if(contact.insertContact(fname,lname,userid,groupid,email, address,phone,pic))
                {
                     MessageBox.Show("New Contact Added", "Add Contact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error", "Add Contact", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Contact", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // button browse image
        private void button_Browse_Click(object sender, EventArgs e)
        {
            // seclect and display image in the picturebox
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxContactImage.Image = Image.FromFile(opf.FileName);
            }
        }
    }
}
