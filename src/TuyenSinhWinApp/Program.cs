using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuyenSinhServiceLib;


namespace TuyenSinhWinApp
{
    internal static class Program
    {


        // Tạo biến toàn cục để giữ host
        public static ServiceHost serviceHost = null;

        [STAThread]
        static void Main()
        {
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Khởi động service trước khi mở form
            try
            {
                serviceHost = new ServiceHost(typeof(Service1),
                    new Uri("http://localhost:8732/TuyenSinhService"));

                serviceHost.AddServiceEndpoint(typeof(IService1), new BasicHttpBinding(), "");
                serviceHost.Open(); // Bắt đầu host

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmDangNhap()); // Mở form đăng nhập
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể khởi động service: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Application.Run(new frmMain());

        }
    }
}
