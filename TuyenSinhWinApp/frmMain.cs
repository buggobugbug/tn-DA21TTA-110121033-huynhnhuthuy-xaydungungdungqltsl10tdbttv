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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnQuanlyhocsinh_Click(object sender, EventArgs e)
        {
            frmHocsinh frm = new frmHocsinh();
            frm.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnXemHocSinh_Click(object sender, EventArgs e)
        {
            frmDanhSachHocSinh frm = new frmDanhSachHocSinh();
            frm.ShowDialog();
        }

        private void btnPhongThi_Click(object sender, EventArgs e)
        {
            frmPhongThi frm = new frmPhongThi();
            frm.ShowDialog(); // hoặc frm.Show(); nếu bạn muốn mở song song
        }
    }
}
