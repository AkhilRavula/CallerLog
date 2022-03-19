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
    public partial class ViewCallDetails : Form
    {
        public ViewCallDetails()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBConnection dB = new DBConnection();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MMM-yyyy";
            string sql = "SELECT * FROM CALLERLOG WHERE DATE_=to_date('" + dateTimePicker1.Text+"')";
            var adp=dB.GetData(sql);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //dataGridView1.DataSource = dt;//this we can use when we want directly datatable in grid 
                foreach (DataRow row in dt.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells["SNo"].Value = n + 1;
                    dataGridView1.Rows[n].Cells["Name_"].Value = row["Name"].ToString();
                    dataGridView1.Rows[n].Cells["FatherName"].Value = row["FATHER_NAME"].ToString();
                    dataGridView1.Rows[n].Cells["Mobile"].Value = row["MOBILE"].ToString();
                    dataGridView1.Rows[n].Cells["Address"].Value = row["ADDRESS"].ToString();
                    dataGridView1.Rows[n].Cells["Status"].Value = row["STATUS"].ToString();
                    dataGridView1.Rows[n].Cells["Date"].Value = Convert.ToDateTime(row["DATE_"].ToString()).ToString("dd-MM-yyyy");
                    dataGridView1.Rows[n].Cells["Time"].Value = row["TIME_"].ToString();
                    dataGridView1.Rows[n].Cells["Duration"].Value = row["DURATION_"].ToString();
                    dataGridView1.Rows[n].Cells["Remarks"].Value = row["REMARKS"].ToString();
                }
                dB.CloseCon();
                dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            }
            else
            {
                dB.CloseCon();
                dateTimePicker1.CustomFormat = "dd-MM-yyyy";
                MessageBox.Show("No Records Found");
            }

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

        private void ViewCallDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();
            this.Dispose();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }
    }
}
