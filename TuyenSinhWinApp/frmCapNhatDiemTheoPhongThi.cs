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
    public partial class frmCapNhatDiemTheoPhongThi : Form
    {

        private readonly Service1Client _service = new Service1Client();
        private Form _formMain;
        public frmCapNhatDiemTheoPhongThi()
        {
            InitializeComponent();
            _formMain = new Form();
            cboMonHoc.Items.Clear();
            cboMonHoc.Items.AddRange(new string[] { "Toán", "Văn", "Anh", "Điểm Khuyến Khích", "Điểm Ưu Tiên" });
            cboMonHoc.SelectedIndex = 0;

            this.Load += frmCapNhatDiemTheoPhongThi_Load;
            cbDotTuyenSinh.SelectedIndexChanged += cbDotTuyenSinh_SelectedIndexChanged;
            cboPhongThi.SelectedIndexChanged += cboPhongThi_SelectedIndexChanged;
            cboMonHoc.SelectedIndexChanged += cboMonHoc_SelectedIndexChanged;
            this.Load += frmCapNhatDiemTheoPhongThi_Load;
        }

        private void frmCapNhatDiemTheoPhongThi_Load(object sender, EventArgs e)
        {
            NapDanhSachDotTuyen();
        }

        private void NapDanhSachDotTuyen()
        {
            var dsDot = _service.LayDanhSachDotTuyen();
            if (dsDot == null || dsDot.Length == 0)
            {
                cbDotTuyenSinh.DataSource = null;
                return;
            }

            cbDotTuyenSinh.DataSource = dsDot;
            cbDotTuyenSinh.DisplayMember = "TenDot";
            cbDotTuyenSinh.ValueMember = "MaDot";
            if (!string.IsNullOrEmpty(Common.MaDot))
                cbDotTuyenSinh.SelectedValue = Common.MaDot;
        }


        private void btnXemHocSinh_Click(object sender, EventArgs e)
        {

        }

        private void cbDotTuyenSinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDotTuyenSinh.SelectedValue != null)
            {
                Common.MaDot = cbDotTuyenSinh.SelectedValue.ToString();
                NapDanhSachPhongThi();
            }
        }


        private void NapDanhSachPhongThi()
        {
            var ds = _service.LayDanhSachPhongThi(Common.MaTruong, Common.MaDot);
            if (ds == null || ds.Length == 0)
            {
                cboPhongThi.DataSource = null;
                return;
            }

            cboPhongThi.DataSource = ds;
            cboPhongThi.DisplayMember = "MaPhongThi";
            cboPhongThi.ValueMember = "MaPhongThi";
            cboPhongThi.SelectedIndex = 0;
        }

        private void cboPhongThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            NapDanhSachHocSinh();
        }

        private void cboMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            NapDanhSachHocSinh();
        }

        private void NapDanhSachHocSinh()
        {
            try
            {
                // Kiểm tra chọn phòng thi và môn học đã hợp lệ chưa
                if (cboPhongThi.SelectedValue == null || cboMonHoc.SelectedItem == null)
                {
                    dgvDanhSachHocSinh.DataSource = null;
                    return;
                }

                string maPhongThi = cboPhongThi.SelectedValue.ToString();
                string mon = cboMonHoc.SelectedItem.ToString();

                // Lấy danh sách học sinh từ Service
                var dsHocSinh = _service.LayDanhSachHocSinhTheoPhong(Common.MaTruong, Common.MaDot, maPhongThi);

                // Tạo bảng dữ liệu
                var dt = new DataTable();
                dt.Columns.Add("MaHocSinh", typeof(int));
                dt.Columns.Add("SBD");
                dt.Columns.Add("Họ và tên");
                dt.Columns.Add("Ngày sinh");
                dt.Columns.Add("Tên trường THCS");
                dt.Columns.Add("Điểm", typeof(decimal));

                foreach (var hs in dsHocSinh)
                {
                    object diem = DBNull.Value;
                    switch (mon)
                    {
                        case "Toán": diem = hs.DiemToan ?? (object)DBNull.Value; break;
                        case "Văn": diem = hs.DiemVan ?? (object)DBNull.Value; break;
                        case "Anh": diem = hs.DiemAnh ?? (object)DBNull.Value; break;
                        case "Điểm Khuyến Khích": diem = hs.DiemKhuyenKhich ?? (object)DBNull.Value; break;
                        case "Điểm Ưu Tiên": diem = hs.DiemUuTien ?? (object)DBNull.Value; break;
                        default: diem = DBNull.Value; break;
                    }
                    dt.Rows.Add(
                        hs.MaHocSinh,
                        hs.MaSoBaoDanh,
                        $"{hs.Ho} {hs.Ten}",
                        hs.NgaySinh.ToString("dd/MM/yyyy"),
                        hs.TruongTHCS,
                        diem
                    );
                }

                // Gán nguồn dữ liệu cho DataGridView
                dgvDanhSachHocSinh.DataSource = dt;

                // Ẩn cột mã học sinh
                if (dgvDanhSachHocSinh.Columns["MaHocSinh"] != null)
                    dgvDanhSachHocSinh.Columns["MaHocSinh"].Visible = false;

                // Căn giữa header, font chuẩn
                dgvDanhSachHocSinh.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhSachHocSinh.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvDanhSachHocSinh.DefaultCellStyle.Font = new Font("Segoe UI", 10);

                // Chỉ cho phép nhập cột điểm, còn lại không cho sửa
                foreach (DataGridViewColumn col in dgvDanhSachHocSinh.Columns)
                {
                    col.ReadOnly = col.Name != "Điểm";
                    col.DefaultCellStyle.Alignment = col.Name == "Điểm" ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft;
                }

                // Đặt lại tiêu đề cho cột điểm
                dgvDanhSachHocSinh.Columns["Điểm"].HeaderText = $"Điểm {mon}";

                // Auto fit columns
                dgvDanhSachHocSinh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách học sinh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvDanhSachHocSinh.DataSource = null;
            }
        }


        private void btnLuuDiem_Click(object sender, EventArgs e)
        {
            int count = 0;
            string mon = cboMonHoc.SelectedItem?.ToString();
            foreach (DataGridViewRow row in dgvDanhSachHocSinh.Rows)
            {
                if (row.IsNewRow) continue;

                int maHS = Convert.ToInt32(row.Cells["MaHocSinh"].Value);
                decimal? diem = TryParseDecimal(row.Cells["Điểm"].Value);

                // Lấy điểm cũ nếu null (không cập nhật)
                var hsGoc = _service.LayDanhSachHocSinhTheoPhong(Common.MaTruong, Common.MaDot, cboPhongThi.SelectedValue.ToString())
                .FirstOrDefault(h => h.MaHocSinh == maHS);

                decimal? diemToan = hsGoc.DiemToan;
                decimal? diemVan = hsGoc.DiemVan;
                decimal? diemAnh = hsGoc.DiemAnh;
                decimal? diemKhuyenKhich = hsGoc.DiemKhuyenKhich;
                decimal? diemUuTien = hsGoc.DiemUuTien;

                switch (mon)
                {
                    case "Toán": diemToan = diem; break;
                    case "Văn": diemVan = diem; break;
                    case "Anh": diemAnh = diem; break;
                    case "Điểm Khuyến Khích": diemKhuyenKhich = diem; break;
                    case "Điểm Ưu Tiên": diemUuTien = diem; break;
                }

                // Gọi cập nhật điểm
                if (_service.CapNhatDiemHocSinh(maHS, diemToan, diemVan, diemAnh, diemKhuyenKhich, diemUuTien))
                    count++;
            }
            MessageBox.Show($"Đã lưu điểm cho {count} học sinh!", "Cập nhật thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NapDanhSachHocSinh();
        }

        private decimal? TryParseDecimal(object val)
        {
            decimal temp;
            if (val == null || string.IsNullOrWhiteSpace(val.ToString())) return null;
            return decimal.TryParse(val.ToString(), out temp) ? temp : (decimal?)null;
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
