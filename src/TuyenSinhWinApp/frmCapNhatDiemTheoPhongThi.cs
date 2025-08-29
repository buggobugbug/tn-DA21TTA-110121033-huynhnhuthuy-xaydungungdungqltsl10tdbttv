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
using System.Globalization;
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
            Guard.DisableForThuKy(
                btnLuuDiem);
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

            // Ưu tiên theo ký tự người dùng gõ
            decimal d;
            if (s.Contains(".") && !s.Contains(",")) // dùng . làm dấu thập phân
            {
                if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out d))
                    return d.ToString("0.##", CultureInfo.InvariantCulture);
            }
            if (s.Contains(",") && !s.Contains(".")) // dùng , làm dấu thập phân
            {
                if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.GetCultureInfo("vi-VN"), out d))
                    return d.ToString("0.##", CultureInfo.InvariantCulture);
            }

            // Fallback: tránh case "7.75" → 775 nên ưu tiên Invariant trước
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out d) ||
                decimal.TryParse(s, NumberStyles.Number, CultureInfo.GetCultureInfo("vi-VN"), out d) ||
                decimal.TryParse(s, NumberStyles.Number, CultureInfo.CurrentCulture, out d))
            {
                return d.ToString("0.##", CultureInfo.InvariantCulture);
            }

            // không parse được thì giữ nguyên (VD: chuỗi đặc biệt)
            return s;
        }

        // ====== OVERLOAD MỚI: nhận string (để dùng được với proxy trả về string) ======
        private string FormatScoreStr(string s)
        {
            // cho phép null / "Vắng" / số dạng text, và chuẩn hóa về chuỗi hiển thị
            return ReadScoreText(s);
        }

        private string FormatScoreStr(decimal? d)
        {
            return d.HasValue ? d.Value.ToString("0.##", CultureInfo.InvariantCulture) : null;
        }

        private string ToScoreString(decimal? d)
        {
            return d.HasValue ? d.Value.ToString("0.##", CultureInfo.InvariantCulture) : null;
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
                if (cboPhongThi.SelectedValue == null || cboMonHoc.SelectedItem == null)
                {
                    dgvDanhSachHocSinh.DataSource = null;
                    return;
                }

                string maPhongThi = cboPhongThi.SelectedValue.ToString();
                string mon = cboMonHoc.SelectedItem.ToString();

                var dsHocSinh = _service.LayDanhSachHocSinhTheoPhong(Common.MaTruong, Common.MaDot, maPhongThi);

                var dt = new DataTable();
                dt.Columns.Add("MaHocSinh", typeof(int));
                dt.Columns.Add("SBD");
                dt.Columns.Add("Họ và tên");
                dt.Columns.Add("Ngày sinh");
                dt.Columns.Add("Tên trường THCS");
                dt.Columns.Add("Điểm", typeof(string)); // cột Điểm là string để hiển thị cả “Vắng”

                foreach (var hs in dsHocSinh)
                {
                    string diemStr = null;
                    switch (mon)
                    {
                        // CHÚ Ý: nhờ overload, dù proxy trả về string hay decimal? đều OK
                        case "Toán": diemStr = FormatScoreStr(hs.DiemToan); break;
                        case "Văn": diemStr = FormatScoreStr(hs.DiemVan); break;
                        case "Anh": diemStr = FormatScoreStr(hs.DiemAnh); break;
                        case "Điểm Khuyến Khích": diemStr = FormatScoreStr(hs.DiemKhuyenKhich); break;
                        case "Điểm Ưu Tiên": diemStr = FormatScoreStr(hs.DiemUuTien); break;
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

                if (dgvDanhSachHocSinh.Columns["MaHocSinh"] != null)
                    dgvDanhSachHocSinh.Columns["MaHocSinh"].Visible = false;

                dgvDanhSachHocSinh.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDanhSachHocSinh.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvDanhSachHocSinh.DefaultCellStyle.Font = new Font("Segoe UI", 10);

                foreach (DataGridViewColumn col in dgvDanhSachHocSinh.Columns)
                {
                    col.ReadOnly = col.Name != "Điểm";
                    col.DefaultCellStyle.Alignment = col.Name == "Điểm"
                        ? DataGridViewContentAlignment.MiddleCenter
                        : DataGridViewContentAlignment.MiddleLeft;
                }

                dgvDanhSachHocSinh.Columns["Điểm"].HeaderText = $"Điểm {mon}";
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
            if (cboPhongThi.SelectedValue == null || cboMonHoc.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn phòng thi và môn học.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mon = cboMonHoc.SelectedItem.ToString();
            string maPhongThi = cboPhongThi.SelectedValue.ToString();

            // lấy danh sách gốc 1 lần
            var hsPhong = _service.LayDanhSachHocSinhTheoPhong(Common.MaTruong, Common.MaDot, maPhongThi) ?? new HocSinh[0];
            var map = hsPhong.ToDictionary(h => h.MaHocSinh);

            int count = 0;

            foreach (DataGridViewRow row in dgvDanhSachHocSinh.Rows)
            {
                if (row.IsNewRow) continue;

                int maHS = Convert.ToInt32(row.Cells["MaHocSinh"].Value);

                if (!map.TryGetValue(maHS, out var hsGoc)) continue;

                // chuẩn bị tham số theo chữ ký mới: (int, string, string, string, decimal?, decimal?)
                // Nhờ overload ở FormatScoreStr, cả kiểu string/decimal? đều OK
                string sToan = FormatScoreStr(hsGoc.DiemToan);
                string sVan = FormatScoreStr(hsGoc.DiemVan);
                string sAnh = FormatScoreStr(hsGoc.DiemAnh);
                decimal? diemKK = TryParseDecimal(hsGoc.DiemKhuyenKhich);
                decimal? diemUT = TryParseDecimal(hsGoc.DiemUuTien);

                // giá trị người dùng vừa nhập
                var cellVal = row.Cells["Điểm"].Value;

                switch (mon)
                {
                    case "Toán":
                        sToan = ReadScoreText(cellVal);          // string
                        break;
                    case "Văn":
                        sVan = ReadScoreText(cellVal);           // string
                        break;
                    case "Anh":
                        sAnh = ReadScoreText(cellVal);           // string
                        break;
                    case "Điểm Khuyến Khích":
                        diemKK = TryParseDecimal(cellVal);       // decimal?
                        break;
                    case "Điểm Ưu Tiên":
                        diemUT = TryParseDecimal(cellVal);       // decimal?
                        break;
                }

                // GỌI API chữ ký mới: (int, string, string, string, decimal?, decimal?)
                if (_service.CapNhatDiemHocSinh(maHS, sToan, sVan, sAnh, diemKK, diemUT))
                    count++;
            }

            MessageBox.Show($"Đã lưu điểm cho {count} học sinh!", "Cập nhật thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            NapDanhSachHocSinh();
        }

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
            if (s.Contains(".") && !s.Contains(","))
            {
                if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out d))
                    return d;
            }
            if (s.Contains(",") && !s.Contains("."))
            {
                if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.GetCultureInfo("vi-VN"), out d))
                    return d;
            }

            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out d) ||
                decimal.TryParse(s, NumberStyles.Number, CultureInfo.GetCultureInfo("vi-VN"), out d) ||
                decimal.TryParse(s, NumberStyles.Number, CultureInfo.CurrentCulture, out d))
                return d;

            return null;
        }


        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
