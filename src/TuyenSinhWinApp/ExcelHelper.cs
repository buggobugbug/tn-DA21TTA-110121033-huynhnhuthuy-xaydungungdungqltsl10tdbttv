using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using System.Threading.Tasks;

namespace TuyenSinhWinApp
{
    public static class ExcelHelper
    {
        public static void ReplacePlaceholder(this ExcelWorksheet worksheet, string placeholder, string value)
        {
            if (worksheet == null) return;

            foreach (var cell in worksheet.Cells)
            {
                if (cell?.Value != null && cell.Value.ToString().Contains(placeholder))
                {
                    cell.Value = cell.Value.ToString().Replace(placeholder, value);
                }
            }
        }
    }
}
