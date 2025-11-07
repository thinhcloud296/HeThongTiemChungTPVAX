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

namespace TPVAXWinform_GUI
{
    public partial class frmThemHSTC : Form
    {
        KhachHangBLL khachHangBLL = new KhachHangBLL();
        HoSoTiemChungBLL hoSoTiemChungBLL = new HoSoTiemChungBLL();
        string[] quanHeOptions = {
        "Bản thân", "Cha", "Mẹ", "Con",
        "Anh ruột", "Chị ruột", "Em ruột",
        "Ông nội", "Bà nội", "Ông ngoại", "Bà ngoại",
        "Vợ", "Chồng",
        "Người giám hộ", "Người chăm sóc", "Đại diện theo pháp luật",
        "Khác"
        };
        string[] gioiTinhOptions = { "Nam", "Nữ", "Khác" };
        public frmThemHSTC()
        {
            InitializeComponent();
            cboQuanHe.DataSource = quanHeOptions;
            cboGioiTinhHSTC.DataSource = gioiTinhOptions;
            cboGioiTinhHSTC.DataSource = gioiTinhOptions;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            DataTable dt = khachHangBLL.GetData();
            if (dt.PrimaryKey == null || dt.PrimaryKey.Length == 0)
                dt.PrimaryKey = new[] { dt.Columns["CCCD"] };
            string cccd = txtTimCCCD.Text.Trim();
            DataRow dr = dt.Rows.Find(cccd);
            if (dr == null)
            {
                MessageBox.Show("Không tìm thấy khách hàng với CCCD đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<String> DSHSTCLienKet = new List<String>();
            string makh = dr["MaKH"]?.ToString() ?? "";
            DataRow[] drDSHSTCLienKet = hoSoTiemChungBLL.GetHSTC_QuanHe_KH(makh).Select();
            foreach (DataRow row in drDSHSTCLienKet)
            {
                string HoTenKH = row["HoTenKH"]?.ToString() ?? "";
                string HoTenHS = row["HoTenHS"]?.ToString() ?? "";
                string quanHe = row["VaiTro"]?.ToString() ?? "Khác";
                
                string tmp = $"HS: {HoTenHS} - KH: {HoTenKH} - ({quanHe})";
                if (quanHe == "Bản thân")
                    tmp = $"{HoTenKH} - {quanHe}";
                DSHSTCLienKet.Add(tmp);
            }
            cboDSHSTCLienKet.DataSource = null;
            if (DSHSTCLienKet.Count > 0)
            {
                cboDSHSTCLienKet.DataSource = DSHSTCLienKet;    
            }

            txtHoTenKH.Text = dr["HoTen"]?.ToString() ?? "";
            txtDiaChi.Text = dr["DiaChi"]?.ToString() ?? "";
            txtSoDT.Text = dr["SoDT"]?.ToString() ?? "";
            dtpNgaySinhKH.Value = dr["NgaySinh"] is DateTime dte ? dte : DateTime.Now;
            txtEmail.Text = dr["Email"]?.ToString() ?? "";
            txtCCCDKH.Text = dr["CCCD"]?.ToString() ?? "";
            btnThemKhachHang.Visible = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtTimCCCD.Clear();
            txtHoTenKH.Clear();
            txtDiaChi.Clear();
            txtSoDT.Clear();
            txtEmail.Clear();
            txtCCCDKH.Clear();
            btnThemKhachHang.Visible = true;
        }

    }
}
