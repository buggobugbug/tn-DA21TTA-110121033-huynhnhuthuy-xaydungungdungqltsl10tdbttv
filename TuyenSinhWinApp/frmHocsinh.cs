using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TuyenSinhServiceLib;
using TuyenSinhWinApp.TuyenSinhServiceReference;
using OfficeOpenXml;
using System.IO;
using DevExpress.Data.NetCompatibility.Extensions;

namespace TuyenSinhWinApp
{
    public partial class frmHocsinh : Form
    {
        private readonly Service1Client _serviceClient;
        private bool _isEditMode = false;
        private HocSinh _hocSinhDangSua = null;
        private List<DotTuyenSinh> danhSachDotTuyen;
        private List<HocSinh> _dsHocSinhGoc = new List<HocSinh>();
        // Phần chuyển form
        private Form _formMain;

        public frmHocsinh(Form formMain)
        {
            InitializeComponent();
            _serviceClient = new Service1Client();
            _formMain = formMain;
            dgvDanhSachHocSinh.EnableHeadersVisualStyles = false;
            dgvDanhSachHocSinh.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDanhSachHocSinh.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11);
            dgvDanhSachHocSinh.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dgvDanhSachHocSinh.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvDanhSachHocSinh.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            SetupFormControls();
        }

        public frmHocsinh(Form formMain, HocSinh hocSinhCanSua) : this(formMain)
        {
            _isEditMode = true;
            _hocSinhDangSua = hocSinhCanSua;
            HienThiThongTinHocSinh(hocSinhCanSua);
        }

        private void LoadDotTuyen()
        {
            // Lấy danh sách đợt từ service
            danhSachDotTuyen = _serviceClient.LayDanhSachDotTuyen()
                                .Where(d => d.TrangThai == "DangMo") // chỉ lấy đợt mở
                                .ToList();
            cboDotTuyen.DataSource = danhSachDotTuyen;
            cboDotTuyen.DisplayMember = "TenDot";  // Hiển thị tên đợt
            cboDotTuyen.ValueMember = "MaDot";     // Giá trị thực là MaDot

            // Chọn đợt đang mở mặc định
            var dotDangMo = danhSachDotTuyen.FirstOrDefault(d => d.TrangThai == "DangMo");
            if (dotDangMo != null)
                cboDotTuyen.SelectedValue = dotDangMo.MaDot;
                Common.MaDot = dotDangMo.MaDot;
        }

        public frmHocsinh()
        {
            InitializeComponent();
            _serviceClient = new Service1Client();
            dgvDanhSachHocSinh.CellClick += dgvDanhSachHocSinh_CellClick;
            SetupFormControls();
        }

        public frmHocsinh(HocSinh hocSinhCanSua) : this()
        {
            _isEditMode = true;
            _hocSinhDangSua = hocSinhCanSua;
            HienThiThongTinHocSinh(hocSinhCanSua);
        }

        private void SetupFormControls()
        {
            dtpNgaySinh.Value = DateTime.Now.AddYears(-15);
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.AddRange(new[] { "Nam", "Nữ" });
            if (cboGioiTinh.Items.Count > 0)
                cboGioiTinh.SelectedIndex = 0;
            else
                cboGioiTinh.SelectedIndex = -1;

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new[] { "Đăng ký", "Hợp lệ", "Không hợp lệ", "Trúng tuyển", "Rớt" });
            cboTrangThai.SelectedIndex = 0;
        }

        private void HienThiThongTinHocSinh(HocSinh hs)
        {
            txtHo.Text = hs.Ho;
            txtTen.Text = hs.Ten;
            dtpNgaySinh.Value = hs.NgaySinh;
            cboGioiTinh.SelectedItem = hs.GioiTinh ?? "Nam";
            string truong = hs.TruongTHCS ?? "";
            if (!string.IsNullOrWhiteSpace(truong))
            {
                // Nếu danh sách hiện tại không có trường này thì thêm vào
                if (cboTruongTHCS.Items.Cast<string>().All(x => x != truong))
                {
                    cboTruongTHCS.Items.Add(truong);
                }
                cboTruongTHCS.SelectedItem = truong;
            }
            else
            {
                cboTruongTHCS.SelectedIndex = -1;
            }
            txtDanToc.Text = hs.DanToc;
            txtNoiSinh.Text = hs.NoiSinh;
            cboTrangThai.SelectedItem = HienThiTrangThai(hs.TrangThai);
            cboTrangThai.SelectedItem = HienThiTrangThai(hs.TrangThai);
            txtGhiChu.Text = hs.GhiChu;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }

        private string MaTrangThai(string hienThi)
        {
            switch (hienThi)
            {
                case "Đăng ký": return "DangKy";
                case "Hợp lệ": return "HopLe";
                case "Không hợp lệ": return "KhongHopLe";
                case "Trúng tuyển": return "TrungTuyen";
                case "Rớt": return "Rot";
                default: return "DangKy";
            }
        }


        private string HienThiTrangThai(string ma)
        {
            switch (ma)
            {
                case "DangKy": return "Đăng ký";
                case "HopLe": return "Hợp lệ";
                case "KhongHopLe": return "Không hợp lệ";
                case "TrungTuyen": return "Trúng tuyển";
                case "Rot": return "Rớt";
                default: return "Đăng ký";
            }
        }


        private decimal? ParseDecimal(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            return decimal.TryParse(text, out var value) ? (decimal?)value : null;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            txtHo.Clear();
            txtTen.Clear();
            dtpNgaySinh.Value = DateTime.Now.AddYears(-15);
            cboGioiTinh.SelectedIndex = 0;
            txtDanToc.Clear();
            txtNoiSinh.Clear();
            cboTruongTHCS.SelectedIndex =0;
            cboTrangThai.SelectedIndex = 0;
            txtGhiChu.Clear();
            txtHo.Focus();
            _isEditMode = false;
            _hocSinhDangSua = null;
            //btnThem.Text = "Thêm";
        }

        private void frmHocsinh_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_serviceClient.State == System.ServiceModel.CommunicationState.Opened)
                _serviceClient.Close();
        }

        // Load lại danh sách học sinh

        private void LoadDanhSachHocSinh()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Common.MaTruong))
                {
                    MessageBox.Show("Không tìm thấy mã trường, vui lòng đăng nhập lại.");
                    return;
                }
                string maDot = cboDotTuyen.SelectedValue?.ToString();
                if (string.IsNullOrEmpty(maDot))
                {
                    MessageBox.Show("Vui lòng chọn đợt tuyển sinh!");
                    return;
                }

                var ds = _serviceClient.LayDanhSachHocSinh(Common.MaTruong, maDot);
                _dsHocSinhGoc = ds.ToList();
                dgvDanhSachHocSinh.DataSource = null;
                dgvDanhSachHocSinh.DataSource = ds;
                CapNhatSoLuongHocSinh();
                NapDuLieuTruongTHCSVaoCombo(_dsHocSinhGoc);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }
        // Chức năng lọc 
        private void NapDuLieuTruongTHCSVaoCombo(IEnumerable<HocSinh> ds)
        {
            var truongs = _dsHocSinhGoc
    .Select(x => x.TruongTHCS)
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .Distinct()
    .ToList();
            truongs.Insert(0, "Tất cả");
            cboLocTruongTHCS.DataSource = truongs;
        }

        private bool KiemTraTenChuaTatCaTuKhoa(HocSinh hs, string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa)) return true;
            var arr = tuKhoa.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string fullName = (hs.Ho + " " + hs.Ten).ToLower();
            return arr.All(t => fullName.Contains(t));
        }

        private void frmHocsinh_Load(object sender, EventArgs e) {
            LoadDotTuyen();
            FormatDanhSachHocSinhGrid();
            LoadDanhSachHocSinh();
            btnLoc.Click += btnLoc_Click;
        }

        private void grpThongTin_Enter(object sender, EventArgs e)
        {

        }

        private void grpDiemThi_Enter(object sender, EventArgs e)
        {

        }

        // Cập nhật học sinh bằng excel

        private List<HocSinh> DocExcel_HocSinh(string filePath)
        {
            var danhSach = new List<HocSinh>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var ws = package.Workbook.Worksheets[0];
                int rowCount = ws.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    // Không đọc mã số báo danh ở file (bỏ qua cột 2)
                    if (string.IsNullOrWhiteSpace(ws.Cells[row, 3].Text) && string.IsNullOrWhiteSpace(ws.Cells[row, 4].Text))
                        continue;

                    var hs = new HocSinh
                    {
                        Ho = ws.Cells[row, 3].Text.Trim(),
                        Ten = ws.Cells[row, 4].Text.Trim(),
                        NgaySinh = DateTime.TryParse(ws.Cells[row, 5].Text, out DateTime ns) ? ns : DateTime.Now.AddYears(-15),
                        NoiSinh = ws.Cells[row, 6].Text.Trim(),
                        GioiTinh = ws.Cells[row, 7].Text.Trim(),
                        DanToc = ws.Cells[row, 8].Text.Trim(),
                        TruongTHCS = ws.Cells[row, 9].Text.Trim(),
                        MaTruong = Common.MaTruong,
                        MaDot = cboDotTuyen.SelectedValue?.ToString(),
                        TrangThai = "DangKy"
                    };
                    danhSach.Add(hs);
                }
            }
            return danhSach;
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            if (_formMain != null)
            {
                _formMain.Show();
            }

            this.Close();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void excelpic_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn file Excel danh sách học sinh";
                ofd.Filter = "Excel Files|*.xlsx;*.xls";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;
                    var danhSach = DocExcel_HocSinh(filePath);

                    if (danhSach.Count == 0)
                    {
                        MessageBox.Show("File Excel không có dữ liệu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Hiển thị lên DataGridView cho kiểm tra lại (có thể bỏ nếu muốn import luôn)
                    dgvDanhSachHocSinh.DataSource = danhSach;

                    // Hỏi xác nhận import
                    if (MessageBox.Show(
                        $"Bạn có chắc muốn nhập {danhSach.Count} học sinh vào hệ thống?\n" +
                        "Dữ liệu sẽ được lưu ngay vào cơ sở dữ liệu.",
                        "Xác nhận nhập từ Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int countThanhCong = 0;
                        int countLoi = 0;
                        List<string> danhSachLoi = new List<string>();

                        foreach (var hs in danhSach)
                        {
                            try
                            {
                                var result = _serviceClient.ThemHocSinh(hs);
                                if (result != null && result.ThanhCong)
                                    countThanhCong++;
                                else
                                {
                                    countLoi++;
                                    danhSachLoi.Add($"{hs.Ho} {hs.Ten}: {result?.ThongBao ?? "Lỗi không xác định"}");
                                }
                            }
                            catch (Exception ex)
                            {
                                countLoi++;
                                danhSachLoi.Add($"{hs.Ho} {hs.Ten}: {ex.Message}");
                            }
                        }

                        string thongBao = $"Nhập thành công {countThanhCong}/{danhSach.Count} học sinh.";
                        if (countThanhCong > 0)
                        {
                            LoadDanhSachHocSinh(); // Tự động reload sau import
                        }

                        if (countLoi > 0)
                        {
                            thongBao += $"\n{countLoi} học sinh lỗi:\n" + string.Join("\n", danhSachLoi);
                        }
                        MessageBox.Show(thongBao, "Kết quả nhập Excel", MessageBoxButtons.OK,
                            countLoi == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            LoadDanhSachHocSinh();
            MessageBox.Show("Đã gọi load lại danh sách!"); // Test xem có click vào không
        }

        private void PicThemMSBD_Click(object sender, EventArgs e)
        {
            if (cboDotTuyen.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đợt tuyển sinh!");
                return;
            }

            string maTruong = Common.MaTruong;
            string maDot = cboDotTuyen.SelectedValue.ToString();

            try
            {
                _serviceClient.GanMaSoBaoDanhHangLoat(maTruong, maDot);
                MessageBox.Show("Đã gán số báo danh cho học sinh chưa có số báo danh!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachHocSinh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi gán số báo danh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sửa thông tin học sinh
        private HocSinh TaoBanSaoHocSinhDaCapNhat()
        {
            // Lấy thông tin gốc
            var hsCu = _hocSinhDangSua;
            var hsMoi = new HocSinh
            {
                MaHocSinh = hsCu.MaHocSinh,
                MaSoBaoDanh = hsCu.MaSoBaoDanh,
                MaTruong = hsCu.MaTruong,
                MaDot = hsCu.MaDot,
                // So sánh từng trường, nếu khác thì lấy dữ liệu mới, nếu không thì giữ dữ liệu cũ
                Ho = txtHo.Text.Trim() != hsCu.Ho ? txtHo.Text.Trim() : hsCu.Ho,
                Ten = txtTen.Text.Trim() != hsCu.Ten ? txtTen.Text.Trim() : hsCu.Ten,
                NgaySinh = dtpNgaySinh.Value != hsCu.NgaySinh ? dtpNgaySinh.Value : hsCu.NgaySinh,
                GioiTinh = cboGioiTinh.SelectedItem?.ToString() != hsCu.GioiTinh ? cboGioiTinh.SelectedItem?.ToString() : hsCu.GioiTinh,
                DanToc = txtDanToc.Text.Trim() != hsCu.DanToc ? txtDanToc.Text.Trim() : hsCu.DanToc,
                NoiSinh = txtNoiSinh.Text.Trim() != hsCu.NoiSinh ? txtNoiSinh.Text.Trim() : hsCu.NoiSinh,
                TruongTHCS = cboTruongTHCS.SelectedItem?.ToString() != hsCu.TruongTHCS ? cboTruongTHCS.SelectedItem?.ToString() : hsCu.TruongTHCS,
                TrangThai = MaTrangThai(cboTrangThai.SelectedItem?.ToString()) != hsCu.TrangThai ? MaTrangThai(cboTrangThai.SelectedItem?.ToString()) : hsCu.TrangThai,
                GhiChu = txtGhiChu.Text.Trim() != hsCu.GhiChu ? txtGhiChu.Text.Trim() : hsCu.GhiChu
            };
            return hsMoi;
        }

        private bool Khac(string a, string b) => (a ?? "") != (b ?? "");

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            var selectedMaDot = cboDotTuyen.SelectedValue?.ToString();
            MessageBox.Show("MaDot chọn: " + selectedMaDot);

            if (string.IsNullOrWhiteSpace(txtHo.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên học sinh", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHo.Focus();
                return;
            }

            try
            {
                if (cboGioiTinh.SelectedIndex < 0) cboGioiTinh.SelectedIndex = 0;
                if (cboTrangThai.SelectedIndex < 0) cboTrangThai.SelectedIndex = 0;

                if (_isEditMode)
                {
                    var cu = _hocSinhDangSua;
                    var moi = new HocSinh
                    {
                        MaHocSinh = cu.MaHocSinh,
                        MaSoBaoDanh = cu.MaSoBaoDanh,
                        MaTruong = cu.MaTruong,
                        MaDot = cu.MaDot,
                        Ho = Khac(txtHo.Text.Trim(), cu.Ho) ? txtHo.Text.Trim() : cu.Ho,
                        Ten = Khac(txtTen.Text.Trim(), cu.Ten) ? txtTen.Text.Trim() : cu.Ten,
                        NgaySinh = dtpNgaySinh.Value != cu.NgaySinh ? dtpNgaySinh.Value : cu.NgaySinh,
                        GioiTinh = Khac(cboGioiTinh.SelectedItem?.ToString(), cu.GioiTinh) ? cboGioiTinh.SelectedItem?.ToString() : cu.GioiTinh,
                        DanToc = Khac(txtDanToc.Text.Trim(), cu.DanToc) ? txtDanToc.Text.Trim() : cu.DanToc,
                        NoiSinh = Khac(txtNoiSinh.Text.Trim(), cu.NoiSinh) ? txtNoiSinh.Text.Trim() : cu.NoiSinh,
                        TruongTHCS = Khac(cboTruongTHCS.SelectedItem?.ToString(), cu.TruongTHCS) ? cboTruongTHCS.SelectedItem?.ToString() : cu.TruongTHCS,
                        TrangThai = Khac(MaTrangThai(cboTrangThai.SelectedItem?.ToString()), cu.TrangThai) ? MaTrangThai(cboTrangThai.SelectedItem?.ToString()) : cu.TrangThai,
                        GhiChu = Khac(txtGhiChu.Text.Trim(), cu.GhiChu) ? txtGhiChu.Text.Trim() : cu.GhiChu
                    };

                    bool thanhCong = _serviceClient.CapNhatHocSinh(moi);

                    if (thanhCong)
                    {
                        MessageBox.Show("Cập nhật học sinh thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDanhSachHocSinh();
                        ResetForm();
                        _isEditMode = false;
                        _hocSinhDangSua = null;
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại. Vui lòng kiểm tra dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // *** XỬ LÝ THÊM MỚI ***
                    var hocSinhMoi = new HocSinh
                    {
                        Ho = txtHo.Text.Trim(),
                        Ten = txtTen.Text.Trim(),
                        NgaySinh = dtpNgaySinh.Value,
                        GioiTinh = cboGioiTinh.SelectedItem?.ToString(),
                        DanToc = string.IsNullOrWhiteSpace(txtDanToc.Text) ? null : txtDanToc.Text.Trim(),
                        NoiSinh = string.IsNullOrWhiteSpace(txtNoiSinh.Text) ? null : txtNoiSinh.Text.Trim(),
                        TruongTHCS = cboTruongTHCS.SelectedItem?.ToString(),
                        MaTruong = Common.MaTruong,
                        TrangThai = MaTrangThai(cboTrangThai.SelectedItem?.ToString()),
                        GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim(),
                        MaDot = cboDotTuyen.SelectedValue?.ToString(),
                    };

                    var result = _serviceClient.ThemHocSinh(hocSinhMoi);
                    if (result != null && result.ThanhCong)
                    {
                        MessageBox.Show($"Thêm học sinh thành công! Mã số: {result.HocSinh.MaSoBaoDanh}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetForm();
                        LoadDanhSachHocSinh();
                    }
                    else
                    {
                        MessageBox.Show(result?.ThongBao ?? "Lỗi không xác định!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        // Phần sửa học sinh chọn học sinh để sửa
        private void dgvDanhSachHocSinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvDanhSachHocSinh.Rows[e.RowIndex];
            if (row.DataBoundItem is HocSinh hs)
            {
                _isEditMode = true;            
                _hocSinhDangSua = hs;           
                HienThiThongTinHocSinh(hs);     

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Kiểm tra có đang ở chế độ sửa không
            if (!_isEditMode || _hocSinhDangSua == null)
            {
                MessageBox.Show("Vui lòng chọn học sinh cần sửa!");
                return;
            }

            // Lấy dữ liệu vừa sửa từ các textbox
            var hocSinh = new HocSinh
            {
                MaHocSinh = _hocSinhDangSua.MaHocSinh, 
                Ho = txtHo.Text.Trim(),
                Ten = txtTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = cboGioiTinh.SelectedItem?.ToString(),
                DanToc = txtDanToc.Text.Trim(),
                NoiSinh = txtNoiSinh.Text.Trim(),
                TruongTHCS = cboTruongTHCS.SelectedItem?.ToString(),
                MaTruong = _hocSinhDangSua.MaTruong, // Không cho sửa mã trường
                MaDot = _hocSinhDangSua.MaDot,       // Không cho sửa mã đợt
                TrangThai = MaTrangThai(cboTrangThai.SelectedItem?.ToString()),
                GhiChu = txtGhiChu.Text.Trim(),
            };

            // Gọi service để cập nhật
            bool result = _serviceClient.CapNhatHocSinh(hocSinh);

            if (result)
            {
                MessageBox.Show("Cập nhật thông tin học sinh thành công!");
                LoadDanhSachHocSinh();   
                ResetForm();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại. Vui lòng kiểm tra lại dữ liệu.");
            }
        }

        private void pictureBoxXoa_Click(object sender, EventArgs e)
        {
            if (_hocSinhDangSua == null)
            {
                MessageBox.Show("Vui lòng chọn học sinh cần xóa!");
                return;
            }

            // Hỏi xác nhận trước khi xóa
            var confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa học sinh này không?\nThao tác này không thể hoàn tác.",
                "Xác nhận xóa học sinh",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                bool xoaThanhCong = _serviceClient.XoaHocSinh(_hocSinhDangSua.MaHocSinh);

                if (xoaThanhCong)
                {
                    MessageBox.Show("Đã xóa học sinh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachHocSinh();
                    ResetForm();
                    _hocSinhDangSua = null;
                    _isEditMode = false;
                }
                else
                {
                    MessageBox.Show("Xóa không thành công. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Phần chỉnh dgv sao cho đẹp
        private void FormatDanhSachHocSinhGrid()
        {
            var dgv = dgvDanhSachHocSinh;
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
            if (dgv.Columns.Contains("MaTruong")) dgv.Columns["MaTruong"].Visible = false;
            if (dgv.Columns.Contains("MaDot")) dgv.Columns["MaDot"].Visible = false;

            // Đặt tên cột
            var map = new Dictionary<string, string>
    {
        { "Ho", "Họ" }, { "Ten", "Tên" }, { "NgaySinh", "Ngày sinh" }, { "GioiTinh", "Giới tính" },
        { "DanToc", "Dân tộc" }, { "NoiSinh", "Nơi sinh" }, { "TruongTHCS", "Trường THCS" },
        { "MaSoBaoDanh", "Số báo danh" }, { "DiemToan", "Toán" }, { "DiemVan", "Văn" },
        { "DiemAnh", "Anh" }, { "DiemKhuyenKhich", "Khuyến khích" }, { "DiemUuTien", "Ưu tiên" },
        { "DiemTong", "Tổng điểm" }, { "TrangThai", "Trạng thái" }, { "GhiChu", "Ghi chú" }
    };
            foreach (var m in map)
                if (dgv.Columns.Contains(m.Key))
                    dgv.Columns[m.Key].HeaderText = m.Value;

            // Căn lề giữa cho điểm và ngày sinh
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.HeaderText.Contains("điểm", StringComparison.OrdinalIgnoreCase) || col.HeaderText.Contains("Ngày"))
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                else
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {

            string tenTim = txtTimTen.Text.Trim();
            string truongTHCS = cboLocTruongTHCS.SelectedItem?.ToString();
            decimal? diemTu = numDiemTu.Value > 0 ? numDiemTu.Value : (decimal?)null;
            decimal? diemDen = numDiemDen.Value > 0 ? numDiemDen.Value : (decimal?)null;

            var ketQua = _dsHocSinhGoc.Where(hs =>
                // 1. Lọc chính xác theo trường "Ten"
                (string.IsNullOrWhiteSpace(tenTim) ||
                 string.Equals(hs.Ten, tenTim, StringComparison.OrdinalIgnoreCase))
                // 2. Lọc trường THCS nếu có chọn
                && (string.IsNullOrWhiteSpace(truongTHCS) || truongTHCS == "Tất cả" || hs.TruongTHCS == truongTHCS)
                // 3. Lọc điểm
                && (!diemTu.HasValue || (hs.DiemTong ?? -999) >= diemTu)
                && (!diemDen.HasValue || (hs.DiemTong ?? 999) <= diemDen)
            ).ToList();

            dgvDanhSachHocSinh.DataSource = ketQua;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        // Cập nhật số lượng học sinh có trong datagridview
        private void CapNhatSoLuongHocSinh()
        {
            int soLuong = 0;
            if (dgvDanhSachHocSinh.DataSource is List<HocSinh> ds)
                soLuong = ds.Count;
            else if (dgvDanhSachHocSinh.DataSource is System.Collections.ICollection coll)
                soLuong = coll.Count;
            lblSoLuongHocSinh.Text = $"Tổng số học sinh có trong danh sách: {soLuong} học sinh";
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dgvDanhSachHocSinh_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtGhiChu_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
