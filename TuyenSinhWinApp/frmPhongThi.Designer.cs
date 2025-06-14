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
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.phongThiBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.phongThiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnXemHocSinh = new System.Windows.Forms.Button();
            this.maPhongThiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maTruongDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diaDiemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuongToiDaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuongHienTaiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.giamThi1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.giamThi2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayThiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongThi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPhongThi
            // 
            this.dgvPhongThi.AutoGenerateColumns = false;
            this.dgvPhongThi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhongThi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhongThi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.maPhongThiDataGridViewTextBoxColumn,
            this.maTruongDataGridViewTextBoxColumn,
            this.diaDiemDataGridViewTextBoxColumn,
            this.soLuongToiDaDataGridViewTextBoxColumn,
            this.soLuongHienTaiDataGridViewTextBoxColumn,
            this.giamThi1DataGridViewTextBoxColumn,
            this.giamThi2DataGridViewTextBoxColumn,
            this.ngayThiDataGridViewTextBoxColumn});
            this.dgvPhongThi.DataSource = this.phongThiBindingSource1;
            this.dgvPhongThi.Location = new System.Drawing.Point(51, 31);
            this.dgvPhongThi.Name = "dgvPhongThi";
            this.dgvPhongThi.ReadOnly = true;
            this.dgvPhongThi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhongThi.Size = new System.Drawing.Size(962, 150);
            this.dgvPhongThi.TabIndex = 0;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(51, 220);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(75, 23);
            this.btnLamMoi.TabIndex = 1;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // phongThiBindingSource1
            // 
            this.phongThiBindingSource1.DataSource = typeof(TuyenSinhServiceLib.PhongThi);
            // 
            // phongThiBindingSource
            // 
            this.phongThiBindingSource.DataSource = typeof(TuyenSinhServiceLib.PhongThi);
            // 
            // btnXemHocSinh
            // 
            this.btnXemHocSinh.Location = new System.Drawing.Point(170, 220);
            this.btnXemHocSinh.Name = "btnXemHocSinh";
            this.btnXemHocSinh.Size = new System.Drawing.Size(134, 23);
            this.btnXemHocSinh.TabIndex = 2;
            this.btnXemHocSinh.Text = "Xem danh sách học sinh";
            this.btnXemHocSinh.UseVisualStyleBackColor = true;
            this.btnXemHocSinh.Click += new System.EventHandler(this.btnXemHocSinh_Click);
            // 
            // maPhongThiDataGridViewTextBoxColumn
            // 
            this.maPhongThiDataGridViewTextBoxColumn.DataPropertyName = "MaPhongThi";
            this.maPhongThiDataGridViewTextBoxColumn.HeaderText = "Mã phòng thi";
            this.maPhongThiDataGridViewTextBoxColumn.Name = "maPhongThiDataGridViewTextBoxColumn";
            this.maPhongThiDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // maTruongDataGridViewTextBoxColumn
            // 
            this.maTruongDataGridViewTextBoxColumn.DataPropertyName = "MaTruong";
            this.maTruongDataGridViewTextBoxColumn.HeaderText = "MaTruong";
            this.maTruongDataGridViewTextBoxColumn.Name = "maTruongDataGridViewTextBoxColumn";
            this.maTruongDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // diaDiemDataGridViewTextBoxColumn
            // 
            this.diaDiemDataGridViewTextBoxColumn.DataPropertyName = "DiaDiem";
            this.diaDiemDataGridViewTextBoxColumn.HeaderText = "DiaDiem";
            this.diaDiemDataGridViewTextBoxColumn.Name = "diaDiemDataGridViewTextBoxColumn";
            this.diaDiemDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // soLuongToiDaDataGridViewTextBoxColumn
            // 
            this.soLuongToiDaDataGridViewTextBoxColumn.DataPropertyName = "SoLuongToiDa";
            this.soLuongToiDaDataGridViewTextBoxColumn.HeaderText = "SoLuongToiDa";
            this.soLuongToiDaDataGridViewTextBoxColumn.Name = "soLuongToiDaDataGridViewTextBoxColumn";
            this.soLuongToiDaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // soLuongHienTaiDataGridViewTextBoxColumn
            // 
            this.soLuongHienTaiDataGridViewTextBoxColumn.DataPropertyName = "SoLuongHienTai";
            this.soLuongHienTaiDataGridViewTextBoxColumn.HeaderText = "SoLuongHienTai";
            this.soLuongHienTaiDataGridViewTextBoxColumn.Name = "soLuongHienTaiDataGridViewTextBoxColumn";
            this.soLuongHienTaiDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // giamThi1DataGridViewTextBoxColumn
            // 
            this.giamThi1DataGridViewTextBoxColumn.DataPropertyName = "GiamThi1";
            this.giamThi1DataGridViewTextBoxColumn.HeaderText = "GiamThi1";
            this.giamThi1DataGridViewTextBoxColumn.Name = "giamThi1DataGridViewTextBoxColumn";
            this.giamThi1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // giamThi2DataGridViewTextBoxColumn
            // 
            this.giamThi2DataGridViewTextBoxColumn.DataPropertyName = "GiamThi2";
            this.giamThi2DataGridViewTextBoxColumn.HeaderText = "GiamThi2";
            this.giamThi2DataGridViewTextBoxColumn.Name = "giamThi2DataGridViewTextBoxColumn";
            this.giamThi2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ngayThiDataGridViewTextBoxColumn
            // 
            this.ngayThiDataGridViewTextBoxColumn.DataPropertyName = "NgayThi";
            this.ngayThiDataGridViewTextBoxColumn.HeaderText = "NgayThi";
            this.ngayThiDataGridViewTextBoxColumn.Name = "ngayThiDataGridViewTextBoxColumn";
            this.ngayThiDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // frmPhongThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 476);
            this.Controls.Add(this.btnXemHocSinh);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.dgvPhongThi);
            this.Name = "frmPhongThi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPhongThi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPhongThi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongThi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPhongThi;
        private System.Windows.Forms.BindingSource phongThiBindingSource;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.BindingSource phongThiBindingSource1;
        private System.Windows.Forms.Button btnXemHocSinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn maPhongThiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maTruongDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diaDiemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongToiDaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongHienTaiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn giamThi1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn giamThi2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayThiDataGridViewTextBoxColumn;
    }
}