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
    public partial class userChildSignUp : Form
    {
        MySqlConnection conn;
        String uid = "root";
        String passwd = "";
        String dbase = "projek";
        String server = "127.0.0.1";
        String connString;

        public userChildSignUp()
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

        private void tb5_click(object sender, EventArgs e)
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

        private void tb5_leave(object sender, EventArgs e)
        {
            TextBox temp = (TextBox)sender;
            if (temp.Text == "")
            {
                temp.ForeColor = Color.DarkGray;
                temp.Text = temp.Name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bt1_Click_1(object sender, EventArgs e)
        {

            if (Username.Text == "Username" || Nama.Text == "Nama" || Password.Text == "Password" || NomorHP.Text == "NomorHP")
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
                    String sql = "insert into PELANGGAN (USERNAME_PELANGGAN,PASSWORD_PELANGGAN,NAMA_PELANGGAN,NO_HP_PELANGGAN) values ('" + Username.Text + "','" + Password.Text + "','" + Nama.Text + "','" + NomorHP.Text + "');";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    int affected = cmd.ExecuteNonQuery();
                    if (affected >= 1)
                    {
                        ((Form)this.TopLevelControl).Hide();
                        MessageBox.Show("Mashok");
                        this.Hide();
                        Form1 f1 = new Form1();
                        f1.Show();
                    }
                }

            }
        }
        
        private void tb_leave(object sender, EventArgs e)
        {
            TextBox temp = (TextBox)sender;
            if (temp.Text == "")
            {
                temp.ForeColor = Color.Gray;
                temp.Text = temp.Name;
            }
            else
            {
                temp.ForeColor = Color.Black;
            }
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

        private void nomorHp_keypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
