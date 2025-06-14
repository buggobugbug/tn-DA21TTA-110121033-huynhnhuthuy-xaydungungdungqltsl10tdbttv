namespace TuyenSinhWinApp
{
    partial class frmDotTuyenSinh
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
            this.txtMaDot = new System.Windows.Forms.TextBox();
            this.txtTenDot = new System.Windows.Forms.TextBox();
            this.dtpBatDau = new System.Windows.Forms.DateTimePicker();
            this.dtpKetThuc = new System.Windows.Forms.DateTimePicker();
            this.btnThem = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dotTuyenSinhBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.maDotDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenDotDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.namDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayBatDauDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayKetThucDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trangThaiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dotTuyenSinhBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMaDot
            // 
            this.txtMaDot.Location = new System.Drawing.Point(30, 184);
            this.txtMaDot.Name = "txtMaDot";
            this.txtMaDot.Size = new System.Drawing.Size(195, 20);
            this.txtMaDot.TabIndex = 0;
            // 
            // txtTenDot
            // 
            this.txtTenDot.Location = new System.Drawing.Point(285, 184);
            this.txtTenDot.Name = "txtTenDot";
            this.txtTenDot.Size = new System.Drawing.Size(195, 20);
            this.txtTenDot.TabIndex = 1;
            // 
            // dtpBatDau
            // 
            this.dtpBatDau.Location = new System.Drawing.Point(30, 250);
            this.dtpBatDau.Name = "dtpBatDau";
            this.dtpBatDau.Size = new System.Drawing.Size(195, 20);
            this.dtpBatDau.TabIndex = 2;
            // 
            // dtpKetThuc
            // 
            this.dtpKetThuc.Location = new System.Drawing.Point(30, 310);
            this.dtpKetThuc.Name = "dtpKetThuc";
            this.dtpKetThuc.Size = new System.Drawing.Size(195, 20);
            this.dtpKetThuc.TabIndex = 3;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(30, 376);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 4;
            this.btnThem.Text = "Thêm đợt";
            this.btnThem.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.maDotDataGridViewTextBoxColumn,
            this.tenDotDataGridViewTextBoxColumn,
            this.namDataGridViewTextBoxColumn,
            this.ngayBatDauDataGridViewTextBoxColumn,
            this.ngayKetThucDataGridViewTextBoxColumn,
            this.trangThaiDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.dotTuyenSinhBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(30, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(644, 150);
            this.dataGridView1.TabIndex = 5;
            // 
            // dotTuyenSinhBindingSource
            // 
            this.dotTuyenSinhBindingSource.DataSource = typeof(TuyenSinhServiceLib.DotTuyenSinh);
            // 
            // maDotDataGridViewTextBoxColumn
            // 
            this.maDotDataGridViewTextBoxColumn.DataPropertyName = "MaDot";
            this.maDotDataGridViewTextBoxColumn.HeaderText = "MaDot";
            this.maDotDataGridViewTextBoxColumn.Name = "maDotDataGridViewTextBoxColumn";
            // 
            // tenDotDataGridViewTextBoxColumn
            // 
            this.tenDotDataGridViewTextBoxColumn.DataPropertyName = "TenDot";
            this.tenDotDataGridViewTextBoxColumn.HeaderText = "TenDot";
            this.tenDotDataGridViewTextBoxColumn.Name = "tenDotDataGridViewTextBoxColumn";
            // 
            // namDataGridViewTextBoxColumn
            // 
            this.namDataGridViewTextBoxColumn.DataPropertyName = "Nam";
            this.namDataGridViewTextBoxColumn.HeaderText = "Nam";
            this.namDataGridViewTextBoxColumn.Name = "namDataGridViewTextBoxColumn";
            // 
            // ngayBatDauDataGridViewTextBoxColumn
            // 
            this.ngayBatDauDataGridViewTextBoxColumn.DataPropertyName = "NgayBatDau";
            this.ngayBatDauDataGridViewTextBoxColumn.HeaderText = "NgayBatDau";
            this.ngayBatDauDataGridViewTextBoxColumn.Name = "ngayBatDauDataGridViewTextBoxColumn";
            // 
            // ngayKetThucDataGridViewTextBoxColumn
            // 
            this.ngayKetThucDataGridViewTextBoxColumn.DataPropertyName = "NgayKetThuc";
            this.ngayKetThucDataGridViewTextBoxColumn.HeaderText = "NgayKetThuc";
            this.ngayKetThucDataGridViewTextBoxColumn.Name = "ngayKetThucDataGridViewTextBoxColumn";
            // 
            // trangThaiDataGridViewTextBoxColumn
            // 
            this.trangThaiDataGridViewTextBoxColumn.DataPropertyName = "TrangThai";
            this.trangThaiDataGridViewTextBoxColumn.HeaderText = "TrangThai";
            this.trangThaiDataGridViewTextBoxColumn.Name = "trangThaiDataGridViewTextBoxColumn";
            // 
            // frmDotTuyenSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dtpKetThuc);
            this.Controls.Add(this.dtpBatDau);
            this.Controls.Add(this.txtTenDot);
            this.Controls.Add(this.txtMaDot);
            this.Name = "frmDotTuyenSinh";
            this.Text = "frmDotTuyenSinh";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dotTuyenSinhBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMaDot;
        private System.Windows.Forms.TextBox txtTenDot;
        private System.Windows.Forms.DateTimePicker dtpBatDau;
        private System.Windows.Forms.DateTimePicker dtpKetThuc;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn maDotDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenDotDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn namDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayBatDauDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayKetThucDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trangThaiDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource dotTuyenSinhBindingSource;
    }
}