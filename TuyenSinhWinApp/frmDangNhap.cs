using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration; // ✔️ Đảm bảo bạn đã thêm reference này
using TuyenSinhWinApp.TuyenSinhServiceReference;
using TuyenSinhServiceLib;

namespace TuyenSinhWinApp
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var client = new TuyenSinhServiceReference.Service1Client();
            try
            {
                var result = client.DangNhap(txtTenDangNhap.Text.Trim(), txtMatKhau.Text);

                if (result.ThanhCong)
                {
                    // Lưu thông tin vào biến toàn cục
                    Common.MaNguoiDung = result.NguoiDung.MaNguoiDung;
                    Common.HoTen = result.NguoiDung.HoTen;
                    Common.MaTruong = result.NguoiDung.MaTruong;

                    // Lấy tên trường từ CSDL
                    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TuyenSinhDB"].ConnectionString))
                    {
                        var cmd = new SqlCommand("SELECT TenTruong FROM TRUONG_HOC WHERE MaTruong=@MaTruong", conn);
                        cmd.Parameters.AddWithValue("@MaTruong", Common.MaTruong);
                        conn.Open();
                        Common.TenTruong = cmd.ExecuteScalar()?.ToString();
                    }

                    var dsDot = client.LayDanhSachDotTuyen();
                    var dotDangMo = dsDot != null ? Array.Find(dsDot, d => d.TrangThai == "DangMo") : null;
                    if (dotDangMo != null)
                        Common.MaDot = dotDangMo.MaDot;
                    else
                        Common.MaDot = null;

                    this.Hide();
                    var mainForm = new frmMain();
                    mainForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.ThongBao, "Đăng nhập thất bại",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kết nối dịch vụ: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                client.Close();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
