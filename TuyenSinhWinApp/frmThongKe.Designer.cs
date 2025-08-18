namespace TuyenSinhWinApp
{
    partial class frmThongKe
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
            this.cbDotTuyenSinh = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvThongKeMon = new System.Windows.Forms.DataGridView();
            this.dgvBoThiMon = new System.Windows.Forms.DataGridView();
            this.grpThongtin = new System.Windows.Forms.GroupBox();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKeMon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoThiMon)).BeginInit();
            this.SuspendLayout();
            // 
            // cbDotTuyenSinh
            // 
            this.cbDotTuyenSinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDotTuyenSinh.FormattingEnabled = true;
            this.cbDotTuyenSinh.Location = new System.Drawing.Point(1024, 25);
            this.cbDotTuyenSinh.Name = "cbDotTuyenSinh";
            this.cbDotTuyenSinh.Size = new System.Drawing.Size(294, 24);
            this.cbDotTuyenSinh.TabIndex = 63;
            this.cbDotTuyenSinh.SelectedIndexChanged += new System.EventHandler(this.cbDotTuyenSinh_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(894, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 21);
            this.label2.TabIndex = 64;
            this.label2.Text = "Đợt tuyển sinh :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvThongKeMon
            // 
            this.dgvThongKeMon.BackgroundColor = System.Drawing.Color.White;
            this.dgvThongKeMon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThongKeMon.Location = new System.Drawing.Point(15, 66);
            this.dgvThongKeMon.Name = "dgvThongKeMon";
            this.dgvThongKeMon.Size = new System.Drawing.Size(1631, 298);
            this.dgvThongKeMon.TabIndex = 66;
            // 
            // dgvBoThiMon
            // 
            this.dgvBoThiMon.BackgroundColor = System.Drawing.Color.White;
            this.dgvBoThiMon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBoThiMon.Location = new System.Drawing.Point(15, 422);
            this.dgvBoThiMon.Name = "dgvBoThiMon";
            this.dgvBoThiMon.Size = new System.Drawing.Size(825, 412);
            this.dgvBoThiMon.TabIndex = 67;
            // 
            // grpThongtin
            // 
            this.grpThongtin.Location = new System.Drawing.Point(878, 418);
            this.grpThongtin.Name = "grpThongtin";
            this.grpThongtin.Size = new System.Drawing.Size(768, 416);
            this.grpThongtin.TabIndex = 68;
            this.grpThongtin.TabStop = false;
            // 
            // btnThongKe
            // 
            this.btnThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKe.Image = global::TuyenSinhWinApp.Properties.Resources._3d_report;
            this.btnThongKe.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThongKe.Location = new System.Drawing.Point(1342, 12);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(304, 48);
            this.btnThongKe.TabIndex = 65;
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(464, 36);
            this.label1.TabIndex = 69;
            this.label1.Text = "DANH SÁCH THỐNG KÊ ĐIỂM THEO MÔN ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1658, 846);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpThongtin);
            this.Controls.Add(this.dgvBoThiMon);
            this.Controls.Add(this.dgvThongKeMon);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbDotTuyenSinh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmThongKe";
            this.Text = "frmThongKe";
            this.Load += new System.EventHandler(this.frmThongKe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKeMon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoThiMon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cbDotTuyenSinh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.DataGridView dgvThongKeMon;
        private System.Windows.Forms.DataGridView dgvBoThiMon;
        private System.Windows.Forms.GroupBox grpThongtin;
        private System.Windows.Forms.Label label1;
    }
}