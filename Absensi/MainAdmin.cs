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
    public partial class MainAdmin : Form
    {
        public MainAdmin()
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
        }
        private void MainAdmin_Load(object sender, EventArgs e)
        {
            onload();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MasterEmployee employee = new MasterEmployee()
            {
                TopLevel = false,
                TopMost = true
            };
            panelMain.Controls.Clear();
            panelMain.Controls.Add(employee);
            employee.Show();
        }
    }
}
