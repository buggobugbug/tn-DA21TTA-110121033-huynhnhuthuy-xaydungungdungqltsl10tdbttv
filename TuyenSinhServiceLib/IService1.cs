using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TuyenSinhServiceLib
{

    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        KetQuaDangNhap DangNhap(string tenDangNhap, string matKhau);

        [OperationContract]
        KetQuaThemHocSinh ThemHocSinh(HocSinh hocSinh);

        [OperationContract]
        List<HocSinh> LayDanhSachHocSinh(string maTruong, string maDot);

        [OperationContract]
        void GanMaSoBaoDanhHangLoat(string maTruong, string maDot);

        [OperationContract]
        bool CapNhatHocSinh(HocSinh hocSinh);

        [OperationContract]
        bool CapNhatDiemHocSinh(int maHocSinh,
        string diemToan, string diemVan, string diemAnh,
        decimal? diemKhuyenKhich, decimal? diemUuTien);

        [OperationContract]
        bool XoaHocSinh(int maHocSinh);

        [OperationContract]
        bool ChiaPhongThi(string maTruong, string maDot);

        [OperationContract]
        List<PhongThi> LayDanhSachPhongThi(string maTruong, string maDot);

        [OperationContract]
        List<HocSinh> LayDanhSachHocSinhTheoPhong(string maTruong, string maDot, string maPhongThi);

        [OperationContract]
        List<DotTuyenSinh> LayDanhSachDotTuyen();

        [OperationContract]
        bool ThemDotTuyen(DotTuyenSinh dot);

        [OperationContract]
        bool DongDotTuyen(string maDot);

        [OperationContract]
        bool CapNhatDotTuyen(DotTuyenSinh dot);

        [OperationContract]
        void CapNhatGiamThi(string maPhong, string giamThi1, string giamThi2);

        [OperationContract]
        bool CapNhatDiemTheoMon(int maHocSinh, string tenMon, string diemText);

        [OperationContract]
        ChiTieuTuyenSinh LayChiTieuTruong(string maTruong, string maDot);

        [OperationContract]
        bool CapNhatChiTieu(string maTruong, string maDot, int chiTieu);

        [OperationContract]
        bool XetTrungTuyen(string maTruong, string maDot);

        [OperationContract]
        List<HocSinh> LayDanhSachTrungTuyen(string maTruong, string maDot);

        [OperationContract]
        List<ThongKeDiemMon> ThongKeDiemTheoMon(string maTruong, string maDot);

        [OperationContract]
        List<ThongKeTHCSRow> ThongKeTheoTHCS(string maTruong, string maDot);

        [OperationContract]
        VangThiResult ThongKeVangThi(string maDot, string maTruong);



        // Phần cho admin
        [OperationContract]
        List<TruongItem> Admin_LayDanhSachTruong();

        [OperationContract]
        List<HocSinh> Admin_LayDanhSachHocSinhTheoTruongDot(string maTruong, string maDot);

        [OperationContract]
        List<HocSinh> Admin_LayDanhSachHocSinhTheoPhong(string maTruong, string maDot, string maPhongThi);

        [OperationContract]
        bool Admin_CapNhatDiemHocSinh(int maHocSinh,
        string diemToan, string diemVan, string diemAnh,
        decimal? diemKhuyenKhich, decimal? diemUuTien);

        [OperationContract]
        List<PhongThi> Admin_LayDanhSachPhongThi(string maTruong, string maDot);

        // CRUD Tài khoản người dùng
        [OperationContract]
        List<NguoiDung> LayDanhSachNguoiDungTheoTruong(string maTruong);  

        [OperationContract]
        bool ThemNguoiDung(NguoiDung nguoiDung, string matKhau);

        [OperationContract]
        bool CapNhatNguoiDung(NguoiDung nguoiDung, string matKhauMoi = null); 

        [OperationContract]
        bool XoaNguoiDung(int maNguoiDung);

        [OperationContract]
        bool KiemTraTruongTonTai(string maTruong);

        [OperationContract]
        bool ThemTruong(TruongHoc truong);

        [OperationContract]
        string GetTenTruongFromMaTruong(string maTruong);

        // Nếu cần thêm CRUD đầy đủ cho trường
        [OperationContract]
        List<TruongHoc> LayDanhSachTruong();

        [OperationContract]
        bool CapNhatTruong(TruongHoc truong);

        [OperationContract]
        bool XoaTruong(string maTruong);


    }

}
