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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHocSinhTrongPhong));
            this.phongThiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dgvHocSinh = new System.Windows.Forms.DataGridView();
            this.phongThiBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnXuatThe = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocSinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // phongThiBindingSource
            // 
            this.phongThiBindingSource.DataSource = typeof(TuyenSinhServiceLib.PhongThi);
            // 
            // dgvHocSinh
            // 
            this.dgvHocSinh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHocSinh.Location = new System.Drawing.Point(12, 53);
            this.dgvHocSinh.Name = "dgvHocSinh";
            this.dgvHocSinh.Size = new System.Drawing.Size(530, 496);
            this.dgvHocSinh.TabIndex = 0;
            // 
            // phongThiBindingSource1
            // 
            this.phongThiBindingSource1.DataSource = typeof(TuyenSinhServiceLib.PhongThi);
            // 
            // btnXuatThe
            // 
            this.btnXuatThe.Location = new System.Drawing.Point(52, 197);
            this.btnXuatThe.Name = "btnXuatThe";
            this.btnXuatThe.Size = new System.Drawing.Size(110, 23);
            this.btnXuatThe.TabIndex = 1;
            this.btnXuatThe.Text = "Xuất thẻ dự thi";
            this.btnXuatThe.UseVisualStyleBackColor = true;
            this.btnXuatThe.Click += new System.EventHandler(this.btnXuatThe_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.btnXuatThe);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(557, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 496);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CONTROL";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(38, 240);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(136, 122);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 36;
            this.pictureBox1.TabStop = false;
            // 
            // frmHocSinhTrongPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvHocSinh);
            this.Name = "frmHocSinhTrongPhong";
            this.Text = "frmHocSinhTrongPhong";
            this.Load += new System.EventHandler(this.frmHocSinhTrongPhong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocSinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phongThiBindingSource1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource phongThiBindingSource;
        private System.Windows.Forms.DataGridView dgvHocSinh;
        private System.Windows.Forms.BindingSource phongThiBindingSource1;
        private System.Windows.Forms.Button btnXuatThe;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}