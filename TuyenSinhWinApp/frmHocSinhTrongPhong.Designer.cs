namespace TuyenSinhWinApp
{
    partial class frmHocSinhTrongPhong
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
            this.phongThiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dgvHocSinh = new System.Windows.Forms.DataGridView();
            this.phongThiBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnXuatThe = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocSinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // phongThiBindingSource
            // 
            this.phongThiBindingSource.DataSource = typeof(TuyenSinhServiceLib.PhongThi);
            // 
            // dgvHocSinh
            // 
            this.dgvHocSinh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHocSinh.Location = new System.Drawing.Point(12, 76);
            this.dgvHocSinh.Name = "dgvHocSinh";
            this.dgvHocSinh.Size = new System.Drawing.Size(763, 150);
            this.dgvHocSinh.TabIndex = 0;
            // 
            // phongThiBindingSource1
            // 
            this.phongThiBindingSource1.DataSource = typeof(TuyenSinhServiceLib.PhongThi);
            // 
            // btnXuatThe
            // 
            this.btnXuatThe.Location = new System.Drawing.Point(13, 285);
            this.btnXuatThe.Name = "btnXuatThe";
            this.btnXuatThe.Size = new System.Drawing.Size(110, 23);
            this.btnXuatThe.TabIndex = 1;
            this.btnXuatThe.Text = "Xuất thẻ dự thi";
            this.btnXuatThe.UseVisualStyleBackColor = true;
            this.btnXuatThe.Click += new System.EventHandler(this.btnXuatThe_Click);
            // 
            // frmHocSinhTrongPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnXuatThe);
            this.Controls.Add(this.dgvHocSinh);
            this.Name = "frmHocSinhTrongPhong";
            this.Text = "frmHocSinhTrongPhong";
            this.Load += new System.EventHandler(this.frmHocSinhTrongPhong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocSinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource phongThiBindingSource;
        private System.Windows.Forms.DataGridView dgvHocSinh;
        private System.Windows.Forms.BindingSource phongThiBindingSource1;
        private System.Windows.Forms.Button btnXuatThe;
    }
}