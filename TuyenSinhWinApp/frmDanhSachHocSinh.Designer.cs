namespace TuyenSinhWinApp
{
    partial class frmDanhSachHocSinh
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvHocSinh = new System.Windows.Forms.DataGridView();
            this.hocSinhBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.hocSinhBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnChiaPhongThi = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.hocSinhBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.maHocSinhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maSoBaoDanhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaDot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hoTenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngaySinhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gioiTinhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.danTocDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noiSinhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.truongTHCSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maTruongDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diemToanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diemVanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diemAnhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diemTongDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diemKhuyenKhichDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diemUuTienDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phongThiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trangThaiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayDangKyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ghiChuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocSinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hocSinhBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hocSinhBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hocSinhBindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHocSinh
            // 
            this.dgvHocSinh.AllowUserToAddRows = false;
            this.dgvHocSinh.AllowUserToDeleteRows = false;
            this.dgvHocSinh.AutoGenerateColumns = false;
            this.dgvHocSinh.BackgroundColor = System.Drawing.Color.White;
            this.dgvHocSinh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHocSinh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.maHocSinhDataGridViewTextBoxColumn,
            this.maSoBaoDanhDataGridViewTextBoxColumn,
            this.MaDot,
            this.hoTenDataGridViewTextBoxColumn,
            this.ngaySinhDataGridViewTextBoxColumn,
            this.gioiTinhDataGridViewTextBoxColumn,
            this.danTocDataGridViewTextBoxColumn,
            this.noiSinhDataGridViewTextBoxColumn,
            this.truongTHCSDataGridViewTextBoxColumn,
            this.maTruongDataGridViewTextBoxColumn,
            this.diemToanDataGridViewTextBoxColumn,
            this.diemVanDataGridViewTextBoxColumn,
            this.diemAnhDataGridViewTextBoxColumn,
            this.diemTongDataGridViewTextBoxColumn,
            this.diemKhuyenKhichDataGridViewTextBoxColumn,
            this.diemUuTienDataGridViewTextBoxColumn,
            this.phongThiDataGridViewTextBoxColumn,
            this.trangThaiDataGridViewTextBoxColumn,
            this.ngayDangKyDataGridViewTextBoxColumn,
            this.ghiChuDataGridViewTextBoxColumn});
            this.dgvHocSinh.DataSource = this.hocSinhBindingSource2;
            this.dgvHocSinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHocSinh.GridColor = System.Drawing.Color.LightGray;
            this.dgvHocSinh.Location = new System.Drawing.Point(0, 0);
            this.dgvHocSinh.Name = "dgvHocSinh";
            this.dgvHocSinh.ReadOnly = true;
            this.dgvHocSinh.Size = new System.Drawing.Size(1924, 439);
            this.dgvHocSinh.TabIndex = 0;
            // 
            // hocSinhBindingSource1
            // 
            this.hocSinhBindingSource1.DataSource = typeof(TuyenSinhServiceLib.HocSinh);
            // 
            // hocSinhBindingSource
            // 
            this.hocSinhBindingSource.DataSource = typeof(TuyenSinhServiceLib.HocSinh);
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnTaiLai.FlatAppearance.BorderSize = 0;
            this.btnTaiLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaiLai.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaiLai.ForeColor = System.Drawing.Color.White;
            this.btnTaiLai.Location = new System.Drawing.Point(684, 51);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(167, 29);
            this.btnTaiLai.TabIndex = 1;
            this.btnTaiLai.Text = "Tải lại";
            this.btnTaiLai.UseVisualStyleBackColor = false;
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(229, 51);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(168, 29);
            this.btnSua.TabIndex = 2;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(454, 51);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(162, 29);
            this.btnXoa.TabIndex = 3;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnChiaPhongThi
            // 
            this.btnChiaPhongThi.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnChiaPhongThi.FlatAppearance.BorderSize = 0;
            this.btnChiaPhongThi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChiaPhongThi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChiaPhongThi.ForeColor = System.Drawing.Color.White;
            this.btnChiaPhongThi.Location = new System.Drawing.Point(27, 51);
            this.btnChiaPhongThi.Name = "btnChiaPhongThi";
            this.btnChiaPhongThi.Size = new System.Drawing.Size(151, 29);
            this.btnChiaPhongThi.TabIndex = 4;
            this.btnChiaPhongThi.Text = "Chia phòng thi";
            this.btnChiaPhongThi.UseVisualStyleBackColor = false;
            this.btnChiaPhongThi.Click += new System.EventHandler(this.btnChiaPhongThi_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1924, 64);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1921, 40);
            this.label1.TabIndex = 5;
            this.label1.Text = "DANH SÁCH HỌC SINH";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.dgvHocSinh);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 64);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1924, 439);
            this.panel2.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.btnTaiLai);
            this.panel3.Controls.Add(this.btnXoa);
            this.panel3.Controls.Add(this.btnChiaPhongThi);
            this.panel3.Controls.Add(this.btnSua);
            this.panel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 319);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1924, 120);
            this.panel3.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.txtTimKiem);
            this.panel4.Location = new System.Drawing.Point(967, 54);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3);
            this.panel4.Size = new System.Drawing.Size(250, 24);
            this.panel4.TabIndex = 6;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTimKiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTimKiem.ForeColor = System.Drawing.Color.Gray;
            this.txtTimKiem.Location = new System.Drawing.Point(3, 3);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(242, 13);
            this.txtTimKiem.TabIndex = 5;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TuyenSinhWinApp.Properties.Resources.web;
            this.pictureBox1.Location = new System.Drawing.Point(924, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // hocSinhBindingSource2
            // 
            this.hocSinhBindingSource2.DataSource = typeof(TuyenSinhServiceLib.HocSinh);
            // 
            // maHocSinhDataGridViewTextBoxColumn
            // 
            this.maHocSinhDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.maHocSinhDataGridViewTextBoxColumn.DataPropertyName = "MaHocSinh";
            this.maHocSinhDataGridViewTextBoxColumn.FillWeight = 50F;
            this.maHocSinhDataGridViewTextBoxColumn.HeaderText = "Mã Học Sinh";
            this.maHocSinhDataGridViewTextBoxColumn.Name = "maHocSinhDataGridViewTextBoxColumn";
            this.maHocSinhDataGridViewTextBoxColumn.ReadOnly = true;
            this.maHocSinhDataGridViewTextBoxColumn.Visible = false;
            // 
            // maSoBaoDanhDataGridViewTextBoxColumn
            // 
            this.maSoBaoDanhDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.maSoBaoDanhDataGridViewTextBoxColumn.DataPropertyName = "MaSoBaoDanh";
            this.maSoBaoDanhDataGridViewTextBoxColumn.FillWeight = 50F;
            this.maSoBaoDanhDataGridViewTextBoxColumn.HeaderText = "Mã Số Báo Danh";
            this.maSoBaoDanhDataGridViewTextBoxColumn.Name = "maSoBaoDanhDataGridViewTextBoxColumn";
            this.maSoBaoDanhDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // MaDot
            // 
            this.MaDot.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MaDot.DataPropertyName = "MaDot";
            this.MaDot.HeaderText = "Mã Đợt Tuyển Sinh";
            this.MaDot.Name = "MaDot";
            this.MaDot.ReadOnly = true;
            // 
            // hoTenDataGridViewTextBoxColumn
            // 
            this.hoTenDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.hoTenDataGridViewTextBoxColumn.DataPropertyName = "HoTen";
            this.hoTenDataGridViewTextBoxColumn.FillWeight = 150F;
            this.hoTenDataGridViewTextBoxColumn.HeaderText = "Họ Tên";
            this.hoTenDataGridViewTextBoxColumn.Name = "hoTenDataGridViewTextBoxColumn";
            this.hoTenDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ngaySinhDataGridViewTextBoxColumn
            // 
            this.ngaySinhDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ngaySinhDataGridViewTextBoxColumn.DataPropertyName = "NgaySinh";
            this.ngaySinhDataGridViewTextBoxColumn.FillWeight = 150F;
            this.ngaySinhDataGridViewTextBoxColumn.HeaderText = "Ngày Sinh";
            this.ngaySinhDataGridViewTextBoxColumn.Name = "ngaySinhDataGridViewTextBoxColumn";
            this.ngaySinhDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // gioiTinhDataGridViewTextBoxColumn
            // 
            this.gioiTinhDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.gioiTinhDataGridViewTextBoxColumn.DataPropertyName = "GioiTinh";
            this.gioiTinhDataGridViewTextBoxColumn.FillWeight = 50F;
            this.gioiTinhDataGridViewTextBoxColumn.HeaderText = "Giới Tính";
            this.gioiTinhDataGridViewTextBoxColumn.Name = "gioiTinhDataGridViewTextBoxColumn";
            this.gioiTinhDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // danTocDataGridViewTextBoxColumn
            // 
            this.danTocDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.danTocDataGridViewTextBoxColumn.DataPropertyName = "DanToc";
            this.danTocDataGridViewTextBoxColumn.FillWeight = 50F;
            this.danTocDataGridViewTextBoxColumn.HeaderText = "Dân Tộc";
            this.danTocDataGridViewTextBoxColumn.Name = "danTocDataGridViewTextBoxColumn";
            this.danTocDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // noiSinhDataGridViewTextBoxColumn
            // 
            this.noiSinhDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.noiSinhDataGridViewTextBoxColumn.DataPropertyName = "NoiSinh";
            this.noiSinhDataGridViewTextBoxColumn.FillWeight = 150F;
            this.noiSinhDataGridViewTextBoxColumn.HeaderText = "Nơi Sinh";
            this.noiSinhDataGridViewTextBoxColumn.Name = "noiSinhDataGridViewTextBoxColumn";
            this.noiSinhDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // truongTHCSDataGridViewTextBoxColumn
            // 
            this.truongTHCSDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.truongTHCSDataGridViewTextBoxColumn.DataPropertyName = "TruongTHCS";
            this.truongTHCSDataGridViewTextBoxColumn.FillWeight = 150F;
            this.truongTHCSDataGridViewTextBoxColumn.HeaderText = "Trường Trung Học Cơ ";
            this.truongTHCSDataGridViewTextBoxColumn.Name = "truongTHCSDataGridViewTextBoxColumn";
            this.truongTHCSDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // maTruongDataGridViewTextBoxColumn
            // 
            this.maTruongDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.maTruongDataGridViewTextBoxColumn.DataPropertyName = "MaTruong";
            this.maTruongDataGridViewTextBoxColumn.FillWeight = 50F;
            this.maTruongDataGridViewTextBoxColumn.HeaderText = "Mã Trường";
            this.maTruongDataGridViewTextBoxColumn.Name = "maTruongDataGridViewTextBoxColumn";
            this.maTruongDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // diemToanDataGridViewTextBoxColumn
            // 
            this.diemToanDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.diemToanDataGridViewTextBoxColumn.DataPropertyName = "DiemToan";
            this.diemToanDataGridViewTextBoxColumn.FillWeight = 50F;
            this.diemToanDataGridViewTextBoxColumn.HeaderText = "Điểm Toán";
            this.diemToanDataGridViewTextBoxColumn.Name = "diemToanDataGridViewTextBoxColumn";
            this.diemToanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // diemVanDataGridViewTextBoxColumn
            // 
            this.diemVanDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.diemVanDataGridViewTextBoxColumn.DataPropertyName = "DiemVan";
            this.diemVanDataGridViewTextBoxColumn.FillWeight = 50F;
            this.diemVanDataGridViewTextBoxColumn.HeaderText = "Điểm ";
            this.diemVanDataGridViewTextBoxColumn.Name = "diemVanDataGridViewTextBoxColumn";
            this.diemVanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // diemAnhDataGridViewTextBoxColumn
            // 
            this.diemAnhDataGridViewTextBoxColumn.DataPropertyName = "DiemAnh";
            this.diemAnhDataGridViewTextBoxColumn.FillWeight = 50F;
            this.diemAnhDataGridViewTextBoxColumn.HeaderText = "Điểm Anh";
            this.diemAnhDataGridViewTextBoxColumn.Name = "diemAnhDataGridViewTextBoxColumn";
            this.diemAnhDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // diemTongDataGridViewTextBoxColumn
            // 
            this.diemTongDataGridViewTextBoxColumn.DataPropertyName = "DiemTong";
            this.diemTongDataGridViewTextBoxColumn.FillWeight = 50F;
            this.diemTongDataGridViewTextBoxColumn.HeaderText = "Điểm Tổng";
            this.diemTongDataGridViewTextBoxColumn.Name = "diemTongDataGridViewTextBoxColumn";
            this.diemTongDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // diemKhuyenKhichDataGridViewTextBoxColumn
            // 
            this.diemKhuyenKhichDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.diemKhuyenKhichDataGridViewTextBoxColumn.DataPropertyName = "DiemKhuyenKhich";
            this.diemKhuyenKhichDataGridViewTextBoxColumn.FillWeight = 50F;
            this.diemKhuyenKhichDataGridViewTextBoxColumn.HeaderText = "Điểm Khuyến Khích";
            this.diemKhuyenKhichDataGridViewTextBoxColumn.Name = "diemKhuyenKhichDataGridViewTextBoxColumn";
            this.diemKhuyenKhichDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // diemUuTienDataGridViewTextBoxColumn
            // 
            this.diemUuTienDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.diemUuTienDataGridViewTextBoxColumn.DataPropertyName = "DiemUuTien";
            this.diemUuTienDataGridViewTextBoxColumn.FillWeight = 50F;
            this.diemUuTienDataGridViewTextBoxColumn.HeaderText = "Điểm Ưu Tiên";
            this.diemUuTienDataGridViewTextBoxColumn.Name = "diemUuTienDataGridViewTextBoxColumn";
            this.diemUuTienDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // phongThiDataGridViewTextBoxColumn
            // 
            this.phongThiDataGridViewTextBoxColumn.DataPropertyName = "PhongThi";
            this.phongThiDataGridViewTextBoxColumn.FillWeight = 50F;
            this.phongThiDataGridViewTextBoxColumn.HeaderText = "Phòng Thi";
            this.phongThiDataGridViewTextBoxColumn.Name = "phongThiDataGridViewTextBoxColumn";
            this.phongThiDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // trangThaiDataGridViewTextBoxColumn
            // 
            this.trangThaiDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.trangThaiDataGridViewTextBoxColumn.DataPropertyName = "TrangThai";
            this.trangThaiDataGridViewTextBoxColumn.HeaderText = "Trạng ";
            this.trangThaiDataGridViewTextBoxColumn.Name = "trangThaiDataGridViewTextBoxColumn";
            this.trangThaiDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ngayDangKyDataGridViewTextBoxColumn
            // 
            this.ngayDangKyDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ngayDangKyDataGridViewTextBoxColumn.DataPropertyName = "NgayDangKy";
            this.ngayDangKyDataGridViewTextBoxColumn.FillWeight = 150F;
            this.ngayDangKyDataGridViewTextBoxColumn.HeaderText = "Ngày Đăng Ký";
            this.ngayDangKyDataGridViewTextBoxColumn.Name = "ngayDangKyDataGridViewTextBoxColumn";
            this.ngayDangKyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ghiChuDataGridViewTextBoxColumn
            // 
            this.ghiChuDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ghiChuDataGridViewTextBoxColumn.DataPropertyName = "GhiChu";
            this.ghiChuDataGridViewTextBoxColumn.HeaderText = "Ghi Chú";
            this.ghiChuDataGridViewTextBoxColumn.Name = "ghiChuDataGridViewTextBoxColumn";
            this.ghiChuDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // frmDanhSachHocSinh
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1924, 503);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDanhSachHocSinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDanhSachHocSinh";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDanhSachHocSinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocSinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hocSinhBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hocSinhBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hocSinhBindingSource2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHocSinh;
        private System.Windows.Forms.BindingSource hocSinhBindingSource;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnChiaPhongThi;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.BindingSource hocSinhBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn maHocSinhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maSoBaoDanhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaDot;
        private System.Windows.Forms.DataGridViewTextBoxColumn hoTenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngaySinhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gioiTinhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn danTocDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noiSinhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn truongTHCSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maTruongDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diemToanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diemVanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diemAnhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diemTongDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diemKhuyenKhichDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diemUuTienDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phongThiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trangThaiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayDangKyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ghiChuDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource hocSinhBindingSource2;
    }
}