using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Absensi
{
    public partial class MasterPresensi : Form
    {
        public MasterPresensi()
        {
            InitializeComponent();
        }
        void onload()
        {
            ControlBox = false;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.None;
        }
        void loadComboBox()
        {
            comboBox1.DataSource = new Dictionary<int, string>()
            {
                {1,"Semua"},
                {2,"Hari Ini"},
            }.ToList();
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
        }
        void Datagrid()
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
            dataGridView1.RowHeadersVisible = false; 

            string query = "select Employee.nama, Presensi.jam_masuk, Presensi.jam_keluar, Presensi.tanggal from Presensi join Employee on Presensi.id_Employee = Employee.id";
            dataGridView1.DataSource = Connection.getdata(query);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void MasterPresensi_Load(object sender, EventArgs e)
        {
            onload();
            Datagrid();
            loadComboBox();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }
    }
}
