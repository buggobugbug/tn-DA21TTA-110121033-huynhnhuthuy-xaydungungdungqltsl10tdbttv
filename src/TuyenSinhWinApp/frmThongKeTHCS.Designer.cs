namespace TuyenSinhWinApp
{
    partial class frmThongKeTHCS
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
            this.dgvTHCS = new System.Windows.Forms.DataGridView();
            this.dgvTongHop = new System.Windows.Forms.DataGridView();
            this.cbDotTuyenSinh = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTruong = new System.Windows.Forms.Label();
            this.cbTruong = new System.Windows.Forms.ComboBox();
            this.btnTaiLai = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTHCS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTongHop)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTHCS
            // 
            this.dgvTHCS.BackgroundColor = System.Drawing.Color.White;
            this.dgvTHCS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTHCS.Location = new System.Drawing.Point(13, 58);
            this.dgvTHCS.Name = "dgvTHCS";
            this.dgvTHCS.Size = new System.Drawing.Size(1633, 397);
            this.dgvTHCS.TabIndex = 0;
            // 
            // dgvTongHop
            // 
            this.dgvTongHop.BackgroundColor = System.Drawing.Color.White;
            this.dgvTongHop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTongHop.Location = new System.Drawing.Point(13, 511);
            this.dgvTongHop.Name = "dgvTongHop";
            this.dgvTongHop.Size = new System.Drawing.Size(1633, 323);
            this.dgvTongHop.TabIndex = 1;
            // 
            // cbDotTuyenSinh
            // 
            this.cbDotTuyenSinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDotTuyenSinh.FormattingEnabled = true;
            this.cbDotTuyenSinh.Location = new System.Drawing.Point(1129, 20);
            this.cbDotTuyenSinh.Name = "cbDotTuyenSinh";
            this.cbDotTuyenSinh.Size = new System.Drawing.Size(294, 24);
            this.cbDotTuyenSinh.TabIndex = 56;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(999, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 24);
            this.label2.TabIndex = 65;
            this.label2.Text = "Đợt tuyển sinh :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(12, 469);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(208, 30);
            this.label18.TabIndex = 67;
            this.label18.Text = "SỐ LIỆU TỔNG HỢP";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(464, 36);
            this.label1.TabIndex = 68;
            this.label1.Text = "THỐNG KÊ ĐIỂM DỰ THI VÀO LỚP 10";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTruong
            // 
            this.lblTruong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTruong.Location = new System.Drawing.Point(599, 21);
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
            this.cbTruong.Location = new System.Drawing.Point(699, 20);
            this.cbTruong.Name = "cbTruong";
            this.cbTruong.Size = new System.Drawing.Size(294, 24);
            this.cbTruong.TabIndex = 72;
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaiLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaiLai.Image = global::TuyenSinhWinApp.Properties.Resources.reload;
            this.btnTaiLai.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTaiLai.Location = new System.Drawing.Point(1460, 11);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(186, 36);
            this.btnTaiLai.TabIndex = 66;
            this.btnTaiLai.Text = "Tải lại";
            this.btnTaiLai.UseVisualStyleBackColor = true;
            // 
            // frmThongKeTHCS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1658, 846);
            this.Controls.Add(this.lblTruong);
            this.Controls.Add(this.cbTruong);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.btnTaiLai);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbDotTuyenSinh);
            this.Controls.Add(this.dgvTongHop);
            this.Controls.Add(this.dgvTHCS);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmThongKeTHCS";
            this.Text = "frmThongKeTHCS";
            this.Load += new System.EventHandler(this.frmThongKeTHCS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTHCS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTongHop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTHCS;
        private System.Windows.Forms.DataGridView dgvTongHop;
        private System.Windows.Forms.ComboBox cbDotTuyenSinh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTruong;
        private System.Windows.Forms.ComboBox cbTruong;
    }
}