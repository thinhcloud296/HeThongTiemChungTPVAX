using System;
using System.Drawing;
using System.Windows.Forms;
using TPVAXWinform_BLL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_GUI.Forms
{
    public partial class XacNhanTiemForm : Form
    {
        private LichTiemBLL lichTiemBLL = new LichTiemBLL();
        private VaccineBLL vaccineBLL = new VaccineBLL();

        private string maLT;
        private string maHSTC;
        private string maVC;
        private DateTime ngayHenTiem;
        private string tenNguoiTiem;
        private string tenVaccine;
        private string ngayHen;
        private int? soMui;

        public XacNhanTiemForm(
             string maLT,
              string maHSTC,
           string maVC,
                    DateTime ngayHenTiem,
         string tenNguoiTiem,
          string tenVaccine,
             string ngayHen,
                    int? soMui)
        {
            InitializeComponent();

            this.maLT = maLT;
            this.maHSTC = maHSTC;
            this.maVC = maVC;
            this.ngayHenTiem = ngayHenTiem;
            this.tenNguoiTiem = tenNguoiTiem;
            this.tenVaccine = tenVaccine;
            this.ngayHen = ngayHen;
            this.soMui = soMui;

            LoadThongTin();
        }

        private void LoadThongTin()
        {
            // Hi?n th? thông tin lên form
            lblMaLTValue.Text = maLT;
            lblMaHSTCValue.Text = maHSTC;
            lblTenNguoiTiemValue.Text = tenNguoiTiem;
            lblTenVaccineValue.Text = tenVaccine;
            lblNgayHenValue.Text = ngayHen;
            lblSoMuiValue.Text = soMui?.ToString() ?? "N/A";
            lblNgayTiemThucTeValue.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                // Xác nh?n v?i ng??i dùng
                DialogResult result = MessageBox.Show(
          "Bạn có chắc chắn tiêm?",
              "Xác nhận",
              MessageBoxButtons.YesNo,
             MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Ki?m tra s? l??ng t?n kho
                    var vaccine = vaccineBLL.GetVaccineByMaVC(maVC);
                    if (vaccine == null)
                    {
                        MessageBox.Show("Không tìm thấy thông tin vaccine!", "Lỗi",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (vaccine.SoLuongTon <= 0)
                    {
                        MessageBox.Show(
                      $"Vaccine {tenVaccine} đã hết hàng!\nSố lượng tồn: {vaccine.SoLuongTon}",
             "Cảnh báo",
                      MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                        return;
                    }

                  
                    LichTiemDTO lichTiem = new LichTiemDTO
                    {
                        MaLT = maLT,
                        MaHSTC = maHSTC,
                        MaVC = maVC,
                        NgayHenTiem = ngayHenTiem,
                        TrangThai = "Đã tiêm",
                        NgayTiemThucTe = DateTime.Now,
                        GhiChu = txtGhiChu.Text.Trim()
                    };

                    lichTiemBLL.Edit(lichTiem);

                    // Tr? s? l??ng t?n kho
                    vaccineBLL.UpdateSoLuongTon(maVC, -1);

                    MessageBox.Show(
                   "Xác nhận tiêm thành công!\n" +
                $"Số lượng còn lại: {vaccine.SoLuongTon - 1}",
                         "Thành công",
                    MessageBoxButtons.OK,
               MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xác nhận tiêm: {ex.Message}", "Lỗi",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
