namespace TuyenSinhWinApp
{
    partial class frmAdminCapNhatDiemTheoPhongThi
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
            this.label18 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cboTruong = new System.Windows.Forms.ComboBox();
            this.cbDotTuyenSinh = new System.Windows.Forms.ComboBox();
            this.cboPhongThi = new System.Windows.Forms.ComboBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.dgvDanhSachHocSinh = new System.Windows.Forms.DataGridView();
            this.cboMonHoc = new System.Windows.Forms.ComboBox();
            this.btnLuuDiem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachHocSinh)).BeginInit();
            this.SuspendLayout();
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(12, 9);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(386, 40);
            this.label18.TabIndex = 48;
            this.label18.Text = "Cập nhật điểm cho học sinh toàn tỉnh :";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(404, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(137, 30);
            this.label15.TabIndex = 49;
            this.label15.Text = "Chọn trường THPT :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboTruong
            // 
            this.cboTruong.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTruong.FormattingEnabled = true;
            this.cboTruong.Items.AddRange(new object[] {
            "THCS Bình Phú",
            "THCS Hiếu Tử",
            "THCS Hiệp Mỹ Tây",
            "THCS Hiệp Thạnh",
            "THCS Hòa Thuận",
            "THCS Long Hòa",
            "THCS Long Vĩnh",
            "THCS Lý Tự Trọng",
            "THCS Minh Trí",
            "THCS Nguyễn Đáng",
            "THCS Ngũ Lạc",
            "THCS Phan Châu Trinh",
            "THCS Phương Thạnh",
            "THCS Phước Hưng",
            "THCS TT Châu Thành",
            "THCS TT Cầu Kè",
            "THCS TT Cầu Quan",
            "THCS TT Trà Cú",
            "THCS Thanh Mỹ",
            "THCS Thái Bình",
            "THCS Thị Trấn Cầu Kè",
            "THCS Trương Văn Trì",
            "THCS Trường Long Hòa",
            "THCS Trần Phú",
            "THCS Trần Quốc Tuấn",
            "THCS Tập Sơn",
            "THCS Đa Lộc",
            "THCS Đôn Châu",
            "THCS Đôn Xuân",
            "THCS Đại Phước",
            "THPT Hòa Lợi",
            "Thực hành Sư phạm"});
            this.cboTruong.Location = new System.Drawing.Point(586, 15);
            this.cboTruong.Name = "cboTruong";
            this.cboTruong.Size = new System.Drawing.Size(214, 26);
            this.cboTruong.TabIndex = 50;
            // 
            // cbDotTuyenSinh
            // 
            this.cbDotTuyenSinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDotTuyenSinh.FormattingEnabled = true;
            this.cbDotTuyenSinh.Items.AddRange(new object[] {
            "THCS Bình Phú",
            "THCS Hiếu Tử",
            "THCS Hiệp Mỹ Tây",
            "THCS Hiệp Thạnh",
            "THCS Hòa Thuận",
            "THCS Long Hòa",
            "THCS Long Vĩnh",
            "THCS Lý Tự Trọng",
            "THCS Minh Trí",
            "THCS Nguyễn Đáng",
            "THCS Ngũ Lạc",
            "THCS Phan Châu Trinh",
            "THCS Phương Thạnh",
            "THCS Phước Hưng",
            "THCS TT Châu Thành",
            "THCS TT Cầu Kè",
            "THCS TT Cầu Quan",
            "THCS TT Trà Cú",
            "THCS Thanh Mỹ",
            "THCS Thái Bình",
            "THCS Thị Trấn Cầu Kè",
            "THCS Trương Văn Trì",
            "THCS Trường Long Hòa",
            "THCS Trần Phú",
            "THCS Trần Quốc Tuấn",
            "THCS Tập Sơn",
            "THCS Đa Lộc",
            "THCS Đôn Châu",
            "THCS Đôn Xuân",
            "THCS Đại Phước",
            "THPT Hòa Lợi",
            "Thực hành Sư phạm"});
            this.cbDotTuyenSinh.Location = new System.Drawing.Point(835, 15);
            this.cbDotTuyenSinh.Name = "cbDotTuyenSinh";
            this.cbDotTuyenSinh.Size = new System.Drawing.Size(250, 26);
            this.cbDotTuyenSinh.TabIndex = 51;
            // 
            // cboPhongThi
            // 
            this.cboPhongThi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPhongThi.FormattingEnabled = true;
            this.cboPhongThi.Items.AddRange(new object[] {
            "THCS Bình Phú",
            "THCS Hiếu Tử",
            "THCS Hiệp Mỹ Tây",
            "THCS Hiệp Thạnh",
            "THCS Hòa Thuận",
            "THCS Long Hòa",
            "THCS Long Vĩnh",
            "THCS Lý Tự Trọng",
            "THCS Minh Trí",
            "THCS Nguyễn Đáng",
            "THCS Ngũ Lạc",
            "THCS Phan Châu Trinh",
            "THCS Phương Thạnh",
            "THCS Phước Hưng",
            "THCS TT Châu Thành",
            "THCS TT Cầu Kè",
            "THCS TT Cầu Quan",
            "THCS TT Trà Cú",
            "THCS Thanh Mỹ",
            "THCS Thái Bình",
            "THCS Thị Trấn Cầu Kè",
            "THCS Trương Văn Trì",
            "THCS Trường Long Hòa",
            "THCS Trần Phú",
            "THCS Trần Quốc Tuấn",
            "THCS Tập Sơn",
            "THCS Đa Lộc",
            "THCS Đôn Châu",
            "THCS Đôn Xuân",
            "THCS Đại Phước",
            "THPT Hòa Lợi",
            "Thực hành Sư phạm"});
            this.cboPhongThi.Location = new System.Drawing.Point(1124, 17);
            this.cboPhongThi.Name = "cboPhongThi";
            this.cboPhongThi.Size = new System.Drawing.Size(229, 26);
            this.cboPhongThi.TabIndex = 55;
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(13, 1002);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(461, 30);
            this.lblInfo.TabIndex = 57;
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvDanhSachHocSinh
            // 
            this.dgvDanhSachHocSinh.BackgroundColor = System.Drawing.Color.White;
            this.dgvDanhSachHocSinh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachHocSinh.Location = new System.Drawing.Point(16, 52);
            this.dgvDanhSachHocSinh.Name = "dgvDanhSachHocSinh";
            this.dgvDanhSachHocSinh.Size = new System.Drawing.Size(1662, 934);
            this.dgvDanhSachHocSinh.TabIndex = 58;
            // 
            // cboMonHoc
            // 
            this.cboMonHoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMonHoc.FormattingEnabled = true;
            this.cboMonHoc.Items.AddRange(new object[] {
            "THCS Bình Phú",
            "THCS Hiếu Tử",
            "THCS Hiệp Mỹ Tây",
            "THCS Hiệp Thạnh",
            "THCS Hòa Thuận",
            "THCS Long Hòa",
            "THCS Long Vĩnh",
            "THCS Lý Tự Trọng",
            "THCS Minh Trí",
            "THCS Nguyễn Đáng",
            "THCS Ngũ Lạc",
            "THCS Phan Châu Trinh",
            "THCS Phương Thạnh",
            "THCS Phước Hưng",
            "THCS TT Châu Thành",
            "THCS TT Cầu Kè",
            "THCS TT Cầu Quan",
            "THCS TT Trà Cú",
            "THCS Thanh Mỹ",
            "THCS Thái Bình",
            "THCS Thị Trấn Cầu Kè",
            "THCS Trương Văn Trì",
            "THCS Trường Long Hòa",
            "THCS Trần Phú",
            "THCS Trần Quốc Tuấn",
            "THCS Tập Sơn",
            "THCS Đa Lộc",
            "THCS Đôn Châu",
            "THCS Đôn Xuân",
            "THCS Đại Phước",
            "THPT Hòa Lợi",
            "Thực hành Sư phạm"});
            this.cboMonHoc.Location = new System.Drawing.Point(1391, 15);
            this.cboMonHoc.Name = "cboMonHoc";
            this.cboMonHoc.Size = new System.Drawing.Size(130, 26);
            this.cboMonHoc.TabIndex = 59;
            // 
            // btnLuuDiem
            // 
            this.btnLuuDiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuDiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuDiem.Image = global::TuyenSinhWinApp.Properties.Resources.filter;
            this.btnLuuDiem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLuuDiem.Location = new System.Drawing.Point(1546, 5);
            this.btnLuuDiem.Name = "btnLuuDiem";
            this.btnLuuDiem.Size = new System.Drawing.Size(132, 38);
            this.btnLuuDiem.TabIndex = 54;
            this.btnLuuDiem.Text = "Lưu điểm";
            this.btnLuuDiem.UseVisualStyleBackColor = true;
            // 
            // frmAdminCapNhatDiemTheoPhongThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1690, 1041);
            this.Controls.Add(this.cboMonHoc);
            this.Controls.Add(this.dgvDanhSachHocSinh);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cboPhongThi);
            this.Controls.Add(this.btnLuuDiem);
            this.Controls.Add(this.cbDotTuyenSinh);
            this.Controls.Add(this.cboTruong);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label18);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAdminCapNhatDiemTheoPhongThi";
            this.Text = "frmAdminCapNhatDiemTheoPhongThi";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachHocSinh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cboTruong;
        private System.Windows.Forms.ComboBox cbDotTuyenSinh;
        private System.Windows.Forms.Button btnLuuDiem;
        private System.Windows.Forms.ComboBox cboPhongThi;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.DataGridView dgvDanhSachHocSinh;
        private System.Windows.Forms.ComboBox cboMonHoc;
    }
}