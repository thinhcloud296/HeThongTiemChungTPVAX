using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPVAXWinform_BLL;

namespace TPVAXWinform_GUI.UserControls
{
    public partial class KhachHangControl : UserControl
    {
        private DataTable dtRecords;
        private KhachHangBLL _bll = new KhachHangBLL();
        public KhachHangControl()
        {
            InitializeComponent();
            LoadDSKHHG();
        }
        private void BindDataToGrid(DataTable dt)
        {
            dgvKhachHang.AutoGenerateColumns = false;

            colMaKH.DataPropertyName = "MaKH";
            colHoTen.DataPropertyName = "HoTen";
            colGioiTinh.DataPropertyName = "GioiTinh";
            colNgaySinh.DataPropertyName = "NgaySinh";
            colSoDT.DataPropertyName = "SoDT";

            dgvKhachHang.DataSource = dt;
            dgvKhachHang.Columns["colNgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvKhachHang.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            dgvKhachHang.RowTemplate.Height = 36;
        }
        private void LoadDSKHHG()
        {
            // Tạo DataTable với dữ liệu mẫu
            dtRecords = new DataTable();
            dtRecords = _bll.GetData();

            BindDataToGrid(dtRecords);
        }
    }
}
