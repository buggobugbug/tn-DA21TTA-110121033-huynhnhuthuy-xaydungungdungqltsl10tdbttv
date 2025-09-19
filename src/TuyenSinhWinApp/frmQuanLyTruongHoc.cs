using System;
using System.Collections.Generic;
using System.Drawing;
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
            dgvTruongHoc.AutoGenerateColumns = false;   
            BuildGridColumns();                         
            StyleGrid();
        }

        private void BuildGridColumns()
        {
            dgvTruongHoc.Columns.Clear();

            Col("MaTruong", "Mã trường", 12, min: 100, align: DataGridViewContentAlignment.MiddleCenter);
            Col("TenTruong", "Tên trường", 28, min: 180, align: DataGridViewContentAlignment.MiddleLeft);
            Col("DiaChi", "Địa chỉ", 30, min: 220, align: DataGridViewContentAlignment.MiddleLeft);
            Col("SoDienThoai", "Điện thoại", 14, min: 120, align: DataGridViewContentAlignment.MiddleCenter);
            Col("Email", "Email", 16, min: 160, align: DataGridViewContentAlignment.MiddleLeft);

            // Fit trong grid, chỉ scroll dọc — giống form Admin
            dgvTruongHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTruongHoc.ScrollBars = ScrollBars.Vertical;
            dgvTruongHoc.RowHeadersVisible = false;
        }

        private DataGridViewTextBoxColumn Col(string prop, string header, float weight,
                                              int min = 50, string format = null,
                                              DataGridViewContentAlignment? align = null)
        {
            var c = new DataGridViewTextBoxColumn
            {
                DataPropertyName = prop,
                Name = prop,
                HeaderText = header,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = weight,               // tỉ lệ bề rộng
                MinimumWidth = min,
                SortMode = DataGridViewColumnSortMode.Programmatic
            };
            if (!string.IsNullOrEmpty(format)) c.DefaultCellStyle.Format = format;
            if (align.HasValue) c.DefaultCellStyle.Alignment = align.Value;

            dgvTruongHoc.Columns.Add(c);
            return c;
        }

        private void StyleGrid()
        {
            // chỉ cần fit trong chính dgv, không fill cả form
            dgvTruongHoc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // nền & đường kẻ
            dgvTruongHoc.BackgroundColor = Color.White;
            dgvTruongHoc.BorderStyle = BorderStyle.None;
            dgvTruongHoc.GridColor = Color.FromArgb(235, 238, 242);
            dgvTruongHoc.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTruongHoc.RowHeadersVisible = false;

            // header (y hệt admin)
            dgvTruongHoc.EnableHeadersVisualStyles = false;
            dgvTruongHoc.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvTruongHoc.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(76, 95, 173);
            dgvTruongHoc.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTruongHoc.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTruongHoc.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.5f);
            dgvTruongHoc.ColumnHeadersHeight = 36;

            // body
            dgvTruongHoc.DefaultCellStyle.Font = new Font("Segoe UI", 10f);
            dgvTruongHoc.DefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dgvTruongHoc.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 240, 255);
            dgvTruongHoc.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 37, 41);
            dgvTruongHoc.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 255);
            dgvTruongHoc.RowTemplate.Height = 32;

            // hành vi — giống form Admin Học sinh
            dgvTruongHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTruongHoc.MultiSelect = false;
            dgvTruongHoc.AllowUserToAddRows = false;
            dgvTruongHoc.AllowUserToDeleteRows = false;
            dgvTruongHoc.ReadOnly = true;

            // auto-size theo grid (chỉ còn scroll dọc)
            dgvTruongHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTruongHoc.ScrollBars = ScrollBars.Vertical;
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
