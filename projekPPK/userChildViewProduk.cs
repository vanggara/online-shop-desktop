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
    public partial class userChildViewProduk : Form
    {

        MySqlConnection conn;
        String uid = "root";
        String passwd = "";
        String dbase = "projek";
        String server = "127.0.0.1";
        String connString;

        private string idPelanggan;
        private string idProduk;
        private int idProdukInt;

        

        public userChildViewProduk(string x, string send)
        {
            InitializeComponent();
            InitConnection();
            //opener = ParentForm;
            this.idPelanggan = x;
            this.idProduk = send;
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

        public userChildViewProduk(string id)
        {
            this.idProduk = id;
            InitializeComponent();
            InitConnection();
            load();
        }

        private void load()
        {
            idProdukInt = Convert.ToInt32(this.idProduk);
            String sql = "select * from PRODUK where id_produk=" + this.idProdukInt + ";";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            //MessageBox.Show("NO "+this.idPelanggan);

            MySqlDataAdapter da = new MySqlDataAdapter(new MySqlCommand(sql, conn));

            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Button btn = new Button();
                FlowLayoutPanel pnl = new FlowLayoutPanel();
                Label lbl1 = new Label();
                Label lbl2 = new Label();
                PictureBox pic = new PictureBox();
                //flowpanel
                pnl.Name = "pnl" + dt.Rows[i][1];
                pnl.Tag = dt.Rows[i][1];
                pnl.Width = 189;
                pnl.Height = 262;
                pnl.BackColor = Color.Aqua;
                pnl.FlowDirection = FlowDirection.TopDown;

                //pictutebox
                pic.Name = "pic" + dt.Rows[i][2];
                pic.Tag = dt.Rows[i][2];
                pic.Width = 183;
                pic.Height = 172;
                pic.SizeMode = PictureBoxSizeMode.Zoom;

                Byte[] data = new Byte[0];
                data = (Byte[])(dt.Rows[i][6]);
                MemoryStream mem = new MemoryStream(data);
                pictureBox1.Image = Image.FromStream(mem);
                //label
                lbl1.Name = "lbl1" + dt.Rows[i][3];
                lbl1.Tag = dt.Rows[i][2];
                label1.Text = dt.Rows[i][3].ToString();
                lbl1.ForeColor = Color.Black;
                lbl1.Font = new Font("Arial", 16f, FontStyle.Bold);
                //label
                lbl2.Name = "lbl3" + dt.Rows[i][4];
                lbl2.Tag = dt.Rows[i][3];
                label6.Text = "Rp. " + dt.Rows[i][5].ToString();
                lbl2.ForeColor = Color.Black;
                //button
                btn.Name = "" + dt.Rows[i][0];
                btn.Tag = dt.Rows[i][1];
                btn.Text = "BELI";
                btn.Font = new Font("Arial", 14f, FontStyle.Bold);
                btn.UseCompatibleTextRendering = true;
                btn.BackColor = Color.Green;


                label5.Text = dt.Rows[i][2].ToString();
                label7.Text = dt.Rows[i][7].ToString();

                pnl.Controls.Add(pic);
                pnl.Controls.Add(lbl1);
                pnl.Controls.Add(lbl2);
                pnl.Controls.Add(btn);
            }
        }

        private void FormViewProduk_Load(object sender, EventArgs e)
        {
            load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            panel1.Controls.Clear();

            ((Form)this.TopLevelControl).Hide();
            userParent f2 = new userParent(this, this.idPelanggan, this.idProduk, this.idProduk);
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((Form)this.TopLevelControl).Hide();
            userParent main = new userParent(this,this.idPelanggan, this.idProduk);
            main.Show();
        }
    }
}
