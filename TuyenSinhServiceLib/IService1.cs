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
        List<HocSinh> LayDanhSachHocSinh(string maTruong);

        [OperationContract]
        bool CapNhatHocSinh(HocSinh hocSinh);

        [OperationContract]
        bool XoaHocSinh(int maHocSinh);

        [OperationContract]
        bool ChiaPhongThi(string maTruong);

        [OperationContract]
        List<PhongThi> LayDanhSachPhongThi(string maTruong);

        [OperationContract]
        List<HocSinh> LayDanhSachHocSinhTheoPhong(string maPhongThi);

        [OperationContract]
        List<DotTuyenSinh> LayDanhSachDotTuyen();

        [OperationContract]
        bool ThemDotTuyen(DotTuyenSinh dot);

        [OperationContract]
        bool DongDotTuyen(string maDot);
    }

}
