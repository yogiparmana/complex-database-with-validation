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
    public partial class TambahData : Form
    {
        Conn conn = new Conn();
        SqlCommand sCmd;
        public TambahData()
        {
            InitializeComponent();
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        void TambahDataMhs()
        {

            SqlConnection connection = conn.GetConn();
            try
            {
                string query = "insert into TB_MAHASISWA values('" +
                    textBox1.Text + "','" +
                    textBox2.Text + "','" + 
                    comboBox1.Text + "','" +
                    textBox4.Text + "','" +
                    textBox5.Text + "','" +
                    comboBox2.Text + "','" +
                    textBox7.Text + "')";
                connection.Open();
                sCmd = new SqlCommand(query, connection);
                sCmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Di tambah!");
                this.Close();
                
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
            }

        }

        private void TambahData_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "Laki-Laki";
            comboBox2.Text = "Sistem Infomasi";
        }
        bool Validasi()
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "" || textBox7.Text.Trim() == "")
            {
                MessageBox.Show("Form Ada Yang kosong!","WARNING",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            else
            {

                return true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validasi())
            {
                TambahDataMhs();
                Main.main.TampilData("TB_MAHASISWA");
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }
    }
}
