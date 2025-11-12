using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
// --- 1. THÊM THƯ VIỆN REGEX ---
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPVAXWinform.UserControls;
using TPVAXWinform_BLL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_GUI
{
    public partial class frmThemHSTC_KH : Form
    {
        KhachHangBLL khachHangBLL = new KhachHangBLL();
        HoSoTiemChungBLL hoSoTiemChungBLL = new HoSoTiemChungBLL();
        LienKetHoSoBLL lienKetHoSoBLL = new LienKetHoSoBLL();


        string[] quanHeOptions = {
             "Cha", "Mẹ", "Con",
            "Anh ruột", "Chị ruột", "Em ruột",
            "Ông nội", "Bà nội", "Ông ngoại", "Bà ngoại",
            "Vợ", "Chồng",
            "Người giám hộ", "Người chăm sóc", "Đại diện theo pháp luật",
            "Khác"
        };
        string[] gioiTinhOptions = { "Nam", "Nữ", "Khác" };

        // --- 2. THÊM CÁC CHUỖI REGEX LÀM HẰNG SỐ ---
        private const string REGEX_HOTEN = @"^[\p{L}\s']+$"; // Cho phép cả dấu nháy đơn '
        private const string REGEX_SODT = @"^0\d{9}$";
        private const string REGEX_CCCD = @"^\d{12}$";
        private const string REGEX_EMAIL = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        private const string REGEX_DIACHI = @"^[\p{L}\d\s.,/-]+$";
        // Temp
        string tempMaKH = "";
        public frmThemHSTC_KH()
        {
            InitializeComponent();
            cboQuanHe.DataSource = quanHeOptions;
            cboGioiTinhHSTC.DataSource = gioiTinhOptions;
            cboGioiTinhKH.DataSource = gioiTinhOptions.Clone();
            DisableAllInputsHSTC();
        }
        private void DisableAllInputsHSTC()
        {
            txtHoTenHSTC.Enabled = false;
            txtCCCDHSTC.Enabled = false;
            cboGioiTinhHSTC.Enabled = false;
            dtpNgaySinhHSTC.Enabled = false;
            cboQuanHe.Enabled = false;
            txtGhiChuHSTC.Enabled = false;
        }
        private void EnableAllInputsHSTC()
        {
            txtHoTenHSTC.Enabled = true;
            txtCCCDHSTC.Enabled = true;
            cboGioiTinhHSTC.Enabled = true;
            dtpNgaySinhHSTC.Enabled = true;
            cboQuanHe.Enabled = true;
            txtGhiChuHSTC.Enabled = true;
        }
        private void DisableAllInpusKH()
        {
            txtHoTenKH.Enabled = false;
            txtDiaChi.Enabled = false;
            txtSoDT.Enabled = false;
            dtpNgaySinhKH.Enabled = false;
            txtEmail.Enabled = false;
            txtCCCDKH.Enabled = false;
            cboGioiTinhKH.Enabled = false;
            dtpNgaySinhKH.Enabled = false;
        }
        private void EnableAllInpusKH()
        {
            txtHoTenKH.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSoDT.Enabled = true;
            dtpNgaySinhKH.Enabled = true;
            txtEmail.Enabled = true;
            txtCCCDKH.Enabled = true;
            cboGioiTinhKH.Enabled = true;
            dtpNgaySinhKH.Enabled = true;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            string cccd = txtTimCCCD.Text.Trim();
            if (string.IsNullOrWhiteSpace(cccd) || !Regex.IsMatch(cccd, REGEX_CCCD))
            {
                MessageBox.Show("CCCD tìm kiếm không hợp lệ. Phải là 12 số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTimCCCD.Focus();
                return; 
            }


            DataTable dt = khachHangBLL.GetData();
            if (dt.PrimaryKey == null || dt.PrimaryKey.Length == 0)
                dt.PrimaryKey = new[] { dt.Columns["CCCD"] };

            DataRow dr = dt.Rows.Find(cccd);
            if (dr == null)
            {
                MessageBox.Show("Không tìm thấy khách hàng với CCCD đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Maked đã tìm thấy KH thông qua Flag và thêm vào mã kh tmp
            tempMaKH = dr["MaKH"]?.ToString() ?? "";

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

            DisableAllInpusKH();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            EnableAllInpusKH();
            DisableAllInputsHSTC();

            cboDSHSTCLienKet.DataSource = null;

            txtTimCCCD.Clear();
            txtHoTenKH.Clear();
            txtDiaChi.Clear();
            txtSoDT.Clear();
            txtEmail.Clear();
            txtCCCDKH.Clear();

            txtHoTenHSTC.Clear();
            txtCCCDHSTC.Clear();
            cboGioiTinhHSTC.SelectedIndex = 0;
            cboQuanHe.SelectedIndex = 0;
            txtGhiChuHSTC.Clear();

            btnLienKet.Visible = true;
        }
        // Kiểm tra Validation
        public bool CheckValidationBeforeAddKH()
        {
            bool flag = true;
            // Xóa tất cả các icon lỗi cũ trước khi kiểm tra
            errorProvider1.Clear();

            // Kiểm tra Họ tên
            if (string.IsNullOrWhiteSpace(txtHoTenKH.Text) || !Regex.IsMatch(txtHoTenKH.Text.Trim(), REGEX_HOTEN))
            {
                // Gán lỗi cho txtHoTenKH
                errorProvider1.SetError(txtHoTenKH, "Họ tên không hợp lệ. Chỉ được chứa chữ cái và khoảng trắng.");
                flag = false; // Đánh dấu là có lỗi
            }

            // Kiểm tra CCCD
            if (string.IsNullOrWhiteSpace(txtCCCDKH.Text) || !Regex.IsMatch(txtCCCDKH.Text.Trim(), REGEX_CCCD))
            {
                // Gán lỗi cho txtCCCDKH
                errorProvider1.SetError(txtCCCDKH, "CCCD không hợp lệ. Phải là 12 số.");
                flag = false;
            }

            // Kiểm tra Số điện thoại
            if (string.IsNullOrWhiteSpace(txtSoDT.Text) || !Regex.IsMatch(txtSoDT.Text.Trim(), REGEX_SODT))
            {
                // Gán lỗi cho txtSoDT
                errorProvider1.SetError(txtSoDT, "Số điện thoại không hợp lệ. Phải là 10 số, bắt đầu bằng 0.");
                flag = false;
            }

            // Kiểm tra Email
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !Regex.IsMatch(txtEmail.Text.Trim(), REGEX_EMAIL))
            {
                errorProvider1.SetError(txtEmail, "Email không hợp lệ. Vui lòng nhập đúng định dạng (ví dụ: a@b.com).");
                flag = false;
            }

            // Kiểm tra Địa chỉ
            if (!string.IsNullOrWhiteSpace(txtDiaChi.Text) && !Regex.IsMatch(txtDiaChi.Text.Trim(), REGEX_DIACHI))
            {
                errorProvider1.SetError(txtDiaChi, "Địa chỉ chứa ký tự không hợp lệ.");
                flag = false;
            }

            // Kiểm tra ComboBox Giới tính
            if (cboGioiTinhKH.SelectedItem == null)
            {
                errorProvider1.SetError(cboGioiTinhKH, "Vui lòng chọn giới tính khách hàng.");
                flag = false;
            }
            return flag;

        }
        public bool CheckValidationBeforeAddHSTC()
        {
            bool flag = true;
            // Xóa tất cả các icon lỗi cũ trước khi kiểm tra
            errorProvider1.Clear();

            // Kiểm tra Họ tên
            if (string.IsNullOrWhiteSpace(txtHoTenHSTC.Text) || !Regex.IsMatch(txtHoTenHSTC.Text.Trim(), REGEX_HOTEN))
            {
                // Gán lỗi cho txtHoTenKH
                errorProvider1.SetError(txtHoTenHSTC, "Họ tên không hợp lệ. Chỉ được chứa chữ cái và khoảng trắng.");
                flag = false; // Đánh dấu là có lỗi
            }

            // Kiểm tra CCCD
            if (string.IsNullOrWhiteSpace(txtCCCDHSTC.Text) || !Regex.IsMatch(txtCCCDHSTC.Text.Trim(), REGEX_CCCD))
            {
                // Gán lỗi cho txtCCCDKH
                errorProvider1.SetError(txtCCCDHSTC, "CCCD không hợp lệ. Phải là 12 số.");
                flag = false;
            }
            // Kiểm tra ComboBox Giới tính
            if (cboGioiTinhHSTC.SelectedItem == null)
            {
                errorProvider1.SetError(cboGioiTinhHSTC, "Vui lòng chọn giới tính khách hàng.");
                flag = false;
            }
            return flag;

        }
        public bool CheckValidationWhileLinking()
        {
            bool flag = true;
            // Xóa tất cả các icon lỗi cũ trước khi kiểm tra
            errorProvider1.Clear();

            // Kiểm tra Họ tên
            if (string.IsNullOrWhiteSpace(txtHoTenKH.Text) || !Regex.IsMatch(txtHoTenKH.Text.Trim(), REGEX_HOTEN))
            {
                // Gán lỗi cho txtHoTenKH
                errorProvider1.SetError(txtHoTenKH, "Họ tên không hợp lệ. Chỉ được chứa chữ cái và khoảng trắng.");
                flag = false; // Đánh dấu là có lỗi
            }

            // Kiểm tra CCCD
            if (string.IsNullOrWhiteSpace(txtCCCDKH.Text) || !Regex.IsMatch(txtCCCDKH.Text.Trim(), REGEX_CCCD))
            {
                // Gán lỗi cho txtCCCDKH
                errorProvider1.SetError(txtCCCDKH, "CCCD không hợp lệ. Phải là 12 số.");
                flag = false;
            } 

            // Kiểm tra ComboBox Giới tính
            if (cboGioiTinhKH.SelectedItem == null)
            {
                errorProvider1.SetError(cboGioiTinhKH, "Vui lòng chọn giới tính khách hàng.");
                flag = false;
            }
            return flag;

        }
        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            // Validation
            bool hopLe = CheckValidationBeforeAddKH(); // Flag
            if (!hopLe)
            {
                return;
            }

            // Add
            KhachHangDTO newKH = new KhachHangDTO();
            newKH.CCCD = txtCCCDKH.Text.Trim();
            newKH.MaKH = khachHangBLL.CreateMaKH(newKH.CCCD);

            newKH.HoTen = txtHoTenKH.Text.Trim();
            newKH.DiaChi = txtDiaChi.Text.Trim();
            newKH.SoDT = txtSoDT.Text.Trim();
            newKH.NgaySinh = dtpNgaySinhKH.Value;
            newKH.Email = txtEmail.Text.Trim();
            newKH.GioiTinh = cboGioiTinhKH.SelectedItem.ToString();
            try
            {
                khachHangBLL.Insert(newKH);

                MessageBox.Show("Thêm khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemHoSo_Click(object sender, EventArgs e)
        {
            if (hoSoTiemChungBLL.IsHSTCExists(txtCCCDHSTC.Text.Trim()))
            {
                MessageBox.Show("Hồ sơ tiêm chủng với CCCD này đã tồn tại cho khách hàng đã chọn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Validation
            bool hopLe = CheckValidationBeforeAddHSTC(); // Flag
            if (!hopLe)
                return;

            //ADD
            HoSoTiemChungDTO hso = new HoSoTiemChungDTO();
            hso.MaHSTC = hoSoTiemChungBLL.CreateMaHSTC(txtCCCDHSTC.Text.Trim());
            hso.HoTen = txtHoTenHSTC.Text.Trim();
            hso.GioiTinh = cboGioiTinhHSTC.SelectedItem.ToString();
            hso.NgaySinh = dtpNgaySinhHSTC.Value;
            hso.CCCD = txtCCCDHSTC.Text.Trim();
            hso.GhiChu = txtGhiChuHSTC.Text.Trim();
            try
            { 
                hoSoTiemChungBLL.Insert(hso);
                lienKetHoSoBLL.Insert(new LienKetHoSoDTO
                {
                    MaLK = lienKetHoSoBLL.CreateMaLK(hso.CCCD),
                    MaKH = tempMaKH,
                    MaHSTC = hso.MaHSTC,
                    VaiTro = cboQuanHe.SelectedItem.ToString(),
                    NgayLienKet = DateTime.Now
                });
                MessageBox.Show("Thêm hồ sơ tiêm chủng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm hồ sơ tiêm chủng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLienKet_Click(object sender, EventArgs e)
        {
            EnableAllInputsHSTC();
            btnThemHoSo.Enabled = true;
        }
        private void btnDangKyKHAndHSTC_Click(object sender, EventArgs e)
        {
            if (khachHangBLL.IsKHExists(txtCCCDKH.Text.Trim()))
            {
                MessageBox.Show("Khách hàng đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Validation
            bool hopLe = CheckValidationBeforeAddKH(); // Flag
            if (!hopLe)
            {
                return;
            }
            string maKH = khachHangBLL.CreateMaKH(txtCCCDKH.Text.Trim());
            string maHSTC = hoSoTiemChungBLL.CreateMaHSTC(txtCCCDKH.Text.Trim());
            string maLK = lienKetHoSoBLL.CreateMaLK(txtCCCDKH.Text.Trim());
            // Add
            KhachHangDTO newKH = new KhachHangDTO();
            newKH.CCCD = txtCCCDKH.Text.Trim();
            newKH.MaKH = maKH;

            newKH.HoTen = txtHoTenKH.Text.Trim();
            newKH.DiaChi = txtDiaChi.Text.Trim();
            newKH.SoDT = txtSoDT.Text.Trim();
            newKH.NgaySinh = dtpNgaySinhKH.Value;
            newKH.Email = txtEmail.Text.Trim();
            newKH.GioiTinh = cboGioiTinhKH.SelectedItem.ToString();
            // HoSoTiemChung
            HoSoTiemChungDTO hso = new HoSoTiemChungDTO();
            hso.MaHSTC = maHSTC;
            hso.HoTen = txtHoTenKH.Text.Trim();
            hso.GioiTinh = cboGioiTinhKH.SelectedItem.ToString();
            hso.NgaySinh = dtpNgaySinhKH.Value;
            hso.CCCD = txtCCCDKH.Text.Trim();
            hso.GhiChu = "";

            // LienKet
            LienKetHoSoDTO lkhs = new LienKetHoSoDTO();
            lkhs.MaLK = maLK;
            lkhs.NgayLienKet = DateTime.Now;
            lkhs.MaKH = maKH;
            lkhs.MaHSTC = maHSTC;
            lkhs.VaiTro = "Bản thân";
            try
            {
                khachHangBLL.Insert(newKH);
                hoSoTiemChungBLL.Insert(hso);
                lienKetHoSoBLL.Insert(lkhs);
                MessageBox.Show("Thêm khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}