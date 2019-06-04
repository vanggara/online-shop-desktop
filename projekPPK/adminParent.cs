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
    public partial class adminParent : Form
    {
        private string idAdmin;

        public adminParent()
        {
            InitializeComponent();
            this.Hide();
        }

        public adminParent(Form f, string idAdmin)
        {
            InitializeComponent();
            this.idAdmin = idAdmin;
            
            panel2.Controls.Clear();
            adminChildDataView child = new adminChildDataView(this.idAdmin);
            this.IsMdiContainer = true;
            child.TopLevel = true;
            child.MdiParent = this;
            panel2.Controls.Add(child);
            child.Show();
        }

        public adminParent(Form signUp)
        {
            InitializeComponent();

            panel2.Controls.Clear();
            adminChildSignUp child = new adminChildSignUp();
            this.IsMdiContainer = true;
            child.TopLevel = true;
            child.MdiParent = this;
            panel2.Controls.Add(child);
            child.Show();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            formAwal fq = new formAwal();
            fq.Show();
        }
    }
}
