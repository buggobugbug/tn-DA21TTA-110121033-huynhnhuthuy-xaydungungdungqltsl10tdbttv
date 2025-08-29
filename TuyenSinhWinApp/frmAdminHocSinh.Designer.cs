namespace TuyenSinhWinApp
{
    partial class frmAdminHocSinh
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
            this.cboTruong = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cboDot = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTai = new System.Windows.Forms.Button();
            this.dgvHocSinh = new System.Windows.Forms.DataGridView();
            this.lblCount = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocSinh)).BeginInit();
            this.SuspendLayout();
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
            this.cboTruong.Location = new System.Drawing.Point(811, 12);
            this.cboTruong.Name = "cboTruong";
            this.cboTruong.Size = new System.Drawing.Size(239, 26);
            this.cboTruong.TabIndex = 35;
            this.cboTruong.SelectedIndexChanged += new System.EventHandler(this.cboTruongTHCS_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(650, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(137, 30);
            this.label15.TabIndex = 36;
            this.label15.Text = "Chọn trường THPT :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // cboDot
            // 
            this.cboDot.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDot.FormattingEnabled = true;
            this.cboDot.Items.AddRange(new object[] {
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
            this.cboDot.Location = new System.Drawing.Point(1273, 12);
            this.cboDot.Name = "cboDot";
            this.cboDot.Size = new System.Drawing.Size(239, 26);
            this.cboDot.TabIndex = 37;
            this.cboDot.SelectedIndexChanged += new System.EventHandler(this.cboDot_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1107, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 30);
            this.label1.TabIndex = 38;
            this.label1.Text = "Chọn đợt tuyển sinh :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnTai
            // 
            this.btnTai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTai.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTai.Image = global::TuyenSinhWinApp.Properties.Resources.filter;
            this.btnTai.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTai.Location = new System.Drawing.Point(1553, 6);
            this.btnTai.Name = "btnTai";
            this.btnTai.Size = new System.Drawing.Size(125, 38);
            this.btnTai.TabIndex = 44;
            this.btnTai.Text = "Tải lại";
            this.btnTai.UseVisualStyleBackColor = true;
            this.btnTai.Click += new System.EventHandler(this.btnTai_Click);
            // 
            // dgvHocSinh
            // 
            this.dgvHocSinh.BackgroundColor = System.Drawing.Color.White;
            this.dgvHocSinh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHocSinh.Location = new System.Drawing.Point(12, 56);
            this.dgvHocSinh.Name = "dgvHocSinh";
            this.dgvHocSinh.Size = new System.Drawing.Size(1666, 924);
            this.dgvHocSinh.TabIndex = 45;
            // 
            // lblCount
            // 
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(13, 995);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(493, 37);
            this.lblCount.TabIndex = 46;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(12, 4);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(322, 40);
            this.label18.TabIndex = 47;
            this.label18.Text = "Danh sách học sinh toàn tỉnh :";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmAdminHocSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1690, 1041);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.dgvHocSinh);
            this.Controls.Add(this.btnTai);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboDot);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cboTruong);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAdminHocSinh";
            this.Text = "frmAdminHocSinh";
            this.Load += new System.EventHandler(this.frmAdminHocSinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocSinh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboTruong;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cboDot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTai;
        private System.Windows.Forms.DataGridView dgvHocSinh;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label18;
    }
}