using DevExpress.XtraBars.Docking2010.DragEngine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TuyenSinhServiceLib;
using TuyenSinhWinApp.TuyenSinhServiceReference;

namespace TuyenSinhWinApp
{
    public partial class frmThongKeTHCS : Form
    {
        private readonly Service1Client _svc = new Service1Client();
        private bool _columnsBuilt = false;

        public frmThongKeTHCS()
        {
            InitializeComponent();
            this.Load += frmThongKeTHCS_Load;
            cbDotTuyenSinh.SelectedIndexChanged += cbDot_SelectedIndexChanged;
            btnTaiLai.Click += (s, e) => LoadData();
        }

        private void LoadTruongForAdmin()
        {
            if (!Common.IsAdmin)
            {
                lblTruong.Visible = false;
                cbTruong.Visible = false;  
                return;
            }

            lblTruong.Visible = true;
            cbTruong.Visible = true;

            var ds = _svc.Admin_LayDanhSachTruong()?.ToList() ?? new List<TruongItem>();
       
            ds.Insert(0, new TruongItem { MaTruong = null, TenTruong = "— Tất cả trường —" });

            cbTruong.DisplayMember = "TenTruong";
            cbTruong.ValueMember = "MaTruong";
            cbTruong.DataSource = ds;

            cbTruong.SelectedIndexChanged -= CbTruong_SelectedIndexChanged;
            cbTruong.SelectedIndexChanged += CbTruong_SelectedIndexChanged;
        }



        #region UI setup
        private void ApplyGridTheme(DataGridView g)
        {
            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 96, 196);
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            g.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            g.BackgroundColor = Color.White;
            g.GridColor = Color.Gainsboro;
            g.RowHeadersVisible = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.MultiSelect = false;
            g.AllowUserToAddRows = false;
            g.AllowUserToResizeRows = false;
            g.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // zebra
            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);

            // giảm giật
            EnableDoubleBuffer(g);
        }

        private void EnableDoubleBuffer(DataGridView dgv)
        {
            try
            {
                var prop = typeof(DataGridView).GetProperty(
                    "DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                prop?.SetValue(dgv, true, null);
            }
            catch { /* ignore */ }
        }
        #endregion

        private void frmThongKeTHCS_Load(object sender, EventArgs e)
        {
            LoadDots();
            LoadTruongForAdmin();
            BuildColumns();   
            LoadData();       
        }

        private void LoadDots()
        {
            var ds = _svc.LayDanhSachDotTuyen()?.ToList() ?? new List<DotTuyenSinh>();
            cbDotTuyenSinh.DisplayMember = "TenDot";
            cbDotTuyenSinh.ValueMember = "MaDot";
            cbDotTuyenSinh.DataSource = ds;

            var maTruongFilter = GetMaTruongFilter();

            if (!string.IsNullOrEmpty(Common.MaDot))
            {
                var found = ds.FirstOrDefault(x => x.MaDot == Common.MaDot);
                if (found != null) cbDotTuyenSinh.SelectedValue = Common.MaDot;
            }
        }

        private void cbDot_SelectedIndexChanged(object sender, EventArgs e)
        {
            Common.MaDot = cbDotTuyenSinh.SelectedValue?.ToString(); 
            LoadData();
        }

        private void LoadData()
        {
            var maDot = cbDotTuyenSinh.SelectedValue?.ToString() ?? Common.MaDot ?? "";
            var maTruongFilter = GetMaTruongFilter();
            var data = _svc.ThongKeTheoTHCS(maTruongFilter, maDot)
              ?? Array.Empty<ThongKeTHCSRow>();
            var modifiedData = data.Select(row => new ThongKeTHCSRow
            {
                TruongTHCS = row.TruongTHCS,
                Mon = row.Mon == "Anh" ? "Môn 3" : row.Mon,
                TongTS = row.TongTS,
                TSNu = row.TSNu,
                DuThi = row.DuThi,
                BoThi = row.BoThi,
                M0_3 = row.M0_3,
                TyLe0_3 = row.TyLe0_3,
                M3_5 = row.M3_5,
                TyLe3_5 = row.TyLe3_5,
                M5_7 = row.M5_7,
                TyLe5_7 = row.TyLe5_7,
                M7_9 = row.M7_9,
                TyLe7_9 = row.TyLe7_9,
                M9_10 = row.M9_10,
                TyLe9_10 = row.TyLe9_10,
                Dau = row.Dau,
                TyLeDau = row.TyLeDau,
                Hong = row.Hong,
                TyLeHong = row.TyLeHong
            }).ToArray();

            dgvTHCS.DataSource = modifiedData;
            int tongTS = data.GroupBy(x => x.TruongTHCS).Select(g => g.First().TongTS).Sum();
            int tongDau = data.GroupBy(x => x.TruongTHCS).Select(g => g.First().Dau).Sum();
            int tongHong = data.GroupBy(x => x.TruongTHCS).Select(g => g.First().Hong).Sum();

            var tb = new[]
            {
                new {
                    Ten = "TB",
                    TongTS = tongTS,
                    Dau = tongDau,
                    Hong = tongHong,
                    TyLeDau  = tongTS == 0 ? 0m : Math.Round((decimal)tongDau * 100m / tongTS, 2),
                    TyLeHong = tongTS == 0 ? 0m : Math.Round((decimal)tongHong * 100m / tongTS, 2),
                }
            };
            dgvTongHop.DataSource = tb.ToList();
        }

        private void BuildColumns()
        {
            if (_columnsBuilt) return;

          
            var g = dgvTHCS;
            g.AutoGenerateColumns = false;
            g.Columns.Clear();

            g.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TruongTHCS",
                HeaderText = "Trường THCS",
                Width = 220
            });
            g.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Mon",
                HeaderText = "Môn",
                Width = 70
            });
            g.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TongTS",
                HeaderText = "Tổng TS"
            });
            g.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TSNu",
                HeaderText = "TS Nữ"
            });
            g.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DuThi",
                HeaderText = "Dự thi"
            });
            g.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BoThi",
                HeaderText = "Bỏ thi"
            });

            void addPair(string slName, string tlName, string header)
            {
                g.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = slName,
                    HeaderText = $"{header}\nSL",
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter
                    }
                });
                g.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = tlName,
                    HeaderText = $"{header}\nTỉ lệ (%)",
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleRight,
                        Format = "N2"
                    }
                });
            }

            addPair("M0_3", "TyLe0_3", "0–<3");
            addPair("M3_5", "TyLe3_5", "3–<5");
            addPair("M5_7", "TyLe5_7", "5–<7");
            addPair("M7_9", "TyLe7_9", "7–<9");
            addPair("M9_10", "TyLe9_10", "9–10");
            addPair("Dau", "TyLeDau", "KQ đậu");
            addPair("Hong", "TyLeHong", "Hỏng");

            // căn lề mặc định
            g.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            g.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // cột Trường THCS
            var colTruong = new DataGridViewTextBoxColumn
            {
                Name = "TruongTHCS",           // <-- THÊM Name
                DataPropertyName = "TruongTHCS",
                HeaderText = "Trường THCS",
                Width = 220,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            };
            g.Columns.Add(colTruong);

            // cột Môn
            var colMon = new DataGridViewTextBoxColumn
            {
                Name = "Mon",                  // <-- THÊM Name
                DataPropertyName = "Mon",
                HeaderText = "Môn",
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
            g.Columns.Add(colMon);


            ApplyGridTheme(g);
            // freeze cột đầu để kéo ngang vẫn thấy tên trường
            g.Columns[0].Frozen = true;

            // TONG HOP grid
            var h = dgvTongHop;
            h.AutoGenerateColumns = false;
            h.Columns.Clear();
            h.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Ten", HeaderText = "CỘNG/TB", Width = 120 });
            h.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TongTS", HeaderText = "Tổng TS" });
            h.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Dau", HeaderText = "Tổng đậu" });
            h.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Hong", HeaderText = "Tổng hỏng" });
            h.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TyLeDau", HeaderText = "Tỉ lệ đậu (%)", DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
            h.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TyLeHong", HeaderText = "Tỉ lệ hỏng (%)", DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });

            ApplyGridTheme(h);

            _columnsBuilt = true;
        }


        private void CbTruong_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private string GetMaTruongFilter()
        {
            if (Common.IsAdmin)
                return cbTruong.SelectedValue as string; // có thể null (tức toàn tỉnh)
            return Common.MaTruong; // cán bộ trường / thư ký
        }
    }
}
