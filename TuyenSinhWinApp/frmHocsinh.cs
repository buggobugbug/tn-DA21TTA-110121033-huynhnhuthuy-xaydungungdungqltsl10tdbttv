using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TuyenSinhServiceLib;
using TuyenSinhWinApp.TuyenSinhServiceReference;

namespace TuyenSinhWinApp
{
    public partial class frmHocsinh : Form
    {
        private readonly Service1Client _serviceClient;
        private bool _isEditMode = false;
        private HocSinh _hocSinhDangSua = null;
        private List<DotTuyenSinh> danhSachDotTuyen;

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
        }

        public frmHocsinh()
        {
            InitializeComponent();
            _serviceClient = new Service1Client();
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
            cboGioiTinh.Items.AddRange(new[] { "Nam", "Nữ" });
            cboGioiTinh.SelectedIndex = 0;

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new[] { "Đăng ký", "Hợp lệ", "Không hợp lệ", "Trúng tuyển", "Rớt" });
            cboTrangThai.SelectedIndex = 0;

            txtGhiChu.Multiline = true;
            txtGhiChu.ScrollBars = ScrollBars.Vertical;
            txtGhiChu.Height = 80;
        }

        private void HienThiThongTinHocSinh(HocSinh hs)
        {
            txtHoTen.Text = hs.HoTen;
            dtpNgaySinh.Value = hs.NgaySinh;
            cboGioiTinh.SelectedItem = hs.GioiTinh ?? "Nam";
            txtDanToc.Text = hs.DanToc;
            txtNoiSinh.Text = hs.NoiSinh;
            txtTruongTHCS.Text = hs.TruongTHCS;
            txtDiemToan.Text = hs.DiemToan?.ToString();
            txtDiemVan.Text = hs.DiemVan?.ToString();
            txtDiemAnh.Text = hs.DiemAnh?.ToString();
            txtDiemKK.Text = hs.DiemKhuyenKhich?.ToString();
            txtDiemUT.Text = hs.DiemUuTien?.ToString();
            txtPhongThi.Text = hs.PhongThi;
            cboTrangThai.SelectedItem = HienThiTrangThai(hs.TrangThai);
            txtGhiChu.Text = hs.GhiChu;
            btnThem.Text = "Cập nhật";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var selectedMaDot = cboDotTuyen.SelectedValue?.ToString();
            MessageBox.Show("MaDot chọn: " + selectedMaDot);

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên học sinh", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            try
            {
                if (cboGioiTinh.SelectedIndex < 0) cboGioiTinh.SelectedIndex = 0;
                if (cboTrangThai.SelectedIndex < 0) cboTrangThai.SelectedIndex = 0;

                var hocSinh = new HocSinh
                {
                    HoTen = txtHoTen.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = cboGioiTinh.SelectedItem.ToString(),
                    DanToc = string.IsNullOrWhiteSpace(txtDanToc.Text) ? null : txtDanToc.Text.Trim(),
                    NoiSinh = string.IsNullOrWhiteSpace(txtNoiSinh.Text) ? null : txtNoiSinh.Text.Trim(),
                    TruongTHCS = string.IsNullOrWhiteSpace(txtTruongTHCS.Text) ? null : txtTruongTHCS.Text.Trim(),
                    MaTruong = Common.MaTruong,
                    DiemToan = ParseDecimal(txtDiemToan.Text),
                    DiemVan = ParseDecimal(txtDiemVan.Text),
                    DiemAnh = ParseDecimal(txtDiemAnh.Text),
                    DiemKhuyenKhich = ParseDecimal(txtDiemKK.Text),
                    DiemUuTien = ParseDecimal(txtDiemUT.Text),
                    PhongThi = string.IsNullOrWhiteSpace(txtPhongThi.Text) ? null : txtPhongThi.Text.Trim(),
                    TrangThai = MaTrangThai(cboTrangThai.SelectedItem?.ToString()),
                    GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim(),
                    MaDot = cboDotTuyen.SelectedValue?.ToString(),
                };

                if (_isEditMode)
                {
                    hocSinh.MaHocSinh = _hocSinhDangSua.MaHocSinh;
                    hocSinh.MaSoBaoDanh = _hocSinhDangSua.MaSoBaoDanh;

                    bool thanhCong = _serviceClient.CapNhatHocSinh(hocSinh);

                    if (thanhCong)
                    {
                        MessageBox.Show("Cập nhật học sinh thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại. Vui lòng kiểm tra dữ liệu.", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    var result = _serviceClient.ThemHocSinh(hocSinh);
                    if (result.ThanhCong)
                    {
                        MessageBox.Show($"Thêm học sinh thành công! Mã số: {result.HocSinh.MaSoBaoDanh}",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show(result.ThongBao, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
            txtHoTen.Clear();
            dtpNgaySinh.Value = DateTime.Now.AddYears(-15);
            cboGioiTinh.SelectedIndex = 0;
            txtDanToc.Clear();
            txtNoiSinh.Clear();
            txtTruongTHCS.Clear();
            txtDiemToan.Clear();
            txtDiemVan.Clear();
            txtDiemAnh.Clear();
            txtDiemKK.Clear();
            txtDiemUT.Clear();
            txtPhongThi.Clear();
            cboTrangThai.SelectedIndex = 0;
            txtGhiChu.Clear();
            txtHoTen.Focus();

            _isEditMode = false;
            _hocSinhDangSua = null;
            btnThem.Text = "Thêm";
        }

        private void frmHocsinh_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_serviceClient.State == System.ServiceModel.CommunicationState.Opened)
                _serviceClient.Close();
        }

        private void frmHocsinh_Load(object sender, EventArgs e) {
            LoadDotTuyen();
        }
    }
}
