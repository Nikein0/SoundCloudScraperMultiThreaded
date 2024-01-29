using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace SoundCloudScraperV1._4
{
    public partial class Form2 : Form
    {
        string mysqlCon = "server=127.0.0.1; user=root; database=soundcloudscraper; password=";
        public Form2()
        {
            InitializeComponent();

            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);
            try
            {
                mySqlConnection.Open();
                label3.Text = "Connection success";
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            {
                mySqlConnection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);
            string username = textBoxUser.Text.ToString();
            string pass = textBoxPass.Text.ToString();

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Empty input");
            }
            else
            {
                mySqlConnection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand("select * from users", mySqlConnection);
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (username.Equals(reader["username"].ToString()) && pass.Equals(reader["password"])) {
                        var frm = new Form1();
                        frm.Location = this.Location;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.FormClosing += delegate { this.Show(); };
                        frm.Show();
                        this.Hide();
                    }
                    else { MessageBox.Show("Invalid Login"); }
                }
                mySqlConnection.Close();
            }
            
        }
    }
}
