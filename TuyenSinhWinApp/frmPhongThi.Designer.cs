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
            this.GiamThi1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiamThi2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtGiamThi2 = new System.Windows.Forms.TextBox();
            this.lblGiamThi2 = new System.Windows.Forms.Label();
            this.txtGiamThi1 = new System.Windows.Forms.TextBox();
            this.lblGiamThi1 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvHocSinhTrongPhong = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCapNhatDiem = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnXuatDanhSach = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnXemHocSinh = new System.Windows.Forms.Button();
            this.maDotDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phongThiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.phongThiBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.phongThiBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.phongThiBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txtTimTen = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongThi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocSinhTrongPhong)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource1)).BeginInit();
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
            this.GiamThi1,
            this.GiamThi2,
            this.maDotDataGridViewTextBoxColumn});
            this.dgvPhongThi.DataSource = this.phongThiBindingSource;
            this.dgvPhongThi.Location = new System.Drawing.Point(16, 59);
            this.dgvPhongThi.Name = "dgvPhongThi";
            this.dgvPhongThi.ReadOnly = true;
            this.dgvPhongThi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhongThi.Size = new System.Drawing.Size(1119, 272);
            this.dgvPhongThi.TabIndex = 0;
            this.dgvPhongThi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhongThi_CellClick);
            // 
            // MaPhongThi
            // 
            this.MaPhongThi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MaPhongThi.DataPropertyName = "MaPhongThi";
            this.MaPhongThi.HeaderText = "Phòng";
            this.MaPhongThi.Name = "MaPhongThi";
            this.MaPhongThi.ReadOnly = true;
            this.MaPhongThi.Width = 50;
            // 
            // SoLuongToiDa
            // 
            this.SoLuongToiDa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SoLuongToiDa.DataPropertyName = "SoLuongToiDa";
            this.SoLuongToiDa.FillWeight = 119.2592F;
            this.SoLuongToiDa.HeaderText = "Số lượng tối đa";
            this.SoLuongToiDa.Name = "SoLuongToiDa";
            this.SoLuongToiDa.ReadOnly = true;
            this.SoLuongToiDa.Width = 120;
            // 
            // SoLuongHienTai
            // 
            this.SoLuongHienTai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SoLuongHienTai.DataPropertyName = "SoLuongHienTai";
            this.SoLuongHienTai.FillWeight = 218.3234F;
            this.SoLuongHienTai.HeaderText = "Số thí sinh";
            this.SoLuongHienTai.Name = "SoLuongHienTai";
            this.SoLuongHienTai.ReadOnly = true;
            // 
            // GiamThi1
            // 
            this.GiamThi1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GiamThi1.DataPropertyName = "GiamThi1";
            this.GiamThi1.FillWeight = 36.71898F;
            this.GiamThi1.HeaderText = "Giám thị 1";
            this.GiamThi1.Name = "GiamThi1";
            this.GiamThi1.ReadOnly = true;
            this.GiamThi1.Width = 260;
            // 
            // GiamThi2
            // 
            this.GiamThi2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GiamThi2.DataPropertyName = "GiamThi2";
            this.GiamThi2.FillWeight = 22.28736F;
            this.GiamThi2.HeaderText = "Giám thị 2";
            this.GiamThi2.Name = "GiamThi2";
            this.GiamThi2.ReadOnly = true;
            // 
            // txtGiamThi2
            // 
            this.txtGiamThi2.Location = new System.Drawing.Point(95, 214);
            this.txtGiamThi2.Multiline = true;
            this.txtGiamThi2.Name = "txtGiamThi2";
            this.txtGiamThi2.Size = new System.Drawing.Size(375, 38);
            this.txtGiamThi2.TabIndex = 41;
            // 
            // lblGiamThi2
            // 
            this.lblGiamThi2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiamThi2.Location = new System.Drawing.Point(92, 181);
            this.lblGiamThi2.Name = "lblGiamThi2";
            this.lblGiamThi2.Size = new System.Drawing.Size(64, 30);
            this.lblGiamThi2.TabIndex = 40;
            this.lblGiamThi2.Text = "Giám thị 2";
            this.lblGiamThi2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGiamThi2.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtGiamThi1
            // 
            this.txtGiamThi1.Location = new System.Drawing.Point(95, 105);
            this.txtGiamThi1.Multiline = true;
            this.txtGiamThi1.Name = "txtGiamThi1";
            this.txtGiamThi1.Size = new System.Drawing.Size(375, 37);
            this.txtGiamThi1.TabIndex = 39;
            this.txtGiamThi1.TextChanged += new System.EventHandler(this.txtGiamThi1_TextChanged);
            // 
            // lblGiamThi1
            // 
            this.lblGiamThi1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiamThi1.Location = new System.Drawing.Point(92, 72);
            this.lblGiamThi1.Name = "lblGiamThi1";
            this.lblGiamThi1.Size = new System.Drawing.Size(64, 30);
            this.lblGiamThi1.TabIndex = 38;
            this.lblGiamThi1.Text = "Giám thị 1";
            this.lblGiamThi1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(12, 13);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(438, 43);
            this.label18.TabIndex = 44;
            this.label18.Text = "Danh sách phòng thi";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 352);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(438, 30);
            this.label1.TabIndex = 47;
            this.label1.Text = "Danh sách thí sinh trong phòng";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvHocSinhTrongPhong
            // 
            this.dgvHocSinhTrongPhong.BackgroundColor = System.Drawing.Color.White;
            this.dgvHocSinhTrongPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHocSinhTrongPhong.Location = new System.Drawing.Point(16, 398);
            this.dgvHocSinhTrongPhong.Name = "dgvHocSinhTrongPhong";
            this.dgvHocSinhTrongPhong.Size = new System.Drawing.Size(1119, 627);
            this.dgvHocSinhTrongPhong.TabIndex = 48;
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
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(165)))), ((int)(((byte)(249)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1690, 10);
            this.panel3.TabIndex = 52;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtGiamThi2);
            this.groupBox2.Controls.Add(this.btnCapNhatDiem);
            this.groupBox2.Controls.Add(this.lblGiamThi2);
            this.groupBox2.Controls.Add(this.txtGiamThi1);
            this.groupBox2.Controls.Add(this.btnCapNhat);
            this.groupBox2.Controls.Add(this.lblGiamThi1);
            this.groupBox2.Controls.Add(this.btnXuatDanhSach);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.btnXemHocSinh);
            this.groupBox2.Location = new System.Drawing.Point(1141, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(537, 1013);
            this.groupBox2.TabIndex = 53;
            this.groupBox2.TabStop = false;
            // 
            // btnCapNhatDiem
            // 
            this.btnCapNhatDiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhatDiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhatDiem.Image = global::TuyenSinhWinApp.Properties.Resources.software;
            this.btnCapNhatDiem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCapNhatDiem.Location = new System.Drawing.Point(95, 590);
            this.btnCapNhatDiem.Name = "btnCapNhatDiem";
            this.btnCapNhatDiem.Size = new System.Drawing.Size(375, 36);
            this.btnCapNhatDiem.TabIndex = 54;
            this.btnCapNhatDiem.Text = "Cập nhật điểm";
            this.btnCapNhatDiem.UseVisualStyleBackColor = true;
            this.btnCapNhatDiem.Click += new System.EventHandler(this.btnCapNhatDiem_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhat.Image = global::TuyenSinhWinApp.Properties.Resources.plus__1_1;
            this.btnCapNhat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCapNhat.Location = new System.Drawing.Point(95, 301);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(375, 36);
            this.btnCapNhat.TabIndex = 42;
            this.btnCapNhat.Text = "Cập nhật giám thị";
            this.btnCapNhat.UseVisualStyleBackColor = true;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // btnXuatDanhSach
            // 
            this.btnXuatDanhSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatDanhSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatDanhSach.Image = global::TuyenSinhWinApp.Properties.Resources.excel;
            this.btnXuatDanhSach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXuatDanhSach.Location = new System.Drawing.Point(95, 520);
            this.btnXuatDanhSach.Name = "btnXuatDanhSach";
            this.btnXuatDanhSach.Size = new System.Drawing.Size(375, 36);
            this.btnXuatDanhSach.TabIndex = 46;
            this.btnXuatDanhSach.Text = "Xuất danh sách thí sinh trong phòng";
            this.btnXuatDanhSach.UseVisualStyleBackColor = true;
            this.btnXuatDanhSach.Click += new System.EventHandler(this.btnXuatDanhSach_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::TuyenSinhWinApp.Properties.Resources.compose;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(95, 370);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(375, 36);
            this.button1.TabIndex = 45;
            this.button1.Text = "Chia phòng thi";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnXemHocSinh
            // 
            this.btnXemHocSinh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemHocSinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemHocSinh.Image = global::TuyenSinhWinApp.Properties.Resources.view;
            this.btnXemHocSinh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXemHocSinh.Location = new System.Drawing.Point(95, 447);
            this.btnXemHocSinh.Name = "btnXemHocSinh";
            this.btnXemHocSinh.Size = new System.Drawing.Size(375, 36);
            this.btnXemHocSinh.TabIndex = 2;
            this.btnXemHocSinh.Text = "Xem danh sách học sinh";
            this.btnXemHocSinh.UseVisualStyleBackColor = true;
            this.btnXemHocSinh.Click += new System.EventHandler(this.btnXemHocSinh_Click);
            // 
            // maDotDataGridViewTextBoxColumn
            // 
            this.maDotDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.maDotDataGridViewTextBoxColumn.DataPropertyName = "MaDot";
            this.maDotDataGridViewTextBoxColumn.HeaderText = "Mã đợt";
            this.maDotDataGridViewTextBoxColumn.Name = "maDotDataGridViewTextBoxColumn";
            this.maDotDataGridViewTextBoxColumn.ReadOnly = true;
            this.maDotDataGridViewTextBoxColumn.Width = 50;
            // 
            // phongThiBindingSource
            // 
            this.phongThiBindingSource.DataSource = typeof(TuyenSinhServiceLib.PhongThi);
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
            // txtTimTen
            // 
            this.txtTimTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimTen.Location = new System.Drawing.Point(816, 348);
            this.txtTimTen.Multiline = true;
            this.txtTimTen.Name = "txtTimTen";
            this.txtTimTen.Size = new System.Drawing.Size(319, 34);
            this.txtTimTen.TabIndex = 54;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(816, 16);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(319, 34);
            this.textBox1.TabIndex = 55;
            // 
            // frmPhongThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1690, 1041);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtTimTen);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocSinhTrongPhong)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPhongThi;
        private System.Windows.Forms.BindingSource phongThiBindingSource;
        private System.Windows.Forms.BindingSource phongThiBindingSource1;
        private System.Windows.Forms.Button btnXemHocSinh;
        private System.Windows.Forms.Label lblGiamThi1;
        private System.Windows.Forms.Label lblGiamThi2;
        private System.Windows.Forms.TextBox txtGiamThi1;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.TextBox txtGiamThi2;
        private System.Windows.Forms.BindingSource phongThiBindingSource2;
        private System.Windows.Forms.BindingSource phongThiBindingSource3;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvHocSinhTrongPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaPhongThi;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuongToiDa;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuongHienTai;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiamThi1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiamThi2;
        private System.Windows.Forms.DataGridViewTextBoxColumn maDotDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnXuatDanhSach;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCapNhatDiem;
        private System.Windows.Forms.TextBox txtTimTen;
        private System.Windows.Forms.TextBox textBox1;
    }
}