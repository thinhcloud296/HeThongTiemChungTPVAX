using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPVAXWinform_GUI
{
    public partial class frmThemMuiTiem : Form
    {
        public frmThemMuiTiem()
        {
            InitializeComponent();
        }
        public frmThemMuiTiem(string maHSTC, string hoTen, string gioiTinh, string ngaySinh, string tenKH, string quanHe,string soDTKH)
        {
            InitializeComponent();

            lblMaHSTC.Text = maHSTC; 
            lblTenNguoiTiemValue.Text = hoTen;
            lblGioiTinhValue.Text = gioiTinh;
            lblNgaySinhValue.Text = ngaySinh;

            lblTenKhachHangValue.Text = tenKH;
            lblQuanHeValue.Text = quanHe; 
            lblSoDTValue.Text = soDTKH;
        }

        private void lblTenNguoiTiemValue_Click(object sender, EventArgs e)
        {

        }
    }
}
