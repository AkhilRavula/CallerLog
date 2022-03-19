using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Windows.Forms;
using log4net;

namespace CallerLog
{
    public class DBConnection
    {

        public string Connectionstring = ConfigurationManager.AppSettings["dBConnection"];     
        public OracleConnection con=null;
        public OracleCommand cmd=null;
        public OracleDataAdapter adapter=null;

        public void OpenCon()
        {

            
            con = new OracleConnection(Connectionstring);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            
        }

        public void CloseCon()
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        public void DataSend(string query)
        {
            try
            {
                OpenCon();
                cmd = new OracleCommand(query, con);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        public OracleDataAdapter GetData(string query)
        {
            try
            {
                OpenCon();
                adapter = new OracleDataAdapter(query, con);
                return adapter;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

    }
}
