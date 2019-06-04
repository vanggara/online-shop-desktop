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
    public partial class userChildInvoice : Form
    {
        MySqlConnection conn;
        String uid = "root";
        String passwd = "";
        String dbase = "projek";
        String server = "127.0.0.1";
        String connString;

        private string namaPenereima, nomorHp, alamat, harga, namaBarang, jumlahBarang, totalHarga;
        public userChildInvoice(userChildBeli produk)
        {
            InitializeComponent();
            InitConnection();
            this.namaPenereima = produk.namaPenereima;
            this.nomorHp = produk.nomorHp;
            this.alamat = produk.alamat;
            this.harga = produk.harga;
            this.namaBarang = produk.namaBarang;
            this.jumlahBarang = produk.jumlahBarang;
            this.totalHarga = produk.totalHarga;
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

        private void invoice_Load(object sender, EventArgs e)
        {
            label1.Text = namaBarang;
            lbnamaPenerima.Text = namaPenereima;
            lbAlamat.Text = alamat;
            lbHarga.Text = harga;
            lbJumlah.Text = jumlahBarang;
            lbtotalHarga.Text = totalHarga; 
            lbnomorHp.Text=nomorHp;

            String sql = "SELECT * FROM pesanan ORDER BY ID_PESANAN DESC LIMIT 1 ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            MySqlDataAdapter da = new MySqlDataAdapter(new MySqlCommand(sql, conn));

            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                label12.Text = dt.Rows[i][6].ToString();
                //label9
            }
        }
    }
}
