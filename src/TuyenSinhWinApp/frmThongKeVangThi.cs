using OfficeOpenXml;
using EppLicenseContext = OfficeOpenXml.LicenseContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
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
                lblTruong.Visible = false;
                cbTruong.Visible = false;
                return;
            }

            lblTruong.Visible = true;
            cbTruong.Visible = true;

            var ds = _service.Admin_LayDanhSachTruong()?.ToList() ?? new List<TruongItem>();
            ds.Insert(0, new TruongItem { MaTruong = null, TenTruong = "— Tất cả trường —" });

            cbTruong.DisplayMember = "TenTruong";
            cbTruong.ValueMember = "MaTruong";
            cbTruong.DataSource = ds;

            cbTruong.SelectedIndexChanged -= cbTruong_SelectedIndexChanged;
            cbTruong.SelectedIndexChanged += cbTruong_SelectedIndexChanged;
        }

        private string GetMaTruongFilter()
        {
            if (Common.IsAdmin)
                return cbTruong.SelectedValue as string;
            return Common.MaTruong;
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
                lblVangAnh.Text = $"Vắng Môn 3: {t?.VangAnh ?? 0}";

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
            _dtView = new DataTable();
            _dtView.Columns.Add("MaHocSinh", typeof(int));
            _dtView.Columns.Add("SBD");
            _dtView.Columns.Add("Họ và tên");
            _dtView.Columns.Add("THCS");
            _dtView.Columns.Add("Phòng");
            _dtView.Columns.Add("Vắng Toán", typeof(bool));
            _dtView.Columns.Add("Vắng Văn", typeof(bool));
            _dtView.Columns.Add("Vắng Môn 3", typeof(bool));

            foreach (var x in arr)
            {
                _dtView.Rows.Add(
                    x.MaHocSinh,
                    x.MaSoBaoDanh,
                    $"{x.Ho} {x.Ten}".Trim(),
                    x.TruongTHCS,
                    x.PhongThi,
                    x.VangToan, x.VangVan, x.VangAnh
                );
            }

            dgvVang.DataSource = _dtView;
            ConfigureGridLookAndFill();
            ApplyFilter();
        }

        private void ConfigureGridLookAndFill()
        {
            dgvVang.ReadOnly = true;
            dgvVang.RowHeadersVisible = false;
            dgvVang.AllowUserToAddRows = false;
            dgvVang.AllowUserToResizeRows = false;
            dgvVang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVang.MultiSelect = false;

            dgvVang.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvVang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvVang.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            if (dgvVang.Columns.Contains("MaHocSinh"))
                dgvVang.Columns["MaHocSinh"].Visible = false;

            dgvVang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVang.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            SetFill("SBD", 12, DataGridViewContentAlignment.MiddleCenter, 70);
            SetFill("Họ và tên", 30, DataGridViewContentAlignment.MiddleLeft, 160);
            SetFill("THCS", 26, DataGridViewContentAlignment.MiddleLeft, 150);
            SetFill("Phòng", 10, DataGridViewContentAlignment.MiddleCenter, 80);
            SetFill("Vắng Toán", 7, DataGridViewContentAlignment.MiddleCenter, 75);
            SetFill("Vắng Văn", 7, DataGridViewContentAlignment.MiddleCenter, 75);
            SetFill("Vắng Môn 3", 8, DataGridViewContentAlignment.MiddleCenter, 95);

            string[] checkCols = { "Vắng Toán", "Vắng Văn", "Vắng Môn 3" };
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
            col.FillWeight = fillWeight;
            col.MinimumWidth = minWidth;
            col.DefaultCellStyle.Alignment = align;
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

            var view = _dtView.DefaultView.ToTable();
            var rows = view.AsEnumerable()
                           .Where(r => (r.Field<bool?>("Vắng Toán") ?? false)
                                    || (r.Field<bool?>("Vắng Văn") ?? false)
                                    || (r.Field<bool?>("Vắng Môn 3") ?? false))
                           .ToList();
            if (rows.Count == 0)
            {
                MessageBox.Show("Không có học sinh vắng thi.");
                return;
            }

            using (var sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = $"DanhSach_VangThi_{GetHeaderTenTruong()}_{MaDotChon()}_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;

                ExcelPackage.LicenseContext = EppLicenseContext.NonCommercial;
                using (var pkg = new ExcelPackage())
                {
                    var ws = pkg.Workbook.Worksheets.Add("Vắng thi");

                    ws.Cells.Style.Font.Name = "Times New Roman";
                    ws.Cells.Style.Font.Size = 12;
                    ws.PrinterSettings.Orientation = eOrientation.Portrait;
                    ws.PrinterSettings.TopMargin = 0.5m;
                    ws.PrinterSettings.BottomMargin = 0.5m;
                    ws.PrinterSettings.LeftMargin = 0.5m;
                    ws.PrinterSettings.RightMargin = 0.5m;

                    string tenTruong = GetHeaderTenTruong();
                    var dsDot = _service.LayDanhSachDotTuyen();
                    var dot = dsDot?.FirstOrDefault(x => x.MaDot == MaDotChon());
                    string tenDot = dot?.TenDot ?? MaDotChon();
                    DateTime? khoaNgay = dot?.NgayBatDau;

                    ws.Cells["A1:H1"].Merge = true;
                    ws.Cells["A1"].Value = "Sở GD & ĐT Trà Vinh";
                    ws.Cells["A1"].Style.Font.Bold = true;

                    ws.Cells["A2:H2"].Merge = true;
                    ws.Cells["A2"].Value = "Trường: " + tenTruong;

                    ws.Cells["A3:H3"].Merge = true;
                    ws.Cells["A3"].Value = "Kỳ Thi Tuyển Sinh Lớp 10";

                    ws.Cells["A4:H4"].Merge = true;
                    ws.Cells["A4"].Value = "Đợt: " + tenDot + (khoaNgay.HasValue ? $" — {khoaNgay:dd/MM/yyyy}" : "");

                    ws.Cells["A6:H6"].Merge = true;
                    ws.Cells["A6"].Value = "DANH SÁCH HỌC SINH VẮNG THI";
                    ws.Cells["A6"].Style.Font.Size = 14;
                    ws.Cells["A6"].Style.Font.Bold = true;
                    ws.Cells["A6"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    int headerRow = 8;

                    ws.Cells[headerRow, 1].Value = "STT";
                    ws.Cells[headerRow, 2].Value = "SBD";
                    ws.Cells[headerRow, 3].Value = "Họ";
                    ws.Cells[headerRow, 4].Value = "Tên";
                    ws.Cells[headerRow, 5].Value = "Môn thi";
                    ws.Cells[headerRow, 5, headerRow, 7].Merge = true;
                    ws.Cells[headerRow + 1, 5].Value = "Toán";
                    ws.Cells[headerRow + 1, 6].Value = "Văn";
                    ws.Cells[headerRow + 1, 7].Value = "Môn 3";
                    ws.Cells[headerRow, 8].Value = "Ghi chú";

                    ws.Cells[headerRow, 1, headerRow + 1, 1].Merge = true;
                    ws.Cells[headerRow, 2, headerRow + 1, 2].Merge = true;
                    ws.Cells[headerRow, 3, headerRow + 1, 3].Merge = true;
                    ws.Cells[headerRow, 4, headerRow + 1, 4].Merge = true;
                    ws.Cells[headerRow, 8, headerRow + 1, 8].Merge = true;

                    using (var rng = ws.Cells[headerRow, 1, headerRow + 1, 8])
                    {
                        rng.Style.Font.Bold = true;
                        rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        rng.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    int r = headerRow + 2;
                    int stt = 1;
                    foreach (var row in rows)
                    {
                        var sbd = row.Field<string>("SBD") ?? "";
                        var hoten = (row.Field<string>("Họ và tên") ?? "").Trim();
                        string ho = "";
                        string ten = "";
                        if (!string.IsNullOrEmpty(hoten))
                        {
                            var parts = hoten.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            if (parts.Count > 0)
                            {
                                ten = parts.Last();
                                if (parts.Count > 1) ho = string.Join(" ", parts.Take(parts.Count - 1));
                            }
                        }

                        bool vangToan = row.Field<bool?>("Vắng Toán") ?? false;
                        bool vangVan = row.Field<bool?>("Vắng Văn") ?? false;
                        bool vangAnh = row.Field<bool?>("Vắng Môn 3") ?? false;

                        ws.Cells[r, 1].Value = stt++;
                        ws.Cells[r, 2].Value = sbd;
                        ws.Cells[r, 3].Value = ho;
                        ws.Cells[r, 4].Value = ten;
                        ws.Cells[r, 5].Value = vangToan ? "Vắng" : "";
                        ws.Cells[r, 6].Value = vangVan ? "Vắng" : "";
                        ws.Cells[r, 7].Value = vangAnh ? "Vắng" : "";
                        ws.Cells[r, 8].Value = "";
                        r++;
                    }
                    int lastDataRow = r - 1;

                    ws.Column(1).Width = 6;
                    ws.Column(2).Width = 12;
                    ws.Column(3).Width = 22;
                    ws.Column(4).Width = 14;
                    ws.Column(5).Width = 10;
                    ws.Column(6).Width = 10;
                    ws.Column(7).Width = 12;
                    ws.Column(8).Width = 18;

                    ws.Cells[headerRow + 2, 1, lastDataRow, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    ws.Cells[headerRow + 2, 2, lastDataRow, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    ws.Cells[headerRow + 2, 5, lastDataRow, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    using (var rng = ws.Cells[headerRow, 1, lastDataRow, 8])
                    {
                        rng.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        rng.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        rng.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        rng.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    }

                    int footTop = lastDataRow + 2;
                    ws.Cells[footTop, 1].Value = "Tổng cộng";
                    ws.Cells[footTop, 2].Value = rows.Count;
                    ws.Cells[footTop, 1, footTop, 2].Style.Font.Bold = true;

                    ws.Cells[footTop, 6, footTop, 8].Merge = true;
                    ws.Cells[footTop, 6].Value = string.Format("Trà Vinh, ngày {0:dd} tháng {0:MM} năm {0:yyyy}", DateTime.Today);
                    ws.Cells[footTop + 1, 6, footTop + 1, 8].Merge = true;
                    ws.Cells[footTop + 1, 6].Value = "HIỆU TRƯỞNG";
                    ws.Cells[footTop + 1, 6].Style.Font.Bold = true;
                    ws.Cells[footTop + 2, 6, footTop + 2, 8].Merge = true;
                    ws.Cells[footTop + 2, 6].Value = "(Ký tên và đóng dấu)";
                    ws.Cells[footTop + 2, 6].Style.Font.Italic = true;

                    pkg.SaveAs(new System.IO.FileInfo(sfd.FileName));
                }

                MessageBox.Show("Đã xuất Excel.");
            }
        }

        private string GetHeaderTenTruong()
        {
            if (Common.IsAdmin)
            {
                var item = cbTruong.SelectedItem as TruongItem;
                if (item != null && !string.IsNullOrEmpty(item.MaTruong))
                    return item.TenTruong;
                return "Toàn tỉnh";
            }
            return Common.TenTruong ?? "";
        }

        private void lblVangToan_Click(object sender, EventArgs e) { }

        private void cbTruong_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaiDuLieu();
        }
    }
}
