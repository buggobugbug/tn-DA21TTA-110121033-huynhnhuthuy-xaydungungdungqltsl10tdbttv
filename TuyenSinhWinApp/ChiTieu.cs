using DevExpress.Data.NetCompatibility.Extensions;
using DevExpress.XtraBars.Docking2010.DragEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuyenSinhServiceLib;
using TuyenSinhWinApp.TuyenSinhServiceReference;

namespace TuyenSinhWinApp
{
    public partial class ChiTieu : Form
    {
        private readonly Service1Client _service = new Service1Client();
        public ChiTieu()
        {
            InitializeComponent();
        }

        private void ChiTieu_Load(object sender, EventArgs e)
        {

            // Nạp danh sách đợt tuyển sinh vào combobox
            var dsDot = _service.LayDanhSachDotTuyen();
            cbDotTuyenSinh.DataSource = dsDot;
            cbDotTuyenSinh.DisplayMember = "TenDot";
            cbDotTuyenSinh.ValueMember = "MaDot";

            if (!string.IsNullOrEmpty(Common.MaDot))
                cbDotTuyenSinh.SelectedValue = Common.MaDot;

            NapDanhSachTrungTuyen();
        }


        private void NapDanhSachTrungTuyen()
        {
            var ds = _service.LayDanhSachTrungTuyen(Common.MaTruong, Common.MaDot);
            dgvDanhSachTrungTuyen.DataSource = null;
            dgvDanhSachTrungTuyen.DataSource = ds;
            FormatDanhSachTrungTuyenGrid();
        }


        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnLuuChiTieu_Click(object sender, EventArgs e)
        {
            int chiTieu = 0;
            if (!int.TryParse(txtChiTieu.Text, out chiTieu) || chiTieu < 0)
            {
                MessageBox.Show("Chỉ tiêu phải là số nguyên dương!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var maDot = cbDotTuyenSinh.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(maDot))
            {
                MessageBox.Show("Vui lòng chọn đợt tuyển sinh!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool kq = _service.CapNhatChiTieu(Common.MaTruong, maDot, chiTieu);
            if (kq)
                MessageBox.Show("Cập nhật chỉ tiêu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Cập nhật thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cbDotTuyenSinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            var maDot = cbDotTuyenSinh.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(maDot)) return;

            Common.MaDot = maDot; // Lưu lại cho đồng bộ các màn khác

            var chiTieu = _service.LayChiTieuTruong(Common.MaTruong, maDot);
            if (chiTieu != null)
                txtChiTieu.Text = chiTieu.ChiTieu.ToString();
            else
                txtChiTieu.Text = "0";
        }

        private void btnXetTrungTuyen_Click(object sender, EventArgs e)
        {
            bool kq = _service.XetTrungTuyen(Common.MaTruong, Common.MaDot);
            if (kq)
            {
                MessageBox.Show("Xét trúng tuyển thành công!");
                NapDanhSachTrungTuyen();
            }
            else
            {
                MessageBox.Show("Xét trúng tuyển thất bại!");
            }
        }

        private void btnXemTrungTuyen_Click(object sender, EventArgs e)
        {
            var dsTrungTuyen = _service.LayDanhSachTrungTuyen(Common.MaTruong, Common.MaDot);
            dgvDanhSachTrungTuyen.DataSource = dsTrungTuyen?.ToList();
            dgvDanhSachTrungTuyen.DataSource = null;
            dgvDanhSachTrungTuyen.DataSource = dsTrungTuyen;

            FormatDanhSachTrungTuyenGrid();
        }

        private void FormatDanhSachTrungTuyenGrid()
        {
            var dgv = dgvDanhSachTrungTuyen;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = Color.LightCyan;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.RowTemplate.Height = 28;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

            // Ẩn các cột không cần thiết
            if (dgv.Columns.Contains("MaHocSinh")) dgv.Columns["MaHocSinh"].Visible = false;

            // Đặt lại tiêu đề cho thân thiện
            var map = new Dictionary<string, string>
    {
        { "MaSoBaoDanh", "SBD" }, { "Ho", "Họ" }, { "Ten", "Tên" },
        { "NgaySinh", "Ngày sinh" }, { "GioiTinh", "Giới tính" }, { "DanToc", "Dân tộc" },
        { "NoiSinh", "Nơi sinh" }, { "TruongTHCS", "Trường THCS" }, { "PhongThi", "Phòng thi" },
        { "DiemToan", "Toán" }, { "DiemVan", "Văn" }, { "DiemAnh", "Anh" },
        { "DiemKhuyenKhich", "Khuyến khích" }, { "DiemUuTien", "Ưu tiên" }, { "DiemTong", "Tổng điểm" },
        { "TrangThai", "Trạng thái" }
    };
            foreach (var m in map)
                if (dgv.Columns.Contains(m.Key))
                    dgv.Columns[m.Key].HeaderText = m.Value;

            // Căn giữa cho điểm, ngày sinh
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.HeaderText.Contains("điểm", StringComparison.OrdinalIgnoreCase) || col.HeaderText.Contains("Ngày"))
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                else
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void txtTimTen_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
