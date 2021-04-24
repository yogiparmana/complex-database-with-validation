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
    public partial class Main : Form
    {
        CheckKoneksi CheckKoneksi;
        TambahData tambahData;
        UpdateData UpdateData;
        public static Main main;
        SqlCommand sCmd;
        DataSet ds;
        SqlDataAdapter sDa;
        SqlDataReader sRd;
        string[] data = new string[7];

        Conn conn = new Conn();
        public Main()
        {
            if(main == null)
            {
                main = this;
            }
            InitializeComponent();
        }
        void formclose(object sender, FormClosedEventArgs e)
        {
            CheckKoneksi = null;
           
            TampilData("TB_MAHASISWA");
        }
        void formcloseTambahData(object sender, FormClosedEventArgs e)
        {
            tambahData = null;
        }
        void formcloseUpdateData(object sender, FormClosedEventArgs e)
        {
            UpdateData = null;
        }


        private void Main_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;

            if (CheckKoneksi == null)
            {
                CheckKoneksi = new CheckKoneksi();
                CheckKoneksi.FormClosed += new FormClosedEventHandler(formclose);
                CheckKoneksi.ShowDialog();
            }
            else
            {
                CheckKoneksi.Activate();
            }

        }

        public void TampilData(string table)
        {

            SqlConnection connection = conn.GetConn();

            try
            {
                string query = "select * from " + table;
                sCmd = new SqlCommand(query, connection);
                ds = new DataSet();
                sDa = new SqlDataAdapter(sCmd);
                sDa.Fill(ds, table);

                //masukkan ke grid
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = table;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception G)
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tambahData == null)
            {
                tambahData = new TambahData();
                tambahData.FormClosed += new FormClosedEventHandler(formcloseTambahData);
                tambahData.ShowDialog();
            }
            else
            {
                tambahData.Activate();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
              
                data[0] = row.Cells["nim"].Value.ToString();
                data[1] = row.Cells["nama"].Value.ToString();
                data[2] = row.Cells["jenis_kelamin"].Value.ToString();
                string sub =  row.Cells["tanggal_lahir"].Value.ToString().Substring(0,10);
                string[] split = sub.Split('/');
                data[3] = split[1] + "/" + split[0] + "/" + split[2];
                data[4] = row.Cells["no_telp"].Value.ToString();
                data[5] = row.Cells["jurusan"].Value.ToString();
                data[6] = row.Cells["alamat"].Value.ToString();
                button2.Enabled = true;
                button3.Enabled = true;
                button2.Text = "Update Mahasiswa NIM : " + data[0];
                button3.Text = "Delete Mahasiswa NIM : " + data[0];

            }
            catch (Exception g)
            {
                MessageBox.Show(g.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (UpdateData == null)
            {
                UpdateData = new UpdateData(data);
                UpdateData.FormClosed += new FormClosedEventHandler(formcloseUpdateData);
                UpdateData.ShowDialog();
            }
            else
            {
                UpdateData.Activate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HapusData();
        }
        void HapusData()
        {
            MessageBoxButtons btn = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            DialogResult d = MessageBox.Show("Yakin Ingin Hapus?", "Hapus Data Mahasiswa NIM : " + data[0], btn, icon);
            if (d == DialogResult.Yes)
            {
                SqlConnection connection = conn.GetConn();
                string query = "delete TB_MAHASISWA Where nim='" + data[0] + "'";
                connection.Open();
                sCmd = new SqlCommand(query, connection);
                sCmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Di Hapus!");
                TampilData("TB_MAHASISWA");
            }

        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin Ingin Keluar?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CariData("TB_MAHASISWA", "nim", "nama");
        }
        void CariData(string table, string a, string b)
        {

            SqlConnection connection = conn.GetConn();

            try
            {
                connection.Open();
                string query = "select * from " + table + " where " +
                     a + " like '%" + textBox1.Text + "%' " +
                    "or " + b + " like '%" + textBox1.Text + "%'";
                sCmd = new SqlCommand(query, connection);
                ds = new DataSet();
                sDa = new SqlDataAdapter(sCmd);
                sDa.Fill(ds, table);

                //masukkan ke grid
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = table;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
            }

        }


    }
}
