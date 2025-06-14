using System.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;


namespace TuyenSinhServiceLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TuyenSinhDB"].ConnectionString;

        public KetQuaDangNhap DangNhap(string tenDangNhap, string matKhau)
        {
            var ketQua = new KetQuaDangNhap();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT MaNguoiDung, HoTen, MaTruong FROM NGUOI_DUNG " +
                        "WHERE TenDangNhap=@TenDangNhap AND MatKhau=@MatKhau",
                        conn);

                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        ketQua.ThanhCong = true;
                        ketQua.ThongBao = "Đăng nhập thành công";
                        ketQua.NguoiDung = new NguoiDung
                        {
                            MaNguoiDung = reader.GetInt32(0),
                            HoTen = reader.GetString(1),
                            MaTruong = reader.GetString(2)
                        };
                    }
                    else
                    {
                        ketQua.ThanhCong = false;
                        ketQua.ThongBao = "Sai tên đăng nhập hoặc mật khẩu";
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
                    SqlCommand cmd = new SqlCommand("sp_ThemHocSinh", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tất cả các tham số theo stored procedure đã cập nhật
                    cmd.Parameters.AddWithValue("@HoTen", hocSinh.HoTen);
                    cmd.Parameters.AddWithValue("@NgaySinh", hocSinh.NgaySinh);
                    cmd.Parameters.AddWithValue("@GioiTinh", hocSinh.GioiTinh);
                    cmd.Parameters.AddWithValue("@MaTruong", hocSinh.MaTruong);
                    cmd.Parameters.AddWithValue("@MaDot", string.IsNullOrWhiteSpace(hocSinh.MaDot) ? (object)DBNull.Value : hocSinh.MaDot);
                    cmd.Parameters.AddWithValue("@DanToc", string.IsNullOrWhiteSpace(hocSinh.DanToc) ? (object)DBNull.Value : hocSinh.DanToc);
                    cmd.Parameters.AddWithValue("@NoiSinh", string.IsNullOrWhiteSpace(hocSinh.NoiSinh) ? (object)DBNull.Value : hocSinh.NoiSinh);
                    cmd.Parameters.AddWithValue("@TruongTHCS", string.IsNullOrWhiteSpace(hocSinh.TruongTHCS) ? (object)DBNull.Value : hocSinh.TruongTHCS);
                    cmd.Parameters.AddWithValue("@DiemToan", hocSinh.DiemToan.HasValue ? (object)hocSinh.DiemToan.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemVan", hocSinh.DiemVan.HasValue ? (object)hocSinh.DiemVan.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemAnh", hocSinh.DiemAnh.HasValue ? (object)hocSinh.DiemAnh.Value : DBNull.Value);
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
                            // Đọc toàn bộ thông tin học sinh từ kết quả trả về
                            ketQua.HocSinh = new HocSinh
                            {
                                MaHocSinh = reader.GetInt32(reader.GetOrdinal("MaHocSinh")),
                                MaSoBaoDanh = reader.GetString(reader.GetOrdinal("MaSoBaoDanh")),
                                HoTen = reader.GetString(reader.GetOrdinal("HoTen")),
                                NgaySinh = reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                                GioiTinh = reader.GetString(reader.GetOrdinal("GioiTinh")),
                                DanToc = reader.IsDBNull(reader.GetOrdinal("DanToc")) ? null : reader.GetString(reader.GetOrdinal("DanToc")),
                                NoiSinh = reader.IsDBNull(reader.GetOrdinal("NoiSinh")) ? null : reader.GetString(reader.GetOrdinal("NoiSinh")),
                                TruongTHCS = reader.IsDBNull(reader.GetOrdinal("TruongTHCS")) ? null : reader.GetString(reader.GetOrdinal("TruongTHCS")),
                                MaTruong = reader.GetString(reader.GetOrdinal("MaTruong")),
                                MaDot = reader.IsDBNull(reader.GetOrdinal("MaDot")) ? null : reader.GetString(reader.GetOrdinal("MaDot")), // đọc MaDot
                                DiemToan = reader.IsDBNull(reader.GetOrdinal("DiemToan")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("DiemToan")),
                                DiemVan = reader.IsDBNull(reader.GetOrdinal("DiemVan")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("DiemVan")),
                                DiemAnh = reader.IsDBNull(reader.GetOrdinal("DiemAnh")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("DiemAnh")),
                                DiemTong = reader.IsDBNull(reader.GetOrdinal("DiemTong")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("DiemTong")),
                                DiemKhuyenKhich = reader.IsDBNull(reader.GetOrdinal("DiemKhuyenKhich")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("DiemKhuyenKhich")),
                                DiemUuTien = reader.IsDBNull(reader.GetOrdinal("DiemUuTien")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("DiemUuTien")),
                                PhongThi = reader.IsDBNull(reader.GetOrdinal("PhongThi")) ? null : reader.GetString(reader.GetOrdinal("PhongThi")),
                                TrangThai = reader.GetString(reader.GetOrdinal("TrangThai")),
                                NgayDangKy = reader.GetDateTime(reader.GetOrdinal("NgayDangKy")),
                                GhiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? null : reader.GetString(reader.GetOrdinal("GhiChu"))

                            };

                            ketQua.MaSoBaoDanh = ketQua.HocSinh.MaSoBaoDanh;
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

        public List<HocSinh> LayDanhSachHocSinh(string maTruong)
        {
            var danhSach = new List<HocSinh>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT * FROM HOC_SINH WHERE MaTruong = @MaTruong ORDER BY NgayDangKy DESC", conn);
                    cmd.Parameters.AddWithValue("@MaTruong", maTruong);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var hs = new HocSinh
                            {
                                MaHocSinh = reader.GetInt32(reader.GetOrdinal("MaHocSinh")),
                                MaSoBaoDanh = reader.GetString(reader.GetOrdinal("MaSoBaoDanh")),
                                HoTen = reader.GetString(reader.GetOrdinal("HoTen")),
                                NgaySinh = reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                                GioiTinh = reader.GetString(reader.GetOrdinal("GioiTinh")),
                                DanToc = reader["DanToc"] as string,
                                NoiSinh = reader["NoiSinh"] as string,
                                TruongTHCS = reader["TruongTHCS"] as string,
                                MaTruong = reader.GetString(reader.GetOrdinal("MaTruong")),
                                MaDot = reader.IsDBNull(reader.GetOrdinal("MaDot")) ? null : reader.GetString(reader.GetOrdinal("MaDot")),
                                DiemToan = reader["DiemToan"] as decimal?,
                                DiemVan = reader["DiemVan"] as decimal?,
                                DiemAnh = reader["DiemAnh"] as decimal?,
                                DiemTong = reader["DiemTong"] as decimal?,
                                DiemKhuyenKhich = reader["DiemKhuyenKhich"] as decimal?,
                                DiemUuTien = reader["DiemUuTien"] as decimal?,
                                PhongThi = reader["PhongThi"] as string,
                                TrangThai = reader["TrangThai"] as string,
                                NgayDangKy = reader.GetDateTime(reader.GetOrdinal("NgayDangKy")),
                                GhiChu = reader["GhiChu"] as string
                            };

                            danhSach.Add(hs);
                        }
                    }
                }
            }
            catch
            {
                // log hoặc xử lý lỗi nếu cần
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
                    string sql = @"UPDATE HOC_SINH SET 
                HoTen = @HoTen,
                NgaySinh = @NgaySinh,
                GioiTinh = @GioiTinh,
                DanToc = @DanToc,
                NoiSinh = @NoiSinh,
                TruongTHCS = @TruongTHCS,
                DiemToan = @DiemToan,
                DiemVan = @DiemVan,
                DiemAnh = @DiemAnh,
                DiemKhuyenKhich = @DiemKhuyenKhich,
                DiemUuTien = @DiemUuTien,
                DiemTong = @DiemTong,
                PhongThi = @PhongThi,
                TrangThai = @TrangThai,
                GhiChu = @GhiChu,
                MaDot = @MaDot
            WHERE MaHocSinh = @MaHocSinh";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    decimal? tong = null;
                    if (hs.DiemToan.HasValue && hs.DiemVan.HasValue && hs.DiemAnh.HasValue)
                    {
                        tong = hs.DiemToan.Value + hs.DiemVan.Value + hs.DiemAnh.Value +
                               (hs.DiemKhuyenKhich ?? 0) + (hs.DiemUuTien ?? 0);
                    }

                    cmd.Parameters.AddWithValue("@HoTen", hs.HoTen);
                    cmd.Parameters.AddWithValue("@NgaySinh", hs.NgaySinh);
                    cmd.Parameters.AddWithValue("@GioiTinh", hs.GioiTinh);
                    cmd.Parameters.AddWithValue("@DanToc", (object)hs.DanToc ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NoiSinh", (object)hs.NoiSinh ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TruongTHCS", (object)hs.TruongTHCS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemToan", (object)hs.DiemToan ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemVan", (object)hs.DiemVan ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemAnh", (object)hs.DiemAnh ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemKhuyenKhich", (object)hs.DiemKhuyenKhich ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemUuTien", (object)hs.DiemUuTien ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiemTong", (object)tong ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PhongThi", (object)hs.PhongThi ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrangThai", hs.TrangThai);
                    cmd.Parameters.AddWithValue("@GhiChu", (object)hs.GhiChu ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MaDot", string.IsNullOrWhiteSpace(hs.MaDot) ? (object)DBNull.Value : hs.MaDot);
                    cmd.Parameters.AddWithValue("@MaHocSinh", hs.MaHocSinh);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật: " + ex.Message); // hoặc log vào file
                return false;
            }
        }


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

        public bool ChiaPhongThi(string maTruong)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_ChiaPhongThi", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaTruong", maTruong);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi chia phòng: " + ex.Message);
                throw; // hoặc log, hoặc return false
            }
        }


        // Lấy danh sách phòng thi
        public List<PhongThi> LayDanhSachPhongThi(string maTruong)
        {
            var danhSach = new List<PhongThi>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM PHONG_THI WHERE MaTruong = @MaTruong", conn);
                cmd.Parameters.AddWithValue("@MaTruong", maTruong);

                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var phong = new PhongThi
                    {
                        MaPhong = reader.GetInt32(reader.GetOrdinal("MaPhong")),
                        MaPhongThi = reader.GetString(reader.GetOrdinal("MaPhongThi")),
                        MaTruong = reader.GetString(reader.GetOrdinal("MaTruong")),
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


        // Lấy danh sách học sinh theo phòng thi
        public List<HocSinh> LayDanhSachHocSinhTheoPhong(string maPhongThi)
        {
            var danhSach = new List<HocSinh>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM HOC_SINH WHERE PhongThi = @PhongThi ORDER BY HoTen COLLATE Vietnamese_CI_AS", conn);
                cmd.Parameters.AddWithValue("@PhongThi", maPhongThi);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var hs = new HocSinh
                        {
                            MaHocSinh = reader.GetInt32(reader.GetOrdinal("MaHocSinh")),
                            MaSoBaoDanh = reader.GetString(reader.GetOrdinal("MaSoBaoDanh")),
                            HoTen = reader.GetString(reader.GetOrdinal("HoTen")),
                            NgaySinh = reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                            GioiTinh = reader["GioiTinh"] as string,
                            DanToc = reader["DanToc"] as string,
                            NoiSinh = reader["NoiSinh"] as string,
                            TruongTHCS = reader["TruongTHCS"] as string,
                            MaTruong = reader["MaTruong"] as string,
                            DiemToan = reader["DiemToan"] as decimal?,
                            DiemVan = reader["DiemVan"] as decimal?,
                            DiemAnh = reader["DiemAnh"] as decimal?,
                            DiemTong = reader["DiemTong"] as decimal?,
                            DiemKhuyenKhich = reader["DiemKhuyenKhich"] as decimal?,
                            DiemUuTien = reader["DiemUuTien"] as decimal?,
                            PhongThi = reader["PhongThi"] as string,
                            TrangThai = reader["TrangThai"] as string,
                            GhiChu = reader["GhiChu"] as string,
                            NgayDangKy = reader.GetDateTime(reader.GetOrdinal("NgayDangKy"))
                        };

                        danhSach.Add(hs);
                    }
                }
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




    }
}
