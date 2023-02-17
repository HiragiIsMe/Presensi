using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Absensi
{
    public partial class MainLogin : Form
    {
        public MainLogin()
        {
            InitializeComponent();
        }
        void onload()
        {
            ControlBox = false;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.None;
            this.CenterToScreen();
            textBoxPassword.UseSystemPasswordChar = true;
            textBoxUsername.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            onload();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Close Application?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        bool validate()
        {
            if (textBoxUsername.Text == "" || textBoxPassword.Text == "")
            {
                MessageBox.Show("Semua Form Wajib Diisi", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBoxPassword.TextLength < 8)
            {
                MessageBox.Show("Password Setidaknya 8 Character", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void textBoxUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSub.PerformClick();
            }
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSub.PerformClick();
            }
        }

        private void buttonSub_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select * from admin where username=@username and password=@password", Connection.conn);
                    string pass = Enc.getPass(textBoxPassword.Text);
                    cmd.Parameters.AddWithValue("username", textBoxUsername.Text);
                    cmd.Parameters.AddWithValue("password", pass);

                    Connection.conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();

                    if (reader.HasRows)
                    {
                        Connection.conn.Close();
                        MainAdmin admin = new MainAdmin();
                        this.Hide();
                        admin.Show();
                    }
                    else
                    {
                        Connection.conn.Close();
                        MessageBox.Show("Username Atau Password Salah!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
