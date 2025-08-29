using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using EPPlusLicense = OfficeOpenXml.LicenseContext;

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
            var dsDot = _service.LayDanhSachDotTuyen();
            cbDotTuyenSinh.DataSource = dsDot;
            cbDotTuyenSinh.DisplayMember = "TenDot";
            cbDotTuyenSinh.ValueMember = "MaDot";

            if (!string.IsNullOrEmpty(Common.MaDot))
                cbDotTuyenSinh.SelectedValue = Common.MaDot;
            Guard.DisableForThuKy(
    btnXetTrungTuyen, btnLuuChiTieu

);
            NapDanhSachTrungTuyen();
        }

        private void cbDotTuyenSinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            var maDot = cbDotTuyenSinh.SelectedValue != null ? cbDotTuyenSinh.SelectedValue.ToString() : null;
            if (string.IsNullOrEmpty(maDot)) return;

            Common.MaDot = maDot;

            var chiTieu = _service.LayChiTieuTruong(Common.MaTruong, maDot);
            txtChiTieu.Text = chiTieu != null ? chiTieu.ChiTieu.ToString() : "0";

            NapDanhSachTrungTuyen();
        }

        private void btnLuuChiTieu_Click(object sender, EventArgs e)
        {
            int chiTieu;
            if (!int.TryParse(txtChiTieu.Text, out chiTieu) || chiTieu < 0)
            {
                MessageBox.Show("Chỉ tiêu phải là số nguyên dương!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var maDot = cbDotTuyenSinh.SelectedValue != null ? cbDotTuyenSinh.SelectedValue.ToString() : null;
            if (string.IsNullOrEmpty(maDot))
            {
                MessageBox.Show("Vui lòng chọn đợt tuyển sinh!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool kq = _service.CapNhatChiTieu(Common.MaTruong, maDot, chiTieu);
            MessageBox.Show(kq ? "Cập nhật chỉ tiêu thành công!" : "Cập nhật thất bại!",
                "Thông báo",
                MessageBoxButtons.OK, kq ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        private void btnXemTrungTuyen_Click(object sender, EventArgs e)
        {
            NapDanhSachTrungTuyen();
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

        // ============ NẠP & SẮP XẾP DANH SÁCH ============
        private void NapDanhSachTrungTuyen()
        {
            var arr = _service.LayDanhSachTrungTuyen(Common.MaTruong, Common.MaDot);
            var source = (arr as System.Collections.IEnumerable) ?? Array.Empty<object>();

            var vi = StringComparer.Create(new CultureInfo("vi-VN"), true);

            // Sắp xếp: Tổng ↓, Toán ↓, Văn ↓, Anh ↓, Tên ↑, Họ ↑, SBD ↑
            var list = source.Cast<object>()
                .Select(o => new
                {
                    Obj = o,
                    Tong = GetDouble(o, "DiemTong"),
                    Toan = GetDouble(o, "DiemToan"),
                    Van = GetDouble(o, "DiemVan"),
                    Anh = GetDouble(o, "DiemAnh"),
                    Ten = (GetProp(o, "Ten") ?? "").ToString(),
                    Ho = (GetProp(o, "Ho") ?? "").ToString(),
                    SBD = (GetProp(o, "MaSoBaoDanh") ?? "").ToString()
                })
                .OrderByDescending(x => x.Tong)
                .ThenByDescending(x => x.Toan)
                .ThenByDescending(x => x.Van)
                .ThenByDescending(x => x.Anh)
                .ThenBy(x => x.Ten, vi)
                .ThenBy(x => x.Ho, vi)
                .ThenBy(x => x.SBD, vi)
                .Select(x => x.Obj)
                .ToList();

            dgvDanhSachTrungTuyen.DataSource = null;
            dgvDanhSachTrungTuyen.DataSource = list;
            FormatDanhSachTrungTuyenGrid();
        }

        // ============ ĐỊNH DẠNG LƯỚI ============
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

            HideIfExists(dgv, "MaHocSinh");
            HideIfExists(dgv, "NgayDangKy");

            var map = new Dictionary<string, string>
            {
                { "MaSoBaoDanh", "SBD" }, { "Ho", "Họ" }, { "Ten", "Tên" },
                { "NgaySinh", "Ngày sinh" }, { "GioiTinh", "Giới tính" },
                { "DanToc", "Dân tộc" }, { "NoiSinh", "Nơi sinh" },
                { "TruongTHCS", "Học sinh trường" }, { "PhongThi", "Phòng thi" },
                { "DiemToan", "Toán" }, { "DiemVan", "Văn" }, { "DiemAnh", "Anh" },
                { "DiemKhuyenKhich", "KK" }, { "DiemUuTien", "UT" },
                { "DiemTong", "Tổng điểm" }, { "GhiChu", "Ghi chú" }
            };
            foreach (var kv in map)
                if (dgv.Columns.Contains(kv.Key))
                    dgv.Columns[kv.Key].HeaderText = kv.Value;

            if (dgv.Columns.Contains("DiemTong"))
                dgv.Columns["DiemTong"].DisplayIndex = dgv.Columns.Count - 1;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                var h = col.HeaderText.ToLowerInvariant();
                if (h.Contains("điểm") || h.Contains("toán") || h.Contains("văn") ||
                    h.Contains("anh") || h.Contains("ngày"))
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                else
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private static void HideIfExists(DataGridView dgv, string colName)
        {
            if (dgv.Columns.Contains(colName))
                dgv.Columns[colName].Visible = false;
        }

        private void label11_Click(object sender, EventArgs e) { }

        // ============ XUẤT EXCEL THEO MẪU ============
        private void btnXuatDanhSach_Click(object sender, EventArgs e)
        {
            try
            {
                var list = (dgvDanhSachTrungTuyen.DataSource as System.Collections.IEnumerable)?
                           .Cast<object>().ToList() ?? new List<object>();
                if (list.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Tìm file mẫu
                string exeDir = AppDomain.CurrentDomain.BaseDirectory;
                string[] names = { "Mẫu.xlsx", "Mau.xlsx", "Template.xlsx" };
                string templatePath = names
                    .SelectMany(n => new[]
                    {
                        Path.Combine(exeDir, n),
                        Path.Combine(exeDir, "Templates", n)
                    })
                    .FirstOrDefault(File.Exists);

                if (templatePath == null)
                {
                    MessageBox.Show("Không tìm thấy file mẫu (Mẫu.xlsx/Mau.xlsx). Đặt cạnh .exe hoặc trong thư mục Templates.",
                        "Thiếu file mẫu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var sfd = new SaveFileDialog
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = $"DS_TrungTuyen_{Common.MaTruong}_{Common.MaDot}.xlsx",
                    OverwritePrompt = true
                })
                {
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    File.Copy(templatePath, sfd.FileName, true);
                    ExcelPackage.LicenseContext = EPPlusLicense.NonCommercial;

                    using (var pkg = new ExcelPackage(new FileInfo(sfd.FileName)))
                    {
                        var ws = pkg.Workbook.Worksheets.FirstOrDefault()
                                 ?? throw new InvalidOperationException("File mẫu không có worksheet.");

                        var dot = _service.LayDanhSachDotTuyen()?.FirstOrDefault(d => d.MaDot == Common.MaDot);

                        DateTime khoaNgay = dot != null && dot.NgayBatDau.HasValue ? dot.NgayBatDau.Value : DateTime.Today;
                        ReplaceKhoaNgayCell(ws, khoaNgay);

                        int startYear = (dot != null && dot.Nam > 0) ? dot.Nam : 0;
                        if (startYear == 0)
                        {
                            int y;
                            if (TryExtractYearFromMaDot(Common.MaDot, out y)) startYear = y;
                        }
                        if (startYear == 0) startYear = khoaNgay.Year;

                        ReplaceAcademicYear(ws, startYear);
                        ReplaceFooterDate(ws, DateTime.Today);

                        // 1) Xác định header/footer
                        int headerRow = FindHeaderRow(ws);
                        if (headerRow <= 0)
                            throw new InvalidOperationException("Không tìm thấy dòng tiêu đề.");

                        int footerRow = FindFooterRow(ws);
                        if (footerRow <= 0 || footerRow <= headerRow + 1)
                            throw new InvalidOperationException("Không tìm thấy footer trong mẫu.");

                        // 2) Ánh xạ cột (đọc 2 hàng tiêu đề)
                        var cols = MapHeaderColumns(ws, headerRow);

                        // Đảm bảo tiêu đề 2 hàng cho phần “Điểm thi”
                        EnsureTwoRowHeader(ws, headerRow, cols);

                        // Tính dòng bắt đầu dữ liệu (sau 2 hàng header)
                        int dataStartRow = headerRow + 2;

                        // 3) Xoá dữ liệu cũ nhưng giữ lại 1 dòng mẫu để copy style
                        int rowsBetween = footerRow - dataStartRow - 1; // để lại dataStartRow
                        if (rowsBetween > 0)
                            ws.DeleteRow(dataStartRow + 1, rowsBetween);

                        // 4) Chèn đủ số dòng mới, copy style từ dòng mẫu dataStartRow
                        if (list.Count >= 2)
                            ws.InsertRow(dataStartRow + 1, list.Count - 1, dataStartRow);

                        // 5) Ghi dữ liệu
                        for (int i = 0; i < list.Count; i++)
                        {
                            int r = dataStartRow + i;
                            var hs = list[i];

                            string ho = (GetProp(hs, "Ho") ?? "").ToString().Trim();
                            string ten = (GetProp(hs, "Ten") ?? "").ToString().Trim();
                            string hoTen = (ho + " " + ten).Trim();

                            WriteCell(ws, r, cols, "STT", i + 1);
                            WriteCell(ws, r, cols, "SBD", GetProp(hs, "MaSoBaoDanh"));

                            if (cols.ContainsKey("HoTen"))
                                WriteCell(ws, r, cols, "HoTen", hoTen);
                            else
                            {
                                WriteCell(ws, r, cols, "Họ", ho);
                                WriteCell(ws, r, cols, "Tên", ten);
                            }

                            WriteCell(ws, r, cols, "Ngày sinh", GetProp(hs, "NgaySinh"));
                            WriteCell(ws, r, cols, "Nơi sinh", GetProp(hs, "NoiSinh"));
                            WriteCell(ws, r, cols, "Giới tính", GetProp(hs, "GioiTinh"));
                            WriteCell(ws, r, cols, "Dân tộc", GetProp(hs, "DanToc"));
                            WriteCell(ws, r, cols, "Học sinh trường", GetProp(hs, "TruongTHCS"));
                            WriteCell(ws, r, cols, "UT", GetProp(hs, "DiemUuTien"));
                            WriteCell(ws, r, cols, "KK", GetProp(hs, "DiemKhuyenKhich"));
                            WriteCell(ws, r, cols, "Toán", GetProp(hs, "DiemToan"));
                            WriteCell(ws, r, cols, "Văn", GetProp(hs, "DiemVan"));
                            WriteCell(ws, r, cols, "Anh", GetProp(hs, "DiemAnh"));
                            WriteCell(ws, r, cols, "Tổng điểm", GetProp(hs, "DiemTong"));
                            WriteCell(ws, r, cols, "Phòng thi", GetProp(hs, "PhongThi"));
                            WriteCell(ws, r, cols, "Ghi chú", GetProp(hs, "GhiChu"));
                        }

                        // 6) Kẻ viền & căn giữa
                        int lastDataRow = dataStartRow + list.Count - 1;
                        int cStart, cEnd; GetTableSpan(cols, out cStart, out cEnd);

                        ApplyThinBorders(ws, headerRow, lastDataRow, cStart, cEnd);
                        if (list.Count > 0)
                            ApplyThickBottomBorder(ws, lastDataRow, cStart, cEnd);

                        Center(ws, dataStartRow, lastDataRow, cols, "STT", "SBD", "Giới tính", "Phòng thi");

                        pkg.Save();
                    }

                    MessageBox.Show("Xuất danh sách trúng tuyển thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============ Helpers chung ============

        // Bỏ dấu tiếng Việt + hạ chữ thường + gộp khoảng trắng + chuyển "đ" -> "d"
        private static string Canon(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return string.Empty;

            // Bỏ dấu
            string formD = raw.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var ch in formD)
            {
                var cat = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (cat != UnicodeCategory.NonSpacingMark) sb.Append(ch);
            }
            string s = sb.ToString().Normalize(NormalizationForm.FormC);

            // chuyển đ -> d, hạ chữ, gộp khoảng trắng
            s = s.Replace('đ', 'd').Replace('Đ', 'D').ToLowerInvariant();
            s = Regex.Replace(s, @"\s+", " ").Trim();
            return s;
        }

        private static object GetProp(object obj, string name)
        {
            if (obj == null) return null;
            var p = obj.GetType().GetProperty(name);
            return p != null ? p.GetValue(obj, null) : null;
        }

        private static double GetDouble(object obj, string name)
        {
            try
            {
                var v = GetProp(obj, name);
                if (v == null) return 0.0;
                double d;
                if (v is double) return (double)v;
                if (v is float) return Convert.ToDouble(v);
                if (v is decimal) return Convert.ToDouble(v);
                if (double.TryParse(Convert.ToString(v), NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                    return d;
                if (double.TryParse(Convert.ToString(v), NumberStyles.Any, new CultureInfo("vi-VN"), out d))
                    return d;
                return Convert.ToDouble(v, CultureInfo.InvariantCulture);
            }
            catch { return 0.0; }
        }

        // Header: thấy STT & SBD & (Họ và tên hoặc Ngày sinh)
        private static int FindHeaderRow(ExcelWorksheet ws)
        {
            if (ws.Dimension == null) return -1;

            int rStart = ws.Dimension.Start.Row;
            int rEnd = Math.Min(ws.Dimension.End.Row, 200);
            int cStart = ws.Dimension.Start.Column;
            int cEnd = ws.Dimension.End.Column;

            for (int r = rStart; r <= rEnd; r++)
            {
                bool hasSTT = false, hasSBD = false, hasHoTenOrNgaySinh = false;

                for (int c = cStart; c <= cEnd; c++)
                {
                    string v = (ws.Cells[r, c].Text ?? "").Trim().ToLowerInvariant();
                    if (v.Length == 0) continue;

                    if (v.Contains("stt")) hasSTT = true;
                    if (v.Contains("sbd") || v.Contains("số báo danh")) hasSBD = true;
                    if (v.Contains("họ và tên") || v.Contains("ngày sinh")) hasHoTenOrNgaySinh = true;
                }

                if (hasSTT && hasSBD && hasHoTenOrNgaySinh)
                    return r;
            }
            return -1;
        }

        // Footer: “Người lập bảng” hoặc “Trà Vinh, ngày …”
        private static int FindFooterRow(ExcelWorksheet ws)
        {
            if (ws.Dimension == null) return -1;

            int rStart = ws.Dimension.Start.Row, rEnd = ws.Dimension.End.Row;
            int cStart = ws.Dimension.Start.Column, cEnd = ws.Dimension.End.Column;

            for (int r = rStart; r <= rEnd; r++)
            {
                for (int c = cStart; c <= cEnd; c++)
                {
                    string t = (ws.Cells[r, c].Text ?? "").Trim().ToLowerInvariant();
                    if (t.Contains("người lập bảng") || t.Contains("trà vinh, ngày"))
                        return r;
                }
            }
            return -1;
        }

        // Ánh xạ cột từ header (đọc 2 hàng)
        private static Dictionary<string, int> MapHeaderColumns(ExcelWorksheet ws, int headerRow)
        {
            var map = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            int cStart = ws.Dimension.Start.Column, cEnd = ws.Dimension.End.Column;

            for (int r = headerRow; r <= headerRow + 1; r++)
                for (int c = cStart; c <= cEnd; c++)
                {
                    string t = (ws.Cells[r, c].Text ?? "").Trim();
                    if (t.Length == 0) continue;

                    string key = NormalizeHeader(t);
                    if (!string.IsNullOrEmpty(key)) map[key] = c;
                }

            return map;
        }

        private static string NormalizeHeader(string raw)
        {
            string s = Canon(raw);

            if (s.Contains("stt")) return "STT";
            if (s.Contains("sbd") || s.Contains("so bd") || s.Contains("so bao danh")) return "SBD";

            if (s.Contains("ho va ten")) return "HoTen";
            if (s == "ho" || s.StartsWith("ho ")) return "Họ";
            if (s == "ten" || s.StartsWith("ten ")) return "Tên";

            if (s.Contains("ngay sinh")) return "Ngày sinh";
            if (s.Contains("noi sinh")) return "Nơi sinh";
            if (s.Contains("gioi tinh") || s == "gt") return "Giới tính";
            if (s.Contains("dan toc")) return "Dân tộc";
            if (s.Contains("hoc sinh truong") || s.Contains("thcs")) return "Học sinh trường";

            // Quan trọng: nhận mọi biến thể "Điểm UT" / "Điểm ƯT" / "Điểm ưu tiên"
            if (s.Contains("diem ut") || s == "ut" || s.Contains("uu tien")) return "UT";
            if (s.Contains("diem kk") || s == "kk" || s.Contains("khuyen khich")) return "KK";

            if (s.Contains("toan")) return "Toán";
            if (s.Contains("van")) return "Văn";
            if (s.Contains("anh")) return "Anh";

            if (s.Contains("tong diem") || s == "tong") return "Tổng điểm";
            if (s.Contains("phong thi") || s.Contains("phong")) return "Phòng thi";
            if (s.Contains("ghi chu")) return "Ghi chú";

            return null;
        }


        // Bảo đảm header 2 hàng: “Điểm thi” trên – “Toán/Văn/Anh” dưới; các cột khác merge 2 hàng
        // Bảo đảm header 2 hàng: “Điểm thi” trên – “Toán/Văn/Anh” dưới;
        // các cột còn lại merge 2 hàng và luôn đặt lại caption nếu ô trên bị trống.
        private static void EnsureTwoRowHeader(ExcelWorksheet ws, int headerRow, Dictionary<string, int> cols)
        {
            // --- Nhóm "Điểm thi" ---
            bool hasToan = cols.TryGetValue("Toán", out int colToan);
            bool hasVan = cols.TryGetValue("Văn", out int colVan);
            bool hasAnh = cols.TryGetValue("Anh", out int colAnh);

            if (hasToan && hasVan && hasAnh)
            {
                int left = Math.Min(colToan, Math.Min(colVan, colAnh));
                int right = Math.Max(colToan, Math.Max(colVan, colAnh));

                // Merge hàng trên cho cụm "Điểm thi"
                ws.Cells[headerRow, left, headerRow, right].Merge = true;
                var top = ws.Cells[headerRow, left];
                if (string.IsNullOrWhiteSpace(top.Text)) top.Value = "Điểm thi";
                ws.Cells[headerRow, left, headerRow, right].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[headerRow, left, headerRow, right].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                // Hàng dưới: nhãn từng môn
                ws.Cells[headerRow + 1, colToan].Value = "Toán";
                ws.Cells[headerRow + 1, colVan].Value = "Văn";
                ws.Cells[headerRow + 1, colAnh].Value = "Anh";
                ws.Cells[headerRow + 1, left, headerRow + 1, right].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // --- Các cột đơn: merge 2 hàng + đảm bảo caption ở ô trên ---
            string[] singleKeys = {
        "STT","SBD","HoTen","Họ","Tên","Ngày sinh","Nơi sinh","Giới tính","Dân tộc",
        "Học sinh trường","UT","KK","Tổng điểm","Phòng thi","Ghi chú"
    };

            foreach (var key in singleKeys)
            {
                if (!cols.TryGetValue(key, out int c)) continue;

                // Bỏ qua các cột thuộc cụm Điểm thi
                if ((hasToan && c == colToan) || (hasVan && c == colVan) || (hasAnh && c == colAnh))
                    continue;

                var rng = ws.Cells[headerRow, c, headerRow + 1, c];
                if (!rng.Merge) rng.Merge = true;

                rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                // QUAN TRỌNG: nếu ô trên trống thì đặt lại caption chuẩn (fix mất "Điểm UT")
                var cellTop = ws.Cells[headerRow, c];
                if (string.IsNullOrWhiteSpace(cellTop.Text))
                    cellTop.Value = HeaderCaptionOf(key);
            }
        }

        // Trả về caption chuẩn theo "key" chuẩn hóa
        private static string HeaderCaptionOf(string key)
        {
            switch (key)
            {
                case "STT": return "STT";
                case "SBD": return "SBD";
                case "HoTen": return "Họ và tên thí sinh";
                case "Họ": return "Họ";
                case "Tên": return "Tên";
                case "Ngày sinh": return "Ngày sinh";
                case "Nơi sinh": return "Nơi sinh";
                case "Giới tính": return "Giới tính";
                case "Dân tộc": return "Dân tộc";
                case "Học sinh trường": return "Học sinh trường";
                case "UT": return "Điểm UT";    
                case "KK": return "Điểm KK";
                case "Tổng điểm": return "Tổng điểm";
                case "Phòng thi": return "Phòng thi";
                case "Ghi chú": return "Ghi chú";
                default: return key;
            }
        }


        private static void GetTableSpan(Dictionary<string, int> cols, out int cStart, out int cEnd)
        {
            string[] keys = {
                "STT","SBD","HoTen","Họ","Tên","Ngày sinh","Nơi sinh","Giới tính","Dân tộc",
                "Học sinh trường","UT","KK","Toán","Văn","Anh","Tổng điểm","Phòng thi","Ghi chú"
            };
            var list = new List<int>();
            foreach (var k in keys) if (cols.ContainsKey(k)) list.Add(cols[k]);
            cStart = list.Min();
            cEnd = list.Max();
        }

        // Chuẩn hoá điểm nếu lỡ x100
        private static double NormalizeScore(string key, double num)
        {
            if (key == "UT" || key == "KK")
                return (num > 5.0) ? num / 100.0 : num;
            if (key == "Toán" || key == "Văn" || key == "Anh")
                return (num > 20.0) ? num / 100.0 : num;
            if (key == "Tổng điểm")
                return (num > 80.0) ? num / 100.0 : num;
            return num;
        }

        private static void WriteCell(ExcelWorksheet ws, int row,
            Dictionary<string, int> cols, string key, object value)
        {
            int col;
            if (!cols.TryGetValue(key, out col)) return;

            if (value == null)
            {
                ws.Cells[row, col].Value = null;
                return;
            }

            // Ngày sinh
            if (key == "Ngày sinh")
            {
                DateTime dt;
                var s = Convert.ToString(value);

                if (value is DateTime)
                    dt = (DateTime)value;
                else if (!DateTime.TryParse(s, new CultureInfo("vi-VN"), DateTimeStyles.None, out dt) &&
                         !DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                {
                    ws.Cells[row, col].Value = s;
                    return;
                }

                ws.Cells[row, col].Value = dt;
                ws.Cells[row, col].Style.Numberformat.Format = "dd/MM/yyyy";
                ws.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[row, col].Style.WrapText = false;
                return;
            }

            // Điểm số
            if (key == "UT" || key == "KK" || key == "Toán" || key == "Văn" || key == "Anh" || key == "Tổng điểm")
            {
                double num;
                var s = Convert.ToString(value);

                if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out num) ||
                    double.TryParse(s, NumberStyles.Any, new CultureInfo("vi-VN"), out num))
                {
                    num = NormalizeScore(key, num);
                    ws.Cells[row, col].Value = num;
                }
                else
                {
                    try
                    {
                        num = Convert.ToDouble(value, CultureInfo.InvariantCulture);
                        num = NormalizeScore(key, num);
                        ws.Cells[row, col].Value = num;
                    }
                    catch
                    {
                        ws.Cells[row, col].Value = null;
                    }
                }

                ws.Cells[row, col].Style.Numberformat.Format = "0.00";
                ws.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                return;
            }

            // STT
            if (key == "STT")
            {
                ws.Cells[row, col].Value = Convert.ToInt32(value);
                ws.Cells[row, col].Style.Numberformat.Format = "0";
                ws.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                return;
            }

            // Phòng thi: ẩn hậu tố sau dấu '_'
            if (key == "Phòng thi")
            {
                var s = Convert.ToString(value) ?? "";
                int i = s.IndexOf('_');
                if (i > 0) s = s.Substring(0, i);
                ws.Cells[row, col].Value = s;
                ws.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[row, col].Style.WrapText = false;
                return;
            }

            // Mặc định: text
            ws.Cells[row, col].Value = Convert.ToString(value);
        }

        private static void Center(ExcelWorksheet ws, int r1, int r2, Dictionary<string, int> cols, params string[] keys)
        {
            foreach (var k in keys)
            {
                int c;
                if (cols.TryGetValue(k, out c))
                    ws.Cells[r1, c, r2, c].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
        }


        private static void ApplyThinBorders(ExcelWorksheet ws, int r1, int r2, int c1, int c2)
        {
            using (var rng = ws.Cells[r1, c1, r2, c2])
            {
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }
        }


        private static void ApplyThickBottomBorder(ExcelWorksheet ws, int row, int cStart, int cEnd)
        {
            for (int c = cStart; c <= cEnd; c++)
                ws.Cells[row, c].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
        }


        private static void ReplaceKhoaNgayCell(ExcelWorksheet ws, DateTime d)
        {
            if (ws.Dimension == null) return;
            int rStart = ws.Dimension.Start.Row, rEnd = Math.Min(ws.Dimension.Start.Row + 15, ws.Dimension.End.Row);
            int cStart = ws.Dimension.Start.Column, cEnd = ws.Dimension.End.Column;

            for (int r = rStart; r <= rEnd; r++)
            {
                for (int c = cStart; c <= cEnd; c++)
                {
                    string t = (ws.Cells[r, c].Text ?? "").Trim();
                    if (t.ToLowerInvariant().Contains("khóa ngày"))
                    {
                        ws.Cells[r, c].Value = "Khóa ngày: " + d.ToString("dd/MM/yyyy");
                        return;
                    }
                }
            }
        }

        // Tìm & thay dòng "Năm học ..." trong vùng đầu trang (khoảng 15 dòng đầu)
        private static void ReplaceAcademicYear(ExcelWorksheet ws, int startYear)
        {
            if (ws.Dimension == null) return;

            int rStart = ws.Dimension.Start.Row;
            int rEnd = Math.Min(ws.Dimension.Start.Row + 15, ws.Dimension.End.Row);
            int cStart = ws.Dimension.Start.Column;
            int cEnd = ws.Dimension.End.Column;

            for (int r = rStart; r <= rEnd; r++)
            {
                for (int c = cStart; c <= cEnd; c++)
                {
                    string t = (ws.Cells[r, c].Text ?? "").Trim();
                    if (t.Length == 0) continue;

                    // Bắt "Năm học" cả có/không dấu
                    string canon = Canon(t);
                    if (canon.Contains("nam hoc"))
                    {
                        ws.Cells[r, c].Value = $"Năm học {startYear} - {startYear + 1}";
                        return;
                    }
                }
            }
        }

        private static bool TryExtractYearFromMaDot(string maDot, out int year)
        {
            year = 0;
            if (string.IsNullOrWhiteSpace(maDot)) return false;
            var m = Regex.Match(maDot, @"(20\d{2})");
            if (m.Success && int.TryParse(m.Groups[1].Value, out year)) return true;
            return false;
        }



        private static void ReplaceFooterDate(ExcelWorksheet ws, DateTime d)
        {
            if (ws.Dimension == null) return;
            int rStart = ws.Dimension.Start.Row, rEnd = ws.Dimension.End.Row;
            int cStart = ws.Dimension.Start.Column, cEnd = ws.Dimension.End.Column;

            for (int r = rEnd; r >= rStart; r--)
            {
                for (int c = cEnd; c >= cStart; c--)
                {
                    string t = (ws.Cells[r, c].Text ?? "").Trim().ToLowerInvariant();
                    if (t.Contains("trà vinh, ngày"))
                    {
                        ws.Cells[r, c].Value = "Trà Vinh, ngày " + d.ToString("dd/MM/yyyy");
                        ws.Cells[r, c].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        return;
                    }
                }
            }
        }

        private void btnXuatTatCaDiem_Click(object sender, EventArgs e)
        {
            try
            {
                // 0) Lấy toàn bộ thí sinh của trường trong đợt (xếp theo họ tên)
                var allList = LayTatCaHocSinhTheoTen();
                if (allList.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 1) Tìm file mẫu
                string exeDir = AppDomain.CurrentDomain.BaseDirectory;
                string[] names = { "Mẫu.xlsx", "Mau.xlsx", "Template.xlsx" };
                string templatePath = names
                    .SelectMany(n => new[] { Path.Combine(exeDir, n), Path.Combine(exeDir, "Templates", n) })
                    .FirstOrDefault(File.Exists);

                if (templatePath == null)
                {
                    MessageBox.Show("Không tìm thấy file mẫu (Mẫu.xlsx/Mau.xlsx). Đặt cạnh .exe hoặc trong thư mục Templates.",
                        "Thiếu file mẫu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var sfd = new SaveFileDialog
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = $"DS_DiemThi_{Common.MaTruong}_{Common.MaDot}.xlsx",
                    OverwritePrompt = true
                })
                {
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    File.Copy(templatePath, sfd.FileName, true);
                    ExcelPackage.LicenseContext = EPPlusLicense.NonCommercial;

                    using (var pkg = new ExcelPackage(new FileInfo(sfd.FileName)))
                    {
                        var ws = pkg.Workbook.Worksheets.FirstOrDefault()
                                 ?? throw new InvalidOperationException("File mẫu không có worksheet.");

                        // 2) Header/Footer + ánh xạ cột
                        ReplaceKhoaNgayCell(ws, DateTime.Today);
                        ReplaceFooterDate(ws, DateTime.Today);

                        int headerRow = FindHeaderRow(ws);
                        if (headerRow <= 0) throw new InvalidOperationException("Không tìm thấy dòng tiêu đề.");

                        int footerRow = FindFooterRow(ws);
                        if (footerRow <= 0 || footerRow <= headerRow + 1)
                            throw new InvalidOperationException("Không tìm thấy footer trong mẫu.");

                        var cols = MapHeaderColumns(ws, headerRow);
                        EnsureTwoRowHeader(ws, headerRow, cols);       // “Điểm thi” / “Toán, Văn, Anh”

                        int dataStartRow = headerRow + 2;

                        // Xoá dữ cũ, để lại 1 dòng mẫu để copy style
                        int rowsBetween = footerRow - dataStartRow - 1;
                        if (rowsBetween > 0) ws.DeleteRow(dataStartRow + 1, rowsBetween);

                        // Chèn cho đủ số dòng
                        if (allList.Count >= 2)
                            ws.InsertRow(dataStartRow + 1, allList.Count - 1, dataStartRow);

                        // 3) Ghi dữ liệu
                        for (int i = 0; i < allList.Count; i++)
                        {
                            int r = dataStartRow + i;
                            var hs = allList[i];

                            string ho = (GetProp(hs, "Ho") ?? "").ToString().Trim();
                            string ten = (GetProp(hs, "Ten") ?? "").ToString().Trim();
                            string hoTen = (ho + " " + ten).Trim();

                            WriteCell(ws, r, cols, "STT", i + 1);
                            WriteCell(ws, r, cols, "SBD", GetProp(hs, "MaSoBaoDanh"));

                            if (cols.ContainsKey("HoTen"))
                                WriteCell(ws, r, cols, "HoTen", hoTen);
                            else
                            {
                                WriteCell(ws, r, cols, "Họ", ho);
                                WriteCell(ws, r, cols, "Tên", ten);
                            }

                            WriteCell(ws, r, cols, "Ngày sinh", GetProp(hs, "NgaySinh"));
                            WriteCell(ws, r, cols, "Nơi sinh", GetProp(hs, "NoiSinh"));
                            WriteCell(ws, r, cols, "Giới tính", GetProp(hs, "GioiTinh"));
                            WriteCell(ws, r, cols, "Dân tộc", GetProp(hs, "DanToc"));
                            WriteCell(ws, r, cols, "Học sinh trường", GetProp(hs, "TruongTHCS"));

                            // Điểm cộng / khuyến khích
                            WriteCell(ws, r, cols, "UT", GetProp(hs, "DiemUuTien"));
                            WriteCell(ws, r, cols, "KK", GetProp(hs, "DiemKhuyenKhich"));

                            // Điểm môn
                            WriteCell(ws, r, cols, "Toán", GetProp(hs, "DiemToan"));
                            WriteCell(ws, r, cols, "Văn", GetProp(hs, "DiemVan"));
                            WriteCell(ws, r, cols, "Anh", GetProp(hs, "DiemAnh"));

                            // Tổng điểm
                            WriteCell(ws, r, cols, "Tổng điểm", GetProp(hs, "DiemTong"));

                            // Phòng thi (ẩn hậu tố sau dấu '_')
                            WriteCell(ws, r, cols, "Phòng thi", GetProp(hs, "PhongThi"));

                            // Ghi chú (nếu có)
                            WriteCell(ws, r, cols, "Ghi chú", GetProp(hs, "GhiChu"));
                        }

                        // 4) Viền & căn giữa hợp lý
                        int lastDataRow = dataStartRow + allList.Count - 1;
                        GetTableSpan(cols, out int cStart, out int cEnd);

                        ApplyThinBorders(ws, headerRow, lastDataRow, cStart, cEnd);
                        if (allList.Count > 0) ApplyThickBottomBorder(ws, lastDataRow, cStart, cEnd);

                        Center(ws, dataStartRow, lastDataRow, cols, "STT", "SBD", "Giới tính", "Phòng thi");

                        pkg.Save();
                    }

                    MessageBox.Show("Xuất danh sách điểm (toàn bộ thí sinh) thành công!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<object> LayTatCaHocSinhTheoTen()
        {
            var arr = _service.LayDanhSachHocSinh(Common.MaTruong, Common.MaDot);
            var source = (arr as System.Collections.IEnumerable) ?? Array.Empty<object>();
            var vi = StringComparer.Create(new CultureInfo("vi-VN"), true);

            return source.Cast<object>()
                .Select(o => new
                {
                    Obj = o,
                    Ten = (GetProp(o, "Ten") ?? "").ToString(),
                    Ho = (GetProp(o, "Ho") ?? "").ToString(),
                    SBD = (GetProp(o, "MaSoBaoDanh") ?? "").ToString()
                })
                .OrderBy(x => x.Ten, vi)
                .ThenBy(x => x.Ho, vi)
                .ThenBy(x => x.SBD, vi)
                .Select(x => x.Obj)
                .ToList();
        }
    }
}
