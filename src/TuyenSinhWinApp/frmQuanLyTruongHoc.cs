using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TuyenSinhServiceLib;
using TuyenSinhWinApp.TuyenSinhServiceReference;

namespace TuyenSinhWinApp
{
    public partial class frmQuanLyTruongHoc : Form
    {
        private readonly Service1Client _service = new Service1Client();
        private TruongHoc selectedTruong;

        public frmQuanLyTruongHoc()
        {
            InitializeComponent();
            SetupControls();
            WireEvents();            // <— GẮN CÁC SỰ KIỆN CHỌN DÒNG
            LoadData();
        }

        private void frmQuanLyTruongHoc_Load(object sender, EventArgs e)
        {
            LoadData();
            SetupControls();
        }

        private void SetupControls()
        {
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = Common.IsAdmin || Common.VaiTro == "CanBoTruong";
            dgvTruongHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTruongHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            txtMaTruong.ReadOnly = true; // Khóa chính khi đang SỬA
        }

        // Gắn event cho DataGridView
        private void WireEvents()
        {
            dgvTruongHoc.CellClick += dgvTruongHoc_CellClick;
            dgvTruongHoc.SelectionChanged += dgvTruongHoc_SelectionChanged;
        }

        private void LoadData()
        {
            try
            {
                var dsArray = _service.LayDanhSachTruong(); // Nhận mảng
                var ds = dsArray != null ? dsArray.ToList() : new List<TruongHoc>(); // Chuyển thành List
                if (ds != null && ds.Count > 0) // So sánh với Count
                {
                    dgvTruongHoc.DataSource = ds;

                    // Tự chọn dòng đầu và fill để người dùng biết đang chọn ai
                    if (dgvTruongHoc.Rows.Count > 0)
                    {
                        dgvTruongHoc.ClearSelection();
                        dgvTruongHoc.Rows[0].Selected = true;
                        dgvTruongHoc.CurrentCell = dgvTruongHoc.Rows[0].Cells[0];
                        FillFormFromSelectedRow();    // <—
                    }
                }
                else
                {
                    dgvTruongHoc.DataSource = new List<TruongHoc>();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvTruongHoc.DataSource = new List<TruongHoc>();
                ClearInputs();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTruong.Text) || string.IsNullOrWhiteSpace(txtTenTruong.Text) || string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc (Mã Trường, Tên Trường, Địa Chỉ)!");
                return;
            }

            var truong = new TruongHoc
            {
                MaTruong = txtMaTruong.Text,
                TenTruong = txtTenTruong.Text,
                DiaChi = txtDiaChi.Text,
                SoDienThoai = txtSoDienThoai.Text,
                Email = txtEmail.Text
            };

            bool success = _service.ThemTruong(truong);
            if (success)
            {
                MessageBox.Show("Thêm trường thành công!");
                LoadData();
                ClearInputs(); // <— clear để thao tác tiếp
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm trường! Kiểm tra Output window.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedTruong == null)
            {
                MessageBox.Show("Vui lòng chọn trường để sửa!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMaTruong.Text) || string.IsNullOrWhiteSpace(txtTenTruong.Text) || string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc!");
                return;
            }

            selectedTruong.TenTruong = txtTenTruong.Text;
            selectedTruong.DiaChi = txtDiaChi.Text;
            selectedTruong.SoDienThoai = txtSoDienThoai.Text;
            selectedTruong.Email = txtEmail.Text;

            bool success = _service.CapNhatTruong(selectedTruong);
            if (success)
            {
                MessageBox.Show("Cập nhật trường thành công!");
                LoadData();
                ClearInputs(); // <—
            }
            else
            {
                MessageBox.Show("Lỗi khi cập nhật trường! Kiểm tra Output window.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedTruong == null)
            {
                MessageBox.Show("Vui lòng chọn trường để xóa!");
                return;
            }

            if (MessageBox.Show("Xác nhận xóa trường?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool success = _service.XoaTruong(selectedTruong.MaTruong);
                if (success)
                {
                    MessageBox.Show("Xóa trường thành công!");
                    LoadData();
                    ClearInputs(); // <—
                }
                else
                {
                    MessageBox.Show("Lỗi khi xóa trường! Kiểm tra Output window.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearInputs();
        }

        // Khi click chuột vào bất kỳ ô nào của hàng
        private void dgvTruongHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) FillFormFromSelectedRow();
        }

        // Khi đổi selection bằng bàn phím / code
        private void dgvTruongHoc_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTruongHoc.Focused) FillFormFromSelectedRow();
        }

        // Hàm dùng chung: lấy object đang chọn và đổ vào form
        private void FillFormFromSelectedRow()
        {
            try
            {
                if (dgvTruongHoc.CurrentRow == null) return;
                selectedTruong = dgvTruongHoc.CurrentRow.DataBoundItem as TruongHoc;
                if (selectedTruong == null) return;

                txtMaTruong.Text = selectedTruong.MaTruong ?? "";
                txtTenTruong.Text = selectedTruong.TenTruong ?? "";
                txtDiaChi.Text = selectedTruong.DiaChi ?? "";
                txtSoDienThoai.Text = selectedTruong.SoDienThoai ?? "";
                txtEmail.Text = selectedTruong.Email ?? "";

                // Đang sửa bản ghi có sẵn -> khóa mã trường
                txtMaTruong.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi fill dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaTruong.Clear();
            txtTenTruong.Clear();
            txtDiaChi.Clear();
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            selectedTruong = null;

            // Sau khi clear để thêm mới -> mở nhập mã trường
            txtMaTruong.ReadOnly = false;

            // Bỏ chọn dòng trên grid (tránh nhầm là đang sửa)
            dgvTruongHoc.ClearSelection();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_service.State == System.ServiceModel.CommunicationState.Opened)
                _service.Close();
            base.OnFormClosing(e);
        }
    }
}
