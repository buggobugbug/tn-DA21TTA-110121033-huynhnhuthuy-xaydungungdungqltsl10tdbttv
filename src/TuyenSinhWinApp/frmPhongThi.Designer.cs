namespace TuyenSinhWinApp
{
    partial class frmPhongThi
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
            this.dgvPhongThi = new System.Windows.Forms.DataGridView();
            this.MaPhongThi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuongToiDa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuongHienTai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maDotDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phongThiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label18 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvHocSinhTrongPhong = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbDotTuyenSinh = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnXuatDanhSach = new System.Windows.Forms.Button();
            this.btnXemHocSinh = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.phongThiBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.phongThiBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.phongThiBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.phongThiBindingSource4 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongThi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocSinhTrongPhong)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource4)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPhongThi
            // 
            this.dgvPhongThi.AllowUserToDeleteRows = false;
            this.dgvPhongThi.AutoGenerateColumns = false;
            this.dgvPhongThi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhongThi.BackgroundColor = System.Drawing.Color.White;
            this.dgvPhongThi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhongThi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaPhongThi,
            this.SoLuongToiDa,
            this.SoLuongHienTai,
            this.maDotDataGridViewTextBoxColumn});
            this.dgvPhongThi.DataSource = this.phongThiBindingSource;
            this.dgvPhongThi.Location = new System.Drawing.Point(16, 78);
            this.dgvPhongThi.Name = "dgvPhongThi";
            this.dgvPhongThi.ReadOnly = true;
            this.dgvPhongThi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhongThi.Size = new System.Drawing.Size(1666, 431);
            this.dgvPhongThi.TabIndex = 0;
            this.dgvPhongThi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhongThi_CellClick);
            // 
            // MaPhongThi
            // 
            this.MaPhongThi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MaPhongThi.DataPropertyName = "MaPhongThi";
            this.MaPhongThi.HeaderText = "Phòng";
            this.MaPhongThi.Name = "MaPhongThi";
            this.MaPhongThi.ReadOnly = true;
            // 
            // SoLuongToiDa
            // 
            this.SoLuongToiDa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SoLuongToiDa.DataPropertyName = "SoLuongToiDa";
            this.SoLuongToiDa.FillWeight = 119.2592F;
            this.SoLuongToiDa.HeaderText = "Số lượng tối đa";
            this.SoLuongToiDa.Name = "SoLuongToiDa";
            this.SoLuongToiDa.ReadOnly = true;
            // 
            // SoLuongHienTai
            // 
            this.SoLuongHienTai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SoLuongHienTai.DataPropertyName = "SoLuongHienTai";
            this.SoLuongHienTai.FillWeight = 218.3234F;
            this.SoLuongHienTai.HeaderText = "Số thí sinh";
            this.SoLuongHienTai.Name = "SoLuongHienTai";
            this.SoLuongHienTai.ReadOnly = true;
            // 
            // maDotDataGridViewTextBoxColumn
            // 
            this.maDotDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.maDotDataGridViewTextBoxColumn.DataPropertyName = "MaDot";
            this.maDotDataGridViewTextBoxColumn.HeaderText = "Mã đợt";
            this.maDotDataGridViewTextBoxColumn.Name = "maDotDataGridViewTextBoxColumn";
            this.maDotDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // phongThiBindingSource
            // 
            this.phongThiBindingSource.DataSource = typeof(TuyenSinhServiceLib.PhongThi);
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(16, 13);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(273, 62);
            this.label18.TabIndex = 44;
            this.label18.Text = "DANH SÁCH PHÒNG THI";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 512);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(434, 64);
            this.label1.TabIndex = 47;
            this.label1.Text = "DANH SÁCH THÍ SINH TRONG PHÒNG THI";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvHocSinhTrongPhong
            // 
            this.dgvHocSinhTrongPhong.BackgroundColor = System.Drawing.Color.White;
            this.dgvHocSinhTrongPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHocSinhTrongPhong.Location = new System.Drawing.Point(16, 579);
            this.dgvHocSinhTrongPhong.Name = "dgvHocSinhTrongPhong";
            this.dgvHocSinhTrongPhong.Size = new System.Drawing.Size(1666, 450);
            this.dgvHocSinhTrongPhong.TabIndex = 48;
            this.dgvHocSinhTrongPhong.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHocSinhTrongPhong_CellContentClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(165)))), ((int)(((byte)(249)))));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 1036);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1690, 5);
            this.panel1.TabIndex = 51;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(165)))), ((int)(((byte)(249)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, -5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1690, 10);
            this.panel2.TabIndex = 52;
            // 
            // cbDotTuyenSinh
            // 
            this.cbDotTuyenSinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDotTuyenSinh.FormattingEnabled = true;
            this.cbDotTuyenSinh.Location = new System.Drawing.Point(1161, 35);
            this.cbDotTuyenSinh.Name = "cbDotTuyenSinh";
            this.cbDotTuyenSinh.Size = new System.Drawing.Size(304, 24);
            this.cbDotTuyenSinh.TabIndex = 55;
            this.cbDotTuyenSinh.SelectedIndexChanged += new System.EventHandler(this.cbDotTuyenSinh_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(1031, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(124, 21);
            this.label11.TabIndex = 56;
            this.label11.Text = "Đợt tuyển sinh :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // btnXuatDanhSach
            // 
            this.btnXuatDanhSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatDanhSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatDanhSach.Image = global::TuyenSinhWinApp.Properties.Resources.excel;
            this.btnXuatDanhSach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXuatDanhSach.Location = new System.Drawing.Point(1422, 527);
            this.btnXuatDanhSach.Name = "btnXuatDanhSach";
            this.btnXuatDanhSach.Size = new System.Drawing.Size(260, 36);
            this.btnXuatDanhSach.TabIndex = 46;
            this.btnXuatDanhSach.Text = "Xuất danh sách thí sinh trong phòng";
            this.btnXuatDanhSach.UseVisualStyleBackColor = true;
            this.btnXuatDanhSach.Click += new System.EventHandler(this.btnXuatDanhSach_Click);
            // 
            // btnXemHocSinh
            // 
            this.btnXemHocSinh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemHocSinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemHocSinh.Image = global::TuyenSinhWinApp.Properties.Resources.view;
            this.btnXemHocSinh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXemHocSinh.Location = new System.Drawing.Point(1205, 527);
            this.btnXemHocSinh.Name = "btnXemHocSinh";
            this.btnXemHocSinh.Size = new System.Drawing.Size(186, 36);
            this.btnXemHocSinh.TabIndex = 2;
            this.btnXemHocSinh.Text = "Xem danh sách học sinh";
            this.btnXemHocSinh.UseVisualStyleBackColor = true;
            this.btnXemHocSinh.Click += new System.EventHandler(this.btnXemHocSinh_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::TuyenSinhWinApp.Properties.Resources.compose;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(1496, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(186, 36);
            this.button1.TabIndex = 45;
            this.button1.Text = "Chia phòng thi";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // phongThiBindingSource3
            // 
            this.phongThiBindingSource3.DataSource = typeof(TuyenSinhServiceLib.PhongThi);
            // 
            // phongThiBindingSource2
            // 
            this.phongThiBindingSource2.DataSource = typeof(TuyenSinhServiceLib.PhongThi);
            // 
            // phongThiBindingSource1
            // 
            this.phongThiBindingSource1.DataSource = typeof(TuyenSinhServiceLib.PhongThi);
            // 
            // phongThiBindingSource4
            // 
            this.phongThiBindingSource4.DataSource = typeof(TuyenSinhServiceLib.PhongThi);
            // 
            // frmPhongThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1690, 1041);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbDotTuyenSinh);
            this.Controls.Add(this.btnXuatDanhSach);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnXemHocSinh);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvHocSinhTrongPhong);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.dgvPhongThi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPhongThi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phòng Thi";
            this.Load += new System.EventHandler(this.frmPhongThi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongThi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocSinhTrongPhong)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPhongThi;
        private System.Windows.Forms.BindingSource phongThiBindingSource;
        private System.Windows.Forms.BindingSource phongThiBindingSource1;
        private System.Windows.Forms.Button btnXemHocSinh;
        private System.Windows.Forms.BindingSource phongThiBindingSource2;
        private System.Windows.Forms.BindingSource phongThiBindingSource3;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvHocSinhTrongPhong;
        private System.Windows.Forms.Button btnXuatDanhSach;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.BindingSource phongThiBindingSource4;
        private System.Windows.Forms.ComboBox cbDotTuyenSinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaPhongThi;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuongToiDa;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuongHienTai;
        private System.Windows.Forms.DataGridViewTextBoxColumn maDotDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label11;
    }
}