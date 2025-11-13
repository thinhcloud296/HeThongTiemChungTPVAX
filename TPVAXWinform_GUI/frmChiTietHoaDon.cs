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
     // TODO: Thay th? b?ng BLL th?c t? khi có
   DataTable dtChiTiet = CreateSampleChiTietData();
             BindDataToGrid(dtChiTiet);
                
    // Hi?n th? mã hóa ??n
   lblMaHDValue.Text = maHD;
     
    // Tính t?ng ti?n
       decimal tongTien = 0;
           foreach (DataRow row in dtChiTiet.Rows)
      {
     tongTien += Convert.ToDecimal(row["ThanhTien"]);
          }
              lblTongTienValue.Text = tongTien.ToString("N0") + " VN?";
      }
   catch (Exception ex)
            {
         MessageBox.Show($"L?i khi t?i chi ti?t hóa ??n: {ex.Message}", "L?i",
        MessageBoxButtons.OK, MessageBoxIcon.Error);
   }
        }

        private DataTable CreateSampleChiTietData()
        {
    DataTable dt = new DataTable();
       dt.Columns.Add("MaCTHD", typeof(string));
   dt.Columns.Add("MaSanPham", typeof(string));
            dt.Columns.Add("TenSanPham", typeof(string));
            dt.Columns.Add("LoaiSanPham", typeof(string));
      dt.Columns.Add("SoLuong", typeof(int));
         dt.Columns.Add("DonGia", typeof(decimal));
        dt.Columns.Add("ThanhTien", typeof(decimal));

            // Thêm d? li?u m?u
     dt.Rows.Add("CTHD001", "VC0001", "Vaccine COVID-19", "Vaccine", 2, 500000, 1000000);
 dt.Rows.Add("CTHD002", "GOI001", "Gói tiêm ch?ng tr? em", "Gói Vaccine", 1, 2500000, 2500000);
            dt.Rows.Add("CTHD003", "VC0002", "Vaccine viêm gan B", "Vaccine", 1, 300000, 300000);

        return dt;
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

   dgvChiTietHD.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
   dgvChiTietHD.RowTemplate.Height = 36;
     }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
