using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using TuyenSinhServiceLib;
using TuyenSinhWinApp.TuyenSinhServiceReference;

namespace TuyenSinhWinApp
{
    public partial class frmAdminCapNhatDiemTheoPhongThi : Form
    {
        private readonly Service1Client _service = new Service1Client();

        public frmAdminCapNhatDiemTheoPhongThi()
        {
            InitializeComponent();

            // ADMIN: danh sách môn giống form cán bộ
            cboMonHoc.Items.Clear();
            cboMonHoc.Items.AddRange(new string[] { "Toán", "Văn", "Môn thứ 3", "Điểm Khuyến Khích", "Điểm Ưu Tiên" });
            cboMonHoc.SelectedIndex = 0;

            this.Load += Frm_Load;
            cbDotTuyenSinh.SelectedIndexChanged += cbDotTuyenSinh_SelectedIndexChanged;
            cboPhongThi.SelectedIndexChanged += cboPhongThi_SelectedIndexChanged;
            cboMonHoc.SelectedIndexChanged += cboMonHoc_SelectedIndexChanged;

            btnLuuDiem.Click += btnLuuDiem_Click;

            // ADMIN: khi đổi trường thì reload phòng
            cboTruong.SelectedIndexChanged += (s, e) => NapDanhSachPhongThi();

            // Datagrid basic style (như form cán bộ)
            dgvDanhSachHocSinh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSachHocSinh.RowHeadersVisible = false;
            dgvDanhSachHocSinh.AllowUserToAddRows = false;
            dgvDanhSachHocSinh.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvDanhSachHocSinh.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhSachHocSinh.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvDanhSachHocSinh.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvDanhSachHocSinh.DataError += (s, e) => { e.ThrowException = false; }; // tránh crash khi nhập sai định dạng
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            NapDanhSachTruong();       // ADMIN: thêm bước chọn trường
            NapDanhSachDotTuyen();
        }

        // ===== Helper xử lý điểm (giống form cán bộ) =====
        private string ReadScoreText(object val)
        {
            var s = val?.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(s)) return null;

            // các cách nhập “vắng”
            if (string.Equals(s, "v", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(s, "vang", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(s, "vắng", StringComparison.OrdinalIgnoreCase))
                return "Vắng";

            decimal d;

            // Ưu tiên theo ký tự người dùng gõ
            if (s.Contains(".") && !s.Contains(","))
            {
                if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out d))
                    return d.ToString("0.##", CultureInfo.InvariantCulture);
            }
            if (s.Contains(",") && !s.Contains("."))
            {
                if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.GetCultureInfo("vi-VN"), out d))
                    return d.ToString("0.##", CultureInfo.InvariantCulture);
            }

            // Fallback: ưu tiên Invariant trước để tránh “7.75” -> 775
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out d) ||
                decimal.TryParse(s, NumberStyles.Number, CultureInfo.GetCultureInfo("vi-VN"), out d) ||
                decimal.TryParse(s, NumberStyles.Number, CultureInfo.CurrentCulture, out d))
                return d.ToString("0.##", CultureInfo.InvariantCulture);

            return s; // không parse được thì giữ nguyên
        }


        // Overload: proxy có thể trả string hoặc decimal?, nên hỗ trợ cả 2
        private string FormatScoreStr(string s) => ReadScoreText(s);
        private string FormatScoreStr(decimal? d) => d.HasValue ? d.Value.ToString("0.##", CultureInfo.InvariantCulture) : null;

        private decimal? TryParseDecimal(object val)
        {
            if (val == null) return null;
            var s = val.ToString().Trim();
            if (string.IsNullOrEmpty(s)) return null;

            // bỏ qua các giá trị không phải số
            if (s.Equals("v", StringComparison.OrdinalIgnoreCase) ||
                s.Equals("vang", StringComparison.OrdinalIgnoreCase) ||
                s.Equals("vắng", StringComparison.OrdinalIgnoreCase))
                return null;

            decimal d;

            // Ưu tiên theo ký tự người dùng gõ
            if (s.Contains(".") && !s.Contains(","))
                return decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out d) ? d : (decimal?)null;

            if (s.Contains(",") && !s.Contains("."))
                return decimal.TryParse(s, NumberStyles.Number, CultureInfo.GetCultureInfo("vi-VN"), out d) ? d : (decimal?)null;

            // Fallback
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out d) ||
                decimal.TryParse(s, NumberStyles.Number, CultureInfo.GetCultureInfo("vi-VN"), out d) ||
                decimal.TryParse(s, NumberStyles.Number, CultureInfo.CurrentCulture, out d))
                return d;

            return null;
        }


        // =========================
        // NẠP DỮ LIỆU DANH MỤC
        // =========================
        private void NapDanhSachTruong()
        {
            try
            {
                // ADMIN: dùng API Admin lấy toàn bộ trường
                var dsTruong = _service.Admin_LayDanhSachTruong() ?? new TruongItem[0];

                cboTruong.DataSource = dsTruong;
                cboTruong.DisplayMember = "TenTruong";
                cboTruong.ValueMember = "MaTruong";
                if (dsTruong.Length > 0) cboTruong.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách trường: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboTruong.DataSource = null;
            }
        }

        private void NapDanhSachDotTuyen()
        {
            try
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách đợt tuyển: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbDotTuyenSinh.DataSource = null;
            }
        }

        private void cbDotTuyenSinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            NapDanhSachPhongThi();
        }

        private void NapDanhSachPhongThi()
        {
            try
            {
                // ADMIN: phải có Trường + Đợt
                if (cboTruong.SelectedValue == null || cbDotTuyenSinh.SelectedValue == null)
                {
                    cboPhongThi.DataSource = null;
                    cboPhongThi.SelectedIndex = -1;
                    NapDanhSachHocSinh();
                    return;
                }

                var maTruong = cboTruong.SelectedValue.ToString();
                var maDot = cbDotTuyenSinh.SelectedValue.ToString();

                // ADMIN: dùng API Admin lấy phòng theo Trường+Đợt
                var ds = _service.Admin_LayDanhSachPhongThi(maTruong, maDot) ?? new PhongThi[0];

                if (ds.Length == 0)
                {
                    cboPhongThi.DataSource = null;
                    cboPhongThi.SelectedIndex = -1;
                    NapDanhSachHocSinh();
                    return;
                }

                cboPhongThi.DataSource = ds;
                cboPhongThi.DisplayMember = "MaPhongThi";
                cboPhongThi.ValueMember = "MaPhongThi";
                cboPhongThi.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phòng thi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboPhongThi.DataSource = null;
                cboPhongThi.SelectedIndex = -1;
                NapDanhSachHocSinh();
            }
        }

        // =========================
        // NẠP DS HỌC SINH & HIỂN THỊ
        // =========================
        private void cboPhongThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            NapDanhSachHocSinh();
        }

        private void cboMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            NapDanhSachHocSinh();
        }

        private void btnXemHocSinh_Click(object sender, EventArgs e)
        {
            NapDanhSachHocSinh();
        }

        private void NapDanhSachHocSinh()
        {
            try
            {
                if (cboTruong.SelectedValue == null || cbDotTuyenSinh.SelectedValue == null ||
                    cboPhongThi.SelectedValue == null || cboMonHoc.SelectedItem == null)
                {
                    dgvDanhSachHocSinh.DataSource = null;
                    if (lblInfo != null) lblInfo.Text = "";
                    return;
                }

                string maTruong = cboTruong.SelectedValue.ToString(); // ADMIN: chọn trường
                string maDot = cbDotTuyenSinh.SelectedValue.ToString();
                string maPhong = cboPhongThi.SelectedValue.ToString();
                string mon = cboMonHoc.SelectedItem.ToString();

                // ADMIN: gọi API Admin để lấy HS theo phòng
                var dsHocSinh = _service.Admin_LayDanhSachHocSinhTheoPhong(maTruong, maDot, maPhong) ?? new HocSinh[0];

                // Tạo DataTable: cột Điểm dùng string để cho phép “Vắng”
                var dt = new DataTable();
                dt.Columns.Add("MaHocSinh", typeof(int));
                dt.Columns.Add("SBD");
                dt.Columns.Add("Họ và tên");
                dt.Columns.Add("Ngày sinh");
                dt.Columns.Add("Tên trường THCS");
                dt.Columns.Add("Điểm", typeof(string));

                foreach (var hs in dsHocSinh)
                {
                    string diemStr = null;
                    switch (mon)
                    {
                        case "Toán": diemStr = FormatScoreStr(hs.DiemToan); break;
                        case "Văn": diemStr = FormatScoreStr(hs.DiemVan); break;
                        case "Môn thứ 3": diemStr = FormatScoreStr(hs.DiemAnh); break;
                        case "Điểm Khuyến Khích": diemStr = FormatScoreStr(hs.DiemKhuyenKhich); break;
                        case "Điểm Ưu Tiên": diemStr = FormatScoreStr(hs.DiemUuTien); break;
                        default: diemStr = null; break;
                    }

                    dt.Rows.Add(
                        hs.MaHocSinh,
                        hs.MaSoBaoDanh,
                        $"{hs.Ho} {hs.Ten}",
                        hs.NgaySinh.ToString("dd/MM/yyyy"),
                        hs.TruongTHCS,
                        diemStr
                    );
                }

                dgvDanhSachHocSinh.DataSource = dt;

                // Ẩn mã HS
                if (dgvDanhSachHocSinh.Columns["MaHocSinh"] != null)
                    dgvDanhSachHocSinh.Columns["MaHocSinh"].Visible = false;

                // Chỉ cho nhập cột Điểm
                foreach (DataGridViewColumn col in dgvDanhSachHocSinh.Columns)
                    col.ReadOnly = col.Name != "Điểm";

                // Căn giữa & tiêu đề
                dgvDanhSachHocSinh.Columns["Điểm"].HeaderText = "Điểm " + mon;
                dgvDanhSachHocSinh.Columns["Điểm"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                if (lblInfo != null)
                    lblInfo.Text = $"Phòng {maPhong} — {dsHocSinh.Length} học sinh";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách học sinh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvDanhSachHocSinh.DataSource = null;
                if (lblInfo != null) lblInfo.Text = "";
            }
        }

        // =========================
        // LƯU ĐIỂM
        // =========================
        private void btnLuuDiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboTruong.SelectedValue == null || cbDotTuyenSinh.SelectedValue == null || cboPhongThi.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn Trường, Đợt và Phòng thi.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maTruong = cboTruong.SelectedValue.ToString();
                string maDot = cbDotTuyenSinh.SelectedValue.ToString();
                string maPhong = cboPhongThi.SelectedValue.ToString();
                string mon = cboMonHoc.SelectedItem != null ? cboMonHoc.SelectedItem.ToString() : null;

                if (string.IsNullOrEmpty(mon))
                {
                    MessageBox.Show("Vui lòng chọn môn.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dsGoc = _service.Admin_LayDanhSachHocSinhTheoPhong(maTruong, maDot, maPhong) ?? new HocSinh[0];
                var map = dsGoc.ToDictionary(h => h.MaHocSinh, h => h);

                int count = 0;
                foreach (DataGridViewRow row in dgvDanhSachHocSinh.Rows)
                {
                    if (row.IsNewRow) continue;

                    int maHS = Convert.ToInt32(row.Cells["MaHocSinh"].Value);

                    if (!map.TryGetValue(maHS, out var goc)) continue;

                    string sToan = FormatScoreStr(goc.DiemToan);
                    string sVan = FormatScoreStr(goc.DiemVan);
                    string sAnh = FormatScoreStr(goc.DiemAnh);
                    decimal? diemKhuyenKhich = TryParseDecimal(goc.DiemKhuyenKhich);
                    decimal? diemUuTien = TryParseDecimal(goc.DiemUuTien);

                    
                    var entered = row.Cells["Điểm"].Value;

                    switch (mon)
                    {
                        case "Toán": sToan = ReadScoreText(entered); break;
                        case "Văn": sVan = ReadScoreText(entered); break;
                        case "Môn thứ 3": sAnh = ReadScoreText(entered); break;
                        case "Điểm Khuyến Khích": diemKhuyenKhich = TryParseDecimal(entered); break;
                        case "Điểm Ưu Tiên": diemUuTien = TryParseDecimal(entered); break;
                        default: continue;
                    }

                    
                    if (_service.Admin_CapNhatDiemHocSinh(maHS, sToan, sVan, sAnh, diemKhuyenKhich, diemUuTien))
                        count++;
                }

                MessageBox.Show($"Đã lưu điểm cho {count} học sinh!",
                                "Cập nhật thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                NapDanhSachHocSinh(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
