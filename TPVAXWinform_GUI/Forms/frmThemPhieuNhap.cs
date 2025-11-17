using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Transactions; // Thêm Transaction cho an toàn
using TPVAXWinform_BLL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_GUI.Forms
{
    public partial class frmThemPhieuNhap : Form
    {
        private PhieuNhapBLL phieuNhapBLL = new PhieuNhapBLL();
        private ChiTietPhieuNhapBLL chiTietPhieuNhapBLL = new ChiTietPhieuNhapBLL();
        private NhanVienBLL nhanVienBLL = new NhanVienBLL();
        private NhaCungCapBLL nhaCungCapBLL = new NhaCungCapBLL();
        private VaccineBLL vaccineBLL = new VaccineBLL();

        private List<ChiTietPhieuNhapTemp> danhSachChiTiet = new List<ChiTietPhieuNhapTemp>();
        private string maPN = "";
        private bool isEditMode = false;

        // Class tạm (Giữ nguyên)
        private class ChiTietPhieuNhapTemp
        {
            public string MaCTPN { get; set; }
            public string MaVC { get; set; }
            public string TenVC { get; set; }
            public string NuocSanXuat { get; set; }
            public int SoLuong { get; set; } // Đây là SoLuongNhap
            public decimal GiaNhap { get; set; }
            public DateTime? HanSuDung { get; set; }
        }

        public frmThemPhieuNhap()
        {
            InitializeComponent();
            this.dgvChiTiet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTiet_CellContentClick);
        }

        public frmThemPhieuNhap(string maPhieuNhap) : this()
        {
            this.maPN = maPhieuNhap;
            this.isEditMode = true;
        }

        private void frmThemPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();

            if (isEditMode)
            {
                // SỬA ENCODING
                lblTitle.Text = "SỬA PHIẾU NHẬP";
                this.Text = "Sửa phiếu nhập";
                LoadPhieuNhapData();
            }
            else
            {
                dtpNgayLap.Value = DateTime.Now;
            }
        }

        private void LoadComboBoxes()
        {
            // Load Nhân viên
            DataTable dtNhanVien = nhanVienBLL.GetData();
            cboNhanVien.DataSource = dtNhanVien;
            cboNhanVien.DisplayMember = "HoTen";
            cboNhanVien.ValueMember = "MaNV";
            cboNhanVien.SelectedIndex = -1;

            // Load Nhà cung cấp
            DataTable dtNhaCungCap = nhaCungCapBLL.GetData();
            cboNhaCungCap.DataSource = dtNhaCungCap;
            cboNhaCungCap.DisplayMember = "TenNCC";
            cboNhaCungCap.ValueMember = "MaNCC";
            cboNhaCungCap.SelectedIndex = -1;

            // Load Vaccine
            DataTable dtVaccine = vaccineBLL.GetData(); // (Nên dùng proc mới)
            cboVaccine.DataSource = dtVaccine;
            cboVaccine.DisplayMember = "TenVC";
            cboVaccine.ValueMember = "MaVC";
            cboVaccine.SelectedIndex = -1;
        }

        private void LoadPhieuNhapData()
        {
            try
            {
                // (Giả sử proc đã sửa, trả về tên không dấu)
                DataTable dtPhieuNhap = phieuNhapBLL.GetDetailByMaPN(maPN);
                if (dtPhieuNhap.Rows.Count > 0)
                {
                    DataRow row = dtPhieuNhap.Rows[0];
                    // SỬA ENCODING: Dùng tên cột không dấu
                    dtpNgayLap.Value = Convert.ToDateTime(row["NgayLap"]);
                    cboNhanVien.SelectedValue = row["MaNV"]; // Gán bằng Value (MaNV)
                    cboNhaCungCap.SelectedValue = row["MaNCC"]; // Gán bằng Value (MaNCC)
                }

                // Load chi tiết (Giả sử proc đã sửa, trả về tên không dấu)
                DataTable dtChiTiet = chiTietPhieuNhapBLL.GetDataByMaPN(maPN);
                foreach (DataRow row in dtChiTiet.Rows)
                {
                    ChiTietPhieuNhapTemp ct = new ChiTietPhieuNhapTemp
                    {
                        // SỬA ENCODING: Dùng tên cột không dấu
                        MaCTPN = row["MaCTPN"].ToString(),
                        MaVC = row["MaVC"].ToString(),
                        TenVC = row["TenVC"].ToString(),
                        NuocSanXuat = row["NuocSanXuat"]?.ToString() ?? "",
                        SoLuong = Convert.ToInt32(row["SoLuongNhap"]), // Lấy SoLuongNhap
                        GiaNhap = Convert.ToDecimal(row["GiaNhap"]),
                        HanSuDung = row["HanSuDung"] != DBNull.Value ?
                            Convert.ToDateTime(row["HanSuDung"]) : (DateTime?)null
                    };
                    danhSachChiTiet.Add(ct);
                }

                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu phiếu nhập: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemVaccine_Click(object sender, EventArgs e)
        {
            if (cboVaccine.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn vaccine!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // ... (Các validate khác của bạn đã đúng) ...

            string maVC = cboVaccine.SelectedValue.ToString();
            string tenVC = cboVaccine.Text;

            if (danhSachChiTiet.Any(x => x.MaVC == maVC))
            {
                MessageBox.Show("Vaccine này đã có trong danh sách!", "Thông báo",
                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChiTietPhieuNhapTemp ct = new ChiTietPhieuNhapTemp
            {
                MaVC = maVC,
                TenVC = tenVC,
                NuocSanXuat = txtNuocSanXuat.Text.Trim(),
                SoLuong = (int)numSoLuong.Value,
                GiaNhap = numGiaNhap.Value,
                HanSuDung = dtpHanSuDung.Checked ? dtpHanSuDung.Value : (DateTime?)null
            };

            danhSachChiTiet.Add(ct);
            RefreshDataGridView();

            // Reset input (code của bạn đã đúng)
            cboVaccine.SelectedIndex = -1;
            txtNuocSanXuat.Clear();
            numSoLuong.Value = 1;
            numGiaNhap.Value = 0;
            dtpHanSuDung.Value = DateTime.Now.AddYears(2);
        }

        private void RefreshDataGridView()
        {
            dgvChiTiet.Rows.Clear();

            foreach (var ct in danhSachChiTiet)
            {
                int index = dgvChiTiet.Rows.Add();
                DataGridViewRow row = dgvChiTiet.Rows[index];

                row.Cells["colMaVC"].Value = ct.MaVC;
                row.Cells["colTenVC"].Value = ct.TenVC;
                row.Cells["colNuocSanXuat"].Value = ct.NuocSanXuat;
                row.Cells["colSoLuong"].Value = ct.SoLuong; // Hiển thị SoLuongNhap

                // --- SỬA LỖI CRASH: Lưu decimal thô ---
                row.Cells["colGiaNhap"].Value = ct.GiaNhap;
                // (Bạn phải set DefaultCellStyle.Format = "N0" cho cột này trong Designer)

                row.Cells["colHanSuDung"].Value = ct.HanSuDung?.ToString("dd/MM/yyyy") ?? "";
            }
        }

        private void dgvChiTiet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvChiTiet.Columns["colXoa"].Index)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa vaccine này?", "Xác nhận",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    danhSachChiTiet.RemoveAt(e.RowIndex);
                    RefreshDataGridView();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                // --- SỬA: Thêm TransactionScope ---
                using (TransactionScope scope = new TransactionScope())
                {
                    // 1. Lưu Phiếu Nhập
                    PhieuNhapDTO pn = new PhieuNhapDTO
                    {
                        MaPN = isEditMode ? maPN : phieuNhapBLL.CreateNewMaPN(),
                        NgayLap = dtpNgayLap.Value,
                        MaNV = cboNhanVien.SelectedValue?.ToString(),
                        MaNCC = cboNhaCungCap.SelectedValue?.ToString()
                    };

                    if (isEditMode)
                    {
                        phieuNhapBLL.Edit(pn);
                    }
                    else
                    {
                        phieuNhapBLL.Insert(pn);
                        maPN = pn.MaPN;
                    }

                    // (Logic để xóa các chi tiết cũ nếu là Edit Mode)
                    if (isEditMode)
                    {
                        // Bạn cần hàm này để xóa các chi tiết cũ trước khi thêm lại
                        // chiTietPhieuNhapBLL.DeleteByMaPN(maPN);
                    }

                    // 2. Lưu Chi Tiết Phiếu Nhập
                    foreach (var ct in danhSachChiTiet)
                    {
                        ChiTietPhieuNhapDTO ctpn = new ChiTietPhieuNhapDTO
                        {
                            MaCTPN = string.IsNullOrEmpty(ct.MaCTPN) ? chiTietPhieuNhapBLL.CreateNewMaCTPN() : ct.MaCTPN,
                            MaPN = maPN,
                            MaVC = ct.MaVC,
                            NuocSanXuat = ct.NuocSanXuat,

                            // --- SỬA CSDL: Gán cả 2 cột ---
                            SoLuong = ct.SoLuong, // (Đây là SoLuongNhap)
                            SoLuongTonKho = 0,    // <-- Gán Tồn kho = 0 (chờ xác nhận)
                            // --- KẾT THÚC SỬA ---

                            GiaNhap = ct.GiaNhap,
                            HanSuDung = ct.HanSuDung
                        };

                        // (Logic Insert/Edit của bạn đã đúng)
                        if (string.IsNullOrEmpty(ct.MaCTPN))
                        {
                            chiTietPhieuNhapBLL.Insert(ctpn);
                        }
                        else
                        {
                            chiTietPhieuNhapBLL.Edit(ctpn);
                        }
                    }

                    // Hoàn tất giao dịch
                    scope.Complete();
                }

                MessageBox.Show(isEditMode ? "Sửa phiếu nhập thành công!" : "Thêm phiếu nhập thành công!",
                     "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu nhập: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (cboNhanVien.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nhân viên lập phiếu!", "Thông báo",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboNhaCungCap.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Thông báo",
                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (danhSachChiTiet.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một vaccine!", "Thông báo",
             MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}