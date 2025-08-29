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
using System.Drawing;

namespace TuyenSinhWinApp
{
    public partial class frmAdminHocSinh : Form
    {
        private class Item { public string Text { get; set; } public string Value { get; set; } }
        public frmAdminHocSinh()
        {
            InitializeComponent();
            BuildGridColumns();
            StyleGrid();
            dgvHocSinh.AutoGenerateColumns = false;
            BuildGridColumns();
        }

        private void cboTruongTHCS_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadHocSinh();
        }

        private void frmAdminHocSinh_Load(object sender, EventArgs e)
        {
            LoadTruong();
            LoadDot();
            dgvHocSinh.AutoResizeColumns();
            dgvHocSinh.Refresh();
            StyleGrid();
            dgvHocSinh.CellFormatting += dgvHocSinh_CellFormatting;

        }

        private void BuildGridColumns()
        {
            dgvHocSinh.Columns.Clear();

            // Dùng Fill + FillWeight để chia tỉ lệ chiều rộng theo grid
            Col("MaSoBaoDanh", "Số BD", 8, min: 70, align: DataGridViewContentAlignment.MiddleCenter);
            Col("Ho", "Họ", 16, min: 120);
            Col("Ten", "Tên", 10, min: 80);
            Col("NgaySinh", "Ngày sinh", 11, min: 95, format: "dd/MM/yyyy", align: DataGridViewContentAlignment.MiddleCenter);
            Col("GioiTinh", "Giới tính", 7, min: 70, align: DataGridViewContentAlignment.MiddleCenter);
            Col("TruongTHCS", "THCS", 22, min: 160); // cột dài – sẽ nhận nhiều không gian
            Col("MaTruong", "Mã trường", 8, min: 70, align: DataGridViewContentAlignment.MiddleCenter);
            Col("MaDot", "Đợt", 8, min: 70, align: DataGridViewContentAlignment.MiddleCenter);
            Col("PhongThi", "Phòng thi", 12, min: 100);

            Col("DiemToan", "Toán", 7, min: 60, format: "0.00", align: DataGridViewContentAlignment.MiddleRight);
            Col("DiemVan", "Văn", 7, min: 60, format: "0.00", align: DataGridViewContentAlignment.MiddleRight);
            Col("DiemAnh", "Anh", 7, min: 60, format: "0.00", align: DataGridViewContentAlignment.MiddleRight);
            Col("DiemTong", "Tổng", 9, min: 70, format: "0.00", align: DataGridViewContentAlignment.MiddleRight);

            Col("TrangThai", "Trạng thái", 12, min: 100);

            // Fit trong grid, chỉ scroll dọc
            dgvHocSinh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHocSinh.ScrollBars = ScrollBars.Vertical;
            dgvHocSinh.RowHeadersVisible = false;
        }
        private void Col(string prop, string header, float weight,
                         int min = 50, string format = null,
                         DataGridViewContentAlignment? align = null)
        {
            var c = new DataGridViewTextBoxColumn
            {
                DataPropertyName = prop,
                Name = prop,
                HeaderText = header,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = weight,          // tỉ lệ bề rộng tương đối
                MinimumWidth = min,
                SortMode = DataGridViewColumnSortMode.Programmatic
            };
            if (!string.IsNullOrEmpty(format)) c.DefaultCellStyle.Format = format;
            if (align.HasValue) c.DefaultCellStyle.Alignment = align.Value;

            dgvHocSinh.Columns.Add(c);
        }

        private void StyleGrid()
        {
            // chỉ cần fit trong chính dgvHocSinh, không fill cả form
            dgvHocSinh.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // nền & đường kẻ
            dgvHocSinh.BackgroundColor = Color.White;
            dgvHocSinh.BorderStyle = BorderStyle.None;
            dgvHocSinh.GridColor = Color.FromArgb(235, 238, 242);
            dgvHocSinh.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvHocSinh.RowHeadersVisible = false;

            // header
            dgvHocSinh.EnableHeadersVisualStyles = false;
            dgvHocSinh.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvHocSinh.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(76, 95, 173);
            dgvHocSinh.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHocSinh.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvHocSinh.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.5f);
            dgvHocSinh.ColumnHeadersHeight = 36;

            // body
            dgvHocSinh.DefaultCellStyle.Font = new Font("Segoe UI", 10f);
            dgvHocSinh.DefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dgvHocSinh.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 240, 255);
            dgvHocSinh.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 37, 41);
            dgvHocSinh.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 255);
            dgvHocSinh.RowTemplate.Height = 32;

            // hành vi
            dgvHocSinh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHocSinh.MultiSelect = false;
            dgvHocSinh.AllowUserToAddRows = false;
            dgvHocSinh.AllowUserToDeleteRows = false;
            dgvHocSinh.ReadOnly = true;

            // auto-size theo grid (chỉ còn scroll dọc)
            dgvHocSinh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHocSinh.ScrollBars = ScrollBars.Vertical;
        }

        private void dgvHocSinh_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null) return;

            var name = dgvHocSinh.Columns[e.ColumnIndex].DataPropertyName;
            if (name == "TrangThai")
            {
                var v = e.Value.ToString();
                if (v == "TrungTuyen")
                {
                    e.CellStyle.ForeColor = Color.SeaGreen;
                    e.CellStyle.Font = new Font(dgvHocSinh.Font, FontStyle.Bold);
                }
                else if (v == "DangKy")
                {
                    e.CellStyle.ForeColor = Color.DodgerBlue;
                }
                else if (v == "Rot")
                {
                    e.CellStyle.ForeColor = Color.IndianRed;
                }
            }
        }


        private void LoadTruong()
        {
            try
            {
                using (var sv = new Service1Client())
                {
                    var list = sv.Admin_LayDanhSachTruong()?.ToList() ?? new List<TruongItem>();
                    // Thêm lựa chọn "Tất cả"
                    var data = new List<Item> { new Item { Text = "— Tất cả trường —", Value = null } };
                    data.AddRange(list.Select(x => new Item { Text = $"{x.TenTruong} ({x.MaTruong})", Value = x.MaTruong }));

                    cboTruong.DisplayMember = "Text";
                    cboTruong.ValueMember = "Value";
                    cboTruong.DataSource = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách trường: " + ex.Message);
            }
        }

        private void LoadDot()
        {
            try
            {
                using (var sv = new Service1Client())
                {
                    var list = sv.LayDanhSachDotTuyen()?.ToList() ?? new List<DotTuyenSinh>();
                    var data = new List<Item> { new Item { Text = "— Tất cả đợt —", Value = null } };
                    data.AddRange(list
                        .OrderByDescending(d => d.Nam)
                        .Select(d => new Item { Text = $"{d.TenDot} ({d.MaDot})", Value = d.MaDot }));

                    cboDot.DisplayMember = "Text";
                    cboDot.ValueMember = "Value";
                    cboDot.DataSource = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách đợt: " + ex.Message);
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cboDot_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadHocSinh();
        }

        private void btnTai_Click(object sender, EventArgs e)
        {
            LoadHocSinh();
        }

        private void LoadHocSinh()
        {
            try
            {
                string maTruong = (cboTruong.SelectedItem as Item)?.Value; // null => tất cả
                string maDot = (cboDot.SelectedItem as Item)?.Value;    // null => tất cả

                using (var sv = new Service1Client())
                {
                    var ds = sv.Admin_LayDanhSachHocSinhTheoTruongDot(maTruong, maDot);
                    dgvHocSinh.DataSource = ds;
                    lblCount.Text = $"Tổng số học sinh trong danh sách: {(ds?.Length ?? 0)} học sinh";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách học sinh: " + ex.Message);
            }
        }
    }
}
