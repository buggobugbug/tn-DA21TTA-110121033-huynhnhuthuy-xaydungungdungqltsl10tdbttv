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
            this.txtTimTen = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnXemTrungTuyen = new System.Windows.Forms.Button();
            this.btnXetTrungTuyen = new System.Windows.Forms.Button();
            this.btnLuuChiTieu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachTrungTuyen)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 41);
            this.label1.TabIndex = 59;
            this.label1.Text = "Chỉ tiêu hiện tại :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtChiTieu
            // 
            this.txtChiTieu.Location = new System.Drawing.Point(146, 23);
            this.txtChiTieu.Multiline = true;
            this.txtChiTieu.Name = "txtChiTieu";
            this.txtChiTieu.Size = new System.Drawing.Size(335, 24);
            this.txtChiTieu.TabIndex = 60;
            // 
            // cbDotTuyenSinh
            // 
            this.cbDotTuyenSinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDotTuyenSinh.FormattingEnabled = true;
            this.cbDotTuyenSinh.Location = new System.Drawing.Point(684, 26);
            this.cbDotTuyenSinh.Name = "cbDotTuyenSinh";
            this.cbDotTuyenSinh.Size = new System.Drawing.Size(304, 24);
            this.cbDotTuyenSinh.TabIndex = 62;
            this.cbDotTuyenSinh.SelectedIndexChanged += new System.EventHandler(this.cbDotTuyenSinh_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(544, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 21);
            this.label2.TabIndex = 63;
            this.label2.Text = "Đợt tuyển sinh :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvDanhSachTrungTuyen
            // 
            this.dgvDanhSachTrungTuyen.BackgroundColor = System.Drawing.Color.White;
            this.dgvDanhSachTrungTuyen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachTrungTuyen.Location = new System.Drawing.Point(4, 137);
            this.dgvDanhSachTrungTuyen.Name = "dgvDanhSachTrungTuyen";
            this.dgvDanhSachTrungTuyen.Size = new System.Drawing.Size(1666, 853);
            this.dgvDanhSachTrungTuyen.TabIndex = 64;
            // 
            // txtTimTen
            // 
            this.txtTimTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimTen.Location = new System.Drawing.Point(146, 92);
            this.txtTimTen.Multiline = true;
            this.txtTimTen.Name = "txtTimTen";
            this.txtTimTen.Size = new System.Drawing.Size(511, 34);
            this.txtTimTen.TabIndex = 67;
            this.txtTimTen.TextChanged += new System.EventHandler(this.txtTimTen_TextChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 34);
            this.label3.TabIndex = 68;
            this.label3.Text = "Tìm kiếm tên :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnXemTrungTuyen
            // 
            this.btnXemTrungTuyen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemTrungTuyen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemTrungTuyen.Image = global::TuyenSinhWinApp.Properties.Resources.list;
            this.btnXemTrungTuyen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXemTrungTuyen.Location = new System.Drawing.Point(684, 90);
            this.btnXemTrungTuyen.Name = "btnXemTrungTuyen";
            this.btnXemTrungTuyen.Size = new System.Drawing.Size(304, 36);
            this.btnXemTrungTuyen.TabIndex = 66;
            this.btnXemTrungTuyen.Text = "Danh sách học sinh trúng tuyển";
            this.btnXemTrungTuyen.UseVisualStyleBackColor = true;
            this.btnXemTrungTuyen.Click += new System.EventHandler(this.btnXemTrungTuyen_Click);
            // 
            // btnXetTrungTuyen
            // 
            this.btnXetTrungTuyen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXetTrungTuyen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXetTrungTuyen.Image = global::TuyenSinhWinApp.Properties.Resources._checked;
            this.btnXetTrungTuyen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXetTrungTuyen.Location = new System.Drawing.Point(1366, 90);
            this.btnXetTrungTuyen.Name = "btnXetTrungTuyen";
            this.btnXetTrungTuyen.Size = new System.Drawing.Size(304, 36);
            this.btnXetTrungTuyen.TabIndex = 65;
            this.btnXetTrungTuyen.Text = "Xét học sinh trúng tuyển";
            this.btnXetTrungTuyen.UseVisualStyleBackColor = true;
            this.btnXetTrungTuyen.Click += new System.EventHandler(this.btnXetTrungTuyen_Click);
            // 
            // btnLuuChiTieu
            // 
            this.btnLuuChiTieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuChiTieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuChiTieu.Image = global::TuyenSinhWinApp.Properties.Resources.update__2_;
            this.btnLuuChiTieu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLuuChiTieu.Location = new System.Drawing.Point(1026, 90);
            this.btnLuuChiTieu.Name = "btnLuuChiTieu";
            this.btnLuuChiTieu.Size = new System.Drawing.Size(304, 36);
            this.btnLuuChiTieu.TabIndex = 61;
            this.btnLuuChiTieu.Text = "Cập nhật chỉ tiêu";
            this.btnLuuChiTieu.UseVisualStyleBackColor = true;
            this.btnLuuChiTieu.Click += new System.EventHandler(this.btnLuuChiTieu_Click);
            // 
            // ChiTieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1674, 1002);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTimTen);
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
        private System.Windows.Forms.TextBox txtTimTen;
        private System.Windows.Forms.Label label3;
    }
}