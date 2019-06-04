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
    public partial class adminChildSignUp : Form
    {
        private string idAdmin;

        MySqlConnection conn;
        String uid = "root";
        String passwd = "";
        String dbase = "projek";
        String server = "127.0.0.1";
        String connString;

        public adminChildSignUp()
        {
            InitializeComponent();
            InitConnection();
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
                //MessageBox.Show(e.Message);
            }
            finally
            {
                //conn.Close();
            }
        }

        public adminChildSignUp(string idAdmin)
        {
            this.idAdmin = idAdmin;
        }

        private void signUpAdmin_Load(object sender, EventArgs e)
        {
            
        }

        private void tb_enter(object sender, EventArgs e)
        {
            TextBox temp = (TextBox)sender;
            if (temp.Text == temp.Name)
            {
                temp.ForeColor = Color.Black;
                temp.Clear();
            }
            else
            {
                temp.ForeColor = Color.Black;
            }
        }

        private void tb_leave(object sender, EventArgs e)
        {
            TextBox temp = (TextBox)sender;
            if (temp.Text == "")
            {
                temp.ForeColor = Color.DarkGray;
                temp.Text = temp.Name;
            }
        }

        private void bt1_Click(object sender, EventArgs e)
        {

            if (Username.Text == "Username" || Nama.Text == "Nama" || Password.Text == "Password" || Email.Text == "NomorHP")
            {
                MessageBox.Show("PLEASE FILL THE BLANK");
            }
            else
            {

                if (Retype.Text != Password.Text)
                {
                    label2.Visible = true;

                }
                else
                {
                    label2.Visible = false;
                    String sql = "insert into ADMIN (USERNAME_ADMIN,PASSWORD_ADMIN,NAMA_ADMIN,EMAIL_ADMIN) values ('" + Username.Text + "','" + Password.Text + "','" + Nama.Text + "','" + Email.Text + "');";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    int affected = cmd.ExecuteNonQuery();
                    if (affected >= 1)
                    {
                        ((Form)this.TopLevelControl).Hide();
                        MessageBox.Show("Mashok");
                        adminParent ap = new adminParent();
                        ap.Hide();
                        adminLogin f1 = new adminLogin();
                        f1.Show();
                    }
                }

            }
        }
    }
}
