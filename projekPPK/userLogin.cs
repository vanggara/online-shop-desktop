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
    public partial class Form1 : Form
    {
       
        Form1 form1;
        private userParent form2;
        userChildBeli formproduk;

        private string user, idPelanggan;
        public int idPelangganInt;

        MySqlConnection conn;
        String uid = "root";
        String passwd = "";
        String dbase = "projek";
        String server = "127.0.0.1";
        String connString;

        public Form1()
        {
            InitializeComponent();
            InitConnection();
        }

        public Form1(userParent form2)
        {
            this.form2 = form2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void tb_click(object sender, EventArgs e)
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

        private void lb3_mouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.DeepSkyBlue;
        }

        private void lb3_mouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
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

        private void label3_Click(object sender, EventArgs e)
        {

            ((Form)this.TopLevelControl).Hide();
            userParent main = new userParent(this);
            //this.Hide();
            main.Show();
        }

        
        private void bt1_Click_1(object sender, EventArgs e)
        {
            int count = 0;
            String sql = "select * from PELANGGAN where USERNAME_PELANGGAN='" + Username.Text + "' and PASSWORD_PELANGGAN='" + Password.Text + "';";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            this.user = Username.Text;
            while (reader.Read())
            {
                count += 1;
                this.idPelanggan = reader["ID_PELANGGAN"].ToString();
                this.idPelangganInt = Convert.ToInt32(this.idPelanggan);
            }
            //reader.Close();
            if (count == 1)
            {
                //menuju
                userParent main = new userParent(this, this.idPelanggan);
                //this.Hide();
                main.Show();

                ((Form)this.TopLevelControl).Hide();
            }
            else if (Username.Text == "Username" || Password.Text == "Password" || Username.Text == "")
            {
                MessageBox.Show("Fill Username and Password");
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password");
            }
            reader.Close();
        }
    }
}
