using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TuyenSinhWinApp
{
    public static class Guard
    {
        public static bool DemandEdit(Control owner = null)
        {
            if (Common.IsThuKy)
            {
                MessageBox.Show(owner ?? new Form(),
                    "Tài khoản Thư ký chỉ có quyền xem và xuất báo cáo.",
                    "Không được phép", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public static void DisableForThuKy(params Control[] controls)
        {
            if (!Common.IsThuKy) return;
            foreach (var c in controls)
            {
                if (c == null) continue;
                c.Enabled = false;
                // Nếu muốn giấu hẳn: c.Visible = false;
            }
        }

        public static void ReadOnlyGridForThuKy(DataGridView dgv)
        {
            if (!Common.IsThuKy || dgv == null) return;
            dgv.ReadOnly = true;
            dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
        }
    }
}
