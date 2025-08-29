using System.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Globalization; // <-- NEW: nếu cần format số

namespace TuyenSinhServiceLib
{

    public class Service1 : IService1
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TuyenSinhDB"].ConnectionString;

        // Đăng nhập

        public KetQuaDangNhap DangNhap(string tenDangNhap, string matKhau)
        {
            var ketQua = new KetQuaDangNhap();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT MaNguoiDung, HoTen, MaTruong, ISNULL(VaiTro, N'CanBoTruong') AS VaiTro
              FROM NGUOI_DUNG
              WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau", conn))
                {
                    cmd.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar, 50).Value = tenDangNhap;
                    cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 255).Value = matKhau;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ketQua.ThanhCong = true;
                            ketQua.ThongBao = "Đăng nhập thành công";
                            ketQua.NguoiDung = new NguoiDung
                            {
                                MaNguoiDung = reader.GetInt32(0),
                                HoTen = reader.GetString(1),
                                MaTruong = reader.IsDBNull(2) ? null : reader.GetString(2),
                                VaiTro = reader.GetString(3),
                                TenDangNhap = tenDangNhap
                            };
                        }
                        else
                        {
                            ketQua.ThanhCong = false;
                            ketQua.ThongBao = "Sai tên đăng nhập hoặc mật khẩu";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ketQua.ThanhCong = false;
                ketQua.ThongBao = "Lỗi hệ thống: " + ex.Message;
            }

            return ketQua;
        }


        public KetQuaThemHocSinh ThemHocSinh(HocSinh hocSinh)
        {
            var ketQua = new KetQuaThemHocSinh();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ThemHocSinh", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Thêm tham số tương ứng với thủ tục SQL
                        cmd.Parameters.AddWithValue("@Ho", hocSinh.Ho);
                        cmd.Parameters.AddWithValue("@Ten", hocSinh.Ten);
                        cmd.Parameters.AddWithValue("@NgaySinh", hocSinh.NgaySinh);
                        cmd.Parameters.AddWithValue("@GioiTinh", hocSinh.GioiTinh);
                        cmd.Parameters.AddWithValue("@MaTruong", hocSinh.MaTruong);
                        cmd.Parameters.AddWithValue("@MaDot", string.IsNullOrWhiteSpace(hocSinh.MaDot) ? (object)DBNull.Value : hocSinh.MaDot);
                        cmd.Parameters.AddWithValue("@DanToc", string.IsNullOrWhiteSpace(hocSinh.DanToc) ? (object)DBNull.Value : hocSinh.DanToc);
                        cmd.Parameters.AddWithValue("@NoiSinh", string.IsNullOrWhiteSpace(hocSinh.NoiSinh) ? (object)DBNull.Value : hocSinh.NoiSinh);
                        cmd.Parameters.AddWithValue("@TruongTHCS", string.IsNullOrWhiteSpace(hocSinh.TruongTHCS) ? (object)DBNull.Value : hocSinh.TruongTHCS);
                        cmd.Parameters.AddWithValue("@DiemToan",
    string.IsNullOrWhiteSpace(hocSinh.DiemToan) ? (object)DBNull.Value : hocSinh.DiemToan);
                        cmd.Parameters.AddWithValue("@DiemVan",
                            string.IsNullOrWhiteSpace(hocSinh.DiemVan) ? (object)DBNull.Value : hocSinh.DiemVan);
                        cmd.Parameters.AddWithValue("@DiemAnh",
                            string.IsNullOrWhiteSpace(hocSinh.DiemAnh) ? (object)DBNull.Value : hocSinh.DiemAnh);
                        cmd.Parameters.AddWithValue("@DiemKhuyenKhich", hocSinh.DiemKhuyenKhich.HasValue ? (object)hocSinh.DiemKhuyenKhich.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiemUuTien", hocSinh.DiemUuTien.HasValue ? (object)hocSinh.DiemUuTien.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@PhongThi", string.IsNullOrWhiteSpace(hocSinh.PhongThi) ? (object)DBNull.Value : hocSinh.PhongThi);
                        cmd.Parameters.AddWithValue("@TrangThai", string.IsNullOrWhiteSpace(hocSinh.TrangThai) ? "DangKy" : hocSinh.TrangThai);
                        cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrWhiteSpace(hocSinh.GhiChu) ? (object)DBNull.Value : hocSinh.GhiChu);

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                var hs = new HocSinh
                                {
                                    MaHocSinh = reader.GetInt32(reader.GetOrdinal("MaHocSinh")),
                                    MaSoBaoDanh = reader.IsDBNull(reader.GetOrdinal("MaSoBaoDanh")) ? null : reader.GetString(reader.GetOrdinal("MaSoBaoDanh")),
                                    Ho = reader.GetString(reader.GetOrdinal("Ho")),
                                    Ten = reader.GetString(reader.GetOrdinal("Ten")),
                                    NgaySinh = reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                                    GioiTinh = reader.GetString(reader.GetOrdinal("GioiTinh")),
                                    DanToc = reader.IsDBNull(reader.GetOrdinal("DanToc")) ? null : reader.GetString(reader.GetOrdinal("DanToc")),
                                    NoiSinh = reader.IsDBNull(reader.GetOrdinal("NoiSinh")) ? null : reader.GetString(reader.GetOrdinal("NoiSinh")),
                                    TruongTHCS = reader.IsDBNull(reader.GetOrdinal("TruongTHCS")) ? null : reader.GetString(reader.GetOrdinal("TruongTHCS")),
                                    MaTruong = reader.GetString(reader.GetOrdinal("MaTruong")),
                                    MaDot = reader.IsDBNull(reader.GetOrdinal("MaDot")) ? null : reader.GetString(reader.GetOrdinal("MaDot")),
                                    DiemToan = reader["DiemToan"] as string,
                                    DiemVan = reader["DiemVan"] as string,
                                    DiemAnh = reader["DiemAnh"] as string,
                                    DiemTong = reader.IsDBNull(reader.GetOrdinal("DiemTong")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("DiemTong")),
                                    DiemKhuyenKhich = reader.IsDBNull(reader.GetOrdinal("DiemKhuyenKhich")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("DiemKhuyenKhich")),
                                    DiemUuTien = reader.IsDBNull(reader.GetOrdinal("DiemUuTien")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("DiemUuTien")),
                                    PhongThi = reader.IsDBNull(reader.GetOrdinal("PhongThi")) ? null : reader.GetString(reader.GetOrdinal("PhongThi")),
                                    TrangThai = reader.GetString(reader.GetOrdinal("TrangThai")),
                                    NgayDangKy = reader.GetDateTime(reader.GetOrdinal("NgayDangKy")),
                                    GhiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? null : reader.GetString(reader.GetOrdinal("GhiChu"))
                                };

                                ketQua.HocSinh = hs;
                                ketQua.MaSoBaoDanh = hs.MaSoBaoDanh;
                                ketQua.ThanhCong = true;
                                ketQua.ThongBao = "Thêm học sinh thành công";
                            }
                            else
                            {
                                ketQua.ThanhCong = false;
                                ketQua.ThongBao = "Không thể thêm học sinh";
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                ketQua.ThanhCong = false;
                ketQua.ThongBao = $"Lỗi SQL: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                ketQua.ThanhCong = false;
                ketQua.ThongBao = $"Lỗi hệ thống: {ex.Message}";
            }

            return ketQua;
        }

        // Tạo mã số báo danh

        public void GanMaSoBaoDanhHangLoat(string maTruong, string maDot)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GanMaSoBaoDanhHangLoat", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaTruong", maTruong);
                    cmd.Parameters.AddWithValue("@MaDot", maDot);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return "Kết nối CSDL thành công!";
                }
            }
            catch (Exception ex)
            {
                return $"Lỗi kết nối CSDL: {ex.Message}";
            }
        }

        // Lấy danh sách học sinh để xem
        public List<HocSinh> LayDanhSachHocSinh(string maTruong, string maDot)
        {
            var danhSach = new List<HocSinh>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(
                        @"SELECT * FROM HOC_SINH 
                  WHERE MaTruong = @MaTruong AND MaDot = @MaDot
                  ORDER BY Ten COLLATE Vietnamese_CI_AI, Ho COLLATE Vietnamese_CI_AI", conn);

                    cmd.Parameters.AddWithValue("@MaTruong", maTruong);
                    cmd.Parameters.AddWithValue("@MaDot", maDot);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var hs = new HocSinh
                            {
                                MaHocSinh = reader.GetInt32(reader.GetOrdinal("MaHocSinh")),
                                MaSoBaoDanh = reader.IsDBNull(reader.GetOrdinal("MaSoBaoDanh")) ? null : reader.GetString(reader.GetOrdinal("MaSoBaoDanh")),
                                Ho = reader.IsDBNull(reader.GetOrdinal("Ho")) ? null : reader.GetString(reader.GetOrdinal("Ho")),
                                Ten = reader.IsDBNull(reader.GetOrdinal("Ten")) ? null : reader.GetString(reader.GetOrdinal("Ten")),
                                NgaySinh = reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                                GioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh")) ? null : reader.GetString(reader.GetOrdinal("GioiTinh")),
                                DanToc = reader.IsDBNull(reader.GetOrdinal("DanToc")) ? null : reader.GetString(reader.GetOrdinal("DanToc")),
                                NoiSinh = reader.IsDBNull(reader.GetOrdinal("NoiSinh")) ? null : reader.GetString(reader.GetOrdinal("NoiSinh")),
                                TruongTHCS = reader.IsDBNull(reader.GetOrdinal("TruongTHCS")) ? null : reader.GetString(reader.GetOrdinal("TruongTHCS")),
                                MaTruong = reader.GetString(reader.GetOrdinal("MaTruong")),
                                MaDot = reader.IsDBNull(reader.GetOrdinal("MaDot")) ? null : reader.GetString(reader.GetOrdinal("MaDot")),
                                DiemToan = reader["DiemToan"] as string,
                                DiemVan = reader["DiemVan"] as string,
                                DiemAnh = reader["DiemAnh"] as string,
                                DiemTong = reader.IsDBNull(reader.GetOrdinal("DiemTong")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("DiemTong")),
                                DiemKhuyenKhich = reader.IsDBNull(reader.GetOrdinal("DiemKhuyenKhich")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("DiemKhuyenKhich")),
                                DiemUuTien = reader.IsDBNull(reader.GetOrdinal("DiemUuTien")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("DiemUuTien")),
                                PhongThi = reader.IsDBNull(reader.GetOrdinal("PhongThi")) ? null : reader.GetString(reader.GetOrdinal("PhongThi")),
                                TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai")),
                                NgayDangKy = reader.GetDateTime(reader.GetOrdinal("NgayDangKy")),
                                GhiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? null : reader.GetString(reader.GetOrdinal("GhiChu"))
                            };
                            danhSach.Add(hs);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi LayDanhSachHocSinh: " + ex.Message);
            }

            return danhSach;
        }


        // Hàm cập nhật học sinh

        public bool CapNhatHocSinh(HocSinh hs)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // CHỈ cập nhật thông tin hành chính, KHÔNG đụng cột điểm & DiemTong
                    string sql = @"
UPDATE HOC_SINH SET 
    Ho = @Ho,
    Ten = @Ten,
    NgaySinh = @NgaySinh,
    GioiTinh = @GioiTinh,
    DanToc = @DanToc,
    NoiSinh = @NoiSinh,
    TruongTHCS = @TruongTHCS,
    PhongThi = @PhongThi,
    TrangThai = @TrangThai,
    GhiChu = @GhiChu,
    MaDot = @MaDot
WHERE MaHocSinh = @MaHocSinh";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Ho", (object)hs.Ho ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Ten", (object)hs.Ten ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@NgaySinh", hs.NgaySinh);
                        cmd.Parameters.AddWithValue("@GioiTinh", (object)hs.GioiTinh ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DanToc", (object)hs.DanToc ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@NoiSinh", (object)hs.NoiSinh ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TruongTHCS", (object)hs.TruongTHCS ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PhongThi", (object)hs.PhongThi ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TrangThai", (object)hs.TrangThai ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@GhiChu", (object)hs.GhiChu ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MaDot", string.IsNullOrWhiteSpace(hs.MaDot) ? (object)DBNull.Value : hs.MaDot);
                        cmd.Parameters.AddWithValue("@MaHocSinh", hs.MaHocSinh);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật: " + ex.Message);
                return false;
            }
        }


        // Cập nhật điểm (cũ: decimal?) — giữ nguyên
        public bool CapNhatDiemHocSinh(int maHocSinh, decimal? diemToan, decimal? diemVan, decimal? diemAnh, decimal? diemKhuyenKhich, decimal? diemUuTien)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Tính tổng điểm nếu có đủ điểm thành phần
                    decimal? tong = null;
                    if (diemToan.HasValue && diemVan.HasValue && diemAnh.HasValue)
                        tong = diemToan.Value + diemVan.Value + diemAnh.Value + (diemKhuyenKhich ?? 0) + (diemUuTien ?? 0);

                    string sql = @"
                UPDATE HOC_SINH SET
                    DiemToan = @DiemToan,
                    DiemVan = @DiemVan,
                    DiemAnh = @DiemAnh,
                    DiemKhuyenKhich = @DiemKhuyenKhich,
                    DiemUuTien = @DiemUuTien,
                    DiemTong = @DiemTong
                WHERE MaHocSinh = @MaHocSinh
            ";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@DiemToan", (object)diemToan ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemVan", (object)diemVan ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemAnh", (object)diemAnh ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemKhuyenKhich", (object)diemKhuyenKhich ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemUuTien", (object)diemUuTien ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemTong", (object)tong ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MaHocSinh", maHocSinh);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        // ---------------- NEW: Overload đúng chữ ký interface (điểm dạng CHUỖI) ----------------
        public bool CapNhatDiemHocSinh(int maHocSinh, string diemToan, string diemVan, string diemAnh, decimal? diemKhuyenKhich, decimal? diemUuTien)
        {
            return CapNhatDiemHocSinhV2(maHocSinh, diemToan, diemVan, diemAnh, diemKhuyenKhich, diemUuTien);
        }

        // NEW: Hàm nội bộ gọi SP - hỗ trợ "Vắng" hoặc chuỗi
        private bool CapNhatDiemHocSinhV2(int maHocSinh, string diemToan, string diemVan, string diemAnh, decimal? diemKhuyenKhich, decimal? diemUuTien)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Lấy Mã SBD
                    string maSBD;
                    using (var get = new SqlCommand("SELECT MaSoBaoDanh FROM HOC_SINH WHERE MaHocSinh=@id", conn))
                    {
                        get.Parameters.AddWithValue("@id", maHocSinh);
                        maSBD = get.ExecuteScalar() as string;
                        if (string.IsNullOrWhiteSpace(maSBD)) return false;
                    }

                    using (var cmd = new SqlCommand("sp_CapNhatDiem", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaBaoDanh", maSBD);
                        cmd.Parameters.AddWithValue("@DiemToan", string.IsNullOrWhiteSpace(diemToan) ? (object)DBNull.Value : diemToan);
                        cmd.Parameters.AddWithValue("@DiemVan", string.IsNullOrWhiteSpace(diemVan) ? (object)DBNull.Value : diemVan);
                        cmd.Parameters.AddWithValue("@DiemAnh", string.IsNullOrWhiteSpace(diemAnh) ? (object)DBNull.Value : diemAnh);
                        cmd.Parameters.AddWithValue("@DiemKhuyenKhich", (object)diemKhuyenKhich ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiemUuTien", (object)diemUuTien ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CapNhatDiemHocSinhV2 error: " + ex);
                return false;
            }
        }
        // ---------------------------------------------------------------------------------------

        // Hàm xóa học sinh
        public bool XoaHocSinh(int maHocSinh)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM HOC_SINH WHERE MaHocSinh = @MaHocSinh", conn);
                    cmd.Parameters.AddWithValue("@MaHocSinh", maHocSinh);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool ChiaPhongThi(string maTruong, string maDot)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_ChiaPhongThi", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaTruong", maTruong);
                    cmd.Parameters.AddWithValue("@MaDot", maDot);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi chia phòng: " + ex.Message);
                return false;
            }
        }



        // Lấy danh sách phòng thi
        public List<PhongThi> LayDanhSachPhongThi(string maTruong, string maDot)
        {
            var danhSach = new List<PhongThi>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM PHONG_THI WHERE MaTruong = @MaTruong";
                if (!string.IsNullOrEmpty(maDot))
                    sql += " AND MaDot = @MaDot";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTruong", maTruong);
                if (!string.IsNullOrEmpty(maDot))
                    cmd.Parameters.AddWithValue("@MaDot", maDot);

                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var phong = new PhongThi
                    {
                        MaPhong = reader.GetInt32(reader.GetOrdinal("MaPhong")),
                        MaPhongThi = reader.GetString(reader.GetOrdinal("MaPhongThi")),
                        MaTruong = reader.GetString(reader.GetOrdinal("MaTruong")),
                        MaDot = reader["MaDot"] as string,
                        DiaDiem = reader["DiaDiem"] as string,
                        SoLuongToiDa = reader.GetInt32(reader.GetOrdinal("SoLuongToiDa")),
                        SoLuongHienTai = reader.GetInt32(reader.GetOrdinal("SoLuongHienTai")),
                        GiamThi1 = reader["GiamThi1"] as string,
                        GiamThi2 = reader["GiamThi2"] as string,
                        NgayThi = reader["NgayThi"] as DateTime?
                    };
                    danhSach.Add(phong);
                }
            }
            return danhSach;
        }



        public List<HocSinh> LayDanhSachHocSinhTheoPhong(string maTruong, string maDot, string maPhongThi)
        {
            var danhSach = new List<HocSinh>();
            try
            {
                // Log ra file, messagebox hoặc console
                System.Diagnostics.Debug.WriteLine($"maTruong={maTruong}, maDot={maDot}, maPhongThi={maPhongThi}");
                // hoặc
                // File.AppendAllText("log.txt", $"{DateTime.Now} - maTruong={maTruong}, maDot={maDot}, maPhongThi={maPhongThi}\n");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(
    @"SELECT * FROM HOC_SINH 
      WHERE PhongThi = @PhongThi AND MaTruong = @MaTruong AND MaDot = @MaDot
      ORDER BY Ten COLLATE Vietnamese_CI_AI, Ho COLLATE Vietnamese_CI_AI", conn);

                    cmd.Parameters.AddWithValue("@PhongThi", maPhongThi ?? "");
                    cmd.Parameters.AddWithValue("@MaTruong", maTruong ?? "");
                    cmd.Parameters.AddWithValue("@MaDot", maDot ?? "");

                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var hs = new HocSinh
                        {
                            MaHocSinh = (int)reader["MaHocSinh"],
                            MaSoBaoDanh = reader["MaSoBaoDanh"] as string,
                            Ho = reader["Ho"] as string,
                            Ten = reader["Ten"] as string,
                            NgaySinh = (DateTime)reader["NgaySinh"],
                            GioiTinh = reader["GioiTinh"] as string,
                            DanToc = reader["DanToc"] as string,
                            NoiSinh = reader["NoiSinh"] as string,
                            TruongTHCS = reader["TruongTHCS"] as string,
                            DiemToan = reader["DiemToan"] as string,
                            DiemVan = reader["DiemVan"] as string,
                            DiemAnh = reader["DiemAnh"] as string,
                            DiemKhuyenKhich = reader["DiemKhuyenKhich"] as decimal?,
                            DiemUuTien = reader["DiemUuTien"] as decimal?,
                            DiemTong = reader["DiemTong"] as decimal?,
                            TrangThai = reader["TrangThai"] as string,
                            GhiChu = reader["GhiChu"] as string,
                        };
                        danhSach.Add(hs);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log lỗi cụ thể ra file hoặc messagebox
                System.Diagnostics.Debug.WriteLine("Lỗi SQL: " + ex.ToString());
            }
            return danhSach;
        }



        public List<DotTuyenSinh> LayDanhSachDotTuyen()
        {
            List<DotTuyenSinh> ds = new List<DotTuyenSinh>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM DOT_TUYEN_SINH", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ds.Add(new DotTuyenSinh
                    {
                        MaDot = reader["MaDot"].ToString(),
                        TenDot = reader["TenDot"].ToString(),
                        Nam = Convert.ToInt32(reader["Nam"]),
                        NgayBatDau = reader["NgayBatDau"] as DateTime?,
                        NgayKetThuc = reader["NgayKetThuc"] as DateTime?,
                        TrangThai = reader["TrangThai"].ToString()
                    });
                }
            }
            return ds;
        }

        public bool ThemDotTuyen(DotTuyenSinh dot)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO DOT_TUYEN_SINH (MaDot, TenDot, Nam, NgayBatDau, NgayKetThuc) VALUES (@MaDot, @TenDot, @Nam, @NgayBatDau, @NgayKetThuc)", conn);
                cmd.Parameters.AddWithValue("@MaDot", dot.MaDot);
                cmd.Parameters.AddWithValue("@TenDot", dot.TenDot);
                cmd.Parameters.AddWithValue("@Nam", dot.Nam);
                cmd.Parameters.AddWithValue("@NgayBatDau", dot.NgayBatDau ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayKetThuc", dot.NgayKetThuc ?? (object)DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DongDotTuyen(string maDot)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE DOT_TUYEN_SINH SET TrangThai = N'DaDong' WHERE MaDot = @MaDot", conn);
                cmd.Parameters.AddWithValue("@MaDot", maDot);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Cập nhật đợt tuyển sinh
        public bool CapNhatDotTuyen(DotTuyenSinh dot)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
            UPDATE DOT_TUYEN_SINH 
            SET 
                TenDot = @TenDot,
                Nam = @Nam,
                NgayBatDau = @NgayBatDau,
                NgayKetThuc = @NgayKetThuc,
                TrangThai = @TrangThai
            WHERE MaDot = @MaDot", conn);

                cmd.Parameters.AddWithValue("@MaDot", dot.MaDot);
                cmd.Parameters.AddWithValue("@TenDot", dot.TenDot);
                cmd.Parameters.AddWithValue("@Nam", dot.Nam);
                cmd.Parameters.AddWithValue("@NgayBatDau", dot.NgayBatDau ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayKetThuc", dot.NgayKetThuc ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TrangThai", string.IsNullOrEmpty(dot.TrangThai) ? (object)DBNull.Value : dot.TrangThai);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Cập nhật giám thị
        public void CapNhatGiamThi(string maPhong, string giamThi1, string giamThi2)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_CapNhatGiamThi", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaPhongThi", maPhong);
                cmd.Parameters.AddWithValue("@GiamThi1", giamThi1);
                cmd.Parameters.AddWithValue("@GiamThi2", giamThi2);
                cmd.ExecuteNonQuery();
            }
        }


        // Cập nhật điểm theo môn (cũ: decimal?) — giữ nguyên
        public bool CapNhatDiemTheoMon(int maHocSinh, string tenMon, decimal? diem)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = "";
                    switch (tenMon)
                    {
                        case "Toán":
                            sql = "UPDATE HOC_SINH SET DiemToan = @Diem WHERE MaHocSinh = @MaHocSinh"; break;
                        case "Văn":
                            sql = "UPDATE HOC_SINH SET DiemVan = @Diem WHERE MaHocSinh = @MaHocSinh"; break;
                        case "Anh":
                            sql = "UPDATE HOC_SINH SET DiemAnh = @Diem WHERE MaHocSinh = @MaHocSinh"; break;
                        default: return false;
                    }

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Diem", (object)diem ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MaHocSinh", maHocSinh);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        // ---------------- NEW: Overload đúng chữ ký interface (môn + chuỗi) ----------------
        public bool CapNhatDiemTheoMon(int maHocSinh, string tenMon, string diemText)
        {
            return CapNhatDiemTheoMonV2(maHocSinh, tenMon, diemText);
        }

        // NEW: Hàm nội bộ set theo môn + gọi SP sp_CapNhatDiem
        private bool CapNhatDiemTheoMonV2(int maHocSinh, string tenMon, string diemText)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string maSBD;
                    string toan = null, van = null, anh = null;
                    decimal? kk = null, ut = null;

                    // Lấy hiện trạng 3 môn & SBD
                    using (var rdCmd = new SqlCommand(@"
                        SELECT MaSoBaoDanh, DiemToan, DiemVan, DiemAnh, DiemKhuyenKhich, DiemUuTien
                        FROM HOC_SINH WHERE MaHocSinh=@id", conn))
                    {
                        rdCmd.Parameters.AddWithValue("@id", maHocSinh);
                        using (var rd = rdCmd.ExecuteReader())
                        {
                            if (!rd.Read()) return false;
                            maSBD = rd["MaSoBaoDanh"] as string;
                            toan = rd["DiemToan"] as string;
                            van = rd["DiemVan"] as string;
                            anh = rd["DiemAnh"] as string;
                            kk = rd["DiemKhuyenKhich"] as decimal?;
                            ut = rd["DiemUuTien"] as decimal?;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(maSBD)) return false;

                    // Cập nhật 1 môn
                    switch (tenMon)
                    {
                        case "Toán": toan = string.IsNullOrWhiteSpace(diemText) ? null : diemText; break;
                        case "Văn": van = string.IsNullOrWhiteSpace(diemText) ? null : diemText; break;
                        case "Anh": anh = string.IsNullOrWhiteSpace(diemText) ? null : diemText; break;
                        default: return false;
                    }

                    // Gọi SP tính toán/tổng hợp ở DB
                    using (var cmd = new SqlCommand("sp_CapNhatDiem", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaBaoDanh", maSBD);
                        cmd.Parameters.AddWithValue("@DiemToan", (object)toan ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiemVan", (object)van ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiemAnh", (object)anh ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiemKhuyenKhich", (object)kk ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiemUuTien", (object)ut ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CapNhatDiemTheoMonV2 error: " + ex);
                return false;
            }
        }
        // ------------------------------------------------------------------------------------

        // Danh sách chỉ tiêu

        public ChiTieuTuyenSinh LayChiTieuTruong(string maTruong, string maDot)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT MaTruong, MaDot, ChiTieu FROM CHI_TIEU_TUYEN_SINH WHERE MaTruong = @MaTruong AND MaDot = @MaDot";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTruong", maTruong);
                cmd.Parameters.AddWithValue("@MaDot", maDot);
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new ChiTieuTuyenSinh
                    {
                        MaTruong = reader["MaTruong"].ToString(),
                        MaDot = reader["MaDot"].ToString(),
                        ChiTieu = Convert.ToInt32(reader["ChiTieu"])
                    };
                }
                return null;
            }
        }

        public bool CapNhatChiTieu(string maTruong, string maDot, int chiTieu)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
    IF EXISTS (SELECT 1 FROM CHI_TIEU_TUYEN_SINH WHERE MaTruong=@MaTruong AND MaDot=@MaDot)
    UPDATE CHI_TIEU_TUYEN_SINH SET ChiTieu=@ChiTieu WHERE MaTruong=@MaTruong AND MaDot=@MaDot
ELSE
    INSERT INTO CHI_TIEU_TUYEN_SINH (MaTruong, MaDot, ChiTieu) VALUES (@MaTruong, @MaDot, @ChiTieu)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTruong", maTruong);
                cmd.Parameters.AddWithValue("@MaDot", maDot);
                cmd.Parameters.AddWithValue("@ChiTieu", chiTieu);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Xử lý trúng tuyển

        public bool XetTrungTuyen(string maTruong, string maDot)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_XetTrungTuyen", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaTruong", maTruong);
                    cmd.Parameters.AddWithValue("@MaDot", maDot);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi XetTrungTuyen: " + ex.Message);
                return false;
            }
        }

        // Lấy danh sách học sinh trúng tuyển

        public List<HocSinh> LayDanhSachTrungTuyen(string maTruong, string maDot)
        {
            var ds = new List<HocSinh>();
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(
                    @"SELECT * FROM HOC_SINH 
              WHERE MaTruong = @MaTruong AND MaDot = @MaDot AND TrangThai = 'TrungTuyen'
              ORDER BY DiemTong DESC, Ten COLLATE Vietnamese_CI_AI, Ho COLLATE Vietnamese_CI_AI", conn);
                cmd.Parameters.AddWithValue("@MaTruong", maTruong);
                cmd.Parameters.AddWithValue("@MaDot", maDot);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ds.Add(new HocSinh
                        {
                            MaHocSinh = (int)reader["MaHocSinh"],
                            MaSoBaoDanh = reader["MaSoBaoDanh"] as string,
                            Ho = reader["Ho"] as string,
                            Ten = reader["Ten"] as string,
                            NgaySinh = (DateTime)reader["NgaySinh"],
                            GioiTinh = reader["GioiTinh"] as string,
                            DanToc = reader["DanToc"] as string,
                            NoiSinh = reader["NoiSinh"] as string,
                            TruongTHCS = reader["TruongTHCS"] as string,
                            MaTruong = reader["MaTruong"] as string,
                            MaDot = reader["MaDot"] as string,
                            DiemToan = reader["DiemToan"] as string,
                            DiemVan = reader["DiemVan"] as string,
                            DiemAnh = reader["DiemAnh"] as string,
                            DiemKhuyenKhich = reader["DiemKhuyenKhich"] as decimal?,
                            DiemUuTien = reader["DiemUuTien"] as decimal?,
                            DiemTong = reader["DiemTong"] as decimal?,
                            PhongThi = reader["PhongThi"] as string,
                            TrangThai = reader["TrangThai"] as string,
                            NgayDangKy = (DateTime)reader["NgayDangKy"],
                            GhiChu = reader["GhiChu"] as string,
                        });
                    }
                }
            }
            return ds;
        }

        // Thống kê điểm theo môn học

        public List<ThongKeDiemMon> ThongKeDiemTheoMon(string maTruong, string maDot)
        {
            var rs = new List<ThongKeDiemMon>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("sp_ThongKeDiemTheoMon", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaTruong", (object)maTruong ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaDot", (object)maDot ?? DBNull.Value);

                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        rs.Add(new ThongKeDiemMon
                        {
                            Mon = rd["Mon"] as string ?? "",
                            Muc = rd["Muc"] == DBNull.Value ? 0m : Convert.ToDecimal(rd["Muc"]),
                            SoLuong = rd["SoLuong"] == DBNull.Value ? 0 : Convert.ToInt32(rd["SoLuong"]),
                            BoThi = rd["BoThi"] != DBNull.Value && Convert.ToInt32(rd["BoThi"]) == 1
                        });
                    }
                }
            }
            return rs;
        }

        public List<ThongKeTHCSRow> ThongKeTheoTHCS(string maTruong, string maDot)
        {
            var rs = new List<ThongKeTHCSRow>();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand("sp_ThongKeTheoTHCS", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaTruong", maTruong ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MaDot", maDot ?? (object)DBNull.Value);
                    conn.Open();
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            var r = new ThongKeTHCSRow
                            {
                                TruongTHCS = rd["TruongTHCS"] as string,
                                Mon = rd["Mon"] as string,
                                TongTS = rd["TongTS"] as int? ?? 0,
                                TSNu = rd["TSNu"] as int? ?? 0,
                                DuThi = rd["DuThi"] as int? ?? 0,
                                BoThi = rd["BoThi"] as int? ?? 0,

                                M0_3 = rd["M0_3"] as int? ?? 0,
                                TyLe0_3 = rd["TyLe0_3"] as decimal? ?? 0,
                                M3_5 = rd["M3_5"] as int? ?? 0,
                                TyLe3_5 = rd["TyLe3_5"] as decimal? ?? 0,
                                M5_7 = rd["M5_7"] as int? ?? 0,
                                TyLe5_7 = rd["TyLe5_7"] as decimal? ?? 0,
                                M7_9 = rd["M7_9"] as int? ?? 0,
                                TyLe7_9 = rd["TyLe7_9"] as decimal? ?? 0,
                                M9_10 = rd["M9_10"] as int? ?? 0,
                                TyLe9_10 = rd["TyLe9_10"] as decimal? ?? 0,

                                Dau = rd["Dau"] as int? ?? 0,
                                TyLeDau = rd["TyLeDau"] as decimal? ?? 0,
                                Hong = rd["Hong"] as int? ?? 0,
                                TyLeHong = rd["TyLeHong"] as decimal? ?? 0,
                            };
                            rs.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ThongKeTheoTHCS error: " + ex);
            }
            return rs;
        }

        // Phần xử lý cho admin

        public List<TruongItem> Admin_LayDanhSachTruong()
        {
            var ds = new List<TruongItem>();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(
                    @"SELECT MaTruong, TenTruong 
              FROM TRUONG_HOC
              ORDER BY TenTruong", conn))
                {
                    conn.Open();
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            ds.Add(new TruongItem
                            {
                                MaTruong = rd.GetString(0),
                                TenTruong = rd.IsDBNull(1) ? null : rd.GetString(1)
                            });
                        }
                    }
                }
            }
            catch { /* log nếu cần */ }
            return ds;
        }

        public List<HocSinh> Admin_LayDanhSachHocSinhTheoTruongDot(string maTruong, string maDot)
        {
            var rs = new List<HocSinh>();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(
                    @"SELECT *
              FROM HOC_SINH
              WHERE (@MaTruong IS NULL OR MaTruong = @MaTruong)
                AND (@MaDot    IS NULL OR MaDot    = @MaDot)
              ORDER BY Ten COLLATE Vietnamese_CI_AI,
                       Ho  COLLATE Vietnamese_CI_AI", conn))
                {
                    cmd.Parameters.AddWithValue("@MaTruong", string.IsNullOrWhiteSpace(maTruong) ? (object)DBNull.Value : maTruong);
                    cmd.Parameters.AddWithValue("@MaDot", string.IsNullOrWhiteSpace(maDot) ? (object)DBNull.Value : maDot);

                    conn.Open();
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            rs.Add(new HocSinh
                            {
                                MaHocSinh = rd.GetInt32(rd.GetOrdinal("MaHocSinh")),
                                MaSoBaoDanh = rd["MaSoBaoDanh"] as string,
                                Ho = rd["Ho"] as string,
                                Ten = rd["Ten"] as string,
                                NgaySinh = rd.GetDateTime(rd.GetOrdinal("NgaySinh")),
                                GioiTinh = rd["GioiTinh"] as string,
                                DanToc = rd["DanToc"] as string,
                                NoiSinh = rd["NoiSinh"] as string,
                                TruongTHCS = rd["TruongTHCS"] as string,
                                MaTruong = rd["MaTruong"] as string,
                                MaDot = rd["MaDot"] as string,
                                DiemToan = rd["DiemToan"] as string,
                                DiemVan = rd["DiemVan"] as string,
                                DiemAnh = rd["DiemAnh"] as string,
                                DiemTong = rd["DiemTong"] as decimal?,
                                DiemKhuyenKhich = rd["DiemKhuyenKhich"] as decimal?,
                                DiemUuTien = rd["DiemUuTien"] as decimal?,
                                PhongThi = rd["PhongThi"] as string,
                                TrangThai = rd["TrangThai"] as string,
                                NgayDangKy = rd.GetDateTime(rd.GetOrdinal("NgayDangKy")),
                                GhiChu = rd["GhiChu"] as string
                            });
                        }
                    }
                }
            }
            catch { /* log nếu cần */ }
            return rs;
        }

        // Danh sách phòng thi theo trường/đợt (admin)
        public List<PhongThi> Admin_LayDanhSachPhongThi(string maTruong, string maDot)
        {
            var ds = new List<PhongThi>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(@"
        SELECT * FROM PHONG_THI
        WHERE (@MaTruong IS NULL OR MaTruong = @MaTruong)
          AND (@MaDot    IS NULL OR MaDot    = @MaDot)
        ORDER BY MaTruong, MaPhongThi", conn))
            {
                cmd.Parameters.AddWithValue("@MaTruong", (object)maTruong ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaDot", (object)maDot ?? DBNull.Value);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        ds.Add(new PhongThi
                        {
                            MaPhong = rd.GetInt32(rd.GetOrdinal("MaPhong")),
                            MaPhongThi = rd.GetString(rd.GetOrdinal("MaPhongThi")),
                            MaTruong = rd.GetString(rd.GetOrdinal("MaTruong")),
                            MaDot = rd["MaDot"] as string,
                            DiaDiem = rd["DiaDiem"] as string,
                            SoLuongToiDa = rd.GetInt32(rd.GetOrdinal("SoLuongToiDa")),
                            SoLuongHienTai = rd.GetInt32(rd.GetOrdinal("SoLuongHienTai")),
                            GiamThi1 = rd["GiamThi1"] as string,
                            GiamThi2 = rd["GiamThi2"] as string,
                            NgayThi = rd["NgayThi"] as DateTime?
                        });
                    }
                }
            }
            return ds;
        }

        // Học sinh theo phòng (admin)
        public List<HocSinh> Admin_LayDanhSachHocSinhTheoPhong(string maTruong, string maDot, string maPhongThi)
        {
            var ds = new List<HocSinh>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(@"
        SELECT * FROM HOC_SINH
        WHERE (@MaTruong IS NULL OR MaTruong = @MaTruong)
          AND (@MaDot    IS NULL OR MaDot    = @MaDot)
          AND (@PhongThi IS NULL OR PhongThi = @PhongThi)
        ORDER BY Ten COLLATE Vietnamese_CI_AI, Ho COLLATE Vietnamese_CI_AI", conn))
            {
                cmd.Parameters.AddWithValue("@MaTruong", (object)maTruong ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaDot", (object)maDot ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PhongThi", (object)maPhongThi ?? DBNull.Value);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        ds.Add(new HocSinh
                        {
                            MaHocSinh = rd.GetInt32(rd.GetOrdinal("MaHocSinh")),
                            MaSoBaoDanh = rd["MaSoBaoDanh"] as string,
                            Ho = rd["Ho"] as string,
                            Ten = rd["Ten"] as string,
                            NgaySinh = rd.GetDateTime(rd.GetOrdinal("NgaySinh")),
                            GioiTinh = rd["GioiTinh"] as string,
                            DanToc = rd["DanToc"] as string,
                            NoiSinh = rd["NoiSinh"] as string,
                            TruongTHCS = rd["TruongTHCS"] as string,
                            MaTruong = rd["MaTruong"] as string,
                            MaDot = rd["MaDot"] as string,
                            DiemToan = rd["DiemToan"] as string,
                            DiemVan = rd["DiemVan"] as string,
                            DiemAnh = rd["DiemAnh"] as string,
                            DiemKhuyenKhich = rd["DiemKhuyenKhich"] as decimal?,
                            DiemUuTien = rd["DiemUuTien"] as decimal?,
                            DiemTong = rd["DiemTong"] as decimal?,
                            PhongThi = rd["PhongThi"] as string,
                            TrangThai = rd["TrangThai"] as string,
                            NgayDangKy = rd.GetDateTime(rd.GetOrdinal("NgayDangKy")),
                            GhiChu = rd["GhiChu"] as string
                        });
                    }
                }
            }
            return ds;
        }

        // Cập nhật điểm cho admin (cũ: decimal?) — giữ nguyên
        public bool Admin_CapNhatDiemHocSinh(int maHocSinh,
            decimal? diemToan, decimal? diemVan, decimal? diemAnh,
            decimal? diemKhuyenKhich, decimal? diemUuTien)
        {
            return CapNhatDiemHocSinh(maHocSinh, diemToan, diemVan, diemAnh, diemKhuyenKhich, diemUuTien);
        }

        // ---------------- NEW: Overload đúng chữ ký interface cho Admin (chuỗi) ---------------
        public bool Admin_CapNhatDiemHocSinh(int maHocSinh,
            string diemToan, string diemVan, string diemAnh,
            decimal? diemKhuyenKhich, decimal? diemUuTien)
        {
            return CapNhatDiemHocSinhV2(maHocSinh, diemToan, diemVan, diemAnh, diemKhuyenKhich, diemUuTien);
        }
        // --------------------------------------------------------------------------------------


        // CRUD Cho tài khoản mật khẩu người dùng
        // Lấy danh sách người dùng theo trường (lọc nếu maTruong không null)
        public List<NguoiDung> LayDanhSachNguoiDungTheoTruong(string maTruong)
        {
            var ds = new List<NguoiDung>();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    string sql = @"
                SELECT MaNguoiDung, TenDangNhap, HoTen, MaTruong, VaiTro, MatKhau
                FROM NGUOI_DUNG
                WHERE (@MaTruong IS NULL OR MaTruong = @MaTruong)
                  AND VaiTro IN ('CanBoTruong', 'ThuKy')
                ORDER BY HoTen";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTruong",
                            string.IsNullOrEmpty(maTruong) ? (object)DBNull.Value : maTruong);
                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ds.Add(new NguoiDung
                                {
                                    MaNguoiDung = reader.GetInt32(0),
                                    TenDangNhap = reader.GetString(1),
                                    HoTen = reader.GetString(2),
                                    MaTruong = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    VaiTro = reader.GetString(4),
                                    MatKhau = reader.IsDBNull(5) ? null : reader.GetString(5)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi LayDanhSachNguoiDungTheoTruong: " + ex.Message);
            }
            return ds;
        }


        // Thêm người dùng mới (tài khoản trường)
        public bool ThemNguoiDung(NguoiDung nguoiDung, string matKhau)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    string sql = @"INSERT INTO NGUOI_DUNG (TenDangNhap, MatKhau, HoTen, MaTruong, VaiTro)
                                   VALUES (@TenDangNhap, @MatKhau, @HoTen, @MaTruong, @VaiTro)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", nguoiDung.TenDangNhap);
                        cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                        cmd.Parameters.AddWithValue("@HoTen", nguoiDung.HoTen);
                        cmd.Parameters.AddWithValue("@MaTruong", nguoiDung.MaTruong);
                        cmd.Parameters.AddWithValue("@VaiTro", nguoiDung.VaiTro ?? "CanBoTruong");  // Default CanBoTruong
                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi ThemNguoiDung: " + ex.Message);
                return false;
            }
        }

        // Cập nhật người dùng (có thể đổi mật khẩu nếu matKhauMoi không null)
        public bool CapNhatNguoiDung(NguoiDung nguoiDung, string matKhauMoi = null)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    string sql = @"UPDATE NGUOI_DUNG SET 
                                   TenDangNhap = @TenDangNhap, 
                                   HoTen = @HoTen, 
                                   MaTruong = @MaTruong, 
                                   VaiTro = @VaiTro";
                    if (!string.IsNullOrEmpty(matKhauMoi))
                    {
                        sql += ", MatKhau = @MatKhauMoi";
                    }
                    sql += " WHERE MaNguoiDung = @MaNguoiDung";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", nguoiDung.TenDangNhap);
                        cmd.Parameters.AddWithValue("@HoTen", nguoiDung.HoTen);
                        cmd.Parameters.AddWithValue("@MaTruong", nguoiDung.MaTruong);
                        cmd.Parameters.AddWithValue("@VaiTro", nguoiDung.VaiTro);
                        cmd.Parameters.AddWithValue("@MaNguoiDung", nguoiDung.MaNguoiDung);
                        if (!string.IsNullOrEmpty(matKhauMoi))
                        {
                            cmd.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);
                        }
                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi CapNhatNguoiDung: " + ex.Message);
                return false;
            }
        }

        // Xóa người dùng
        public bool XoaNguoiDung(int maNguoiDung)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    string sql = "DELETE FROM NGUOI_DUNG WHERE MaNguoiDung = @MaNguoiDung AND VaiTro IN ('CanBoTruong', 'ThuKy')";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi XoaNguoiDung: " + ex.Message);
                return false;
            }
        }

        public bool KiemTraTruongTonTai(string maTruong)
        {
            if (string.IsNullOrEmpty(maTruong)) return false;
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM TRUONG_HOC WHERE MaTruong = @MaTruong";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTruong", maTruong);
                        conn.Open();
                        return (int)cmd.ExecuteScalar() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool ThemTruong(TruongHoc truong)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO TRUONG_HOC (MaTruong, TenTruong, DiaChi, SoDienThoai, Email) VALUES (@MaTruong, @TenTruong, @DiaChi, @SoDienThoai, @Email)";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTruong", truong.MaTruong);
                        cmd.Parameters.AddWithValue("@TenTruong", truong.TenTruong);
                        cmd.Parameters.AddWithValue("@DiaChi", (object)truong.DiaChi ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SoDienThoai", (object)truong.SoDienThoai ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", (object)truong.Email ?? DBNull.Value);
                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi ThemTruong: " + ex.Message);
                return false;
            }
        }

        public string GetTenTruongFromMaTruong(string maTruong)
        {
            if (string.IsNullOrEmpty(maTruong)) return "Sở GD&ĐT Trà Vinh";
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT TenTruong FROM TRUONG_HOC WHERE MaTruong = @MaTruong";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTruong", maTruong);
                        conn.Open();
                        var result = cmd.ExecuteScalar();
                        return result?.ToString() ?? "Không xác định";
                    }
                }
            }
            catch
            {
                return "Không xác định";
            }
        }

        public List<TruongHoc> LayDanhSachTruong()
        {
            var ds = new List<TruongHoc>(); // Khởi tạo List thay vì mảng
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT MaTruong, TenTruong, DiaChi, SoDienThoai, Email FROM TRUONG_HOC";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ds.Add(new TruongHoc
                                {
                                    MaTruong = reader.IsDBNull(0) ? null : reader.GetString(0),
                                    TenTruong = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    DiaChi = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    SoDienThoai = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    Email = reader.IsDBNull(4) ? null : reader.GetString(4)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi LayDanhSachTruong: " + ex.Message);
            }
            return ds; // Trả về List
        }

        public bool CapNhatTruong(TruongHoc truong)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE TRUONG_HOC SET TenTruong = @TenTruong, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai, Email = @Email WHERE MaTruong = @MaTruong";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTruong", truong.MaTruong);
                        cmd.Parameters.AddWithValue("@TenTruong", truong.TenTruong);
                        cmd.Parameters.AddWithValue("@DiaChi", (object)truong.DiaChi ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SoDienThoai", (object)truong.SoDienThoai ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", (object)truong.Email ?? DBNull.Value);
                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi CapNhatTruong: " + ex.Message);
                return false;
            }
        }

        public bool XoaTruong(string maTruong)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM TRUONG_HOC WHERE MaTruong = @MaTruong";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTruong", maTruong);
                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi XoaTruong: " + ex.Message);
                return false;
            }
        }

        public VangThiResult ThongKeVangThi(string maDot, string maTruong)
        {
            var result = new VangThiResult
            {
                DanhSach = Array.Empty<VangThiHocSinhItem>(),
                TongHop = new VangThiTongHop()
            };

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(@"
-- DS học sinh có vắng ÍT NHẤT một môn
SELECT 
    hs.MaHocSinh,
    hs.MaSoBaoDanh,
    hs.Ho,
    hs.Ten,
    hs.TruongTHCS,
    hs.PhongThi,
    hs.MaTruong,
    VangToan = CASE WHEN dbo.fn_IsVang(hs.DiemToan)=1 THEN 1 ELSE 0 END,
    VangVan  = CASE WHEN dbo.fn_IsVang(hs.DiemVan )=1 THEN 1 ELSE 0 END,
    VangAnh  = CASE WHEN dbo.fn_IsVang(hs.DiemAnh )=1 THEN 1 ELSE 0 END,
    VangAny  = CASE WHEN 
                    dbo.fn_IsVang(hs.DiemToan)=1 
                 OR dbo.fn_IsVang(hs.DiemVan )=1 
                 OR dbo.fn_IsVang(hs.DiemAnh )=1 
               THEN 1 ELSE 0 END
FROM HOC_SINH hs
WHERE hs.MaDot = @MaDot
  AND (@MaTruong IS NULL OR hs.MaTruong = @MaTruong)
  AND (
        dbo.fn_IsVang(hs.DiemToan)=1
     OR dbo.fn_IsVang(hs.DiemVan )=1
     OR dbo.fn_IsVang(hs.DiemAnh )=1
  )
ORDER BY hs.MaTruong, hs.PhongThi, hs.MaSoBaoDanh;

-- Tổng hợp
SELECT
    TongVangAny = SUM(CASE WHEN 
                              dbo.fn_IsVang(hs.DiemToan)=1 
                           OR dbo.fn_IsVang(hs.DiemVan )=1 
                           OR dbo.fn_IsVang(hs.DiemAnh )=1 
                         THEN 1 ELSE 0 END),
    VangToan    = SUM(CASE WHEN dbo.fn_IsVang(hs.DiemToan)=1 THEN 1 ELSE 0 END),
    VangVan     = SUM(CASE WHEN dbo.fn_IsVang(hs.DiemVan )=1 THEN 1 ELSE 0 END),
    VangAnh     = SUM(CASE WHEN dbo.fn_IsVang(hs.DiemAnh )=1 THEN 1 ELSE 0 END)
FROM HOC_SINH hs
WHERE hs.MaDot = @MaDot
  AND (@MaTruong IS NULL OR hs.MaTruong = @MaTruong);
", conn))
                {
                    cmd.Parameters.AddWithValue("@MaDot", maDot);
                    cmd.Parameters.AddWithValue("@MaTruong", string.IsNullOrWhiteSpace(maTruong) ? (object)DBNull.Value : maTruong);

                    conn.Open();
                    using (var rd = cmd.ExecuteReader())
                    {
                        var list = new List<VangThiHocSinhItem>();
                        while (rd.Read())
                        {
                            list.Add(new VangThiHocSinhItem
                            {
                                MaHocSinh = Convert.ToInt32(rd["MaHocSinh"]),
                                MaSoBaoDanh = rd["MaSoBaoDanh"] as string,
                                Ho = rd["Ho"] as string,
                                Ten = rd["Ten"] as string,
                                TruongTHCS = rd["TruongTHCS"] as string,
                                PhongThi = rd["PhongThi"] as string,
                                MaTruong = rd["MaTruong"] as string,
                                VangToan = Convert.ToInt32(rd["VangToan"]) == 1,
                                VangVan = Convert.ToInt32(rd["VangVan"]) == 1,
                                VangAnh = Convert.ToInt32(rd["VangAnh"]) == 1,
                                VangAny = Convert.ToInt32(rd["VangAny"]) == 1
                            });
                        }
                        result.DanhSach = list.ToArray();

                        if (rd.NextResult() && rd.Read())
                        {
                            result.TongHop = new VangThiTongHop
                            {
                                TongVangAny = rd["TongVangAny"] == DBNull.Value ? 0 : Convert.ToInt32(rd["TongVangAny"]),
                                VangToan = rd["VangToan"] == DBNull.Value ? 0 : Convert.ToInt32(rd["VangToan"]),
                                VangVan = rd["VangVan"] == DBNull.Value ? 0 : Convert.ToInt32(rd["VangVan"]),
                                VangAnh = rd["VangAnh"] == DBNull.Value ? 0 : Convert.ToInt32(rd["VangAnh"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ThongKeVangThi error: " + ex);
            }

            return result;
        }



    }
}
