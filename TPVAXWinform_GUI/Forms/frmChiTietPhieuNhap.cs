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

namespace TPVAXWinform_GUI.Forms
{
    public partial class frmChiTietPhieuNhap : Form
    {
        private string maPN;
        private ChiTietPhieuNhapBLL chiTietPhieuNhapBLL = new ChiTietPhieuNhapBLL();
        private PhieuNhapBLL phieuNhapBLL = new PhieuNhapBLL();

        public frmChiTietPhieuNhap(string maPhieuNhap)
        {
            InitializeComponent();
            this.maPN = maPhieuNhap;
        }

        private void frmChiTietPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadThongTinPhieuNhap();
            LoadChiTietPhieuNhap();
        }

        private void LoadThongTinPhieuNhap()
        {
            try
            {
                // SỬA VẤN ĐỀ HIỆU NĂNG:
                // Gọi proc mới (qua BLL) để chỉ lấy 1 dòng
                DataTable dtPhieuNhap = phieuNhapBLL.GetDetailByMaPN(maPN);

                if (dtPhieuNhap.Rows.Count > 0)
                {
                    DataRow row = dtPhieuNhap.Rows[0];

                    // SỬA LỖI ENCODING (DÙNG TÊN CỘT ĐÚNG)
                    lblMaPNValue.Text = row["Mã Phiếu Nhập"].ToString();
                    lblNgayLapValue.Text = Convert.ToDateTime(row["Ngày Lập"]).ToString("dd/MM/yyyy");
                    lblNhanVienValue.Text = row["Tên Nhân Viên Lập"].ToString();
                    lblNhaCungCapValue.Text = row["Tên Nhà Cung Cấp"].ToString();
                }
            }
            catch (Exception ex)
            {
                // SỬA LỖI ENCODING
                MessageBox.Show($"Lỗi khi load thông tin phiếu nhập: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChiTietPhieuNhap()
        {
            try
            {
                // Giả định hàm BLL này gọi proc usp_ChiTietPhieuNhap_GetByMaPN
                DataTable dtChiTiet = chiTietPhieuNhapBLL.GetDataByMaPN(maPN);
                BindDataToGrid(dtChiTiet);

                decimal tongTien = 0;
                foreach (DataRow row in dtChiTiet.Rows)
                {
                    // SỬA LỖI ENCODING (DÙNG TÊN CỘT ĐÚNG TỪ PROC)
                    tongTien += Convert.ToDecimal(row["Thành Tiền"]);
                }

                // SỬA LỖI ENCODING
                lblTongTienValue.Text = tongTien.ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load chi tiết: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindDataToGrid(DataTable dt)
        {
            dgvChiTietPN.AutoGenerateColumns = false;

            // SỬA LỖI ENCODING (DÙNG TÊN CỘT ĐÚNG TỪ PROC)
            colMaCTPN.DataPropertyName = "Mã Chi Tiết";
            colMaVC.DataPropertyName = "Mã Vaccine";
            colTenVC.DataPropertyName = "Tên Vaccine";
            colNuocSanXuat.DataPropertyName = "Nước Sản Xuất";
            colSoLuong.DataPropertyName = "Số Lượng";
            colSoLuongTon.DataPropertyName = "Số Lượng Tồn";
            colGiaNhap.DataPropertyName = "Giá Nhập";
            colHanSuDung.DataPropertyName = "Hạn Sử Dụng";
            colThanhTien.DataPropertyName = "Thành Tiền";

            dgvChiTietPN.DataSource = dt;

            // Định dạng cột (code của bạn đã đúng)
            dgvChiTietPN.Columns["colGiaNhap"].DefaultCellStyle.Format = "N0";
            dgvChiTietPN.Columns["colThanhTien"].DefaultCellStyle.Format = "N0";
            dgvChiTietPN.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            dgvChiTietPN.RowTemplate.Height = 36;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}