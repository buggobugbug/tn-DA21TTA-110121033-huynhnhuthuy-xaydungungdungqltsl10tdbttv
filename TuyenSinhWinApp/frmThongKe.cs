using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                var maTruong = Common.MaTruong;
                var maDot = cbDotTuyenSinh.SelectedValue?.ToString();

                if (string.IsNullOrWhiteSpace(maTruong) || string.IsNullOrWhiteSpace(maDot))
                {
                    dgvThongKeMon.DataSource = null;
                    return;
                }

                var data = _service.ThongKeDiemTheoMon(maTruong, maDot);
                if (data == null) { dgvThongKeMon.DataSource = null; return; }

                // PIVOT thành bảng như ảnh: hàng = Văn/AV/Toán, cột 0..10 bước 0.25 + TC
                var dt = new DataTable();
                dt.Columns.Add("Môn");

                var mucs = Enumerable.Range(0, 41).Select(i => Math.Round(i * 0.25m, 2)).ToList();
                foreach (var m in mucs)
                    dt.Columns.Add(m.ToString("0.##"), typeof(int));

                dt.Columns.Add("TC", typeof(int));

                foreach (var monHienThi in new[] { "Văn", "AV", "Toán" }) // thứ tự như ảnh
                {
                    string monTrongData = monHienThi == "AV" ? "Anh" : monHienThi;

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

                var dtBoThi = new DataTable();
                dtBoThi.Columns.Add("Môn");
                dtBoThi.Columns.Add("Bỏ thi", typeof(int));
                string[] monList = new[] { "Văn", "Anh", "Toán" };
                foreach (var mon in monList)
                {
                    int countBoThi = data.Where(x => x.BoThi && x.Mon == mon).Sum(x => x.SoLuong);
                    var r = dtBoThi.NewRow();
                    r["Môn"] = mon;
                    r["Bỏ thi"] = countBoThi;
                    dtBoThi.Rows.Add(r);
                }

                dgvBoThiMon.DataSource = dtBoThi;
                dgvBoThiMon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thống kê: " + ex.Message);
            }
        }

        private void FormatGrid()
        {
            var dgv = dgvThongKeMon;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.RowHeadersVisible = false;
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
