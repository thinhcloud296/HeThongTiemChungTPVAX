using System;
using System.Drawing;
using System.Windows.Forms;
using System.Transactions; // <-- THÊM THƯ VIỆN NÀY
using TPVAXWinform_BLL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_GUI.Forms
{
    public partial class XacNhanTiemForm : Form
    {
        private LichTiemBLL lichTiemBLL = new LichTiemBLL();
        private VaccineBLL vaccineBLL = new VaccineBLL();

        // (Các biến của bạn)
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
            // Sửa lỗi encoding (nếu có)
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
                DialogResult result = MessageBox.Show(
                  "Bạn có chắc chắn tiêm?",
                  "Xác nhận",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Bắt đầu Transaction
                    using (TransactionScope scope = new TransactionScope())
                    {
                        // 1. Kiểm tra số lượng tồn kho
                        // (Lưu ý: hàm BLL này nên gọi proc đã trừ HSD)
                        var vaccine = vaccineBLL.GetVaccineByMaVC(maVC);
                        if (vaccine == null)
                        {
                            MessageBox.Show("Không tìm thấy thông tin vaccine!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // (Bạn nên kiểm tra 'SoLuongTonThucTe' nếu dùng proc mới)
                        if (vaccine.SoLuongTon <= 0)
                        {
                            MessageBox.Show(
                                $"Vaccine {tenVaccine} đã hết hàng!\nSố lượng tồn: {vaccine.SoLuongTon}",
                                "Cảnh báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }

                        // 2. Cập nhật lịch tiêm hiện tại
                        LichTiemDTO lichTiem = new LichTiemDTO
                        {
                            MaLT = maLT,
                            MaHSTC = maHSTC,
                            MaVC = maVC,
                            NgayHenTiem = ngayHenTiem,
                            TrangThai = "Đã tiêm", // Cập nhật trạng thái
                            NgayTiemThucTe = DateTime.Now,
                            GhiChu = txtGhiChu.Text.Trim()
                            // Giữ nguyên SoMui (không cần gán)
                        };
                        lichTiemBLL.Edit(lichTiem);

                        // 3. Trừ số lượng tồn kho
                        // (Lưu ý: hàm này cần cập nhật cả 'ChiTietPhieuNhap' và 'Vaccine.SoLuongTon')
                        vaccineBLL.UpdateSoLuongTon(maVC, -1);

                        // 4. GỌI HÀM TẠO LỊCH HẸN KẾ TIẾP
                        // Hàm này sẽ tự xử lý cho cả mũi gói và mũi lẻ
                        lichTiemBLL.TaoLichHenKeTiep(this.maHSTC, this.maVC);

                        // 5. Hoàn tất giao dịch
                        scope.Complete();
                    }

                    // (Thông báo thành công nằm BÊN NGOÀI TransactionScope)
                    MessageBox.Show(
                        "Xác nhận tiêm thành công!\n" +
                        "Hệ thống đã tự động kiểm tra và tạo lịch hẹn kế tiếp (nếu có).",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, Transaction sẽ tự động rollback
                MessageBox.Show($"Lỗi khi xác nhận tiêm (toàn bộ thao tác đã được hủy):\n{ex.Message}", "Lỗi",
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