using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TuyenSinhServiceLib;
using TuyenSinhWinApp.TuyenSinhServiceReference;

namespace TuyenSinhWinApp
{
    public partial class frmDotTuyenSinh : Form
    {
        private readonly Service1Client client;
        private DotTuyenSinh selected;

        public frmDotTuyenSinh()
        {
            InitializeComponent();
            client = new Service1Client();

            dgvDotTuyenSinh.AutoGenerateColumns = false;
            BuildGridColumns();
            StyleGrid();

            dgvDotTuyenSinh.CellClick += (s, e) => { if (e.RowIndex >= 0) FillFormFromSelectedRow(); };
            dgvDotTuyenSinh.SelectionChanged += (s, e) => { if (dgvDotTuyenSinh.Focused) FillFormFromSelectedRow(); };
            dgvDotTuyenSinh.CellFormatting += dgvDotTuyenSinh_CellFormatting;

            btnThem.Click += btnThem_Click;
            btnCapNhat.Click += btnCapNhat_Click;

            Load += frmDotTuyenSinh_Load;
        }

        private void frmDotTuyenSinh_Load(object sender, EventArgs e)
        {
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new[] { "DangMo", "DaDong" });
            cboTrangThai.SelectedIndex = 0;

            LoadDanhSachDot();
            ApplyPermissions();
        }

        private void LoadDanhSachDot()
        {
            try
            {
                var ds = client.LayDanhSachDotTuyen() ?? Array.Empty<DotTuyenSinh>();
                dgvDotTuyenSinh.DataSource = ds.ToList();

                if (dgvDotTuyenSinh.Rows.Count > 0)
                {
                    dgvDotTuyenSinh.ClearSelection();
                    dgvDotTuyenSinh.Rows[0].Selected = true;
                    dgvDotTuyenSinh.CurrentCell = dgvDotTuyenSinh.Rows[0].Cells[0];
                    FillFormFromSelectedRow();
                }
                else
                {
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvDotTuyenSinh.DataSource = new List<DotTuyenSinh>();
                ClearInputs();
            }
        }

        private void ApplyPermissions()
        {
            bool canEdit = Common.IsAdmin;

            btnThem.Enabled = canEdit;
            btnCapNhat.Enabled = canEdit;

            txtMaDot.ReadOnly = !canEdit;
            txtTenDot.ReadOnly = !canEdit;
            dtpBatDau.Enabled = canEdit;
            dtpKetThuc.Enabled = canEdit;
            cboTrangThai.Enabled = canEdit;

            dgvDotTuyenSinh.ReadOnly = true;
            dgvDotTuyenSinh.AllowUserToAddRows = false;
            dgvDotTuyenSinh.AllowUserToDeleteRows = false;
            dgvDotTuyenSinh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            btnThem.Cursor = canEdit ? Cursors.Hand : Cursors.No;
            btnCapNhat.Cursor = canEdit ? Cursors.Hand : Cursors.No;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!Common.IsAdmin)
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác này.", "Không có quyền",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(txtMaDot.Text) || string.IsNullOrWhiteSpace(txtTenDot.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ mã đợt và tên đợt.", "Thiếu thông tin",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dtpBatDau.Value > dtpKetThuc.Value)
                {
                    MessageBox.Show("Ngày bắt đầu không được sau ngày kết thúc.", "Lỗi dữ liệu",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                bool result = client.ThemDotTuyen(dot);

                if (result)
                {
                    MessageBox.Show("Thêm đợt tuyển sinh thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachDot();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại. Vui lòng kiểm tra lại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!Common.IsAdmin)
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác này.", "Không có quyền",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(txtMaDot.Text))
                {
                    MessageBox.Show("Vui lòng chọn đợt tuyển sinh để cập nhật.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dtpBatDau.Value > dtpKetThuc.Value)
                {
                    MessageBox.Show("Ngày bắt đầu không hợp lệ.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Cập nhật thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachDot();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDotTuyenSinh_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var colName = dgvDotTuyenSinh.Columns[e.ColumnIndex].DataPropertyName;
            if (colName == "TrangThai" && e.Value != null)
            {
                var v = e.Value.ToString();
                if (v.Equals("DangMo", StringComparison.OrdinalIgnoreCase)) e.Value = "Đang mở";
                else if (v.Equals("DaDong", StringComparison.OrdinalIgnoreCase)) e.Value = "Đã đóng";
            }
        }

        private void FillFormFromSelectedRow()
        {
            if (dgvDotTuyenSinh.CurrentRow == null) return;
            selected = dgvDotTuyenSinh.CurrentRow.DataBoundItem as DotTuyenSinh;
            if (selected == null) return;

            txtMaDot.Text = selected.MaDot ?? "";
            txtTenDot.Text = selected.TenDot ?? "";
            dtpBatDau.Value = selected.NgayBatDau ?? DateTime.Now;
            dtpKetThuc.Value = selected.NgayKetThuc ?? DateTime.Now;

            var st = string.IsNullOrEmpty(selected.TrangThai) ? "DangMo" : selected.TrangThai;
            if (!cboTrangThai.Items.Contains(st)) cboTrangThai.Items.Add(st);
            cboTrangThai.SelectedItem = st;
        }

        private void ClearInputs()
        {
            txtMaDot.Clear();
            txtTenDot.Clear();
            dtpBatDau.Value = DateTime.Now;
            dtpKetThuc.Value = DateTime.Now;
            cboTrangThai.SelectedIndex = 0;
            selected = null;
            dgvDotTuyenSinh.ClearSelection();
        }

        private void BuildGridColumns()
        {
            dgvDotTuyenSinh.Columns.Clear();

            AddCol("MaDot", "Mã đợt", 16, 110, DataGridViewContentAlignment.MiddleLeft);
            AddCol("TenDot", "Tên đợt", 28, 180, DataGridViewContentAlignment.MiddleLeft);
            AddCol("Nam", "Năm", 10, 70, DataGridViewContentAlignment.MiddleCenter);
            AddCol("NgayBatDau", "Ngày bắt đầu", 18, 130, DataGridViewContentAlignment.MiddleCenter, "dd/MM/yyyy");
            AddCol("NgayKetThuc", "Ngày kết thúc", 18, 130, DataGridViewContentAlignment.MiddleCenter, "dd/MM/yyyy");
            AddCol("TrangThai", "Trạng thái", 10, 100, DataGridViewContentAlignment.MiddleCenter);

            dgvDotTuyenSinh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDotTuyenSinh.RowHeadersVisible = false;
            dgvDotTuyenSinh.ReadOnly = true;
            dgvDotTuyenSinh.AllowUserToAddRows = false;
            dgvDotTuyenSinh.AllowUserToDeleteRows = false;
            dgvDotTuyenSinh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDotTuyenSinh.MultiSelect = false;
        }

        private DataGridViewTextBoxColumn AddCol(string prop, string header, float weight, int minWidth,
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
            c.DefaultCellStyle.Alignment = align;
            if (!string.IsNullOrWhiteSpace(format)) c.DefaultCellStyle.Format = format;
            dgvDotTuyenSinh.Columns.Add(c);
            return c;
        }

        private void StyleGrid()
        {
            var dgv = dgvDotTuyenSinh;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = Color.FromArgb(180, 186, 194);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(76, 95, 173);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.5f);
            dgv.ColumnHeadersHeight = 36;

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10f);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 240, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(33, 37, 41);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 255);
            dgv.RowTemplate.Height = 32;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (client.State == System.ServiceModel.CommunicationState.Opened)
                client.Close();
            base.OnFormClosing(e);
        }

        private void dgvDotTuyenSinh_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txtTenDot_TextChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}
