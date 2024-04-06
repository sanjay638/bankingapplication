using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            banking_dbEntities1 dbe = new banking_dbEntities1();
            if(usrtxt.Text!=string.Empty || passtxt.Text!=string.Empty)
            {
                var user1 = dbe.Admin_Table.FirstOrDefault(a => a.Username.Equals(usrtxt.Text));
                if(user1!=null)
                {
                    if(user1.Password.Equals(passtxt.Text))
                    {
                        Menu m1 = new Menu();
                        m1.ShowDialog();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Password Incorrect");
                    }
                }
                else 
                {
                    MessageBox.Show("Null Value");
                }

            }
            else
            {
                MessageBox.Show("Please Enter UserName And Password");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            usrtxt.Clear();
            passtxt.Clear();
            usrtxt.Focus();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            usrtxt.Focus();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
