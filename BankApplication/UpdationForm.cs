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

namespace BankApplication
{
    public partial class UpdationForm : Form
    {
        banking_dbEntities1 dbe;
        MemoryStream ms;
        BindingList<userAccount> bi;
        public UpdationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bi = new BindingList<userAccount>();
            dbe = new banking_dbEntities1();
            decimal accno = Convert.ToDecimal(acctxt.Text);
            var item = dbe.userAccounts.Where(a => a.Account_No == accno);
            foreach(var item1 in item)
            {
                bi.Add(item1);
            }
            dataGridView1.DataSource=bi;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bi = new BindingList<userAccount>();
            dbe = new banking_dbEntities1();
            var item = dbe.userAccounts.Where(a => a.Name == nametxt.Text);
            foreach(var item1 in item)
            {
                bi.Add(item1);
            }
            dataGridView1.DataSource = bi;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dbe = new banking_dbEntities1();
            decimal accno = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            var item = dbe.userAccounts.Where(a => a.Account_No == accno).FirstOrDefault();
            acctxt.Text = item.Account_No.ToString();
            nametxt.Text = item.Name;
            mothertxt.Text = item.Mother_Name;
            fathertxt.Text = item.Father_Name;
            phonetxt.Text = item.Phoneno;
            addresstxt.Text = item.Address;
            byte[] img = item.Picture;
            MemoryStream ms = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(ms);
            disttxt.Text = item.District;
            statetxt.Text = item.State;
            if(item.Gender=="Male")
            {
                maleradio.Checked = true;
            }
            else if(item.Gender=="Female")
            {
                femaleradio.Checked = true;
            }
            else if(item.Gender=="Other")
            {
                otherradio.Checked = true;
            }
            else if(item.maritial_status=="Married")
            {
                marriedradio.Checked = true;
            }
            else if(item.maritial_status=="Unmarried")
            {
                unmarriedradio.Checked = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog opendlg = new OpenFileDialog();
            if(opendlg.ShowDialog()==DialogResult.OK)
            {
                Image img = Image.FromFile(opendlg.FileName);
                pictureBox1.Image = img;
                ms = new MemoryStream();
                img.Save(ms, img.RawFormat);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bi.RemoveAt(dataGridView1.SelectedRows[0].Index);
            dbe = new banking_dbEntities1();
            decimal a = Convert.ToDecimal(acctxt.Text);
            userAccount acc = dbe.userAccounts.First(s => s.Account_No.Equals(a));
            dbe.userAccounts.Remove(acc);
            dbe.SaveChanges();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dbe = new banking_dbEntities1();
            decimal accountno = Convert.ToDecimal(acctxt.Text);
            userAccount useraccount = dbe.userAccounts.First(s => s.Account_No.Equals(accountno));
            useraccount.Account_No = Convert.ToDecimal(acctxt.Text);
            useraccount.Name = nametxt.Text;
            useraccount.Date = dateTimePicker1.Value.ToString();
            useraccount.Mother_Name = mothertxt.Text;
            useraccount.Father_Name = fathertxt.Text;
            useraccount.Phoneno = phonetxt.Text;
            if(maleradio.Checked==true)
            {
                useraccount.Gender = "male";
            }
            else
            {
                if (femaleradio.Checked == true)
                    useraccount.Gender = "female";
            }
            if(marriedradio.Checked==true)
            {
                useraccount.maritial_status = "married";
            }
            else
            {
                if (unmarriedradio.Checked == true)
                    useraccount.maritial_status = "Un-married";
            }
            Image img = pictureBox1.Image;
            if(img.RawFormat!=null)
            {
                if(ms!=null)
                {
                    img.Save(ms, img.RawFormat);
                    useraccount.Picture = ms.ToArray();
                }
            }
            useraccount.Address = addresstxt.Text;
            useraccount.District = disttxt.Text;
            useraccount.State = statetxt.Text;
            dbe.SaveChanges();
            MessageBox.Show("Record Updated");
        }
    }
}
