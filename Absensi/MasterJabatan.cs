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
    public partial class MasterJabatan : Form
    {
        public MasterJabatan()
        {
            InitializeComponent();
        }
        void onload()
        {
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
        }
        void Hidee()
        {
            panelForm.Hide();
            buttonTam.Show();
        }
        void Showw()
        {
            panelForm.Show();
            buttonTam.Hide();
        }
        void Clear()
        {
            textBox1.Text = "";
        }
        void loadgrid()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10, FontStyle.Bold);

            Color header = ColorTranslator.FromHtml("#1A120B");
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = header;
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;

            Color cell = ColorTranslator.FromHtml("#E5E5CB");
            dataGridView1.DefaultCellStyle.BackColor = cell;
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 2;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Name = "Jabatan";

            SqlCommand cmd = new SqlCommand("select * from Jabatan", Connection.conn);
            Connection.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string[] rows = { reader.GetInt32(0).ToString(), reader.GetString(1) };
                dataGridView1.Rows.Add(rows);
            }
            Connection.conn.Close();

            DataGridViewImageColumn img = new DataGridViewImageColumn();
            Image image = Image.FromFile("C:\\Users\\SMKN 1 BONDOWOSO\\source\\repos\\Presensi\\Asset\\edit.png");
            img.Image = image;
            img.HeaderText = "Update";
            dataGridView1.Columns.Add(img);

            DataGridViewImageColumn img1 = new DataGridViewImageColumn();
            Image image1 = Image.FromFile("C:\\Users\\SMKN 1 BONDOWOSO\\source\\repos\\Presensi\\Asset\\delete.png");
            img1.Image = image1;
            img1.HeaderText = "Delete";
            dataGridView1.Columns.Add(img1);
        }
        private void MasterJabatan_Load(object sender, EventArgs e)
        {
            onload();
            loadgrid();
            Hidee();
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Cells[1].Selected = false;
            }
        }
        bool validate()
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Form Wajib Diisi", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        int cond = 0;
        private void buttonTam_Click(object sender, EventArgs e)
        {
            Showw();
            Clear();
            cond = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(cond == 1)
            {
                if (validate())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("insert into Jabatan values(@jabatan)", Connection.conn);
                        cmd.Parameters.AddWithValue("@jabatan", textBox1.Text);
                        Connection.conn.Open();
                        cmd.ExecuteNonQuery();
                        Connection.conn.Close();

                        MessageBox.Show("Data Berhasil Di Tambah", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Hidee();
                        loadgrid();
                        cond = 0;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }

            if(cond == 2)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("update Jabatan set jabatan=@jabatan where id=" + id + "", Connection.conn);
                    cmd.Parameters.AddWithValue("@jabatan", textBox1.Text);
                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    MessageBox.Show("Data Berhasil Di Update", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Hidee();
                    loadgrid();
                    cond = 0;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        int id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 2)
            {
                cond = 2;
                id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                Showw();
            }

            if(e.ColumnIndex == 3)
            {
                DialogResult result = MessageBox.Show("Yakin Ingin Menghapus " + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    SqlCommand cmd = new SqlCommand("delete from Jabatan where id=" + dataGridView1.Rows[e.RowIndex].Cells[0].Value + "", Connection.conn);

                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    MessageBox.Show("Data Berhasil Di Hapus", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadgrid();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hidee();
            Clear();
        }
    }
}
