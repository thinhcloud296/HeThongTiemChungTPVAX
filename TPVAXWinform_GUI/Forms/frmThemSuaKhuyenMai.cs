using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TPVAXWinform_BLL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_GUI.Forms
{
    public partial class frmThemSuaKhuyenMai : Form
    {
        private KhuyenMaiBLL khuyenMaiBLL = new KhuyenMaiBLL();
        private ChiTietKhuyenMaiBLL chiTietKhuyenMaiBLL = new ChiTietKhuyenMaiBLL();
        private VaccineBLL vaccineBLL = new VaccineBLL();
        private GoiVaccineBLL goiVaccineBLL = new GoiVaccineBLL();

        private string maKM = "";
        private bool isEditMode = false;
        private List<ChiTietKhuyenMaiTemp> danhSachChiTiet = new List<ChiTietKhuyenMaiTemp>();

        private class ChiTietKhuyenMaiTemp
        {
            public string MaCTKM { get; set; }
            public string LoaiSanPham { get; set; }
            public string MaSanPham { get; set; }
            public string TenSanPham { get; set; }
        }

        public frmThemSuaKhuyenMai()
        {
            InitializeComponent();
        }

        public frmThemSuaKhuyenMai(string maKhuyenMai) : this()
        {
            this.maKM = maKhuyenMai;
            this.isEditMode = true;
        }

        private void frmThemSuaKhuyenMai_Load(object sender, EventArgs e)
        {
            LoadCombos();

            if (isEditMode)
            {
                this.Text = "Sửa Khuyến Mãi";
                LoadKhuyenMaiData();
            }
            else
            {
                this.Text = "Thêm Khuyến Mãi";
                dtpNgayBatDau.Value = DateTime.Now;
                dtpNgayKetThuc.Value = DateTime.Now.AddMonths(1);
            }
        }

        private void LoadCombos()
        {
            // Load Loại KM
            cboLoaiKM.Items.Clear();
            cboLoaiKM.Items.Add("Giảm giá sản phẩm");
            cboLoaiKM.Items.Add("Giảm giá hóa đơn");
            cboLoaiKM.Items.Add("Khuyến mãi đặc biệt");
            cboLoaiKM.SelectedIndex = 0;

            // Load Kiểu giảm
            cboKieuGiam.Items.Clear();
            cboKieuGiam.Items.Add("PhanTram");
            cboKieuGiam.Items.Add("SoTien");
            cboKieuGiam.SelectedIndex = 0;

            // Load Loại sản phẩm
            cboLoaiSanPham.Items.Clear();
            cboLoaiSanPham.Items.Add("VACCINE");
            cboLoaiSanPham.Items.Add("GOIVACCINE");
            cboLoaiSanPham.SelectedIndex = 0;

            // Load sản phẩm
            LoadSanPham();
        }

        private void LoadSanPham()
        {
            if (cboLoaiSanPham.SelectedItem == null) return;

            string loai = cboLoaiSanPham.SelectedItem.ToString();
            DataTable dt;

            if (loai == "VACCINE")
            {
                // Giả sử hàm này lấy được cột "MaTenVC"
                dt = vaccineBLL.GetDataForComboBox();
                cboSanPham.DataSource = dt;
                cboSanPham.DisplayMember = "MaTenVC";
                cboSanPham.ValueMember = "MaVC";
            }
            else
            {
                dt = goiVaccineBLL.GetData();
                cboSanPham.DataSource = dt;
                cboSanPham.DisplayMember = "TenGoi";
                cboSanPham.ValueMember = "MaGoi";
            }
        }

        private void LoadKhuyenMaiData()
        {
            try
            {
                DataTable dtKM = khuyenMaiBLL.GetAll();
                DataRow[] rows = dtKM.Select($"MaKM = '{maKM}'");

                if (rows.Length > 0)
                {
                    DataRow row = rows[0];
                    txtTenKM.Text = row["TenKM"].ToString();
                    txtMoTa.Text = row["MoTa"].ToString();
                    cboLoaiKM.SelectedItem = row["LoaiKM"].ToString();
                    cboKieuGiam.SelectedItem = row["KieuGiam"].ToString();
                    numGiaTriGiam.Value = Convert.ToDecimal(row["GiaTriGiam"]);
                    dtpNgayBatDau.Value = Convert.ToDateTime(row["NgayBatDau"]);
                    dtpNgayKetThuc.Value = Convert.ToDateTime(row["NgayKetThuc"]);
                    chkTrangThai.Checked = Convert.ToBoolean(row["TrangThai"]);
                }

                // Load chi tiết
                DataTable dtChiTiet = khuyenMaiBLL.GetChiTietByMaKM(maKM);
                foreach (DataRow row in dtChiTiet.Rows)
                {
                    ChiTietKhuyenMaiTemp ct = new ChiTietKhuyenMaiTemp
                    {
                        MaCTKM = row["MaCTKM"].ToString(),
                        LoaiSanPham = row["LoaiSanPham"].ToString(),
                        MaSanPham = row["MaSanPham"].ToString(),
                        TenSanPham = "" // Sẽ load sau
                    };
                    danhSachChiTiet.Add(ct);
                }

                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshDataGridView()
        {
            dgvChiTiet.Rows.Clear();

            foreach (var ct in danhSachChiTiet)
            {
                string tenSP = "";
                if (ct.LoaiSanPham == "VACCINE")
                {
                    DataTable dt = vaccineBLL.GetDataForComboBox();
                    DataRow[] rows = dt.Select($"MaVC = '{ct.MaSanPham}'");
                    if (rows.Length > 0)
                        tenSP = rows[0]["MaTenVC"].ToString();
                }
                else
                {
                    DataTable dt = goiVaccineBLL.GetData();
                    DataRow[] rows = dt.Select($"MaGoi = '{ct.MaSanPham}'");
                    if (rows.Length > 0)
                        tenSP = rows[0]["TenGoi"].ToString();
                }

                int index = dgvChiTiet.Rows.Add();
                DataGridViewRow row = dgvChiTiet.Rows[index];
                row.Cells["colLoaiSP"].Value = ct.LoaiSanPham;
                row.Cells["colMaSP"].Value = ct.MaSanPham;
                row.Cells["colTenSP"].Value = tenSP;
            }
        }

        private void cboLoaiSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSanPham();
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            if (cboLoaiSanPham.SelectedIndex == -1 || cboSanPham.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm và sản phẩm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string loaiSP = cboLoaiSanPham.SelectedItem.ToString();
            string maSP = cboSanPham.SelectedValue.ToString();

            if (danhSachChiTiet.Any(x => x.MaSanPham == maSP && x.LoaiSanPham == loaiSP))
            {
                MessageBox.Show("Sản phẩm này đã có trong danh sách!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChiTietKhuyenMaiTemp ct = new ChiTietKhuyenMaiTemp
            {
                LoaiSanPham = loaiSP,
                MaSanPham = maSP
            };

            danhSachChiTiet.Add(ct);
            RefreshDataGridView();
        }

        private void dgvChiTiet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvChiTiet.Columns["colXoa"].Index)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận",
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
                KhuyenMaiDTO km = new KhuyenMaiDTO
                {
                    MaKM = isEditMode ? maKM : khuyenMaiBLL.CreateNewMaKM(),
                    TenKM = txtTenKM.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim(),
                    LoaiKM = cboLoaiKM.SelectedItem.ToString(),
                    KieuGiam = cboKieuGiam.SelectedItem.ToString(),
                    GiaTriGiam = numGiaTriGiam.Value,
                    NgayBatDau = dtpNgayBatDau.Value,
                    NgayKetThuc = dtpNgayKetThuc.Value,
                    TrangThai = chkTrangThai.Checked
                };

                if (isEditMode)
                {
                    khuyenMaiBLL.Update(km);
                    // Xóa chi tiết cũ và thêm mới
                    chiTietKhuyenMaiBLL.DeleteByMaKM(maKM);
                }
                else
                {
                    khuyenMaiBLL.Insert(km);
                    maKM = km.MaKM;
                }

                // Lưu chi tiết
                foreach (var ct in danhSachChiTiet)
                {
                    ChiTietKhuyenMaiDTO ctDTO = new ChiTietKhuyenMaiDTO
                    {
                        MaCTKM = 0,
                        MaKM = maKM,
                        LoaiSanPham = ct.LoaiSanPham,
                        MaSanPham = ct.MaSanPham
                    };
                    chiTietKhuyenMaiBLL.InsertDetail(ctDTO);
                }

                MessageBox.Show(isEditMode ? "Sửa khuyến mãi thành công!" : "Thêm khuyến mãi thành công!",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu khuyến mãi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenKM.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khuyến mãi!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKM.Focus();
                return false;
            }

            if (numGiaTriGiam.Value <= 0)
            {
                MessageBox.Show("Giá trị giảm phải lớn hơn 0!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numGiaTriGiam.Focus();
                return false;
            }

            if (cboKieuGiam.SelectedItem.ToString() == "PhanTram" && numGiaTriGiam.Value > 100)
            {
                MessageBox.Show("Giá trị giảm theo phần trăm không được vượt quá 100%!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numGiaTriGiam.Focus();
                return false;
            }

            if (dtpNgayBatDau.Value >= dtpNgayKetThuc.Value)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (danhSachChiTiet.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một sản phẩm áp dụng!", "Thông báo",
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