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

namespace TuyenSinhWinApp
{
    public partial class frmDanhSachHocSinh : Form
    {

        private readonly Service1 _service = new Service1();
        private List<HocSinh> danhSachGoc = new List<HocSinh>();


        public frmDanhSachHocSinh()
        {
            InitializeComponent();
        }


        private void frmDanhSachHocSinh_Load(object sender, EventArgs e)
        {
            NapDuLieuHocSinh();
            txtTimKiem.Text = "Nhập tên hoặc mã số báo danh học sinh";
        }



        private void NapDuLieuHocSinh()
        {
            try
            {
                var maTruong = Common.MaTruong;
                danhSachGoc = _service.LayDanhSachHocSinh(maTruong); // Lưu gốc
                dgvHocSinh.DataSource = danhSachGoc;

                dgvHocSinh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvHocSinh.ReadOnly = true;
                dgvHocSinh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách học sinh: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            NapDuLieuHocSinh();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvHocSinh.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn học sinh cần sửa.");
                return;
            }

            var hocSinh = (HocSinh)dgvHocSinh.SelectedRows[0].DataBoundItem;
            var frm = new frmHocsinh(hocSinh);
            frm.ShowDialog();
            NapDuLieuHocSinh(); // làm mới danh sách sau khi sửa
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHocSinh.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn học sinh để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var hocSinh = dgvHocSinh.CurrentRow.DataBoundItem as HocSinh;

            if (hocSinh == null)
            {
                MessageBox.Show("Không thể lấy thông tin học sinh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa học sinh: {hocSinh.HoTen} ({hocSinh.MaSoBaoDanh})?",
                                          "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bool ketQua = _service.XoaHocSinh(hocSinh.MaHocSinh);

                    if (ketQua)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        NapDuLieuHocSinh(); // Cập nhật lại danh sách
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnChiaPhongThi_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Bạn có chắc muốn chia phòng thi cho các học sinh chưa có phòng?",
                                          "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bool ketQua = _service.ChiaPhongThi(Common.MaTruong);

                    if (ketQua)
                    {
                        MessageBox.Show("Đã chia phòng thi thành công!", "Thành công",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        NapDuLieuHocSinh(); // reload lại danh sách
                    }
                    else
                    {
                        MessageBox.Show("Chia phòng thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Thanh tìm kiếm
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim().ToLower();

            var ketQua = danhSachGoc
                .Where(hs => hs.HoTen.ToLower().Contains(tuKhoa)
                          || hs.MaSoBaoDanh.ToLower().Contains(tuKhoa))
                .ToList();

            dgvHocSinh.DataSource = ketQua;
        }
    }
}
