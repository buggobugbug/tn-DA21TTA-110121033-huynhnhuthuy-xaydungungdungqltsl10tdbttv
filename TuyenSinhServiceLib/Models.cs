using System;
using System.Runtime.Serialization;

namespace TuyenSinhServiceLib
{
    [DataContract]
    public class NguoiDung
    {
        [DataMember] public int MaNguoiDung { get; set; }
        [DataMember] public string TenDangNhap { get; set; }
        [DataMember] public string HoTen { get; set; }
        [DataMember] public string MaTruong { get; set; }
    }

    [DataContract]
    public class KetQuaDangNhap
    {
        [DataMember] public bool ThanhCong { get; set; }
        [DataMember] public string ThongBao { get; set; }
        [DataMember] public NguoiDung NguoiDung { get; set; }
    }

    [DataContract]
    public class HocSinh
    {
        [DataMember] public int MaHocSinh { get; set; }
        [DataMember] public string MaSoBaoDanh { get; set; }
        [DataMember] public string Ho { get; set; }
        [DataMember] public string Ten { get; set; }
        [DataMember] public DateTime NgaySinh { get; set; }
        [DataMember] public string GioiTinh { get; set; }
        [DataMember] public string DanToc { get; set; }
        [DataMember] public string NoiSinh { get; set; }
        [DataMember] public string TruongTHCS { get; set; }
        [DataMember] public string MaTruong { get; set; }
        [DataMember] public string MaDot { get; set; }
        [DataMember] public decimal? DiemToan { get; set; }
        [DataMember] public decimal? DiemVan { get; set; }
        [DataMember] public decimal? DiemAnh { get; set; }
        [DataMember] public decimal? DiemTong { get; set; }
        [DataMember] public decimal? DiemKhuyenKhich { get; set; }
        [DataMember] public decimal? DiemUuTien { get; set; }

        // Thông tin phòng thi
        [DataMember] public string PhongThi { get; set; }

        // Trạng thái
        [DataMember] public string TrangThai { get; set; }

        // Thông tin thời gian
        [DataMember] public DateTime NgayDangKy { get; set; }

        // Ghi chú
        [DataMember] public string GhiChu { get; set; }
    }

    [DataContract]
    public class KetQuaThemHocSinh
    {
        [DataMember] public bool ThanhCong { get; set; }
        [DataMember] public string ThongBao { get; set; }
        [DataMember] public HocSinh HocSinh { get; set; }
        [DataMember] public string MaSoBaoDanh { get; set; }
    }

    [DataContract]
    public class PhongThi
    {
        [DataMember] public int MaPhong { get; set; }
        [DataMember] public string MaPhongThi { get; set; }
        [DataMember] public string MaTruong { get; set; }
        [DataMember] public string MaDot { get; set; }
        [DataMember] public string DiaDiem { get; set; }
        [DataMember] public int SoLuongToiDa { get; set; }
        [DataMember] public int SoLuongHienTai { get; set; }
        [DataMember] public string GiamThi1 { get; set; }
        [DataMember] public string GiamThi2 { get; set; }
        [DataMember] public DateTime? NgayThi { get; set; }
    }

    [DataContract]
    public class DotTuyenSinh
    {
        [DataMember] public string MaDot { get; set; }
        [DataMember] public string TenDot { get; set; }
        [DataMember] public int Nam { get; set; }
        [DataMember] public DateTime? NgayBatDau { get; set; }
        [DataMember] public DateTime? NgayKetThuc { get; set; }
        [DataMember] public string TrangThai { get; set; }
    }

    [DataContract]
    public class ChiTieuTuyenSinh
    {
        [DataMember]
        public string MaTruong { get; set; }
        [DataMember]
        public string MaDot { get; set; }
        [DataMember]
        public int ChiTieu { get; set; }
    }

    [DataContract]
    public class ThongKeDiemMon
    {
        [DataMember] public string Mon { get; set; }     
        [DataMember] public decimal Muc { get; set; }    
        [DataMember] public int SoLuong { get; set; }
        [DataMember] public bool BoThi { get; set; }
    }

    [DataContract]
    public class ThongKeTHCSRow
    {
        [DataMember] public string TruongTHCS { get; set; }
        [DataMember] public string Mon { get; set; }

        [DataMember] public int TongTS { get; set; }
        [DataMember] public int TSNu { get; set; }
        [DataMember] public int DuThi { get; set; }
        [DataMember] public int BoThi { get; set; }

        [DataMember] public int M0_3 { get; set; }
        [DataMember] public decimal TyLe0_3 { get; set; }
        [DataMember] public int M3_5 { get; set; }
        [DataMember] public decimal TyLe3_5 { get; set; }
        [DataMember] public int M5_7 { get; set; }
        [DataMember] public decimal TyLe5_7 { get; set; }
        [DataMember] public int M7_9 { get; set; }
        [DataMember] public decimal TyLe7_9 { get; set; }
        [DataMember] public int M9_10 { get; set; }
        [DataMember] public decimal TyLe9_10 { get; set; }

        [DataMember] public int Dau { get; set; }
        [DataMember] public decimal TyLeDau { get; set; }
        [DataMember] public int Hong { get; set; }
        [DataMember] public decimal TyLeHong { get; set; }
    }


}

