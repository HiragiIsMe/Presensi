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
    public partial class EmployeeForm : Form
    {
        MasterEmployee form;
        public EmployeeForm(MasterEmployee f)
        {
            InitializeComponent();
            this.form = f;

            loadJabatan();
            loadJk();
        }
       
        void onload()
        {
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            this.CenterToScreen();
            textBox1.Visible = false;
            textBoxId.Visible = false;
        }
        void loadJabatan()
        {
            string query = "select * from Jabatan";
            comboBoxJabatan.DataSource = Connection.getdata(query);
            comboBoxJabatan.DisplayMember = "jabatan";
            comboBoxJabatan.ValueMember = "id";
        }

        void loadJk()
        {
            comboBoxJk.DataSource = new Dictionary<int, string>()
            {
                {1, "Laki-Laki" },
                {2, "Perempuan" }
            }.ToList();

            comboBoxJk.DisplayMember = "Value";
            comboBoxJk.ValueMember = "Key";
        }
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            onload();
        }
        bool validate()
        {
            if(textBoxNama.Text == "" || textBoxNohp.Text == "" || comboBoxJk.Text == "" || comboBoxJabatan.Text == "" || richTextBoxAlamat.Text == "" || pictureBox1.Image == null)
            {
                MessageBox.Show("Semua Form Dan Foto Wajib Diisi", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void buttonSav_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                if(textBox1.Text == "insert")
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("insert into Employee values(@nama,@jk,@nohp,@alamat,@id_jabatan,@foto)", Connection.conn);
                        cmd.Parameters.AddWithValue("@nama", textBoxNama.Text);
                        cmd.Parameters.AddWithValue("@jk", comboBoxJk.SelectedValue);
                        cmd.Parameters.AddWithValue("@nohp", textBoxNohp.Text);
                        cmd.Parameters.AddWithValue("@alamat", richTextBoxAlamat.Text);
                        cmd.Parameters.AddWithValue("@id_jabatan", comboBoxJabatan.SelectedValue);
                        cmd.Parameters.AddWithValue("@foto", Enc.Encode(pictureBox1.Image));

                        Connection.conn.Open();
                        cmd.ExecuteNonQuery();
                        Connection.conn.Close();

                        MessageBox.Show("Data Berhasil Di Tambah", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        form.loadgrid();
                        this.Close();
                    }
                    catch(Exception ex)
                    {
                        throw;
                    }
                }

                if(textBox1.Text == "update")
                {
                    SqlCommand cmd = new SqlCommand("update Employee set nama=@nama, jenis_kelamin=@jk, no_hp=@nohp, alamat=@alamat, id_jabatan=@id_jabatan, foto=@foto where id="+ textBoxId.Text +"", Connection.conn);
                    cmd.Parameters.AddWithValue("@nama", textBoxNama.Text);
                    cmd.Parameters.AddWithValue("@jk", comboBoxJk.SelectedValue);
                    cmd.Parameters.AddWithValue("@nohp", textBoxNohp.Text);
                    cmd.Parameters.AddWithValue("@alamat", richTextBoxAlamat.Text);
                    cmd.Parameters.AddWithValue("@id_jabatan", comboBoxJabatan.SelectedValue);
                    cmd.Parameters.AddWithValue("@foto", Enc.Encode(pictureBox1.Image));

                    Connection.conn.Open();
                    cmd.ExecuteNonQuery();
                    Connection.conn.Close();

                    MessageBox.Show("Data Berhasil Di Update", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    form.loadgrid();
                    this.Close();
                }
            }
        }

        private void textBoxNohp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonBrow_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Chose An Image(*.jpg; *.jpeg; *.png;)|*.jpg; *.jpeg; *.png;";

            if (open.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(open.FileName);
                Bitmap bmp = (Bitmap)img;

                pictureBox1.Image = bmp;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
