using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Absensi
{
    public partial class MainAbsensi : Form
    {
        public MainAbsensi()
        {
            InitializeComponent();
            
        }
        void Clear()
        {
            textBoxCode.Text = "";
            textBoxNama.Text = "";
            textBoxJk.Text = "";
            textBoxJabatan.Text = "";
            richTextBox1.Text = "";
            pictureBox1.Image = null;
        }
        void onload()
        {
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            timer1.Start();
            textBoxCode.Focus();
            this.CenterToScreen();
        }
        private void MainAbsensi_Load(object sender, EventArgs e)
        {
            onload();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            labelTime.Text = dt.ToString();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private async void textBoxCode_KeyDown(object sender, KeyEventArgs e)
        {
            int id;
            if (e.KeyCode == Keys.Enter)
            {
                SqlCommand cmd = new SqlCommand("select * from Employee join Jabatan on Employee.id_jabatan = Jabatan.id where rfid='" + textBoxCode.Text + "'", Connection.conn);
                Connection.conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                id = reader.GetInt32(0);
                textBoxNama.Text = reader["nama"].ToString();
                if (reader["jenis_kelamin"].ToString() == "1")
                {
                    textBoxJk.Text = "Laki-Laki";
                }
                else
                {
                    textBoxJk.Text = "Perempuan";
                }

                textBoxJabatan.Text = reader["jabatan"].ToString();
                richTextBox1.Text = reader["alamat"].ToString();

                MemoryStream stream = new MemoryStream((byte[])reader["foto"]);
                pictureBox1.Image = Image.FromStream(stream);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                Connection.conn.Close();

                SqlCommand insert = new SqlCommand("insert into Presensi values(" + id + ", getdate(), null,CONVERT(date, GETDATE()))", Connection.conn);
                Connection.conn.Open();
                insert.ExecuteNonQuery();
                Connection.conn.Close();
                await Task.Delay(1500);
                Clear();
            }
        }
    }
}
