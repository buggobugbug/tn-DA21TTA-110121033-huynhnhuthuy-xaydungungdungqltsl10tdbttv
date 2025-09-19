using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using TuyenSinhServiceLib;
using TuyenSinhWinApp.TuyenSinhServiceReference;

namespace TuyenSinhWinApp
{
    public partial class frmThongKeDiemChuan : Form
    {
        private readonly Service1Client _svc = new Service1Client();
        private static readonly CultureInfo ViVN = CultureInfo.GetCultureInfo("vi-VN");

        public frmThongKeDiemChuan()
        {
            InitializeComponent();

            // events
            this.Load += Frm_Load;
            if (btnTaiLai != null) btnTaiLai.Click += (s, e) => LoadData();
            if (cbTruong != null)
                cbTruong.SelectedIndexChanged += (s, e) => { if (Common.IsAdmin) LoadData(); };

            // grid like frmAdminHocSinh
            dgvDiemChuan.AutoGenerateColumns = false;
            BuildGridColumns();
            StyleGrid();
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            LoadTruongForAdmin();
            LoadData();
        }

        // ====================== PARSE / NORMALIZE ======================

        // Nếu service trả về 2025 (x100) thì chia lại 100.
        // Ngưỡng 30 là "điểm tối đa hợp lý" (tuỳ thang), >30 coi là đã bị x100.
        private decimal FixHundredScaled(decimal d)
            => (d > 30m && d <= 3000m) ? d / 100m : d;

        private decimal? FlexParseDecimal(object val)
        {
            if (val == null) return null;

            // nếu đã là decimal
            if (val is decimal dd) return FixHundredScaled(dd);

            var s = val.ToString().Trim();
            if (s.Length == 0) return null;

            decimal d;

            // Ưu tiên đúng dấu người dùng gõ
            if (s.Contains(".") && !s.Contains(",") &&
                decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out d))
                return FixHundredScaled(d);

            if (s.Contains(",") && !s.Contains(".") &&
                decimal.TryParse(s, NumberStyles.Number, ViVN, out d))
                return FixHundredScaled(d);

            // Fallback: thử 3 culture
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out d) ||
                decimal.TryParse(s, NumberStyles.Number, ViVN, out d) ||
                decimal.TryParse(s, NumberStyles.Number, CultureInfo.CurrentCulture, out d))
                return FixHundredScaled(d);

            // Trường hợp chuỗi toàn số nguyên (2025) -> thử thêm lần nữa
            if (decimal.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out d))
                return FixHundredScaled(d);

            return null;
        }

        // ====================== UI ======================
        private void BuildGridColumns()
        {
            dgvDiemChuan.Columns.Clear();

            Col("MaTruong", "Mã trường", 10, 80, DataGridViewContentAlignment.MiddleCenter);
            Col("TenTruong", "Trường", 28, 180, DataGridViewContentAlignment.MiddleLeft);
            Col("MaDot", "Mã đợt", 12, 80, DataGridViewContentAlignment.MiddleCenter);
            Col("TenDot", "Tên đợt", 22, 140, DataGridViewContentAlignment.MiddleLeft);
            Col("Nam", "Năm", 10, 70, DataGridViewContentAlignment.MiddleCenter);
            Col("SoTrungTuyen", "Trúng tuyển", 9, 90, DataGridViewContentAlignment.MiddleRight);
            Col("TongDK", "Tổng đăng ký", 9, 100, DataGridViewContentAlignment.MiddleRight);

            // Điểm chuẩn: hiển thị 2 số lẻ, dùng vi-VN để hiện "20,25" nếu máy đang vi-VN
            var cDiem = Col("DiemChuan", "Điểm chuẩn", 10, 90,
                            DataGridViewContentAlignment.MiddleRight, "0.00");
            cDiem.DefaultCellStyle.FormatProvider = ViVN;

            dgvDiemChuan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDiemChuan.ScrollBars = ScrollBars.Vertical;
            dgvDiemChuan.RowHeadersVisible = false;
            dgvDiemChuan.ReadOnly = true;
            dgvDiemChuan.AllowUserToAddRows = false;
            dgvDiemChuan.AllowUserToDeleteRows = false;
            dgvDiemChuan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDiemChuan.MultiSelect = false;
        }

        private DataGridViewTextBoxColumn Col(string prop, string header, float weight, int minWidth,
                                              DataGridViewContentAlignment align,
                                              string format = null)
        {
            var c = new DataGridViewTextBoxColumn
            {
                DataPropertyName = prop,
                Name = prop,
                HeaderText = header,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = weight,
                MinimumWidth = minWidth,
                SortMode = DataGridViewColumnSortMode.Programmatic
            };

            c.DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = align,
                Format = string.IsNullOrEmpty(format) ? null : format
            };

            dgvDiemChuan.Columns.Add(c);
            return c;
        }

        private void StyleGrid()
        {
            dgvDiemChuan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            dgvDiemChuan.BackgroundColor = Color.White;
            dgvDiemChuan.BorderStyle = BorderStyle.None;
            dgvDiemChuan.GridColor = Color.FromArgb(235, 238, 242);
            dgvDiemChuan.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            dgvDiemChuan.EnableHeadersVisualStyles = false;
            dgvDiemChuan.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDiemChuan.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(76, 95, 173);
            dgvDiemChuan.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDiemChuan.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDiemChuan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.5f);
            dgvDiemChuan.ColumnHeadersHeight = 36;

            dgvDiemChuan.DefaultCellStyle.Font = new Font("Segoe UI", 10f);
            dgvDiemChuan.DefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dgvDiemChuan.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 240, 255);
            dgvDiemChuan.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 37, 41);
            dgvDiemChuan.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 255);
            dgvDiemChuan.RowTemplate.Height = 32;
        }

        // ====================== DATA ======================
        private void LoadTruongForAdmin()
        {
            if (!Common.IsAdmin)
            {
                if (lblTruong != null) lblTruong.Visible = false;
                if (cbTruong != null) cbTruong.Visible = false;
                return;
            }

            var ds = _svc.Admin_LayDanhSachTruong()?.ToList() ?? new List<TruongItem>();
            ds.Insert(0, new TruongItem { MaTruong = null, TenTruong = "— Tất cả trường —" });

            cbTruong.DisplayMember = "TenTruong";
            cbTruong.ValueMember = "MaTruong";
            cbTruong.DataSource = ds;
        }

        private IEnumerable<(string MaTruong, string TenTruong)> GetTruongScope()
        {
            if (Common.IsAdmin)
            {
                var ds = (_svc.Admin_LayDanhSachTruong() ?? Array.Empty<TruongItem>())
                         .Select(t => (t.MaTruong, t.TenTruong)).ToList();

                var pick = cbTruong.SelectedValue as string;
                if (string.IsNullOrEmpty(pick)) return ds;          // toàn tỉnh
                return ds.Where(x => x.MaTruong == pick);           // 1 trường
            }
            // Cán bộ/Thư ký
            return new[] { (Common.MaTruong, Common.TenTruong ?? Common.MaTruong) };
        }

        private void LoadData()
        {
            try
            {
                var dots = _svc.LayDanhSachDotTuyen()?.ToList() ?? new List<DotTuyenSinh>();
                dots = dots.OrderBy(d => d.Nam).ThenBy(d => d.MaDot).ToList();

                var rows = new List<RowOut>();

                foreach (var (maTruong, tenTruong) in GetTruongScope())
                {
                    foreach (var dot in dots)
                    {
                        HocSinh[] ds;

                        if (Common.IsAdmin)
                            ds = _svc.Admin_LayDanhSachHocSinhTheoTruongDot(maTruong, dot.MaDot) ?? Array.Empty<HocSinh>();
                        else
                            ds = _svc.LayDanhSachHocSinh(maTruong, dot.MaDot) ?? Array.Empty<HocSinh>();

                        int tong = ds.Length;

                        var trung = ds.Where(h => string.Equals(h.TrangThai, "TrungTuyen",
                                        StringComparison.OrdinalIgnoreCase))
                                      .ToList();

                        // Điểm chuẩn = điểm tổng nhỏ nhất trong danh sách trúng tuyển
                        decimal? cutoff = null;
                        if (trung.Count > 0)
                        {
                            var pts = trung.Select(h => FlexParseDecimal(h.DiemTong))
                                           .Where(x => x.HasValue)
                                           .Select(x => x.Value);

                            if (pts.Any()) cutoff = pts.Min();
                        }

                        rows.Add(new RowOut
                        {
                            MaTruong = maTruong,
                            TenTruong = tenTruong,
                            MaDot = dot.MaDot,
                            TenDot = dot.TenDot,
                            Nam = dot.Nam,
                            SoTrungTuyen = trung.Count,
                            TongDK = tong,
                            DiemChuan = cutoff
                        });
                    }
                }

                // sort gợi ý: Trường -> Năm -> Mã đợt
                var view = rows.OrderBy(r => r.TenTruong)
                               .ThenBy(r => r.Nam)
                               .ThenBy(r => r.MaDot)
                               .ToList();

                dgvDiemChuan.DataSource = view;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thống kê điểm chuẩn: " + ex.Message);
                dgvDiemChuan.DataSource = null;
            }
        }

        private class RowOut
        {
            public string MaTruong { get; set; }
            public string TenTruong { get; set; }
            public string MaDot { get; set; }
            public string TenDot { get; set; }
            public int Nam { get; set; }
            public int SoTrungTuyen { get; set; }
            public int TongDK { get; set; }
            public decimal? DiemChuan { get; set; }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_svc.State == System.ServiceModel.CommunicationState.Opened) _svc.Close();
            base.OnFormClosing(e);
        }
    }
}
