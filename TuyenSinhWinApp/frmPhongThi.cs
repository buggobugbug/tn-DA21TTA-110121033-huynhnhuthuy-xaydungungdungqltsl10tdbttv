using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TuyenSinhServiceLib;
using TuyenSinhWinApp.TuyenSinhServiceReference;

namespace TuyenSinhWinApp
{
    public partial class frmPhongThi : Form
    {
        private readonly Service1Client _service = new Service1Client();

        public frmPhongThi()
        {
            InitializeComponent();
        }

        private void frmPhongThi_Load(object sender, EventArgs e)
        {
            NapDanhSachPhongThi();
        }

        private void NapDanhSachPhongThi()
        {
            try
            {
                var ds = _service.LayDanhSachPhongThi(Common.MaTruong);
                dgvPhongThi.DataSource = ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải phòng thi: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            NapDanhSachPhongThi();
        }

        private void frmPhongThi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_service.State == System.ServiceModel.CommunicationState.Opened)
                _service.Close();
        }

        private void btnXemHocSinh_Click(object sender, EventArgs e)
        {
            if (dgvPhongThi.CurrentRow?.DataBoundItem is PhongThi phong)
            {
                string maPhongThi = phong.MaPhongThi;

                if (string.IsNullOrEmpty(maPhongThi))
                {
                    MessageBox.Show("Không thể xác định mã phòng thi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var frm = new frmHocSinhTrongPhong(maPhongThi);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng thi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
