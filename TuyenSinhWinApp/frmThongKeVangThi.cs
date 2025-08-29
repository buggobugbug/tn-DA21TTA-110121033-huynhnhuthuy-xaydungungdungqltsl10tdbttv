using OfficeOpenXml;
using EppLicenseContext = OfficeOpenXml.LicenseContext;
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
    public partial class frmThongKeVangThi : Form
    {
        private readonly Service1Client _service = new Service1Client();
        private DataTable _dtView;
        public frmThongKeVangThi()
        {
            InitializeComponent();
            dgvVang.AutoGenerateColumns = true;
            dgvVang.CellFormatting += dgvVang_CellFormatting;
        }

        private void frmThongKeVangThi_Load(object sender, EventArgs e)
        {
            NapDanhSachDotTuyen();
            LoadTruongForAdmin();
            TaiDuLieu();
            txtTim.TextChanged += (s, ev) => ApplyFilter();
        }

        private void LoadTruongForAdmin()
        {
            if (!Common.IsAdmin)
            {
                lblTruong.Visible = false;  // label “Trường:”
                cbTruong.Visible = false;  // combobox chọn trường
                return;
            }

            lblTruong.Visible = true;
            cbTruong.Visible = true;

            var ds = _service.Admin_LayDanhSachTruong()?.ToList() ?? new List<TruongItem>();
            // Dòng đầu là “Tất cả”
            ds.Insert(0, new TruongItem { MaTruong = null, TenTruong = "— Tất cả trường —" });

            cbTruong.DisplayMember = "TenTruong";
            cbTruong.ValueMember = "MaTruong";
            cbTruong.DataSource = ds;

            cbTruong.SelectedIndexChanged -= cbTruong_SelectedIndexChanged;
            cbTruong.SelectedIndexChanged += cbTruong_SelectedIndexChanged;
        }


        // Lấy mã trường để truyền vào service (đã xử lý admin/toàn tỉnh)
        private string GetMaTruongFilter()
        {
            if (Common.IsAdmin)
                return cbTruong.SelectedValue as string; // có thể null (tức toàn tỉnh)
            return Common.MaTruong; // cán bộ trường / thư ký
        }

        private void frmThongKeVangThi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_service.State == System.ServiceModel.CommunicationState.Opened)
                _service.Close();
        }

        private void NapDanhSachDotTuyen()
        {
            try
            {
                var dsDot = _service.LayDanhSachDotTuyen();
                cbDotTuyenSinh.DataSource = dsDot;
                cbDotTuyenSinh.DisplayMember = "TenDot";
                cbDotTuyenSinh.ValueMember = "MaDot";
                if (dsDot != null && dsDot.Length > 0)
                    cbDotTuyenSinh.SelectedValue = Common.MaDot;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách đợt: " + ex.Message);
            }
        }

        private string MaDotChon() =>
            cbDotTuyenSinh.SelectedValue?.ToString() ?? Common.MaDot;

        private void cbDotTuyenSinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsHandleCreated) return;
            Common.MaDot = MaDotChon();
            TaiDuLieu();
        }

        private void btnTai_Click(object sender, EventArgs e) => TaiDuLieu();

        private void TaiDuLieu()
        {
            try
            {
                var maDot = MaDotChon();
                var maTruongFilter = GetMaTruongFilter();

                var rs = _service.ThongKeVangThi(
                    maDot,
                    string.IsNullOrEmpty(maTruongFilter) ? null : maTruongFilter
                );

                var t = rs?.TongHop;
                lblTongVangAny.Text = $"Vắng ≥1 môn: {t?.TongVangAny ?? 0}";
                lblVangToan.Text = $"Vắng Toán: {t?.VangToan ?? 0}";
                lblVangVan.Text = $"Vắng Văn: {t?.VangVan ?? 0}";
                lblVangAnh.Text = $"Vắng Anh: {t?.VangAnh ?? 0}";

                BuildGrid(rs?.DanhSach ?? Array.Empty<VangThiHocSinhItem>());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu vắng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BuildGrid(VangThiHocSinhItem[] arr)
        {
            // dựng DataTable nguồn
            _dtView = new DataTable();
            _dtView.Columns.Add("MaHocSinh", typeof(int));
            _dtView.Columns.Add("SBD");
            _dtView.Columns.Add("Họ và tên");
            _dtView.Columns.Add("THCS");
            _dtView.Columns.Add("Phòng");
            _dtView.Columns.Add("Vắng Toán", typeof(bool));
            _dtView.Columns.Add("Vắng Văn", typeof(bool));
            _dtView.Columns.Add("Vắng Anh", typeof(bool));
            _dtView.Columns.Add("Vắng bất kỳ", typeof(bool));

            foreach (var x in arr)
            {
                _dtView.Rows.Add(
                    x.MaHocSinh,
                    x.MaSoBaoDanh,
                    $"{x.Ho} {x.Ten}".Trim(),
                    x.TruongTHCS,
                    x.PhongThi,
                    x.VangToan, x.VangVan, x.VangAnh, x.VangAny
                );
            }

            // bind
            dgvVang.DataSource = _dtView;

            // cấu hình hiển thị/lề/chế độ fill
            ConfigureGridLookAndFill();

            ApplyFilter();
        }

        private void ConfigureGridLookAndFill()
        {
            // tổng quan
            dgvVang.ReadOnly = true;
            dgvVang.RowHeadersVisible = false;
            dgvVang.AllowUserToAddRows = false;
            dgvVang.AllowUserToResizeRows = false;
            dgvVang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVang.MultiSelect = false;

            dgvVang.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvVang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvVang.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            // Ẩn khoá chính
            if (dgvVang.Columns.Contains("MaHocSinh"))
                dgvVang.Columns["MaHocSinh"].Visible = false;

            // fill toàn bộ chiều rộng grid
            dgvVang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVang.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            // phân bổ tỉ trọng (FillWeight) cho từng cột
            SetFill("SBD", 10, DataGridViewContentAlignment.MiddleCenter, 70);
            SetFill("Họ và tên", 28, DataGridViewContentAlignment.MiddleLeft, 160);
            SetFill("THCS", 26, DataGridViewContentAlignment.MiddleLeft, 150);
            SetFill("Phòng", 10, DataGridViewContentAlignment.MiddleCenter, 80);
            SetFill("Vắng Toán", 6, DataGridViewContentAlignment.MiddleCenter, 75);
            SetFill("Vắng Văn", 6, DataGridViewContentAlignment.MiddleCenter, 75);
            SetFill("Vắng Anh", 6, DataGridViewContentAlignment.MiddleCenter, 75);
            SetFill("Vắng bất kỳ", 8, DataGridViewContentAlignment.MiddleCenter, 95);

            // checkbox cột vắng – đảm bảo hiện dạng checkbox & chỉ đọc
            string[] checkCols = { "Vắng Toán", "Vắng Văn", "Vắng Anh", "Vắng bất kỳ" };
            foreach (var name in checkCols)
            {
                if (dgvVang.Columns[name] is DataGridViewCheckBoxColumn ckb)
                {
                    ckb.ThreeState = false;
                    ckb.ReadOnly = true;
                    ckb.FlatStyle = FlatStyle.Standard;
                }
            }
        }

        private void SetFill(string columnName, float fillWeight,
                             DataGridViewContentAlignment align, int minWidth)
        {
            if (!dgvVang.Columns.Contains(columnName)) return;
            var col = dgvVang.Columns[columnName];

            col.FillWeight = fillWeight;            // tỉ trọng (tổng tuỳ bạn đặt)
            col.MinimumWidth = minWidth;            // đề phòng form thu nhỏ quá
            col.DefaultCellStyle.Alignment = align; // canh lề
        }


        private void dgvVang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvVang.Columns[e.ColumnIndex].HeaderText == "Phòng" && e.Value != null)
            {
                var v = e.Value.ToString();
                if (v.Contains("_")) e.Value = v.Split('_')[0];
            }
        }

        private void ApplyFilter()
        {
            if (_dtView == null) return;
            var kw = (txtTim.Text ?? "").Trim().Replace("'", "''");
            _dtView.DefaultView.RowFilter = string.IsNullOrEmpty(kw)
                ? ""
                : $"[SBD] LIKE '%{kw}%' OR [Họ và tên] LIKE '%{kw}%' OR [THCS] LIKE '%{kw}%' OR [Phòng] LIKE '%{kw}%'";
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (_dtView == null || _dtView.DefaultView.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.");
                return;
            }

            using (var sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = $"VangThi_{Common.MaTruong}_{MaDotChon()}_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;

                ExcelPackage.LicenseContext = EppLicenseContext.NonCommercial;
                using (var pkg = new OfficeOpenXml.ExcelPackage())
                {
                    // Sheet 1: Danh sách
                    var ws = pkg.Workbook.Worksheets.Add("Danh sách vắng");
                    ws.Cells["A1"].Value = $"Thống kê vắng thi - Trường {Common.TenTruong}";
                    ws.Cells["A2"].Value = $"Đợt: {MaDotChon()}   Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}";
                    ws.Cells["A1:A2"].Style.Font.Bold = true;

                    var tbl = _dtView.DefaultView.ToTable(); // theo filter hiện tại
                    ws.Cells["A4"].LoadFromDataTable(tbl, true);
                    ws.Cells.AutoFitColumns();

                    // Sheet 2: Tổng hợp
                    var rs = _service.ThongKeVangThi(MaDotChon(), Common.MaTruong);
                    var ws2 = pkg.Workbook.Worksheets.Add("Tổng hợp");
                    ws2.Cells["A1"].Value = "Vắng ≥1 môn"; ws2.Cells["B1"].Value = rs?.TongHop?.TongVangAny ?? 0;
                    ws2.Cells["A2"].Value = "Vắng Toán"; ws2.Cells["B2"].Value = rs?.TongHop?.VangToan ?? 0;
                    ws2.Cells["A3"].Value = "Vắng Văn"; ws2.Cells["B3"].Value = rs?.TongHop?.VangVan ?? 0;
                    ws2.Cells["A4"].Value = "Vắng Anh"; ws2.Cells["B4"].Value = rs?.TongHop?.VangAnh ?? 0;
                    ws2.Cells.AutoFitColumns();

                    pkg.SaveAs(new System.IO.FileInfo(sfd.FileName));
                }

                MessageBox.Show("Đã xuất Excel.");
            }
        }

        private void lblVangToan_Click(object sender, EventArgs e)
        {

        }

        private void cbTruong_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaiDuLieu();
        }
    }
}
