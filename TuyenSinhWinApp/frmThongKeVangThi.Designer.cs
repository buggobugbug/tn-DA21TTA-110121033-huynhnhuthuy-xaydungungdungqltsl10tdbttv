namespace TuyenSinhWinApp
{
    partial class frmThongKeVangThi
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
            this.label11 = new System.Windows.Forms.Label();
            this.cbDotTuyenSinh = new System.Windows.Forms.ComboBox();
            this.btnTai = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.lblTongVangAny = new System.Windows.Forms.Label();
            this.lblVangToan = new System.Windows.Forms.Label();
            this.lblVangVan = new System.Windows.Forms.Label();
            this.lblVangAnh = new System.Windows.Forms.Label();
            this.dgvVang = new System.Windows.Forms.DataGridView();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.lblTruong = new System.Windows.Forms.Label();
            this.cbTruong = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVang)).BeginInit();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(669, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(124, 21);
            this.label11.TabIndex = 60;
            this.label11.Text = "Đợt tuyển sinh :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbDotTuyenSinh
            // 
            this.cbDotTuyenSinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDotTuyenSinh.FormattingEnabled = true;
            this.cbDotTuyenSinh.Location = new System.Drawing.Point(799, 30);
            this.cbDotTuyenSinh.Name = "cbDotTuyenSinh";
            this.cbDotTuyenSinh.Size = new System.Drawing.Size(294, 24);
            this.cbDotTuyenSinh.TabIndex = 59;
            this.cbDotTuyenSinh.SelectedIndexChanged += new System.EventHandler(this.cbDotTuyenSinh_SelectedIndexChanged);
            // 
            // btnTai
            // 
            this.btnTai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTai.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTai.Image = global::TuyenSinhWinApp.Properties.Resources.list;
            this.btnTai.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTai.Location = new System.Drawing.Point(1157, 24);
            this.btnTai.Name = "btnTai";
            this.btnTai.Size = new System.Drawing.Size(177, 36);
            this.btnTai.TabIndex = 58;
            this.btnTai.Text = "Tải danh sách";
            this.btnTai.UseVisualStyleBackColor = true;
            this.btnTai.Click += new System.EventHandler(this.btnTai_Click);
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(12, 9);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(232, 62);
            this.label18.TabIndex = 57;
            this.label18.Text = "HỌC SINH VẮNG THI";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatExcel.Image = global::TuyenSinhWinApp.Properties.Resources.excel;
            this.btnXuatExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXuatExcel.Location = new System.Drawing.Point(1362, 24);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(177, 36);
            this.btnXuatExcel.TabIndex = 61;
            this.btnXuatExcel.Text = "Xuất file excel";
            this.btnXuatExcel.UseVisualStyleBackColor = true;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // lblTongVangAny
            // 
            this.lblTongVangAny.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongVangAny.Location = new System.Drawing.Point(17, 816);
            this.lblTongVangAny.Name = "lblTongVangAny";
            this.lblTongVangAny.Size = new System.Drawing.Size(227, 21);
            this.lblTongVangAny.TabIndex = 62;
            this.lblTongVangAny.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVangToan
            // 
            this.lblVangToan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVangToan.Location = new System.Drawing.Point(316, 816);
            this.lblVangToan.Name = "lblVangToan";
            this.lblVangToan.Size = new System.Drawing.Size(227, 21);
            this.lblVangToan.TabIndex = 63;
            this.lblVangToan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVangToan.Click += new System.EventHandler(this.lblVangToan_Click);
            // 
            // lblVangVan
            // 
            this.lblVangVan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVangVan.Location = new System.Drawing.Point(575, 816);
            this.lblVangVan.Name = "lblVangVan";
            this.lblVangVan.Size = new System.Drawing.Size(227, 21);
            this.lblVangVan.TabIndex = 64;
            this.lblVangVan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVangAnh
            // 
            this.lblVangAnh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVangAnh.Location = new System.Drawing.Point(791, 728);
            this.lblVangAnh.Name = "lblVangAnh";
            this.lblVangAnh.Size = new System.Drawing.Size(227, 21);
            this.lblVangAnh.TabIndex = 65;
            this.lblVangAnh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvVang
            // 
            this.dgvVang.BackgroundColor = System.Drawing.Color.White;
            this.dgvVang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVang.Location = new System.Drawing.Point(20, 74);
            this.dgvVang.Name = "dgvVang";
            this.dgvVang.Size = new System.Drawing.Size(1630, 726);
            this.dgvVang.TabIndex = 66;
            // 
            // txtTim
            // 
            this.txtTim.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTim.Location = new System.Drawing.Point(1554, 25);
            this.txtTim.Multiline = true;
            this.txtTim.Name = "txtTim";
            this.txtTim.Size = new System.Drawing.Size(96, 34);
            this.txtTim.TabIndex = 67;
            // 
            // lblTruong
            // 
            this.lblTruong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTruong.Location = new System.Drawing.Point(269, 30);
            this.lblTruong.Name = "lblTruong";
            this.lblTruong.Size = new System.Drawing.Size(94, 21);
            this.lblTruong.TabIndex = 73;
            this.lblTruong.Text = "Chọn trường :";
            this.lblTruong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbTruong
            // 
            this.cbTruong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTruong.FormattingEnabled = true;
            this.cbTruong.Location = new System.Drawing.Point(369, 30);
            this.cbTruong.Name = "cbTruong";
            this.cbTruong.Size = new System.Drawing.Size(294, 24);
            this.cbTruong.TabIndex = 72;
            this.cbTruong.SelectedIndexChanged += new System.EventHandler(this.cbTruong_SelectedIndexChanged);
            // 
            // frmThongKeVangThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1658, 846);
            this.Controls.Add(this.lblTruong);
            this.Controls.Add(this.cbTruong);
            this.Controls.Add(this.txtTim);
            this.Controls.Add(this.dgvVang);
            this.Controls.Add(this.lblVangAnh);
            this.Controls.Add(this.lblVangVan);
            this.Controls.Add(this.lblVangToan);
            this.Controls.Add(this.lblTongVangAny);
            this.Controls.Add(this.btnXuatExcel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbDotTuyenSinh);
            this.Controls.Add(this.btnTai);
            this.Controls.Add(this.label18);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmThongKeVangThi";
            this.Text = "frmThongKeVangThi";
            this.Load += new System.EventHandler(this.frmThongKeVangThi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbDotTuyenSinh;
        private System.Windows.Forms.Button btnTai;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Label lblTongVangAny;
        private System.Windows.Forms.Label lblVangToan;
        private System.Windows.Forms.Label lblVangVan;
        private System.Windows.Forms.Label lblVangAnh;
        private System.Windows.Forms.DataGridView dgvVang;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.Label lblTruong;
        private System.Windows.Forms.ComboBox cbTruong;
    }
}