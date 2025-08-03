using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TuyenSinhServiceLib;
using TuyenSinhWinApp.TuyenSinhServiceReference;

namespace TuyenSinhWinApp
{
    public partial class frmPhongThi : Form
    {
        private readonly Service1Client _service = new Service1Client();
        private Form _formMain;

        public frmPhongThi(Form formMain)
        {
            InitializeComponent();
            _formMain = formMain;
        }

        private void frmPhongThi_Load(object sender, EventArgs e)
        {
            NapDanhSachPhongThi();
            NapDanhSachDotTuyen();
        }

        private void NapDanhSachDotTuyen()
        {
            var dsDot = _service.LayDanhSachDotTuyen();
            cbDotTuyenSinh.DataSource = dsDot;
            cbDotTuyenSinh.DisplayMember = "TenDot";
            cbDotTuyenSinh.ValueMember = "MaDot";
            if (dsDot.Length > 0)
            {
                cbDotTuyenSinh.SelectedValue = Common.MaDot; // Nếu có sẵn
            }
        }

        // Xử lý phần tạo click chọn giám thị
        private void dgvPhongThi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPhongThi.Rows[e.RowIndex];
                txtGiamThi1.Text = row.Cells["GiamThi1"].Value?.ToString();
                txtGiamThi2.Text = row.Cells["GiamThi2"].Value?.ToString();

            }
        }
        private void NapDanhSachPhongThi()
        {
            try
            {
                var maDot = Common.MaDot;
                var ds = _service.LayDanhSachPhongThi(Common.MaTruong, maDot);
                dgvPhongThi.DataSource = ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải phòng thi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string maDot = phong.MaDot;

                if (string.IsNullOrEmpty(maPhongThi))
                {
                    MessageBox.Show("Không thể xác định mã phòng thi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy danh sách học sinh theo phòng thi
                var dsHocSinh = _service.LayDanhSachHocSinhTheoPhong(Common.MaTruong, Common.MaDot, maPhongThi);

                // Chuẩn bị bảng dữ liệu (giống mẫu bạn cần)
                var table = new DataTable();
                table.Columns.Add("MaHocSinh");
                table.Columns.Add("SBD");           // MaSoBaoDanh
                table.Columns.Add("Họ và tên");     // Ho + " " + Ten
                table.Columns.Add("Ngày sinh");     // NgaySinh (dd/MM/yyyy)
                table.Columns.Add("Tên trường THCS"); // TruongTHCS
                table.Columns.Add("Điểm Toán", typeof(decimal));
                table.Columns.Add("Điểm Văn", typeof(decimal));
                table.Columns.Add("Điểm Anh", typeof(decimal));
                table.Columns.Add("Điểm Khuyến Khích", typeof(decimal));
                table.Columns.Add("Điểm Ưu Tiên", typeof(decimal));

                foreach (var hs in dsHocSinh)
                {
                    table.Rows.Add(
                        hs.MaHocSinh,
                        hs.MaSoBaoDanh,
                        $"{hs.Ho} {hs.Ten}",
                        hs.NgaySinh.ToString("dd/MM/yyyy"),
                        hs.TruongTHCS,
                        hs.DiemToan,    // new column
                        hs.DiemVan,
                        hs.DiemAnh,
                        hs.DiemKhuyenKhich,
                        hs.DiemUuTien
                    );
                }
                foreach (DataGridViewColumn col in dgvHocSinhTrongPhong.Columns)
                {
                    if (col.HeaderText.Contains("Điểm"))
                        col.ReadOnly = false;
                    else
                        col.ReadOnly = true;
                }
                // Hiển thị lên grid bên phải
                dgvHocSinhTrongPhong.DataSource = table;
                dgvHocSinhTrongPhong.Columns["MaHocSinh"].Visible = false;
                dgvHocSinhTrongPhong.Columns["SBD"].HeaderText = "SBD";
                dgvHocSinhTrongPhong.Columns["Họ và tên"].HeaderText = "Họ và tên";
                dgvHocSinhTrongPhong.Columns["Ngày sinh"].HeaderText = "Ngày sinh";
                dgvHocSinhTrongPhong.Columns["Tên trường THCS"].HeaderText = "Tên trường THCS";
                dgvHocSinhTrongPhong.Columns["Điểm Toán"].HeaderText = "Điểm toán";
                dgvHocSinhTrongPhong.Columns["Điểm Văn"].HeaderText = "Điểm văn";
                dgvHocSinhTrongPhong.Columns["Điểm Anh"].HeaderText = "Điểm môn 3";
                dgvHocSinhTrongPhong.Columns["Điểm Khuyến Khích"].HeaderText = "Điểm Khuyến Khích";
                dgvHocSinhTrongPhong.Columns["Điểm Ưu Tiên"].HeaderText = "Điểm Ưu Tiên";
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng thi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dgvHocSinhTrongPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHocSinhTrongPhong.AllowUserToAddRows = false;
            dgvHocSinhTrongPhong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvHocSinhTrongPhong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvHocSinhTrongPhong.DefaultCellStyle.Font = new Font("Segoe UI", 10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvPhongThi.CurrentRow?.DataBoundItem is PhongThi phong)
            {
                string maPhongThi = phong.MaPhongThi;
                string giamThi1 = txtGiamThi1.Text.Trim();
                string giamThi2 = txtGiamThi2.Text.Trim();

                if (string.IsNullOrEmpty(maPhongThi))
                {
                    MessageBox.Show("Không thể xác định mã phòng thi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    _service.CapNhatGiamThi(maPhongThi, giamThi1, giamThi2);
                    MessageBox.Show("Cập nhật giám thị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NapDanhSachPhongThi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật giám thị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng thi để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string maTruong = Common.MaTruong;
            string maDot = Common.MaDot;

            MessageBox.Show($"DEBUG: MaTruong={maTruong}, MaDot={maDot}");

            if (string.IsNullOrWhiteSpace(maDot))
            {
                MessageBox.Show("Không tìm thấy mã đợt tuyển sinh. Vui lòng chọn hoặc cấu hình lại.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận trước khi chia
            if (MessageBox.Show("Bạn có chắc chắn muốn chia lại phòng thi cho tất cả thí sinh chưa được chia?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                bool thanhCong = _service.ChiaPhongThi(maTruong, maDot);
                if (thanhCong)
                {
                    MessageBox.Show("Đã chia phòng thi thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NapDanhSachPhongThi();
                }
                else
                {
                    MessageBox.Show("Chia phòng thi thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chia phòng thi: " + ex.Message + Environment.NewLine + ex.StackTrace, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGiamThi1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnXuatDanhSach_Click(object sender, EventArgs e)
        {
            if (dgvPhongThi.CurrentRow?.DataBoundItem is PhongThi phong)
            {
                string maPhongThi = phong.MaPhongThi;
                string tenPhong = phong.MaPhongThi;
                string tenTruong = Common.TenTruong; 
                string diemCoiThi = tenTruong;
                string tenDot = Common.MaDot;

                // Lấy danh sách học sinh theo phòng
                var dsHocSinh = _service.LayDanhSachHocSinhTheoPhong(phong.MaTruong, phong.MaDot, phong.MaPhongThi);

                if (dsHocSinh == null || dsHocSinh.Length == 0)
                {
                    MessageBox.Show("Phòng này chưa có học sinh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog()
                {
                    Filter = "Excel Workbook|*.xlsx",
                    FileName = $"DanhSachHocSinh_Phong_{tenPhong}_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
                })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (var package = new ExcelPackage())
                        {
                            var ws = package.Workbook.Worksheets.Add("DanhSachHocSinh");

                            // ===== Header thông tin trường & kỳ thi
                            ws.Cells["A1:F1"].Merge = true;
                            ws.Cells["A1"].Value = "Sở GD & ĐT Trà Vinh";
                            ws.Cells["A1"].Style.Font.Bold = true;

                            ws.Cells["A2:F2"].Merge = true;
                            ws.Cells["A2"].Value = "Điểm coi thi: " + diemCoiThi;

                            ws.Cells["A3:F3"].Merge = true;
                            ws.Cells["A3"].Value = "Kỳ Thi Tuyển Sinh Lớp 10";

                            ws.Cells["A4:F4"].Merge = true;
                            ws.Cells["A4"].Value = $"Khóa ngày: {DateTime.Now:dd/MM/yyyy}";

                            ws.Cells["A6:F6"].Merge = true;
                            ws.Cells["A6"].Value = "DANH SÁCH THÍ SINH TRONG PHÒNG THI";
                            ws.Cells["A6"].Style.Font.Size = 14;
                            ws.Cells["A6"].Style.Font.Bold = true;
                            ws.Cells["A6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            ws.Cells["E7:F7"].Merge = true;
                            ws.Cells["E7"].Value = $"Phòng thi số: {tenPhong}";
                            ws.Cells["E7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                            // ===== Header bảng
                            int startRow = 9;
                            ws.Cells[startRow, 1].Value = "STT";
                            ws.Cells[startRow, 2].Value = "SBD";
                            ws.Cells[startRow, 3].Value = "Họ và tên";
                            ws.Cells[startRow, 4].Value = "Ngày sinh";
                            ws.Cells[startRow, 5].Value = "Tên trường THCS";
                            ws.Cells[startRow, 6].Value = "Ghi chú";
                            ws.Row(startRow).Style.Font.Bold = true;
                            ws.Row(startRow).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            // ===== Dữ liệu
                            int row = startRow + 1;
                            int stt = 1;
                            foreach (var hs in dsHocSinh)
                            {
                                ws.Cells[row, 1].Value = stt++;
                                ws.Cells[row, 2].Value = hs.MaSoBaoDanh;
                                ws.Cells[row, 3].Value = hs.Ho + " " + hs.Ten;
                                ws.Cells[row, 4].Value = hs.NgaySinh.ToString("dd/MM/yyyy");
                                ws.Cells[row, 5].Value = hs.TruongTHCS;
                                ws.Cells[row, 6].Value = hs.GhiChu ?? "";
                                row++;
                            }

                            // ==== Định dạng
                            ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            ws.Cells[$"A{startRow}:F{row - 1}"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                            ws.Cells[$"A{startRow}:F{row - 1}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            ws.Cells[$"A{startRow}:F{row - 1}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            ws.Cells[$"A{startRow}:F{row - 1}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws.Cells[$"A{startRow}:F{row - 1}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                            package.SaveAs(new FileInfo(sfd.FileName));
                        }

                        MessageBox.Show("Xuất danh sách học sinh thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn phòng thi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCapNhatDiem_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in dgvHocSinhTrongPhong.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["MaHocSinh"].Value == null) continue;

                int maHS = Convert.ToInt32(row.Cells["MaHocSinh"].Value);
                decimal? diemToan = TryParseDecimal(row.Cells["Điểm Toán"].Value);
                decimal? diemVan = TryParseDecimal(row.Cells["Điểm Văn"].Value);
                decimal? diemAnh = TryParseDecimal(row.Cells["Điểm Anh"].Value);
                decimal? diemKK = TryParseDecimal(row.Cells["Điểm khuyến khích"].Value);
                decimal? diemUT = TryParseDecimal(row.Cells["Điểm ưu tiên"].Value);

                // Gọi WCF Service Reference:
                if (_service.CapNhatDiemHocSinh(maHS, diemToan, diemVan, diemAnh, diemKK, diemUT))
                    count++;
            }
            MessageBox.Show($"Đã cập nhật điểm học sinh", "Cập nhật thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private decimal? TryParseDecimal(object val)
{
    decimal temp;
    if (val == null || string.IsNullOrWhiteSpace(val.ToString())) return null;
    return decimal.TryParse(val.ToString(), out temp) ? temp : (decimal?)null;
}

        private void dgvHocSinhTrongPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
    }
}
