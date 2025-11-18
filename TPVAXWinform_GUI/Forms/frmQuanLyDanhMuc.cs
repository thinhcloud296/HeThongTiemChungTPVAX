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

namespace TPVAXWinform_GUI.Forms
{
    public partial class frmQuanLyDanhMuc : Form
    {
        private LoaiBenhBLL loaiBenhBLL = new LoaiBenhBLL();
        private LoaiVaccineBLL loaiVaccineBLL = new LoaiVaccineBLL();
        private NhaCungCapBLL nhaCungCapBLL = new NhaCungCapBLL();
        private VaccineBLL vaccineBLL = new VaccineBLL();
        private DataTable dtLoaiBenh;
        private DataTable dtLoaiVaccine;
        private DataTable dtNhaCungCap;
        private DataTable dtVaccine;

        public frmQuanLyDanhMuc()
        {
            InitializeComponent();
            InitializeFormSettings();
        }

        private void InitializeFormSettings()
        {
            this.Width = 1200;
            this.Height = 700;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmQuanLyDanhMuc_Load(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 0;
            LoadLoaiBenh();
            LoadLoaiVaccine();
            LoadNhaCungCap();
            LoadVaccine();
        }

        #region Loại Bệnh
        private void LoadLoaiBenh()
        {
            dtLoaiBenh = loaiBenhBLL.GetData();
            BindDataToGridLoaiBenh(dtLoaiBenh);
        }

        private void BindDataToGridLoaiBenh(DataTable dt)
        {
            dgvLoaiBenh.AutoGenerateColumns = false;
            colMaLoaiBenh.DataPropertyName = "MaLoaiBenh";
            colTenBenh.DataPropertyName = "TenBenh";
            colMoTaBenh.DataPropertyName = "MoTa";
            colNhomDoiTuong.DataPropertyName = "NhomDoiTuong";
            dgvLoaiBenh.DataSource = dt;
        }

        private void txtTimKiemBenh_TextChanged(object sender, EventArgs e)
        {
            if (dtLoaiBenh == null) return;

            string searchText = txtTimKiemBenh.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                dgvLoaiBenh.DataSource = dtLoaiBenh;
                return;
            }

            DataView dv = dtLoaiBenh.DefaultView;
            dv.RowFilter = $"TenBenh LIKE '%{searchText.Replace("'", "''")}%'";
            dgvLoaiBenh.DataSource = dv.ToTable();
        }

        private void btnThemLoaiBenh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenBenh.Text))
            {
                MessageBox.Show("Vui lòng nh?p tên b?nh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                LoaiBenhDTO loaiBenh = new LoaiBenhDTO
                {
                    MaLoaiBenh = loaiBenhBLL.CreateNewMaLoaiBenh(),
                    TenBenh = txtTenBenh.Text.Trim(),
                    MoTa = txtMoTaBenh.Text.Trim(),
                    NhomDoiTuong = txtNhomDoiTuong.Text.Trim()
                };

                loaiBenhBLL.Insert(loaiBenh);
                MessageBox.Show("Thêm lo?i b?nh thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLoaiBenh();
                ClearLoaiBenhInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("L?i khi thêm lo?i b?nh: " + ex.Message, "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaLoaiBenh_Click(object sender, EventArgs e)
        {
            if (dgvLoaiBenh.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng ch?n lo?i b?nh c?n s?a.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenBenh.Text))
            {
                MessageBox.Show("Vui lòng nh?p tên b?nh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maLoaiBenh = dgvLoaiBenh.SelectedRows[0].Cells["colMaLoaiBenh"].Value.ToString();
                LoaiBenhDTO loaiBenh = new LoaiBenhDTO
                {
                    MaLoaiBenh = maLoaiBenh,
                    TenBenh = txtTenBenh.Text.Trim(),
                    MoTa = txtMoTaBenh.Text.Trim(),
                    NhomDoiTuong = txtNhomDoiTuong.Text.Trim()
                };

                loaiBenhBLL.Edit(loaiBenh);
                MessageBox.Show("S?a lo?i b?nh thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLoaiBenh();
                ClearLoaiBenhInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("L?i khi s?a lo?i b?nh: " + ex.Message, "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvLoaiBenh_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoaiBenh.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvLoaiBenh.SelectedRows[0];
                txtTenBenh.Text = row.Cells["colTenBenh"].Value?.ToString() ?? "";
                txtMoTaBenh.Text = row.Cells["colMoTaBenh"].Value?.ToString() ?? "";
                txtNhomDoiTuong.Text = row.Cells["colNhomDoiTuong"].Value?.ToString() ?? "";
            }
        }

        private void btnLamMoiBenh_Click(object sender, EventArgs e)
        {
            ClearLoaiBenhInputs();
            LoadLoaiBenh();
            txtTimKiemBenh.Clear();
        }

        private void ClearLoaiBenhInputs()
        {
            txtTenBenh.Clear();
            txtMoTaBenh.Clear();
            txtNhomDoiTuong.Clear();
            dgvLoaiBenh.ClearSelection();
        }
        #endregion

        #region Loại Vaccine
        private void LoadLoaiVaccine()
        {
            dtLoaiVaccine = loaiVaccineBLL.GetData();
            BindDataToGridLoaiVaccine(dtLoaiVaccine);
        }

        private void BindDataToGridLoaiVaccine(DataTable dt)
        {
            dgvLoaiVaccine.AutoGenerateColumns = false;
            colMaLoai.DataPropertyName = "MaLoai";
            colTenLoai.DataPropertyName = "TenLoai";
            colMoTaLoai.DataPropertyName = "MoTa";
            dgvLoaiVaccine.DataSource = dt;
        }

        private void txtTimKiemVaccine_TextChanged(object sender, EventArgs e)
        {
            if (dtLoaiVaccine == null) return;

            string searchText = txtTimKiemVaccine.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                dgvLoaiVaccine.DataSource = dtLoaiVaccine;
                return;
            }

            DataView dv = dtLoaiVaccine.DefaultView;
            dv.RowFilter = $"TenLoai LIKE '%{searchText.Replace("'", "''")}%'";
            dgvLoaiVaccine.DataSource = dv.ToTable();
        }

        private void btnThemLoaiVaccine_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenLoai.Text))
            {
                MessageBox.Show("Vui lòng nh?p tên lo?i vaccine.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                LoaiVaccineDTO loaiVaccine = new LoaiVaccineDTO
                {
                    MaLoai = loaiVaccineBLL.CreateNewMaLoaiVaccine(),
                    TenLoai = txtTenLoai.Text.Trim(),
                    MoTa = txtMoTaLoai.Text.Trim()
                };

                loaiVaccineBLL.Insert(loaiVaccine);
                MessageBox.Show("Thêm lo?i vaccine thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLoaiVaccine();
                ClearLoaiVaccineInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("L?i khi thêm lo?i vaccine: " + ex.Message, "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaLoaiVaccine_Click(object sender, EventArgs e)
        {
            if (dgvLoaiVaccine.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng ch?n lo?i vaccine c?n s?a.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenLoai.Text))
            {
                MessageBox.Show("Vui lòng nh?p tên lo?i vaccine.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maLoai = dgvLoaiVaccine.SelectedRows[0].Cells["colMaLoai"].Value.ToString();
                LoaiVaccineDTO loaiVaccine = new LoaiVaccineDTO
                {
                    MaLoai = maLoai,
                    TenLoai = txtTenLoai.Text.Trim(),
                    MoTa = txtMoTaLoai.Text.Trim()
                };

                loaiVaccineBLL.Edit(loaiVaccine);
                MessageBox.Show("S?a lo?i vaccine thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLoaiVaccine();
                ClearLoaiVaccineInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("L?i khi s?a lo?i vaccine: " + ex.Message, "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvLoaiVaccine_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoaiVaccine.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvLoaiVaccine.SelectedRows[0];
                txtTenLoai.Text = row.Cells["colTenLoai"].Value?.ToString() ?? "";
                txtMoTaLoai.Text = row.Cells["colMoTaLoai"].Value?.ToString() ?? "";
            }
        }

        private void btnLamMoiVaccine_Click(object sender, EventArgs e)
        {
            ClearLoaiVaccineInputs();
            LoadLoaiVaccine();
            txtTimKiemVaccine.Clear();
        }

        private void ClearLoaiVaccineInputs()
        {
            txtTenLoai.Clear();
            txtMoTaLoai.Clear();
            dgvLoaiVaccine.ClearSelection();
        }
        #endregion

        #region Nhà Cung Cấp
        private void LoadNhaCungCap()
        {
            dtNhaCungCap = nhaCungCapBLL.GetData();
            BindDataToGridNhaCungCap(dtNhaCungCap);
        }

        private void BindDataToGridNhaCungCap(DataTable dt)
        {
            // Note: Bạn cần thêm các cột này vào DataGridView trong Designer
            // colMaNCC, colTenNCC, colDiaChiNCC, colEmailNCC, colSoDTNCC, colTenNganHang, colSoTK
            dgvNhaCungCap.AutoGenerateColumns = false;
            colMaNCC.DataPropertyName = "MaNCC";
            colTenNCC.DataPropertyName = "TenNCC";
            colDiaChiNCC.DataPropertyName = "DiaChi";
            colEmailNCC.DataPropertyName = "Email";
            colSoDTNCC.DataPropertyName = "SoDT";
            colTenNganHang.DataPropertyName = "TenNganHang";
            colSoTK.DataPropertyName = "SoTK";
            dgvNhaCungCap.DataSource = dt;
        }

        private void txtTimKiemNCC_TextChanged(object sender, EventArgs e)
        {
            if (dtNhaCungCap == null) return;

            string searchText = txtTimKiemNCC.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                dgvNhaCungCap.DataSource = dtNhaCungCap;
                return;
            }

            DataView dv = dtNhaCungCap.DefaultView;
            dv.RowFilter = $"TenNCC LIKE '%{searchText.Replace("'", "''")}%'";
            dgvNhaCungCap.DataSource = dv.ToTable();
        }

        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                NhaCungCapDTO ncc = new NhaCungCapDTO
                {
                    MaNCC = nhaCungCapBLL.CreateNewMaNCC(),
                    TenNCC = txtTenNCC.Text.Trim(),
                    DiaChi = txtDiaChiNCC.Text.Trim(),
                    Email = txtEmailNCC.Text.Trim(),
                    SoDT = txtSoDTNCC.Text.Trim(),
                    TenNganHang = txtTenNganHang.Text.Trim(),
                    SoTK = txtSoTK.Text.Trim()
                };

                nhaCungCapBLL.Insert(ncc);
                MessageBox.Show("Thêm nhà cung cấp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNhaCungCap();
                ClearNhaCungCapInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaNCC_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maNCC = dgvNhaCungCap.SelectedRows[0].Cells["colMaNCC"].Value.ToString();
                NhaCungCapDTO ncc = new NhaCungCapDTO
                {
                    MaNCC = maNCC,
                    TenNCC = txtTenNCC.Text.Trim(),
                    DiaChi = txtDiaChiNCC.Text.Trim(),
                    Email = txtEmailNCC.Text.Trim(),
                    SoDT = txtSoDTNCC.Text.Trim(),
                    TenNganHang = txtTenNganHang.Text.Trim(),
                    SoTK = txtSoTK.Text.Trim()
                };

                nhaCungCapBLL.Edit(ncc);
                MessageBox.Show("Sửa nhà cung cấp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNhaCungCap();
                ClearNhaCungCapInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvNhaCungCap_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvNhaCungCap.SelectedRows[0];
                txtTenNCC.Text = row.Cells["colTenNCC"].Value?.ToString() ?? "";
                txtDiaChiNCC.Text = row.Cells["colDiaChiNCC"].Value?.ToString() ?? "";
                txtEmailNCC.Text = row.Cells["colEmailNCC"].Value?.ToString() ?? "";
                txtSoDTNCC.Text = row.Cells["colSoDTNCC"].Value?.ToString() ?? "";
                txtTenNganHang.Text = row.Cells["colTenNganHang"].Value?.ToString() ?? "";
                txtSoTK.Text = row.Cells["colSoTK"].Value?.ToString() ?? "";
            }
        }

        private void btnLamMoiNCC_Click(object sender, EventArgs e)
        {
            ClearNhaCungCapInputs();
            LoadNhaCungCap();
            txtTimKiemNCC.Clear();
        }

        private void ClearNhaCungCapInputs()
        {
            txtTenNCC.Clear();
            txtDiaChiNCC.Clear();
            txtEmailNCC.Clear();
            txtSoDTNCC.Clear();
            txtTenNganHang.Clear();
            txtSoTK.Clear();
            dgvNhaCungCap.ClearSelection();
        }
        #endregion

        #region Vaccine
        private void LoadVaccine()
        {
            dtVaccine = vaccineBLL.GetData();
            BindDataToGridVaccine(dtVaccine);
            LoadLoaiVaccineComboBox();
        }

        private void LoadLoaiVaccineComboBox()
        {
            DataTable dtLoaiVC = loaiVaccineBLL.GetData();
            cboMaLoaiVC.DataSource = dtLoaiVC;
            cboMaLoaiVC.DisplayMember = "TenLoai";
            cboMaLoaiVC.ValueMember = "MaLoai";
            cboMaLoaiVC.SelectedIndex = -1;
        }

        private void BindDataToGridVaccine(DataTable dt)
        {
            dgvVaccine.AutoGenerateColumns = false;
            colMaVC.DataPropertyName = "MaVC";
            colTenVC.DataPropertyName = "TenVC";
            colSoMuiToiDa.DataPropertyName = "SoMuiToiDa";
            colSoThangCho.DataPropertyName = "SoThangCho";
            colGiaBan.DataPropertyName = "GiaBan";
            colSoLuongTon.DataPropertyName = "SoLuongTon";
            colMaLoaiVC.DataPropertyName = "MaLoai";
            dgvVaccine.DataSource = dt;
        }

        private void txtTimKiemVC_TextChanged(object sender, EventArgs e)
        {
            if (dtVaccine == null) return;

            string searchText = txtTimKiemVC.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                dgvVaccine.DataSource = dtVaccine;
                return;
            }

            DataView dv = dtVaccine.DefaultView;
            dv.RowFilter = $"TenVC LIKE '%{searchText.Replace("'", "''")}%'";
            dgvVaccine.DataSource = dv.ToTable();
        }

        private void btnThemVC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenVC.Text))
            {
                MessageBox.Show("Vui lòng nhập tên vaccine.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboMaLoaiVC.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại vaccine.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                VaccineDTO vaccine = new VaccineDTO
                {
                    MaVC = vaccineBLL.CreateNewMaVC(),
                    TenVC = txtTenVC.Text.Trim(),
                    MoTa = txtMoTaVC.Text.Trim(),
                    SoMuiToiDa = string.IsNullOrWhiteSpace(txtSoMuiToiDaVC.Text) ? 0 : int.Parse(txtSoMuiToiDaVC.Text),
                    SoThangCho = string.IsNullOrWhiteSpace(txtSoThangChoVC.Text) ? 0 : int.Parse(txtSoThangChoVC.Text),
                    GiaBan = string.IsNullOrWhiteSpace(txtGiaBanVC.Text) ? 0 : decimal.Parse(txtGiaBanVC.Text),
                    SoLuongTon = string.IsNullOrWhiteSpace(txtSoLuongTonVC.Text) ? 0 : int.Parse(txtSoLuongTonVC.Text),
                    MaLoai = cboMaLoaiVC.SelectedValue.ToString(),
                    HinhAnh = ""
                };

                vaccineBLL.Insert(vaccine);
                MessageBox.Show("Thêm vaccine thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadVaccine();
                ClearVaccineInputs();
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số cho các trường số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm vaccine: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaVC_Click(object sender, EventArgs e)
        {
            if (dgvVaccine.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn vaccine cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenVC.Text))
            {
                MessageBox.Show("Vui lòng nhập tên vaccine.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboMaLoaiVC.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại vaccine.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maVC = dgvVaccine.SelectedRows[0].Cells["colMaVC"].Value.ToString();
                VaccineDTO vaccine = new VaccineDTO
                {
                    MaVC = maVC,
                    TenVC = txtTenVC.Text.Trim(),
                    MoTa = txtMoTaVC.Text.Trim(),
                    SoMuiToiDa = string.IsNullOrWhiteSpace(txtSoMuiToiDaVC.Text) ? 0 : int.Parse(txtSoMuiToiDaVC.Text),
                    SoThangCho = string.IsNullOrWhiteSpace(txtSoThangChoVC.Text) ? 0 : int.Parse(txtSoThangChoVC.Text),
                    GiaBan = string.IsNullOrWhiteSpace(txtGiaBanVC.Text) ? 0 : decimal.Parse(txtGiaBanVC.Text),
                    SoLuongTon = string.IsNullOrWhiteSpace(txtSoLuongTonVC.Text) ? 0 : int.Parse(txtSoLuongTonVC.Text),
                    MaLoai = cboMaLoaiVC.SelectedValue.ToString(),
                    HinhAnh = dgvVaccine.SelectedRows[0].Cells["colMaVC"].Value != DBNull.Value ? 
 vaccineBLL.GetVaccineByMaVC(maVC)?.HinhAnh ?? "" : ""
                };

                vaccineBLL.Edit(vaccine);
                MessageBox.Show("Sửa vaccine thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadVaccine();
                ClearVaccineInputs();
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số cho các trường số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa vaccine: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVaccine_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVaccine.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvVaccine.SelectedRows[0];
                txtTenVC.Text = row.Cells["colTenVC"].Value?.ToString() ?? "";
                txtMoTaVC.Text = "";
                txtSoMuiToiDaVC.Text = row.Cells["colSoMuiToiDa"].Value?.ToString() ?? "0";
                txtSoThangChoVC.Text = row.Cells["colSoThangCho"].Value?.ToString() ?? "0";
                txtGiaBanVC.Text = row.Cells["colGiaBan"].Value?.ToString() ?? "0";
                txtSoLuongTonVC.Text = row.Cells["colSoLuongTon"].Value?.ToString() ?? "0";

                // Set combo box value
                string maLoai = row.Cells["colMaLoaiVC"].Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(maLoai))
                {
                    cboMaLoaiVC.SelectedValue = maLoai;
                }
                else
                {
                    cboMaLoaiVC.SelectedIndex = -1;
                }

                // Load full vaccine details including MoTa
                try
                {
                    string maVC = row.Cells["colMaVC"].Value?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(maVC))
                    {
                        VaccineDTO vaccine = vaccineBLL.GetVaccineByMaVC(maVC);
                        if (vaccine != null)
                        {
                            txtMoTaVC.Text = vaccine.MoTa;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Ignore error when loading details
                    Console.WriteLine("Error loading vaccine details: " + ex.Message);
                }
            }
        }

        private void btnLamMoiVC_Click(object sender, EventArgs e)
        {
            ClearVaccineInputs();
            LoadVaccine();
            txtTimKiemVC.Clear();
        }

        private void ClearVaccineInputs()
        {
            txtTenVC.Clear();
            txtMoTaVC.Clear();
            txtSoMuiToiDaVC.Clear();
            txtSoThangChoVC.Clear();
            txtGiaBanVC.Clear();
            txtSoLuongTonVC.Clear();
            cboMaLoaiVC.SelectedIndex = -1;
            dgvVaccine.ClearSelection();
        }
        #endregion
    }
}
