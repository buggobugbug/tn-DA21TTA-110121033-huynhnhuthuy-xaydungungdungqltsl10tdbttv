namespace TuyenSinhWinApp
{
    partial class frmMain
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
            this.btnQuanlyhocsinh = new System.Windows.Forms.Button();
            this.btnXemHocSinh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPhongThi = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQuanlyhocsinh
            // 
            this.btnQuanlyhocsinh.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnQuanlyhocsinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuanlyhocsinh.ForeColor = System.Drawing.Color.White;
            this.btnQuanlyhocsinh.Location = new System.Drawing.Point(12, 290);
            this.btnQuanlyhocsinh.Name = "btnQuanlyhocsinh";
            this.btnQuanlyhocsinh.Size = new System.Drawing.Size(148, 122);
            this.btnQuanlyhocsinh.TabIndex = 0;
            this.btnQuanlyhocsinh.Text = "QUẢN LÝ HỌC SINH";
            this.btnQuanlyhocsinh.UseVisualStyleBackColor = false;
            this.btnQuanlyhocsinh.Click += new System.EventHandler(this.btnQuanlyhocsinh_Click);
            // 
            // btnXemHocSinh
            // 
            this.btnXemHocSinh.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnXemHocSinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemHocSinh.ForeColor = System.Drawing.Color.White;
            this.btnXemHocSinh.Location = new System.Drawing.Point(12, 155);
            this.btnXemHocSinh.Name = "btnXemHocSinh";
            this.btnXemHocSinh.Size = new System.Drawing.Size(148, 129);
            this.btnXemHocSinh.TabIndex = 1;
            this.btnXemHocSinh.Text = "DANH SÁCH";
            this.btnXemHocSinh.UseVisualStyleBackColor = false;
            this.btnXemHocSinh.Click += new System.EventHandler(this.btnXemHocSinh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(118, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Trang chủ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Học sinh";
            // 
            // btnPhongThi
            // 
            this.btnPhongThi.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPhongThi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhongThi.ForeColor = System.Drawing.Color.White;
            this.btnPhongThi.Location = new System.Drawing.Point(166, 155);
            this.btnPhongThi.Name = "btnPhongThi";
            this.btnPhongThi.Size = new System.Drawing.Size(148, 129);
            this.btnPhongThi.TabIndex = 5;
            this.btnPhongThi.Text = "PHÒNG THI";
            this.btnPhongThi.UseVisualStyleBackColor = false;
            this.btnPhongThi.Click += new System.EventHandler(this.btnPhongThi_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TuyenSinhWinApp.Properties.Resources.digital_learning;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1130, 645);
            this.Controls.Add(this.btnPhongThi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnXemHocSinh);
            this.Controls.Add(this.btnQuanlyhocsinh);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Màn hình chính";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQuanlyhocsinh;
        private System.Windows.Forms.Button btnXemHocSinh;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPhongThi;
    }
}