using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration; // ✔️ Đảm bảo bạn đã thêm reference này
using TuyenSinhWinApp.TuyenSinhServiceReference;
using TuyenSinhServiceLib;
using System.Linq;

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
                    // Lưu session
                    Common.MaNguoiDung = result.NguoiDung.MaNguoiDung;
                    Common.HoTen = result.NguoiDung.HoTen;
                    Common.MaTruong = result.NguoiDung.MaTruong;   // có thể null với AdminSo
                    Common.TenDangNhap = result.NguoiDung.TenDangNhap;
                    Common.VaiTro = result.NguoiDung.VaiTro;     // <-- NEW

                    // Lấy tên trường nếu có MaTruong
                    if (!string.IsNullOrWhiteSpace(Common.MaTruong))
                    {
                        using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TuyenSinhDB"].ConnectionString))
                        {
                            var cmd = new SqlCommand("SELECT TenTruong FROM TRUONG_HOC WHERE MaTruong=@MaTruong", conn);
                            cmd.Parameters.AddWithValue("@MaTruong", Common.MaTruong);
                            conn.Open();
                            Common.TenTruong = cmd.ExecuteScalar()?.ToString();
                        }
                    }
                    else
                    {
                        Common.TenTruong = null;
                    }

                    // Lấy đợt đang mở (nếu có)
                    var dsDot = client.LayDanhSachDotTuyen();
                    var dotDangMo = dsDot?.FirstOrDefault(d =>
                        string.Equals(d.TrangThai, "DangMo", StringComparison.OrdinalIgnoreCase));
                    Common.MaDot = dotDangMo?.MaDot;

                    // Điều hướng theo vai trò
                    this.Hide();
                    Form next;
                    if (Common.IsAdmin)
                        next = new Frmadmin();    
                    else
                        next = new frmMain();

                    next.ShowDialog();
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

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
