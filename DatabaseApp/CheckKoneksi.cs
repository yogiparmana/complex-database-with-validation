using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//database Sql import class
using System.Data.SqlClient;
namespace DatabaseApp
{
    public partial class CheckKoneksi : Form
    {
        Conn conn = new Conn();
        public CheckKoneksi()
        {

            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void CheckKoneksi_Load(object sender, EventArgs e)
        {
            button4.Enabled = false;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = conn.GetConn();
            connection.Open();
            labelinfo.Text = "Database Connected";
            labelinfo.ForeColor = Color.Green;
            button4.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin Ingin Keluar?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = conn.GetConn();
            connection.Close();
            labelinfo.Text = "Database Disconected";
            labelinfo.ForeColor = Color.Red;
            button4.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
          
        }
    }
}
