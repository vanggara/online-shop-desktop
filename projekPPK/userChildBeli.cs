using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekPPK
{
    public partial class userChildBeli : Form
    {
        MySqlConnection conn;
        String uid = "root";
        String passwd = "";
        String dbase = "projek";
        String server = "127.0.0.1";
        String connString;

        Form1 f1;

        public string u;

        //Form opener;
        public int i, idPesanInt, idPelangganInt, idProdukInt;
        public string s;
        public string idPelanggan, idPesan, idProduk;
        public string namaPenereima, nomorHp, alamat, harga, namaBarang, jumlahBarang, totalHarga;

        private void jumlah_keypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void nomorHp_keypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public userChildBeli(string pelanggan, string produk)
        {
            InitializeComponent();
            InitConnection();
            //opener = ParentForm;
            this.idPelanggan = pelanggan;
            this.idProduk = produk;
        }

        private void InitConnection()
        {
            try
            {
                conn = new MySqlConnection();
                connString = "server=" + server + ";" +
                      "database=" + dbase + ";" +
                      "uid=" + uid + ";" +
                      "password=" + passwd + ";" +
                      "sslMode=none";
                conn.ConnectionString = connString;
                conn.Open();
                // MessageBox.Show("Connection Successfully");
            }
            catch (MySqlException e)
            {
                //MessageBox.Show(e.Message);
            }
            finally
            {
                //conn.Close();
            }
        }

        public userChildBeli()
        {
            InitializeComponent();
        }
        
        private void button2_Click_1(object sender, EventArgs e)
        {

            this.namaPenereima = textBox1.Text;
            this.namaBarang = label1.Text;
            this.nomorHp = textBox2.Text;
            this.harga = label2.Text;
            this.alamat = textBox3.Text;
            this.jumlahBarang = textBox5.Text;
            this.totalHarga = label9.Text;

            this.idPelangganInt = Convert.ToInt32(idPelanggan);
            
            String insert = "INSERT INTO pesanan (`ID_PRODUK`, `ID_PELANGGAN`, `NOMOR_HP_PESANAN`, `ALAMAT_PESANAN`,`JUMLAH_BARANG_PESANAN`, `TANGGAL_PESANAN`, `TOTAL_HARGA_PESANAN`) " +
                "VALUES(" + this.idProdukInt + "," + this.idPelangganInt + ",'" + this.nomorHp + "','" + this.alamat + "'," + this.jumlahBarang + ",NOW()," + this.totalHarga + ") " +
                "ON DUPLICATE KEY UPDATE NOMOR_HP_PESANAN='" + this.nomorHp + "',ALAMAT_PESANAN='" + this.alamat + "', JUMLAH_BARANG_PESANAN =" + this.jumlahBarang + ", TANGGAL_PESANAN=CURDATE(),TOTAL_HARGA_PESANAN=" + this.totalHarga + ";";
            MySqlCommand cmd2 = new MySqlCommand(insert, conn);
            int affected = cmd2.ExecuteNonQuery();
            if (affected >= 1)
            {
                String sql = "select * from pesanan where ID_PELANGGAN=" + this.idPelangganInt + " and ID_PRODUK=" + this.idProdukInt + ";";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    this.idPesan = reader[0].ToString();
                    this.idPesanInt = Convert.ToInt32(this.idPesan);
                }
                reader.Close();
                String bayar = "INSERT INTO bayar (ID_PESANAN,TOTAL_BAYAR, TANGGAL_BAYAR) values (" + this.idPesanInt + "," + this.totalHarga + ",NOW());";
                MySqlCommand cmd3 = new MySqlCommand(bayar, conn);
                int affected3 = cmd3.ExecuteNonQuery();
                if (affected3 >= 1)
                {
                    MessageBox.Show("Mashok");
                    userChildInvoice inv = new userChildInvoice(this);
                    inv.Show();
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ((Form)this.TopLevelControl).Hide();
            userParent main = new userParent(this, this.idProduk);
            main.Show();
        }

        private void formProduk_Load(object sender, EventArgs e)
        {
            this.idProdukInt = Convert.ToInt32(idProduk);
            String sql = "select * from PRODUK where ID_PRODUK=" + this.idProdukInt;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            MySqlDataAdapter da = new MySqlDataAdapter(new MySqlCommand(sql, conn));

            DataTable dt = new DataTable();
            da.Fill(dt);
            for (i = 0; i < dt.Rows.Count; i++)
            {
                label1.Text = dt.Rows[i][3].ToString();
                label2.Text = dt.Rows[i][5].ToString();
                label9.Text = dt.Rows[i][5].ToString();
                //label9
            }
        }
        
    }
}
