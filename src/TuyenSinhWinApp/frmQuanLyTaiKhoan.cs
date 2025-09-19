using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TuyenSinhServiceLib;
using System.Drawing;
using TuyenSinhWinApp.TuyenSinhServiceReference;
using System.Drawing;

namespace TuyenSinhWinApp
{
    public partial class frmQuanLyTaiKhoan : Form
    {
        private readonly Service1Client _service = new Service1Client();
        private NguoiDung selectedUser;

        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
            SetupControls();
            dgvTaiKhoan.AutoGenerateColumns = false;
            LoadTruongToCombo();  // nạp danh sách trường vào combobox chọn trường
            LoadData();           // nạp danh sách tài khoản
            BuildGridColumns();
            StyleGrid();
        }

        private void SetupControls()
        {
            cmbVaiTro.Items.Clear();
            cmbVaiTro.Items.AddRange(new[] { "CanBoTruong", "ThuKy" });
            cmbVaiTro.SelectedIndex = 0;
            // combobox chọn trường (hiển thị tên – giá trị là mã)
            if (cmbTruong != null) cmbTruong.DropDownStyle = ComboBoxStyle.DropDownList;

            dgvTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTaiKhoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTaiKhoan.MultiSelect = false;

            // quyền thao tác
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled =
                Common.IsAdmin || Common.VaiTro == "CanBoTruong";

            // gắn event chọn dòng để tự fill
            dgvTaiKhoan.CellClick += dgvTaiKhoan_CellClick;
            dgvTaiKhoan.SelectionChanged += dgvTaiKhoan_SelectionChanged;
        }



        private void LoadTruongToCombo()
        {
            try
            {
                var dsTruong = _service.LayDanhSachTruong()?.ToList() ?? new List<TruongHoc>();
                cmbTruong.DataSource = dsTruong;
                cmbTruong.DisplayMember = "TenTruong";
                cmbTruong.ValueMember = "MaTruong";

                if (Common.IsAdmin)
                {
                    if (dsTruong.Count > 0) cmbTruong.SelectedIndex = 0;
                    cmbTruong.Enabled = true;
                }
                else
                {
                    // cán bộ trường chỉ được thao tác trên trường của mình
                    var idx = dsTruong.FindIndex(t => t.MaTruong == Common.MaTruong);
                    if (idx >= 0) cmbTruong.SelectedIndex = idx;
                    cmbTruong.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách trường: " + ex.Message);
            }
        }

        private void LoadData()
        {
            string maTruong = Common.IsAdmin ? null : Common.MaTruong;
            try
            {
                // Service trả về NguoiDung[] nên null-coalesce bằng mảng
                var arr = _service.LayDanhSachNguoiDungTheoTruong(maTruong);
                var ds = (arr ?? Array.Empty<NguoiDung>()).ToList();

                dgvTaiKhoan.DataSource = ds;

                // Ẩn cột mật khẩu trên lưới (vẫn fill vào textbox khi chọn dòng)
                if (dgvTaiKhoan.Columns.Contains("MatKhau"))
                    dgvTaiKhoan.Columns["MatKhau"].Visible = false;

                if (dgvTaiKhoan.Rows.Count > 0)
                {
                    dgvTaiKhoan.ClearSelection();
                    dgvTaiKhoan.Rows[0].Selected = true;
                    dgvTaiKhoan.CurrentCell = dgvTaiKhoan.Rows[0].Cells[0];
                    FillFormFromSelectedRow();
                }
                else
                {
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvTaiKhoan.DataSource = new List<NguoiDung>();
                ClearInputs();
            }
        }

        // ====== CRUD ======
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                cmbTruong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ: Tên đăng nhập, Mật khẩu, Họ tên và chọn Trường!");
                return;
            }

            var nguoiDung = new NguoiDung
            {
                TenDangNhap = txtTenDangNhap.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                MaTruong = cmbTruong.SelectedValue?.ToString(),
                VaiTro = cmbVaiTro.SelectedItem?.ToString() ?? "CanBoTruong"
            };

            bool success = _service.ThemNguoiDung(nguoiDung, txtMatKhau.Text);
            if (success)
            {
                MessageBox.Show("Thêm tài khoản thành công!");
                LoadData();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm tài khoản!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản để sửa!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                cmbTruong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ: Tên đăng nhập, Họ tên và chọn Trường!");
                return;
            }

            selectedUser.TenDangNhap = txtTenDangNhap.Text.Trim();
            selectedUser.HoTen = txtHoTen.Text.Trim();
            selectedUser.VaiTro = cmbVaiTro.SelectedItem?.ToString() ?? "CanBoTruong";
            selectedUser.MaTruong = cmbTruong.SelectedValue?.ToString();

            // Chỉ đổi mật khẩu nếu người dùng nhập mới
            string matKhauMoi = string.IsNullOrWhiteSpace(txtMatKhau.Text) ? null : txtMatKhau.Text;

            bool success = _service.CapNhatNguoiDung(selectedUser, matKhauMoi);
            if (success)
            {
                MessageBox.Show("Cập nhật tài khoản thành công!");
                LoadData();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Lỗi khi cập nhật tài khoản!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản để xóa!");
                return;
            }

            if (MessageBox.Show("Xác nhận xóa tài khoản?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool success = _service.XoaNguoiDung(selectedUser.MaNguoiDung);
                if (success)
                {
                    MessageBox.Show("Xóa tài khoản thành công!");
                    LoadData();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Lỗi khi xóa tài khoản!");
                }
            }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            LoadTruongToCombo();
            LoadData();
            ClearInputs();
        }

        // ====== Fill form khi chọn dòng ======
        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) FillFormFromSelectedRow();
        }

        private void dgvTaiKhoan_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.Focused) FillFormFromSelectedRow();
        }

        private void FillFormFromSelectedRow()
        {
            try
            {
                if (dgvTaiKhoan.CurrentRow == null) return;
                selectedUser = dgvTaiKhoan.CurrentRow.DataBoundItem as NguoiDung;
                if (selectedUser == null) return;

                txtTenDangNhap.Text = selectedUser.TenDangNhap ?? "";
                txtHoTen.Text = selectedUser.HoTen ?? "";
                txtMatKhau.Text = selectedUser.MatKhau ?? ""; // đã trả về từ service
                cmbVaiTro.SelectedItem = string.IsNullOrEmpty(selectedUser.VaiTro) ? "CanBoTruong" : selectedUser.VaiTro;

                // set trường theo MaTruong
                if (selectedUser.MaTruong != null && cmbTruong.Items.Count > 0)
                {
                    var current = cmbTruong.Items.Cast<TruongHoc>()
                        .FirstOrDefault(t => t.MaTruong == selectedUser.MaTruong);
                    if (current != null) cmbTruong.SelectedValue = current.MaTruong;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi fill dữ liệu: " + ex.Message);
            }
        }

        private void ClearInputs()
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtHoTen.Clear();
            cmbVaiTro.SelectedIndex = 0;

            // reset chọn trường
            if (Common.IsAdmin)
            {
                if (cmbTruong.Items.Count > 0) cmbTruong.SelectedIndex = 0;
                cmbTruong.Enabled = true;
            }
            else
            {
                // để đúng trường của người đang đăng nhập
                var dsTruong = cmbTruong.DataSource as List<TruongHoc>;
                var idx = dsTruong?.FindIndex(t => t.MaTruong == Common.MaTruong) ?? -1;
                if (idx >= 0) cmbTruong.SelectedIndex = idx;
                cmbTruong.Enabled = false;
            }

            selectedUser = null;
            dgvTaiKhoan.ClearSelection();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_service.State == System.ServiceModel.CommunicationState.Opened)
                _service.Close();
            base.OnFormClosing(e);
        }

        private void BuildGridColumns()
        {
            dgvTaiKhoan.Columns.Clear();

            Col("TenDangNhap", "Tài khoản", 18, min: 110, align: DataGridViewContentAlignment.MiddleLeft);
            Col("HoTen", "Họ tên", 26, min: 140, align: DataGridViewContentAlignment.MiddleLeft);
            Col("VaiTro", "Vai trò", 12, min: 90, align: DataGridViewContentAlignment.MiddleCenter);
            Col("MaTruong", "Mã trường", 12, min: 90, align: DataGridViewContentAlignment.MiddleCenter);

            dgvTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTaiKhoan.ScrollBars = ScrollBars.Vertical;
            dgvTaiKhoan.RowHeadersVisible = false;
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
                FillWeight = weight,
                MinimumWidth = min,
                SortMode = DataGridViewColumnSortMode.Programmatic
            };
            if (!string.IsNullOrEmpty(format)) c.DefaultCellStyle.Format = format;
            if (align.HasValue) c.DefaultCellStyle.Alignment = align.Value;

            dgvTaiKhoan.Columns.Add(c);
            return c;
        }

        private void StyleGrid()
        {
            dgvTaiKhoan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            dgvTaiKhoan.BackgroundColor = Color.White;
            dgvTaiKhoan.BorderStyle = BorderStyle.None;
            dgvTaiKhoan.GridColor = Color.FromArgb(235, 238, 242);
            dgvTaiKhoan.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTaiKhoan.RowHeadersVisible = false;

            dgvTaiKhoan.EnableHeadersVisualStyles = false;
            dgvTaiKhoan.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvTaiKhoan.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(76, 95, 173);
            dgvTaiKhoan.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTaiKhoan.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTaiKhoan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.5f);
            dgvTaiKhoan.ColumnHeadersHeight = 36;

            dgvTaiKhoan.DefaultCellStyle.Font = new Font("Segoe UI", 10f);
            dgvTaiKhoan.DefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dgvTaiKhoan.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 240, 255);
            dgvTaiKhoan.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 37, 41);
            dgvTaiKhoan.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 255);
            dgvTaiKhoan.RowTemplate.Height = 32;

            dgvTaiKhoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTaiKhoan.MultiSelect = false;
            dgvTaiKhoan.AllowUserToAddRows = false;
            dgvTaiKhoan.AllowUserToDeleteRows = false;
            dgvTaiKhoan.ReadOnly = true;

            dgvTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTaiKhoan.ScrollBars = ScrollBars.Vertical;
        }

        private void dgvTaiKhoan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null) return;
            var prop = dgvTaiKhoan.Columns[e.ColumnIndex].DataPropertyName;
            if (prop == "VaiTro")
            {
                var v = e.Value.ToString();
                if (v == "CanBoTruong")
                {
                    e.CellStyle.ForeColor = Color.DodgerBlue;
                    e.CellStyle.Font = new Font(dgvTaiKhoan.Font, FontStyle.Bold);
                }
                else if (v == "ThuKy")
                {
                    e.CellStyle.ForeColor = Color.MediumVioletRed;
                }
            }
        }


    }
}
