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
        private string trangThai;

        public XacNhanTiemForm(string maLT,string maHSTC,string maVC,DateTime ngayHenTiem,string tenNguoiTiem,string tenVaccine,string ngayHen,int? soMui,string trangThai)
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
            this.trangThai = trangThai;

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
                    using (TransactionScope scope = new TransactionScope())
                    {
                        // --- SỬA LOGIC KIỂM TRA KHO ---

                        // 1. Kiểm tra số lượng tồn kho THỰC TẾ (còn hạn)
                        // (Gọi hàm BLL mới mà chúng ta đã tạo)
                        int soLuongTonThucTe = vaccineBLL.GetSoLuongTonThucTe(maVC);

                        if (soLuongTonThucTe <= 0)
                        {
                            MessageBox.Show(
                                $"Vaccine {tenVaccine} đã hết hàng (hoặc đã hết hạn)!\n" +
                                $"Số lượng có thể tiêm: {soLuongTonThucTe}",
                                "Cảnh báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return; // Thoát (Transaction sẽ tự động hủy)
                        }

                        // --- KẾT THÚC SỬA ---

                        // 2. Cập nhật lịch tiêm hiện tại (Code cũ của bạn đã đúng)
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

                        // 3. Trừ số lượng tồn kho (Code cũ của bạn đã đúng)
                        // (Hàm này gọi proc usp_Vaccine_GiamTonKho, tự động trừ FEFO và Trigger)
                        if(!trangThai.Equals("Đã hủy", StringComparison.OrdinalIgnoreCase))
                            vaccineBLL.UpdateSoLuongTon(maVC, -1);

                        // 4. GỌI HÀM TẠO LỊCH HẸN KẾ TIẾP (Code cũ của bạn đã đúng)
                        if (!trangThai.Equals("Đã hủy", StringComparison.OrdinalIgnoreCase))
                            lichTiemBLL.TaoLichHenKeTiep(this.maHSTC, this.maVC);

                        // 5. Hoàn tất giao dịch
                        scope.Complete();
                    }

                    // (Thông báo thành công)
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