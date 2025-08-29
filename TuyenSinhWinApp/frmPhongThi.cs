using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Globalization;
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
            dgvPhongThi.CellFormatting += dgvPhongThi_CellFormatting;
        }

        private void frmPhongThi_Load(object sender, EventArgs e)
        {
            NapDanhSachPhongThi();
            NapDanhSachDotTuyen();
            Guard.DisableForThuKy(
    button1
);
            Guard.ReadOnlyGridForThuKy(dgvHocSinhTrongPhong);
        }

        private void NapDanhSachDotTuyen()
        {
            var dsDot = _service.LayDanhSachDotTuyen();
            cbDotTuyenSinh.DataSource = dsDot;
            cbDotTuyenSinh.DisplayMember = "TenDot";
            cbDotTuyenSinh.ValueMember = "MaDot";
            if (dsDot != null && dsDot.Length > 0)
            {
                cbDotTuyenSinh.SelectedValue = Common.MaDot;
            }
        }

        // Ẩn hậu tố sau dấu '_' khi hiển thị mã phòng
        private void dgvPhongThi_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPhongThi.Columns[e.ColumnIndex].Name == "MaPhongThi" && e.Value != null)
            {
                var v = e.Value.ToString();
                if (v.Contains("_")) e.Value = v.Split('_')[0];
            }
        }

        private void dgvPhongThi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // chừa chỗ nếu cần xử lý chọn hàng
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
                MessageBox.Show("Lỗi khi tải phòng thi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dgvPhongThi.CurrentRow != null && dgvPhongThi.CurrentRow.DataBoundItem is PhongThi)
            {
                var phong = (PhongThi)dgvPhongThi.CurrentRow.DataBoundItem;
                string maPhongThi = phong.MaPhongThi;

                if (string.IsNullOrEmpty(maPhongThi))
                {
                    MessageBox.Show("Không thể xác định mã phòng thi.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var dsHocSinh = _service.LayDanhSachHocSinhTheoPhong(Common.MaTruong, Common.MaDot, maPhongThi);

                var table = new DataTable();
                table.Columns.Add("MaHocSinh");
                table.Columns.Add("SBD");
                table.Columns.Add("Họ và tên");
                table.Columns.Add("Ngày sinh");
                table.Columns.Add("Tên trường THCS");
                table.Columns.Add("Điểm Toán", typeof(string));   // FIX: cho phép "Vắng" / chuỗi
                table.Columns.Add("Điểm Văn", typeof(string));    // FIX
                table.Columns.Add("Điểm Anh", typeof(string));    // FIX
                table.Columns.Add("Điểm Khuyến Khích", typeof(decimal));
                table.Columns.Add("Điểm Ưu Tiên", typeof(decimal));
                foreach (var hs in dsHocSinh)
                {
                    table.Rows.Add(
                        hs.MaHocSinh,
                        hs.MaSoBaoDanh,
                        (hs.Ho ?? "") + " " + (hs.Ten ?? ""),
                        hs.NgaySinh.ToString("dd/MM/yyyy"),
                        hs.TruongTHCS,
                        hs.DiemToan,
                        hs.DiemVan,
                        hs.DiemAnh,
                        hs.DiemKhuyenKhich,
                        hs.DiemUuTien
                    );
                }

                dgvHocSinhTrongPhong.DataSource = table;
                dgvHocSinhTrongPhong.Columns["MaHocSinh"].Visible = false;

                foreach (DataGridViewColumn col in dgvHocSinhTrongPhong.Columns)
                {
                    if (col.HeaderText != null && col.HeaderText.Contains("Điểm"))
                        col.ReadOnly = false;
                    else
                        col.ReadOnly = true;
                }

                dgvHocSinhTrongPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvHocSinhTrongPhong.AllowUserToAddRows = false;
                dgvHocSinhTrongPhong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvHocSinhTrongPhong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvHocSinhTrongPhong.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng thi.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }

        private void btnHuy_Click(object sender, EventArgs e) { }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!Guard.DemandEdit(this)) return;
            string maTruong = Common.MaTruong;
            string maDot = Common.MaDot;

            if (string.IsNullOrWhiteSpace(maDot))
            {
                MessageBox.Show("Không tìm thấy mã đợt tuyển sinh. Vui lòng chọn hoặc cấu hình lại.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn chia lại phòng thi cho tất cả thí sinh chưa được chia?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                bool ok = _service.ChiaPhongThi(maTruong, maDot);
                if (ok)
                {
                    MessageBox.Show("Đã chia phòng thi thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NapDanhSachPhongThi();
                }
                else
                {
                    MessageBox.Show("Chia phòng thi thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chia phòng thi: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGiamThi1_TextChanged(object sender, EventArgs e) { }

        // ================== XUẤT DANH SÁCH THEO MẪU ==================
        private void btnXuatDanhSach_Click(object sender, EventArgs e)
        {
            var phong = (dgvPhongThi.CurrentRow != null)
                ? dgvPhongThi.CurrentRow.DataBoundItem as PhongThi
                : null;

            if (phong == null)
            {
                MessageBox.Show("Bạn chưa chọn phòng thi!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhongThiGoc = phong.MaPhongThi ?? "";
            string hienThiMaPhong = maPhongThiGoc.Contains("_") ? maPhongThiGoc.Split('_')[0] : maPhongThiGoc;

            string tenTruong = _service.GetTenTruongFromMaTruong(phong.MaTruong);
            if (string.IsNullOrEmpty(tenTruong)) tenTruong = Common.TenTruong ?? "Điểm coi thi";

            var dot = _service.LayDanhSachDotTuyen();
            DateTime khoaNgay = DateTime.Today;
            if (dot != null)
            {
                var d = dot.FirstOrDefault(x => x.MaDot == phong.MaDot);
                if (d != null && d.NgayBatDau.HasValue) khoaNgay = d.NgayBatDau.Value;
            }

            var dsHocSinh = _service.LayDanhSachHocSinhTheoPhong(phong.MaTruong, phong.MaDot, phong.MaPhongThi);
            if (dsHocSinh == null || dsHocSinh.Length == 0)
            {
                MessageBox.Show("Phòng này chưa có học sinh.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = "DanhSachHocSinh_Phong_" + hienThiMaPhong + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx",
                OverwritePrompt = true
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var pkg = new ExcelPackage())
                {
                    var ws = pkg.Workbook.Worksheets.Add("DanhSachPhong");

                    // ===== CÀI ĐẶT CHUNG
                    ws.Cells.Style.Font.Name = "Times New Roman";
                    ws.Cells.Style.Font.Size = 12;
                    ws.PrinterSettings.Orientation = eOrientation.Portrait;
                    ws.PrinterSettings.TopMargin = 0.5m;
                    ws.PrinterSettings.BottomMargin = 0.5m;
                    ws.PrinterSettings.LeftMargin = 0.5m;
                    ws.PrinterSettings.RightMargin = 0.5m;

                    // ===== PHẦN ĐẦU TRANG
                    ws.Cells["A1:J1"].Merge = true;
                    ws.Cells["A1"].Value = "Sở GD & ĐT Trà Vinh";
                    ws.Cells["A1"].Style.Font.Bold = true;

                    ws.Cells["A2:J2"].Merge = true;
                    ws.Cells["A2"].Value = "Điểm coi thi: " + tenTruong;

                    ws.Cells["A3:J3"].Merge = true;
                    ws.Cells["A3"].Value = "Kỳ Thi Tuyển Sinh Lớp 10";

                    ws.Cells["A4:J4"].Merge = true;
                    ws.Cells["A4"].Value = "Khóa ngày: " + khoaNgay.ToString("dd/MM/yyyy");

                    ws.Cells["A6:J6"].Merge = true;
                    ws.Cells["A6"].Value = "DANH SÁCH THÍ SINH TRONG PHÒNG THI";
                    ws.Cells["A6"].Style.Font.Size = 14;
                    ws.Cells["A6"].Style.Font.Bold = true;
                    ws.Cells["A6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    ws.Cells["A7:C7"].Merge = true;
                    ws.Cells["A7"].Value = "Phòng thi số: " + hienThiMaPhong;

                    // ===== HEADER BẢNG (gộp 2 hàng) =====
                    int headerRow = 9;

                    ws.Cells[headerRow, 1].Value = "STT";
                    ws.Cells[headerRow, 2].Value = "SBD";
                    ws.Cells[headerRow, 3].Value = "Họ và tên";  // phủ 2 cột (C-D)
                    ws.Cells[headerRow, 5].Value = "Ngày sinh";
                    ws.Cells[headerRow, 6].Value = "Tên trường THCS";
                    ws.Cells[headerRow, 7].Value = "Mã đề";
                    ws.Cells[headerRow, 8].Value = "Số tờ";
                    ws.Cells[headerRow, 9].Value = "Ký nộp";
                    ws.Cells[headerRow, 10].Value = "Ghi chú";

                    // Gộp 2 hàng cho toàn bộ tiêu đề
                    ws.Cells[headerRow, 1, headerRow + 1, 1].Merge = true;   // STT
                    ws.Cells[headerRow, 2, headerRow + 1, 2].Merge = true;   // SBD
                    ws.Cells[headerRow, 3, headerRow + 1, 4].Merge = true;   // Họ và tên (2 cột)
                    ws.Cells[headerRow, 5, headerRow + 1, 5].Merge = true;   // Ngày sinh
                    ws.Cells[headerRow, 6, headerRow + 1, 6].Merge = true;   // THCS
                    ws.Cells[headerRow, 7, headerRow + 1, 7].Merge = true;   // Mã đề
                    ws.Cells[headerRow, 8, headerRow + 1, 8].Merge = true;   // Số tờ
                    ws.Cells[headerRow, 9, headerRow + 1, 9].Merge = true;   // Ký nộp
                    ws.Cells[headerRow, 10, headerRow + 1, 10].Merge = true;   // Ghi chú

                    // Định dạng header
                    using (var rng = ws.Cells[headerRow, 1, headerRow + 1, 10])
                    {
                        rng.Style.Font.Bold = true;
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }

                    // ===== ĐỔ DỮ LIỆU
                    int dataStart = headerRow + 2;
                    int r = dataStart;
                    int stt = 1;

                    foreach (var hs in dsHocSinh)
                    {
                        ws.Cells[r, 1].Value = stt++;
                        ws.Cells[r, 2].Value = hs.MaSoBaoDanh;
                        ws.Cells[r, 3].Value = hs.Ho ?? "";
                        ws.Cells[r, 4].Value = hs.Ten ?? "";
                        ws.Cells[r, 5].Value = hs.NgaySinh.ToString("dd/MM/yyyy");
                        ws.Cells[r, 6].Value = hs.TruongTHCS;

                        ws.Cells[r, 7].Value = "";              // Mã đề (điền sau nếu có)
                        ws.Cells[r, 8].Value = "";              // Số tờ
                        ws.Cells[r, 9].Value = "";              // Ký nộp
                        ws.Cells[r, 10].Value = hs.GhiChu ?? ""; // Ghi chú

                        r++;
                    }
                    int lastDataRow = r - 1;

                    // ===== ĐỘ RỘNG CỘT & CANH LỀ
                    ws.Column(1).Width = 5.5;   // STT
                    ws.Column(2).Width = 9.5;   // SBD
                    ws.Column(3).Width = 22;    // Họ
                    ws.Column(4).Width = 12;    // Tên
                    ws.Column(5).Width = 13;    // Ngày sinh
                    ws.Column(6).Width = 28;    // THCS
                    ws.Column(7).Width = 9;     // Mã đề
                    ws.Column(8).Width = 8;     // Số tờ
                    ws.Column(9).Width = 10;    // Ký nộp
                    ws.Column(10).Width = 16;   // Ghi chú

                    ws.Cells[dataStart, 1, lastDataRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // STT
                    ws.Cells[dataStart, 2, lastDataRow, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // SBD
                    ws.Cells[dataStart, 5, lastDataRow, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Ngày sinh
                    ws.Cells[dataStart, 7, lastDataRow, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Mã đề/Số tờ/Ký nộp

                    // ===== VIỀN MẢNH TOÀN BẢNG (kể cả 2 hàng header)
                    using (var rng = ws.Cells[headerRow, 1, lastDataRow, 10])
                    {
                        rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }

                    // ===== PHẦN CUỐI TRANG
                    int footTop = lastDataRow + 2;

                    ws.Cells[footTop, 1].Value = "Danh sách này gồm có " + dsHocSinh.Length + " thí sinh dự thi";
                    ws.Cells[footTop + 1, 1].Value = "Tổng số học sinh vắng:";
                    ws.Cells[footTop + 2, 1].Value = "Tổng số bài thi:";
                    ws.Cells[footTop + 3, 1].Value = "Tổng số tờ:";

                    ws.Cells[footTop, 8, footTop, 10].Merge = true;
                    ws.Cells[footTop, 8].Value = string.Format("Trà Vinh, ngày {0:dd} tháng {0:MM} năm {0:yyyy}", DateTime.Today);
                    ws.Cells[footTop, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    ws.Cells[footTop + 1, 8, footTop + 1, 10].Merge = true;
                    ws.Cells[footTop + 1, 8].Value = "Trưởng Điểm Thi";
                    ws.Cells[footTop + 1, 8].Style.Font.Bold = true;

                    ws.Cells[footTop + 2, 8, footTop + 2, 10].Merge = true;
                    ws.Cells[footTop + 2, 8].Value = "(Ký tên và đóng dấu)";
                    ws.Cells[footTop + 2, 8].Style.Font.Italic = true;

                    int cbctRow = footTop + 5;
                    ws.Cells[cbctRow, 1].Value = "Cán bộ coi thi 1";
                    ws.Cells[cbctRow + 3, 1].Value = "Cán bộ coi thi 2";

                    // Lưu file
                    pkg.SaveAs(new FileInfo(sfd.FileName));
                }

                MessageBox.Show("Xuất danh sách thí sinh trong phòng thi thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                string sToan = ReadScoreText(row.Cells["Điểm Toán"].Value);   // FIX: string
                string sVan = ReadScoreText(row.Cells["Điểm Văn"].Value);    // FIX: string
                string sAnh = ReadScoreText(row.Cells["Điểm Anh"].Value);    // FIX: string

                // KK/ƯT vẫn là số (decimal?)
                decimal? diemKK = TryParseDecimal(row.Cells["Điểm Khuyến Khích"].Value);
                decimal? diemUT = TryParseDecimal(row.Cells["Điểm Ưu Tiên"].Value);      

                // Gọi đúng chữ ký service: (int, string, string, string, decimal?, decimal?)
                if (_service.CapNhatDiemHocSinh(maHS, sToan, sVan, sAnh, diemKK, diemUT))
                    count++;
            }

            MessageBox.Show("Đã cập nhật điểm học sinh", "Cập nhật thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string ReadScoreText(object val)
        {
            var s = val?.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(s)) return null;

            // các cách nhập “vắng”
            if (string.Equals(s, "v", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(s, "vang", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(s, "vắng", StringComparison.OrdinalIgnoreCase))
            {
                return "Vắng";
            }

            // chuẩn hóa số -> string invariant (dùng . làm dấu thập phân)
            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out var d) ||
                decimal.TryParse(s, NumberStyles.Any, CultureInfo.GetCultureInfo("vi-VN"), out d) ||
                decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out d))
            {
                return d.ToString("0.##", CultureInfo.InvariantCulture);
            }

            // nếu nhập chuỗi khác (vd: “0*”...) thì trả nguyên văn (service/SP sẽ quyết định hợp lệ)
            return s;
        }


        private decimal? TryParseDecimal(object val)
        {
            decimal tmp;
            if (val == null || string.IsNullOrWhiteSpace(val.ToString())) return null;
            return decimal.TryParse(val.ToString(), out tmp) ? (decimal?)tmp : null;
        }

        private void dgvHocSinhTrongPhong_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void cbDotTuyenSinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDotTuyenSinh.SelectedValue != null)
            {
                Common.MaDot = cbDotTuyenSinh.SelectedValue.ToString();
                NapDanhSachPhongThi();
            }
        }

        private void label11_Click(object sender, EventArgs e) { }
    }
}
