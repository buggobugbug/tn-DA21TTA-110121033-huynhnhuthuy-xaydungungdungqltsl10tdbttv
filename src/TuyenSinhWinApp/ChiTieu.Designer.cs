namespace TuyenSinhWinApp
{
    partial class ChiTieu
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtChiTieu = new System.Windows.Forms.TextBox();
            this.cbDotTuyenSinh = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDanhSachTrungTuyen = new System.Windows.Forms.DataGridView();
            this.btnXemTrungTuyen = new System.Windows.Forms.Button();
            this.btnXetTrungTuyen = new System.Windows.Forms.Button();
            this.btnLuuChiTieu = new System.Windows.Forms.Button();
            this.btnXuatDanhSach = new System.Windows.Forms.Button();
            this.btnXuatTatCaDiem = new System.Windows.Forms.Button();
            this.cbTruong = new System.Windows.Forms.ComboBox();
            this.lblTruong = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachTrungTuyen)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(274, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 21);
            this.label1.TabIndex = 59;
            this.label1.Text = "Chỉ tiêu:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtChiTieu
            // 
            this.txtChiTieu.Location = new System.Drawing.Point(339, 19);
            this.txtChiTieu.Multiline = true;
            this.txtChiTieu.Name = "txtChiTieu";
            this.txtChiTieu.Size = new System.Drawing.Size(43, 24);
            this.txtChiTieu.TabIndex = 60;
            // 
            // cbDotTuyenSinh
            // 
            this.cbDotTuyenSinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDotTuyenSinh.FormattingEnabled = true;
            this.cbDotTuyenSinh.Location = new System.Drawing.Point(453, 19);
            this.cbDotTuyenSinh.Name = "cbDotTuyenSinh";
            this.cbDotTuyenSinh.Size = new System.Drawing.Size(264, 24);
            this.cbDotTuyenSinh.TabIndex = 62;
            this.cbDotTuyenSinh.SelectedIndexChanged += new System.EventHandler(this.cbDotTuyenSinh_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(388, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 21);
            this.label2.TabIndex = 63;
            this.label2.Text = "Chọn đợt :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvDanhSachTrungTuyen
            // 
            this.dgvDanhSachTrungTuyen.BackgroundColor = System.Drawing.Color.White;
            this.dgvDanhSachTrungTuyen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachTrungTuyen.Location = new System.Drawing.Point(4, 64);
            this.dgvDanhSachTrungTuyen.Name = "dgvDanhSachTrungTuyen";
            this.dgvDanhSachTrungTuyen.Size = new System.Drawing.Size(1674, 965);
            this.dgvDanhSachTrungTuyen.TabIndex = 64;
            // 
            // btnXemTrungTuyen
            // 
            this.btnXemTrungTuyen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemTrungTuyen.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemTrungTuyen.Image = global::TuyenSinhWinApp.Properties.Resources.list;
            this.btnXemTrungTuyen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXemTrungTuyen.Location = new System.Drawing.Point(902, 14);
            this.btnXemTrungTuyen.Name = "btnXemTrungTuyen";
            this.btnXemTrungTuyen.Size = new System.Drawing.Size(186, 36);
            this.btnXemTrungTuyen.TabIndex = 66;
            this.btnXemTrungTuyen.Text = "Danh sách trúng tuyển";
            this.btnXemTrungTuyen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnXemTrungTuyen.UseVisualStyleBackColor = true;
            this.btnXemTrungTuyen.Click += new System.EventHandler(this.btnXemTrungTuyen_Click);
            // 
            // btnXetTrungTuyen
            // 
            this.btnXetTrungTuyen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXetTrungTuyen.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXetTrungTuyen.Image = global::TuyenSinhWinApp.Properties.Resources._checked;
            this.btnXetTrungTuyen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXetTrungTuyen.Location = new System.Drawing.Point(1108, 14);
            this.btnXetTrungTuyen.Name = "btnXetTrungTuyen";
            this.btnXetTrungTuyen.Size = new System.Drawing.Size(149, 36);
            this.btnXetTrungTuyen.TabIndex = 65;
            this.btnXetTrungTuyen.Text = "Xét trúng tuyển";
            this.btnXetTrungTuyen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnXetTrungTuyen.UseVisualStyleBackColor = true;
            this.btnXetTrungTuyen.Click += new System.EventHandler(this.btnXetTrungTuyen_Click);
            // 
            // btnLuuChiTieu
            // 
            this.btnLuuChiTieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuChiTieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuChiTieu.Image = global::TuyenSinhWinApp.Properties.Resources.update__2_;
            this.btnLuuChiTieu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLuuChiTieu.Location = new System.Drawing.Point(723, 14);
            this.btnLuuChiTieu.Name = "btnLuuChiTieu";
            this.btnLuuChiTieu.Size = new System.Drawing.Size(162, 36);
            this.btnLuuChiTieu.TabIndex = 61;
            this.btnLuuChiTieu.Text = "Cập nhật chỉ tiêu";
            this.btnLuuChiTieu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLuuChiTieu.UseVisualStyleBackColor = true;
            this.btnLuuChiTieu.Click += new System.EventHandler(this.btnLuuChiTieu_Click);
            // 
            // btnXuatDanhSach
            // 
            this.btnXuatDanhSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatDanhSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatDanhSach.Image = global::TuyenSinhWinApp.Properties.Resources.excel;
            this.btnXuatDanhSach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXuatDanhSach.Location = new System.Drawing.Point(1281, 14);
            this.btnXuatDanhSach.Name = "btnXuatDanhSach";
            this.btnXuatDanhSach.Size = new System.Drawing.Size(134, 36);
            this.btnXuatDanhSach.TabIndex = 67;
            this.btnXuatDanhSach.Text = "Xuất danh sách";
            this.btnXuatDanhSach.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnXuatDanhSach.UseVisualStyleBackColor = true;
            this.btnXuatDanhSach.Click += new System.EventHandler(this.btnXuatDanhSach_Click);
            // 
            // btnXuatTatCaDiem
            // 
            this.btnXuatTatCaDiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatTatCaDiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatTatCaDiem.Image = global::TuyenSinhWinApp.Properties.Resources.excel;
            this.btnXuatTatCaDiem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXuatTatCaDiem.Location = new System.Drawing.Point(1440, 14);
            this.btnXuatTatCaDiem.Name = "btnXuatTatCaDiem";
            this.btnXuatTatCaDiem.Size = new System.Drawing.Size(238, 36);
            this.btnXuatTatCaDiem.TabIndex = 68;
            this.btnXuatTatCaDiem.Text = "Xuất danh sách điểm toàn bộ học sinh";
            this.btnXuatTatCaDiem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnXuatTatCaDiem.UseVisualStyleBackColor = true;
            this.btnXuatTatCaDiem.Click += new System.EventHandler(this.btnXuatTatCaDiem_Click);
            // 
            // cbTruong
            // 
            this.cbTruong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTruong.FormattingEnabled = true;
            this.cbTruong.Location = new System.Drawing.Point(86, 19);
            this.cbTruong.Name = "cbTruong";
            this.cbTruong.Size = new System.Drawing.Size(182, 24);
            this.cbTruong.TabIndex = 74;
            this.cbTruong.SelectedIndexChanged += new System.EventHandler(this.cbTruong_SelectedIndexChanged);
            // 
            // lblTruong
            // 
            this.lblTruong.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTruong.Location = new System.Drawing.Point(2, 22);
            this.lblTruong.Name = "lblTruong";
            this.lblTruong.Size = new System.Drawing.Size(94, 21);
            this.lblTruong.TabIndex = 73;
            this.lblTruong.Text = "Chọn trường :";
            this.lblTruong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChiTieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1690, 1041);
            this.Controls.Add(this.cbTruong);
            this.Controls.Add(this.lblTruong);
            this.Controls.Add(this.btnXuatTatCaDiem);
            this.Controls.Add(this.btnXuatDanhSach);
            this.Controls.Add(this.btnXemTrungTuyen);
            this.Controls.Add(this.btnXetTrungTuyen);
            this.Controls.Add(this.dgvDanhSachTrungTuyen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbDotTuyenSinh);
            this.Controls.Add(this.btnLuuChiTieu);
            this.Controls.Add(this.txtChiTieu);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChiTieu";
            this.Text = "ChiTieu";
            this.Load += new System.EventHandler(this.ChiTieu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachTrungTuyen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtChiTieu;
        private System.Windows.Forms.Button btnLuuChiTieu;
        private System.Windows.Forms.ComboBox cbDotTuyenSinh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvDanhSachTrungTuyen;
        private System.Windows.Forms.Button btnXetTrungTuyen;
        private System.Windows.Forms.Button btnXemTrungTuyen;
        private System.Windows.Forms.Button btnXuatDanhSach;
        private System.Windows.Forms.Button btnXuatTatCaDiem;
        private System.Windows.Forms.ComboBox cbTruong;
        private System.Windows.Forms.Label lblTruong;
    }
}