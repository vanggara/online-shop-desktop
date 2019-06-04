using MySql.Data.MySqlClient;
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

namespace projekPPK
{
    public partial class adminChildDataView : Form
    {
        DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();

        Bitmap def;
        private string idAdmin, idProduk;
        private int idAdminInt, idProdukInt;

        MySqlConnection conn;
        String uid = "root";
        String passwd = "";
        String dbase = "projek";
        String server = "127.0.0.1";
        String connString;

        MySqlCommand command;
        MySqlDataAdapter da;

        public adminChildDataView()
        {
            InitializeComponent();
            InitConnection();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
                //MessageBox.Show("Connection Successfully");
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                //conn.Close();
            }
        }

        public adminChildDataView(string idAdmin)
        {
            this.idAdmin = idAdmin;
            InitializeComponent();
            InitConnection();
        }

        private void form2Admin_Load(object sender, EventArgs e)
        {
            dataProduk();
        }

        private void dataProduk()
        {
            idAdminInt = Convert.ToInt32(this.idAdmin);

            string selectQuery = "SELECT * from produk where ID_ADMIN=" + idAdminInt + ";";
            command = new MySqlCommand(selectQuery, conn);
            da = new MySqlDataAdapter(command);

            DataTable table = new DataTable();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 120;
            dataGridView1.AllowUserToAddRows = false;

            da.Fill(table);

            dataGridView1.DataSource = table;

            imageColumn = (DataGridViewImageColumn)dataGridView1.Columns[6];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

            da.Dispose();
            conn.Close();
        }

        private void data_cellclick(object sender, DataGridViewCellEventArgs e)
        {
            merk.Text = dataGridView1.SelectedRows[0].Cells["MERK_PRODUK"].Value.ToString();
            judul.Text = dataGridView1.SelectedRows[0].Cells["NAMA_PRODUK"].Value.ToString();
            harga.Text = dataGridView1.SelectedRows[0].Cells["HARGA_PRODUK"].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells["JENIS_PRODUK"].Value.ToString();

            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            var data = (Byte[])(row.Cells["GAMBAR_PRODUK"].Value);
            var stream = new MemoryStream(data);
            pictureBox1.Image = Image.FromStream(stream);

            deskripsi.Text = dataGridView1.SelectedRows[0].Cells["DESKRIPSI_PRODUK"].Value.ToString();
            this.idProduk = dataGridView1.SelectedRows[0].Cells["ID_PRODUK"].Value.ToString();
            this.idProdukInt = Convert.ToInt32(this.idProduk);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image(*.jpg;.png)|*.jpg; *.png";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();

            String insertQuery = "INSERT INTO PRODUK(ID_ADMIN, MERK_PRODUK, NAMA_PRODUK, JENIS_PRODUK, HARGA_PRODUK, GAMBAR_PRODUK, DESKRIPSI_PRODUK) VALUES(@idAdmin, @merkProduk, @namaProduk, @jenisProduk, @hargaProduk, @gambarProduk, @deskripsiProduk)";

            conn.Open();

            command = new MySqlCommand(insertQuery, conn);

            command.Parameters.Add("@idAdmin", MySqlDbType.Int16);
            command.Parameters.Add("@namaProduk", MySqlDbType.VarChar);
            command.Parameters.Add("@jenisProduk", MySqlDbType.VarChar);
            command.Parameters.Add("@merkProduk", MySqlDbType.VarChar);
            command.Parameters.Add("@hargaProduk", MySqlDbType.Int16);
            command.Parameters.Add("@gambarProduk", MySqlDbType.Blob);
            command.Parameters.Add("@deskripsiProduk", MySqlDbType.Text);

            command.Parameters["@deskripsiProduk"].Value = deskripsi.Text;
            command.Parameters["@hargaProduk"].Value = harga.Text;
            command.Parameters["@merkProduk"].Value = merk.Text;
            command.Parameters["@idAdmin"].Value = this.idAdminInt;
            command.Parameters["@gambarProduk"].Value = img;
            command.Parameters["@jenisProduk"].Value = comboBox1.Text;
            command.Parameters["@namaProduk"].Value = judul.Text;

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Data Inserted");
            }

            conn.Close();
            dataProduk();
        }

        private void harga_keypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();

            String insertQuery = "UPDATE PRODUK SET MERK_PRODUK=@merkProduk, NAMA_PRODUK=@namaProduk, JENIS_PRODUK=@jenisProduk, HARGA_PRODUK=@hargaProduk, GAMBAR_PRODUK=@gambarProduk, DESKRIPSI_PRODUK=@deskripsiProduk where ID_PRODUK="+this.idProdukInt+"";

            conn.Open();

            command = new MySqlCommand(insertQuery, conn);
            
            command.Parameters.Add("@namaProduk", MySqlDbType.VarChar);
            command.Parameters.Add("@jenisProduk", MySqlDbType.VarChar);
            command.Parameters.Add("@merkProduk", MySqlDbType.VarChar);
            command.Parameters.Add("@hargaProduk", MySqlDbType.Int16);
            command.Parameters.Add("@gambarProduk", MySqlDbType.Blob);
            command.Parameters.Add("@deskripsiProduk", MySqlDbType.Text);

            command.Parameters["@deskripsiProduk"].Value = deskripsi.Text;
            command.Parameters["@hargaProduk"].Value = harga.Text;
            command.Parameters["@merkProduk"].Value = merk.Text;
            command.Parameters["@gambarProduk"].Value = img;
            command.Parameters["@jenisProduk"].Value = comboBox1.Text;
            command.Parameters["@namaProduk"].Value = judul.Text;

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Data Updated");
            }

            conn.Close();
            dataProduk();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String insertQuery = "DELETE FROM PRODUK where ID_PRODUK="+this.idProdukInt+"";

            conn.Open();
            command = new MySqlCommand(insertQuery, conn);

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Data Deleted");
            }

            conn.Close();
            dataProduk();
        }
    }
}
