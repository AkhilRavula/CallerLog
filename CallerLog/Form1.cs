using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CallerLog
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox1.TextLength > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    textBox2.Focus();
                }
            }
            else
            {
                textBox1.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox2.TextLength > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    textBox3.Focus();
                }
            }
            else
            {
                textBox2.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox3.TextLength > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    textBox7.Focus();
                }
            }
            else
            {
                textBox3.Focus();
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox7.TextLength > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    comboBox1.Focus();
                }
            }
            else
            {
                textBox7.Focus();
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(comboBox1.SelectedIndex==-1)
            {
                if (e.KeyCode == Keys.Enter)
                    comboBox1.Focus();
            }
            else
            {
                dateTimePicker1.Focus();
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (dateTimePicker1.Value!=null)
            {
                if (e.KeyCode == Keys.Enter)
                    textBox4.Focus();
            }
            else
            {
                dateTimePicker1.Focus();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox4.TextLength>0)
            {
                if (e.KeyCode == Keys.Enter)
                    textBox5.Focus();
            }
            else
            {
                textBox4.Focus();
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox5.TextLength > 0)
            {
                if (e.KeyCode == Keys.Enter)
                    textBox6.Focus();
            }
            else
            {
                textBox5.Focus();
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox6.TextLength > 0)
            {
                if (e.KeyCode == Keys.Enter)
                    button1.Focus();
            }
            else
            {
                textBox6.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text)&& string.IsNullOrEmpty(textBox3.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "Name Required");

                errorProvider2.Clear();
                errorProvider2.SetError(textBox3, "Mobile Number Required");
            }
            else if(string.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "Name Required");
            }
            else if (string.IsNullOrEmpty(textBox3.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox3, "Name Required");
            }
            else
            {
               bool flag= ChecklengthMobile();
                if (flag)
                {
                    errorProvider1.Clear();
                    errorProvider2.Clear();
                    DBConnection dB = new DBConnection();
                    dateTimePicker1.Format = DateTimePickerFormat.Custom;
                    dateTimePicker1.CustomFormat = "dd-MMM-yyyy";
                    string Sql = @"Insert into CALLERLOG (NAME,FATHER_NAME,MOBILE,ADDRESS,STATUS,DATE_,TIME_,DURATION_,REMARKS) " +
                        "values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox7.Text + "','" + comboBox1.SelectedText + "',to_date('" + dateTimePicker1.Text + "'),'" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')";
                    dB.DataSend(Sql);
                    dB.CloseCon();

                    MessageBox.Show("Record Added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
                dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            }

        }

        private bool ChecklengthMobile()
        {
            if(textBox3.TextLength==10)
            {

                return true;
            }
            else
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox3, "Number should be 10 digits");
                return false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox3.Text = textBox3.Text.Remove(textBox3.Text.Length - 1);
            }
        }

        void Clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.CustomFormat = "dd-mm-yyyy";
            comboBox1.SelectedIndex = -1;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if
(MessageBox.Show("Quit the Application", "Exit Application Dialog", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewCallDetails Vcd = new ViewCallDetails();
            Vcd.StartPosition = FormStartPosition.CenterParent;
            Vcd.Owner = this;
            this.Hide();
            Vcd.Show();
            
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                if
                (MessageBox.Show("Quit the Application", "Exit Application Dialog", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                    return true;
                }
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
