using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TuyenSinhWinApp.TuyenSinhServiceReference;
using OfficeOpenXml;
using System.IO;
using System.Drawing;
using TuyenSinhServiceLib;


namespace TuyenSinhWinApp
{
    public partial class frmHocSinhTrongPhong : Form
    {
        private readonly Service1Client _service = new Service1Client();
        private readonly string _maPhongThi;

        public frmHocSinhTrongPhong(string maPhongThi)
        {
            InitializeComponent();
            _maPhongThi = maPhongThi;
        }

        private void frmHocSinhTrongPhong_Load(object sender, EventArgs e)
        {
            this.Text = $"Danh sách học sinh - Phòng {_maPhongThi}";

            // Cấu hình DataGridView
            dgvHocSinh.AutoGenerateColumns = false;
            dgvHocSinh.Columns.Clear();

            dgvHocSinh.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "SBD",
                DataPropertyName = "MaSoBaoDanh",
                Width = 100
            });
            dgvHocSinh.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Họ và tên",
                DataPropertyName = "HoTen",
                Width = 180
            });
            dgvHocSinh.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Ngày sinh",
                DataPropertyName = "NgaySinh",
                Width = 110,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });
            dgvHocSinh.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tên trường THCS",
                DataPropertyName = "TruongTHCS",
                Width = 200
            });
            dgvHocSinh.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Ghi chú",
                DataPropertyName = "GhiChu",
                Width = 200
            });

            // Gọi dịch vụ để lấy dữ liệu
            var danhSach = _service.LayDanhSachHocSinhTheoPhong(_maPhongThi);
            dgvHocSinh.DataSource = danhSach;
        }

        private void btnXuatThe_Click(object sender, EventArgs e)
        {
            if (dgvHocSinh.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 học sinh để in thẻ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var hs = dgvHocSinh.SelectedRows[0].DataBoundItem as HocSinh;
            if (hs == null)
            {
                MessageBox.Show("Không lấy được thông tin học sinh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XuatTheDuThiExcel(hs);
        }

        private void XuatTheDuThiExcel(HocSinh hs)
        {
            try
            {
                string templatePath = Path.Combine(Application.StartupPath, "Templates", "TheDuThi_Template.xlsx");
                string outputDir = Path.Combine(Application.StartupPath, "TheDuThi");
                Directory.CreateDirectory(outputDir);

                string outputPath = Path.Combine(outputDir, $"TheDuThi_{hs.MaSoBaoDanh}.xlsx");

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(new FileInfo(templatePath)))
                {
               
                    // Đúng (theo tên sheet rõ ràng)
                    var ws = package.Workbook.Worksheets["In.The"];
                    if (ws == null)
                    {
                        MessageBox.Show("Không tìm thấy sheet 'In.The' trong file mẫu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }



                    // Sử dụng extension để thay thế các placeholder
                    ws.ReplacePlaceholder("{HoTen}", hs.HoTen);
                    ws.ReplacePlaceholder("{NgaySinh}", hs.NgaySinh.ToString("dd/MM/yyyy"));
                    ws.ReplacePlaceholder("{GioiTinh}", hs.GioiTinh);
                    ws.ReplacePlaceholder("{MaSoBaoDanh}", hs.MaSoBaoDanh);
                    ws.ReplacePlaceholder("{TruongTHCS}", hs.TruongTHCS ?? "");
                    ws.ReplacePlaceholder("{NoiSinh}", hs.NoiSinh ?? "");
                    ws.ReplacePlaceholder("{DanToc}", hs.DanToc ?? "");
                    ws.ReplacePlaceholder("{PhongThi}", hs.PhongThi ?? "Chưa xếp");

                    package.SaveAs(new FileInfo(outputPath));
                }

                MessageBox.Show("✅ Xuất thẻ dự thi thành công!\n\n📁 Đã lưu tại:\n" + outputPath,
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
