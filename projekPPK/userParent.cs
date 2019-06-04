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
    public partial class userParent : Form
    {
        userChildMenu menuKanan;
        private string idPelanggan, idProduk;

        private int max, min;

        private bool xx;

        public userParent(string text)
        {
            InitializeComponent();
            this.idPelanggan = text;
        }
        public userParent(int n, int maxx ,bool x)
        {
            this.xx = x;
            this.min = n;
            this.max = maxx;
            InitializeComponent();
            this.Load += new EventHandler(Form2_Load);
        }

        public userParent(Form1 menu, string id)
        {
            InitializeComponent();

            panel3.Controls.Clear();
            menuKanan = new userChildMenu(id);
            this.IsMdiContainer = true;
            menuKanan.TopLevel = true;
            menuKanan.MdiParent = this;
            panel3.Controls.Add(menuKanan);
            menuKanan.Show();
        }

        public userParent(Form1 signup)
        {
            InitializeComponent();

            panel3.Controls.Clear();
            userChildSignUp sign = new userChildSignUp();
            this.IsMdiContainer = true;
            sign.TopLevel = true;
            sign.MdiParent = this;
            panel3.Controls.Add(sign);
            sign.Show();
        }

        public userParent(userChildMenu signup, string idPelangg, string idProd)
        {
            this.idPelanggan = idPelangg;
            this.idProduk = idProd;

            InitializeComponent();

            panel3.Controls.Clear();
            userChildViewProduk view = new userChildViewProduk(idPelangg, idProd);
            this.IsMdiContainer = true;
            view.TopLevel = true;
            view.MdiParent = this;
            panel3.Controls.Add(view);
            view.Show();
        }

        public userParent(userChildViewProduk signup, string idPelangg, string idProd, string id)
        {
            this.idPelanggan = idPelangg;
            this.idProduk = idProd;

            InitializeComponent();

            panel3.Controls.Clear();
            userChildBeli view = new userChildBeli(idPelangg, idProd);
            this.IsMdiContainer = true;
            view.TopLevel = true;
            view.MdiParent = this;
            panel3.Controls.Add(view);
            view.Show();
        }

        public userParent(userChildViewProduk signup, string idPelangg, string idProd)
        {
            this.idPelanggan = idPelangg;
            this.idProduk = idProd;

            InitializeComponent();

            panel3.Controls.Clear();
            userChildMenu view = new userChildMenu();
            this.IsMdiContainer = true;
            view.TopLevel = true;
            view.MdiParent = this;
            panel3.Controls.Add(view);
            view.Show();
        }

        public userParent(userChildBeli signup, string idProd)
        {
            this.idProduk = idProd;

            InitializeComponent();

            panel3.Controls.Clear();
            userChildViewProduk view = new userChildViewProduk(this.idPelanggan, idProd);
            this.IsMdiContainer = true;
            view.TopLevel = true;
            view.MdiParent = this;
            panel3.Controls.Add(view);
            view.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            formAwal fq = new formAwal();
            fq.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
