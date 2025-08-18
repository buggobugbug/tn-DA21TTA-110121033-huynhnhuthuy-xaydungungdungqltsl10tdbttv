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
        bool CapNhatDiemHocSinh(int maHocSinh, decimal? diemToan, decimal? diemVan, decimal? diemAnh, decimal? diemKhuyenKhich, decimal? diemUuTien);


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
        bool CapNhatDiemTheoMon(int maHocSinh, string tenMon, decimal? diem);

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


    }

}
