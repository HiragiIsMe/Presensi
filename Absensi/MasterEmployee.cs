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
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Absensi
{
    public partial class MasterEmployee : Form
    {
        public MasterEmployee()
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

        public void loadgrid() 
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
            dataGridView1.ColumnCount = 6;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Name = "Nama";
            dataGridView1.Columns[2].Name = "Jenis Kelamin";
            dataGridView1.Columns[3].Name = "No Handphone";
            dataGridView1.Columns[4].Name = "Alamat";
            dataGridView1.Columns[5].Name = "Jabatan";

            SqlCommand cmd = new SqlCommand("select Employee.id,nama,jenis_kelamin,no_hp,alamat,jabatan from Employee join Jabatan on Employee.id_jabatan = Jabatan.id",Connection.conn);
            Connection.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string jk;
                if(reader.GetInt32(2) == 1)
                {
                    jk = "Laki-Laki";
                }
                else
                {
                    jk = "Perempuan";
                }
                string[] rows = { reader.GetInt32(0).ToString(), reader.GetString(1), jk, reader.GetString(3), reader.GetString(4), reader.GetString(5) };
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
        private void MasterEmployee_Load(object sender, EventArgs e)
        {
            onload();
            loadgrid();
            if(dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Cells[1].Selected = false;
            }
        }
        private void buttonTam_Click(object sender, EventArgs e)
        {
            EmployeeForm form = new EmployeeForm(this);
            form.textBox1.Text = "insert";
            form.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 6)
            {
                EmployeeForm form = new EmployeeForm(this);
                form.textBox1.Text = "update";
                SqlCommand cmd = new SqlCommand("select *, Jabatan.id, Jabatan.jabatan from Employee join Jabatan on Employee.id_jabatan = Jabatan.id where Employee.id = "+ dataGridView1.Rows[e.RowIndex].Cells[0].Value +"", Connection.conn);
                Connection.conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                form.textBoxId.Text = reader.GetInt32(0).ToString();
                form.textBoxNama.Text = reader.GetString(1);
                form.comboBoxJk.SelectedValue = reader.GetInt32(2);
                form.comboBoxJk.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                form.textBoxNohp.Text = reader.GetString(3);
                form.richTextBoxAlamat.Text = reader.GetString(4);
                form.comboBoxJabatan.SelectedValue = reader.GetInt32(5);
                form.comboBoxJabatan.Text = reader.GetString(8);

                MemoryStream memoryStream = new MemoryStream((byte[])reader["foto"]);
                form.pictureBox1.Image = Image.FromStream(memoryStream);
                form.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                Connection.conn.Close();

                form.ShowDialog();
            }

            if (e.ColumnIndex == 7)
            {
                DialogResult result = MessageBox.Show("Yakin Ingin Menghapus "+ dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if(result == DialogResult.OK)
                {
                    SqlCommand cmd = new SqlCommand("delete from Employee where id=" + dataGridView1.Rows[e.RowIndex].Cells[0].Value + "", Connection.conn);

                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    MessageBox.Show("Data Berhasil Di Hapus", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadgrid();
                }
            }
        }
    }
}
