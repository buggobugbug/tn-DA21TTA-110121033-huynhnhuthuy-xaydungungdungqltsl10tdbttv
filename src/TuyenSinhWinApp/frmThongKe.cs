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
using System.Drawing;
using System.Reflection;
using TuyenSinhWinApp.TuyenSinhServiceReference;

namespace TuyenSinhWinApp
{
    public partial class frmThongKe : Form
    {

        private readonly Service1Client _service = new Service1Client();

        public frmThongKe()
        {
            InitializeComponent();
            this.Load += frmThongKe_Load;
            cbDotTuyenSinh.SelectedIndexChanged += cbDotTuyenSinh_SelectedIndexChanged;
            btnThongKe.Click += (s, e) => LoadThongKeTheoMon();
        }



        private void cbDotTuyenSinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            var maDot = cbDotTuyenSinh.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(maDot)) return;
            Common.MaDot = maDot;
            LoadThongKeTheoMon();
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {

            var dsDot = _service.LayDanhSachDotTuyen();
            cbDotTuyenSinh.DataSource = dsDot;
            cbDotTuyenSinh.DisplayMember = "TenDot";
            cbDotTuyenSinh.ValueMember = "MaDot";
            LoadTruongForAdmin();

            if (!string.IsNullOrEmpty(Common.MaDot))
                cbDotTuyenSinh.SelectedValue = Common.MaDot;

            if (cbDotTuyenSinh.SelectedValue != null)
                LoadThongKeTheoMon();

            FormatGrid();
            LoadThongKeTheoMon();
        }

        private void LoadThongKeTheoMon()
        {
            try
            {
                var maDot = cbDotTuyenSinh.SelectedValue?.ToString();
                if (string.IsNullOrWhiteSpace(maDot))
                {
                    dgvThongKeMon.DataSource = null;
                    dgvBoThiMon.DataSource = null;
                    return;
                }

                // Lấy filter theo quyền:
                // - Admin: lấy từ combobox cbTruong; null => toàn tỉnh
                // - Cán bộ/Thư ký: dùng Common.MaTruong
                var maTruongFilter = GetMaTruongFilter();

                var data = _service.ThongKeDiemTheoMon(
                    string.IsNullOrEmpty(maTruongFilter) ? null : maTruongFilter,
                    maDot);

                if (data == null)
                {
                    dgvThongKeMon.DataSource = null;
                    dgvBoThiMon.DataSource = null;
                    return;
                }

                // PIVOT thành bảng: hàng = Văn/AV/Toán, cột 0..10 bước 0.25 + TC
                var dt = new DataTable();
                dt.Columns.Add("Môn");

                var mucs = Enumerable.Range(0, 41).Select(i => Math.Round(i * 0.25m, 2)).ToList();
                foreach (var m in mucs)
                    dt.Columns.Add(m.ToString("0.##"), typeof(int));

                dt.Columns.Add("TC", typeof(int));

                foreach (var monHienThi in new[] { "Văn", "Môn 3", "Toán" })
                {
                    string monTrongData = monHienThi == "Môn 3" ? "Anh" : monHienThi;

                    var row = dt.NewRow();
                    row["Môn"] = monHienThi;

                    int total = 0;
                    foreach (var m in mucs)
                    {
                        var sl = data.FirstOrDefault(x => x.Mon == monTrongData && (decimal)x.Muc == m)?.SoLuong ?? 0;
                        row[m.ToString("0.##")] = sl;
                        total += sl;
                    }
                    row["TC"] = total;
                    dt.Rows.Add(row);
                }

                dgvThongKeMon.DataSource = dt;

                // căn giữa cột số, riêng cột Môn căn trái
                foreach (DataGridViewColumn c in dgvThongKeMon.Columns)
                    c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvThongKeMon.Columns["Môn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                // Bảng "Bỏ thi"
                var dtBoThi = new DataTable();
                dtBoThi.Columns.Add("Môn");
                dtBoThi.Columns.Add("Bỏ thi", typeof(int));
                string[] monList = new[] { "Văn", "Môn 3", "Toán" };
                foreach (var mon in monList)
                {
                    int countBoThi = data.Where(x => x.BoThi && x.Mon == (mon == "Môn 3" ? "Anh" : mon)).Sum(x => x.SoLuong);
                    var r = dtBoThi.NewRow();
                    r["Môn"] = mon;
                    r["Bỏ thi"] = countBoThi;
                    dtBoThi.Rows.Add(r);
                }

                dgvBoThiMon.DataSource = dtBoThi;
                dgvBoThiMon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                // Re-apply theme sau khi bind, và đóng băng cột “Môn”
                ApplyAdminGridTheme(dgvThongKeMon, fillMode: false, freezeFirstCol: true);
                if (dgvThongKeMon.Columns.Contains("Môn"))
                    dgvThongKeMon.Columns["Môn"].Frozen = true;

                ApplyAdminGridTheme(dgvBoThiMon, fillMode: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thống kê: " + ex.Message);
            }
        }


        private void FormatGrid()
        {
            ApplyAdminGridTheme(dgvThongKeMon, fillMode: false, freezeFirstCol: true);

            // Bỏ thi theo môn: ít cột -> Fill gọn, chỉ scroll dọc
            ApplyAdminGridTheme(dgvBoThiMon, fillMode: true, freezeFirstCol: false);
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

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

        private string GetMaTruongFilter()
        {
            if (Common.IsAdmin)
                return cbTruong.SelectedValue as string; // có thể null (tức toàn tỉnh)
            return Common.MaTruong; // cán bộ trường / thư ký
        }

        private void cbTruong_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadThongKeTheoMon();
        }

        private void ApplyAdminGridTheme(DataGridView dgv, bool fillMode, bool freezeFirstCol = false)
        {
            // NỀN & ĐƯỜNG KẺ
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = Color.FromArgb(235, 238, 242);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowHeadersVisible = false;

            // HEADER GIỐNG ADMIN
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(76, 95, 173);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.5f);
            dgv.ColumnHeadersHeight = 36;

            // BODY
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10f);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 240, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 37, 41);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 255);
            dgv.RowTemplate.Height = 32;

            // HÀNH VI
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;

            // KÍCH THƯỚC CỘT
            dgv.AutoSizeColumnsMode = fillMode
                ? DataGridViewAutoSizeColumnsMode.Fill         // grid ngắn (Bỏ thi)
                : DataGridViewAutoSizeColumnsMode.DisplayedCells; // grid rất nhiều cột (0..10 bước 0.25)
            dgv.ScrollBars = fillMode ? ScrollBars.Vertical : ScrollBars.Both;

            // ĐÓNG BĂNG CỘT ĐẦU (nếu có)
            if (freezeFirstCol && dgv.Columns.Count > 0)
                dgv.Columns[0].Frozen = true;

            // Chống giật
            EnableDoubleBuffer(dgv);
        }

        private void EnableDoubleBuffer(DataGridView dgv)
        {
            try
            {
                typeof(DataGridView)
                    .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(dgv, true, null);
            }
            catch { /* ignore */ }
        }

    }
}
