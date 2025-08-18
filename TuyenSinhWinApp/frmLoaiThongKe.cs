using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TuyenSinhWinApp
{
    public partial class frmLoaiThongKe : Form
    {
        public frmLoaiThongKe()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            try
            {
                loadform(new frmThongKe());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public void loadform(object Form)
        {
            if (this.mainpanel1.Controls.Count > 0)
                this.mainpanel1.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainpanel1.Controls.Add(f);
            this.mainpanel1.Tag = f;
            f.Show();
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                loadform(new frmThongKeTHCS());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
