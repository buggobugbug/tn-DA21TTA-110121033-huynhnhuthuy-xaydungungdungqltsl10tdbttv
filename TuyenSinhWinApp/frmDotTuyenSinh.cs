using System;
using System.Windows.Forms;
using TuyenSinhServiceLib;
using TuyenSinhWinApp.TuyenSinhServiceReference;

namespace TuyenSinhWinApp
{
    public partial class frmDotTuyenSinh : Form
    {
        private Service1Client client;

        public frmDotTuyenSinh()
        {
            InitializeComponent();
            client = new Service1Client(); // Khởi tạo client dùng chung
        }
        private void LoadDanhSachDot()
        {
            try
            {
                dgvDotTuyenSinh.AutoGenerateColumns = false;
                var danhSach = client.LayDanhSachDotTuyen();
                dgvDotTuyenSinh.DataSource = danhSach;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDotTuyenSinh_Load(object sender, EventArgs e)
        {
            dgvDotTuyenSinh.CellClick += dgvDotTuyenSinh_CellClick;
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("DangMo");
            cboTrangThai.Items.Add("DaDong");
            LoadDanhSachDot();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaDot.Text) || string.IsNullOrWhiteSpace(txtTenDot.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ mã đợt và tên đợt.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dtpBatDau.Value > dtpKetThuc.Value)
                {
                    MessageBox.Show("Ngày bắt đầu không được sau ngày kết thúc.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo đối tượng DotTuyenSinh
                var dot = new DotTuyenSinh
                {
                    MaDot = txtMaDot.Text.Trim(),
                    TenDot = txtTenDot.Text.Trim(),
                    Nam = dtpBatDau.Value.Year,
                    NgayBatDau = dtpBatDau.Value,
                    NgayKetThuc = dtpKetThuc.Value
                };

                // Gửi lên service
                bool result = client.ThemDotTuyen(dot);

                if (result)
                {
                    MessageBox.Show("Thêm đợt tuyển sinh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachDot();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xử lý khi chọn dòng trên datagridview
        private void dgvDotTuyenSinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvDotTuyenSinh.Rows[e.RowIndex];

                txtMaDot.Text = row.Cells["MaDot"].Value?.ToString();
                txtTenDot.Text = row.Cells["TenDot"].Value?.ToString();
                dtpBatDau.Value = Convert.ToDateTime(row.Cells["NgayBatDau"].Value);
                dtpKetThuc.Value = Convert.ToDateTime(row.Cells["NgayKetThuc"].Value);
                cboTrangThai.SelectedItem = row.Cells["TrangThai"].Value?.ToString();
            }
        }


        // Reset form

        private void ResetForm()
        {
            txtMaDot.Clear();
            txtTenDot.Clear();
            dtpBatDau.Value = DateTime.Now;
            dtpKetThuc.Value = DateTime.Now;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaDot.Text))
                {
                    MessageBox.Show("Vui lòng chọn đợt tuyển sinh để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dtpBatDau.Value > dtpKetThuc.Value)
                {
                    MessageBox.Show("Ngày bắt đầu không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dot = new DotTuyenSinh
                {
                    MaDot = txtMaDot.Text.Trim(),
                    TenDot = txtTenDot.Text.Trim(),
                    Nam = dtpBatDau.Value.Year,
                    NgayBatDau = dtpBatDau.Value,
                    NgayKetThuc = dtpKetThuc.Value,
                    TrangThai = cboTrangThai.SelectedItem?.ToString()
                };

                bool result = client.CapNhatDotTuyen(dot);
                if (result)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachDot();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
