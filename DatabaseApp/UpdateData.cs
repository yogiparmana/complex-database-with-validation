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
    public partial class UpdateData : Form
    {
        Conn conn = new Conn();
        SqlCommand sCmd;
        public UpdateData(string[] data)
        {
            InitializeComponent();
            textBox1.Text = data[0];
            textBox1.Enabled = false;
            textBox2.Text = data[1];
            comboBox1.Text = data[2];
            textBox4.Text = data[3];
            textBox5.Text = data[4];
            comboBox2.Text = data[5];
            textBox7.Text = data[6];
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        void UpdateDataMhs()
        {
            SqlConnection connection = conn.GetConn();
            try
            {
                connection.Open();
                string query = "Update TB_MAHASISWA set " +
                        "nama='" + textBox2.Text +
                        "',jenis_kelamin='" + comboBox1.Text +
                         "',tanggal_lahir='" + textBox4.Text +
                          "',no_telp='" + textBox5.Text +
                           "',jurusan='" + comboBox2.Text +
                            "',alamat='" + textBox7.Text +
                        "' Where nim='" + textBox1.Text + "'";
                sCmd = new SqlCommand(query, connection);
                sCmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Di Update!");
                this.Close();


            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
            }

        }


        bool Validasi()
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == ""  || textBox7.Text.Trim() == "")
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
                UpdateDataMhs();
                Main.main.TampilData("TB_MAHASISWA");
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void UpdateData_Load(object sender, EventArgs e)
        {

        }
    }
}
