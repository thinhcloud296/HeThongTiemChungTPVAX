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
using TPVAXWinform_DTO;

namespace TPVAXWinform_GUI
{
    public partial class frmChiTietHoaDon : Form
    {
        private string maHD;
        private ChiTietHoaDonBLL chiTietHoaDonBLL = new ChiTietHoaDonBLL();

        public frmChiTietHoaDon(string maHoaDon)
        {
            InitializeComponent();
            this.maHD = maHoaDon;
        }

        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            LoadChiTietHoaDon();
        }

        private void LoadChiTietHoaDon()
        {
            try
            {

                DataTable dtChiTiet = chiTietHoaDonBLL.GetDataByMaHD(maHD);
                BindDataToGrid(dtChiTiet);

                lblMaHDValue.Text = maHD;

                decimal tongTien = 0;
                foreach (DataRow row in dtChiTiet.Rows)
                {
                    tongTien += Convert.ToDecimal(row["ThanhTien"]);
                }
                lblTongTienValue.Text = tongTien.ToString("N0") + " đ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load: {ex.Message}", "Lỗi",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void BindDataToGrid(DataTable dt)
        {
            dgvChiTietHD.AutoGenerateColumns = false;

            colMaCTHD.DataPropertyName = "MaCTHD";
            colMaSanPham.DataPropertyName = "MaSanPham";
            colTenSanPham.DataPropertyName = "TenSanPham";
            colLoaiSanPham.DataPropertyName = "LoaiSanPham";
            colSoLuong.DataPropertyName = "SoLuong";
            colDonGia.DataPropertyName = "DonGia";
            colThanhTien.DataPropertyName = "ThanhTien";

            dgvChiTietHD.DataSource = dt;

            dgvChiTietHD.Columns["colDonGia"].DefaultCellStyle.Format = "N0";
            dgvChiTietHD.Columns["colThanhTien"].DefaultCellStyle.Format = "N0";
            dgvChiTietHD.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            dgvChiTietHD.RowTemplate.Height = 36;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
