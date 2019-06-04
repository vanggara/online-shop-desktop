using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekPPK
{
    public partial class userChildMenu : Form
    {
        public string send, username, idPelanggan, namaPelanggan;
        private string min, max, kemeja, kaos, celana,sepatu, sandal;
        private int idpelangganint, maxInt,minInt;
        private bool sort;

        MySqlConnection conn;
        String uid = "root";
        String passwd = "";
        String dbase = "projek";
        String server = "127.0.0.1";
        String connString;
        public int idProduk;
        
        public int i = 0;
        public int id;

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked==true)
            {
                this.kaos = "kaos";
                filterKategori();
            }
            else
            {
                this.kaos = "";
                filterKategori();
            }
            noCentang();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                this.celana = "celana";
                filterKategori();
            }
            else
            {
                this.celana = "";
                filterKategori();
            }
            noCentang();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked==true)
            {
                this.sepatu = "sepatu";
                filterKategori();
            }
            else
            {
                this.sepatu = "";
                filterKategori();
            }
            noCentang();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox5.Checked == true)
            {
                this.sandal = "sandal";
                filterKategori();
            }
            else
            {
                this.sandal = "";
                filterKategori();
            }
            noCentang();
        }

        private void noCentang()
        {
            if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false && checkBox5.Checked == false)
            {
                belumSorting();
            }
        }

        private void filterKategori()
        {
            flowLayoutPanel1.Controls.Clear();
            String sql = "select * from PRODUK where JENIS_PRODUK = '" + this.kemeja + "' OR JENIS_PRODUK = '" + this.kaos + "' OR JENIS_PRODUK = '" + this.sepatu + "' OR JENIS_PRODUK = '" + this.sandal + "' OR JENIS_PRODUK = '" + this.celana + "';";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            //MessageBox.Show("NO "+this.idPelanggan);

            MySqlDataAdapter da = new MySqlDataAdapter(new MySqlCommand(sql, conn));

            DataTable dt = new DataTable();
            da.Fill(dt);
            for (i = 0; i < dt.Rows.Count; i++)
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

                Byte[] data = new Byte[0];
                data = (Byte[])(dt.Rows[i][6]);
                MemoryStream mem = new MemoryStream(data);
                pic.Image = Image.FromStream(mem);
                pic.SizeMode = PictureBoxSizeMode.Zoom;

                //label
                lbl1.Name = "lbl1" + dt.Rows[i][3];
                lbl1.Tag = dt.Rows[i][2];
                lbl1.Text = dt.Rows[i][3].ToString();
                lbl1.ForeColor = Color.Black;
                lbl1.Font = new Font("Arial", 16f, FontStyle.Bold);
                //label
                lbl2.Name = "lbl3" + dt.Rows[i][4];
                lbl2.Tag = dt.Rows[i][3];
                lbl2.Text = "Rp. " + dt.Rows[i][5].ToString();
                lbl2.ForeColor = Color.Black;
                //button
                btn.Name = "" + dt.Rows[i][0];
                btn.Tag = dt.Rows[i][1];
                btn.Text = "BELI";
                btn.Font = new Font("Arial", 14f, FontStyle.Bold);
                btn.UseCompatibleTextRendering = true;
                btn.BackColor = Color.Green;

                pnl.Controls.Add(pic);
                pnl.Controls.Add(lbl1);
                pnl.Controls.Add(lbl2);
                pnl.Controls.Add(btn);
                flowLayoutPanel1.Controls.Add(pnl);

                btn.Click += flow_Click;
            }
        }

        private void maks_keypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public userChildMenu(string txt)
        {
            InitializeComponent();
            InitConnection();
            this.idPelanggan = txt;
        }

        public userChildMenu()
        {
            InitializeComponent();
            InitConnection();
        }

        public userChildMenu(int a, int b, bool x)
        {
            this.sort = x;
            InitializeComponent();
            InitConnection();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            filterHarga();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                this.kemeja = "kemeja";
                filterKategori();
            }
            else
            {
                this.kemeja = "";
                filterKategori();
            }
            noCentang();
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

        private void belumSorting()
        {
            flowLayoutPanel1.Controls.Clear();
                String sql = "select * from PRODUK;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Close();
                //MessageBox.Show("NO "+this.idPelanggan);

                MySqlDataAdapter da = new MySqlDataAdapter(new MySqlCommand(sql, conn));

                DataTable dt = new DataTable();
                da.Fill(dt);
                for (i = 0; i < dt.Rows.Count; i++)
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
                    pnl.BackColor = Color.LightGray;
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
                    pic.Image = Image.FromStream(mem);
                    //label
                    lbl1.Name = "lbl1" + dt.Rows[i][3];
                    lbl1.Tag = dt.Rows[i][2];
                    lbl1.Text = dt.Rows[i][3].ToString();
                    lbl1.ForeColor = Color.Black;
                    lbl1.Font = new Font("Arial", 16f, FontStyle.Bold);
                    //label
                    lbl2.Name = "lbl3" + dt.Rows[i][4];
                    lbl2.Tag = dt.Rows[i][3];
                    lbl2.Text = "Rp. " + dt.Rows[i][5].ToString();
                    lbl2.ForeColor = Color.Black;
                    //button
                    btn.Name = "" + dt.Rows[i][0];
                    btn.Tag = dt.Rows[i][1];
                    btn.Text = "DETAIL";
                    btn.Font = new Font("Arial", 12f, FontStyle.Bold);
                    btn.UseCompatibleTextRendering = true;
                    btn.BackColor = Color.LightGray;
                    
                    pnl.Controls.Add(pic);
                    pnl.Controls.Add(lbl1);
                    pnl.Controls.Add(lbl2);
                    pnl.Controls.Add(btn);
                    flowLayoutPanel1.Controls.Add(pnl);

                    btn.Click += flow_Click;
                }
        }
        
        private void menukanan_Load(object sender, EventArgs e)
        {
            belumSorting();
            namaPengguna();
        }

        private void namaPengguna()
        {
            idpelangganint = Convert.ToInt32(this.idPelanggan);
            String sql = "select * from pelanggan where ID_PELANGGAN=" + this.idpelangganint + ";";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                this.namaPelanggan = reader[3].ToString();
                label4.Text = this.namaPelanggan;
            }
            reader.Close();
        }

        private void filterHarga()
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                belumSorting();
            }
            else
            {
                this.min = textBox1.Text;
                this.max = textBox2.Text;
                this.maxInt = Convert.ToInt32(this.max);
                this.minInt = Convert.ToInt32(this.min);

                flowLayoutPanel1.Controls.Clear();
                String sql = "select * from PRODUK where HARGA_PRODUK BETWEEN " + this.minInt + " AND " + this.maxInt + " ;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Close();
                //MessageBox.Show("NO "+this.idPelanggan);

                MySqlDataAdapter da = new MySqlDataAdapter(new MySqlCommand(sql, conn));

                DataTable dt = new DataTable();
                da.Fill(dt);
                for (i = 0; i < dt.Rows.Count; i++)
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
                    pnl.BackColor = Color.LightGray;
                    pnl.FlowDirection = FlowDirection.TopDown;

                    //pictutebox
                    pic.Name = "pic" + dt.Rows[i][2];
                    pic.Tag = dt.Rows[i][2];
                    pic.Width = 183;
                    pic.Height = 172;

                    Byte[] data = new Byte[0];
                    data = (Byte[])(dt.Rows[i][6]);
                    MemoryStream mem = new MemoryStream(data);
                    pic.Image = Image.FromStream(mem);
                    pic.SizeMode = PictureBoxSizeMode.Zoom;

                    //label
                    lbl1.Name = "lbl1" + dt.Rows[i][3];
                    lbl1.Tag = dt.Rows[i][2];
                    lbl1.Text = dt.Rows[i][3].ToString();
                    lbl1.ForeColor = Color.Black;
                    lbl1.Font = new Font("Arial", 16f, FontStyle.Bold);
                    //label
                    lbl2.Name = "lbl3" + dt.Rows[i][4];
                    lbl2.Tag = dt.Rows[i][3];
                    lbl2.Text = "Rp. " + dt.Rows[i][5].ToString();
                    lbl2.ForeColor = Color.Black;
                    //button
                    btn.Name = "" + dt.Rows[i][0];
                    btn.Tag = dt.Rows[i][1];
                    btn.Text = "DETAIL";
                    btn.Font = new Font("Arial", 12f, FontStyle.Bold);
                    btn.UseCompatibleTextRendering = true;
                    //btn.BackColor = Color.Green;

                    pnl.Controls.Add(pic);
                    pnl.Controls.Add(lbl1);
                    pnl.Controls.Add(lbl2);
                    pnl.Controls.Add(btn);
                    flowLayoutPanel1.Controls.Add(pnl);

                    btn.Click += flow_Click;
                }
            }
        }

        private void flow_Click(object sender, EventArgs e)
        {

            FlowLayoutPanel pn2 = new FlowLayoutPanel();
            pn2.Width = 569;
            pn2.Height = 532;
            pn2.BackColor = Color.LightGray;
            
            Button button = (Button)sender;
            send = button.Name.ToString();
            Int32.TryParse(send, out id);

            this.idProduk = Convert.ToInt32(send);
            
            ((Form)this.TopLevelControl).Hide();
            userParent f2 = new userParent(this, this.idPelanggan, this.send);
            f2.Show();
        }

    }
}