using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuyenSinhWinApp.TuyenSinhServiceReference;

namespace TuyenSinhWinApp
{

    public partial class Frmadmin : Form
    {
        private Service1Client service;
        public Frmadmin()
        {
            InitializeComponent();
            service = new Service1Client();
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

        private void button6_Click(object sender, EventArgs e)
        {
            loadform(new frmAdminCapNhatDiemTheoPhongThi());
        }

        private void panelside_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mainpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadform(new frmQuanLyTaiKhoan());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadform(new frmAdminHocSinh());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadform(new frmQuanLyTruongHoc());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDangNhap frm = new frmDangNhap();
            frm.ShowDialog();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            loadform(new frmLoaiThongKe());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadform(new frmDotTuyenSinh());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            loadform(new ChiTieu());
        }
    }
}
