namespace TuyenSinhWinApp
{
    partial class frmThongKeDiemChuan
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
            this.dgvDiemChuan = new System.Windows.Forms.DataGridView();
            this.cbTruong = new System.Windows.Forms.ComboBox();
            this.lblTruong = new System.Windows.Forms.Label();
            this.btnTaiLai = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiemChuan)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDiemChuan
            // 
            this.dgvDiemChuan.BackgroundColor = System.Drawing.Color.White;
            this.dgvDiemChuan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiemChuan.Location = new System.Drawing.Point(5, 58);
            this.dgvDiemChuan.Name = "dgvDiemChuan";
            this.dgvDiemChuan.Size = new System.Drawing.Size(1611, 737);
            this.dgvDiemChuan.TabIndex = 1;
            // 
            // cbTruong
            // 
            this.cbTruong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTruong.FormattingEnabled = true;
            this.cbTruong.Location = new System.Drawing.Point(1062, 18);
            this.cbTruong.Name = "cbTruong";
            this.cbTruong.Size = new System.Drawing.Size(294, 24);
            this.cbTruong.TabIndex = 74;
            // 
            // lblTruong
            // 
            this.lblTruong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTruong.Location = new System.Drawing.Point(962, 18);
            this.lblTruong.Name = "lblTruong";
            this.lblTruong.Size = new System.Drawing.Size(94, 21);
            this.lblTruong.TabIndex = 73;
            this.lblTruong.Text = "Chọn trường :";
            this.lblTruong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaiLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaiLai.Image = global::TuyenSinhWinApp.Properties.Resources.reload;
            this.btnTaiLai.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTaiLai.Location = new System.Drawing.Point(1424, 12);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(192, 36);
            this.btnTaiLai.TabIndex = 75;
            this.btnTaiLai.Text = "Tải lại";
            this.btnTaiLai.UseVisualStyleBackColor = true;
            // 
            // frmThongKeDiemChuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1642, 807);
            this.Controls.Add(this.btnTaiLai);
            this.Controls.Add(this.cbTruong);
            this.Controls.Add(this.lblTruong);
            this.Controls.Add(this.dgvDiemChuan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmThongKeDiemChuan";
            this.Text = "frmThongKeDiemChuan";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiemChuan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDiemChuan;
        private System.Windows.Forms.ComboBox cbTruong;
        private System.Windows.Forms.Label lblTruong;
        private System.Windows.Forms.Button btnTaiLai;
    }
}