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

    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        public void loadform(object Form)
        {
            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(f);
            this.mainpanel.Tag = f;
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnQuanlyhocsinh_Click(object sender, EventArgs e)
        {
            
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnXemHocSinh_Click(object sender, EventArgs e)
        {
        }

        private void btnPhongThi_Click(object sender, EventArgs e)
        {
            
        }

        private void frmDottuyensinh_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadform(new frmDotTuyenSinh());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadform(new frmHocsinh());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadform(new frmPhongThi(this));
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDangNhap frm = new frmDangNhap();
            frm.ShowDialog();
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
