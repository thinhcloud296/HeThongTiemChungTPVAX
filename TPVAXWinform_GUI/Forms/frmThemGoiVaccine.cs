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
    public partial class frmThemGoiVaccine : Form
    {
        private VaccineBLL vaccineBLL = new VaccineBLL();
        private GoiVaccineBLL goiVaccineBLL = new GoiVaccineBLL();
        private ChiTietGoiVaccineBLL chiTietGoiVaccineBLL = new ChiTietGoiVaccineBLL();

        private DataTable dtVaccines;
        private DataTable dtDanhSachChon; // Danh sách vaccine đã chọn

        public frmThemGoiVaccine()
        {
            InitializeComponent();
            InitDanhSachChon();
        }

        private void InitDanhSachChon()
        {
            dtDanhSachChon = new DataTable();
            dtDanhSachChon.Columns.Add("MaVC", typeof(string));
            dtDanhSachChon.Columns.Add("TenVC", typeof(string));
            dtDanhSachChon.Columns.Add("GiaBan", typeof(decimal));
            dtDanhSachChon.Columns.Add("SoMui", typeof(int));
            dtDanhSachChon.Columns.Add("GhiChu", typeof(string));
            dtDanhSachChon.PrimaryKey = new DataColumn[] { dtDanhSachChon.Columns["MaVC"] };
        }

        private void frmThemGoiVaccine_Load(object sender, EventArgs e)
        {
            // Set form size to 90% of screen and center it
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = (int)(screenWidth * 0.9);
            this.Height = (int)(screenHeight * 0.9);
            this.Location = new Point(
                (screenWidth - this.Width) / 2,
                (screenHeight - this.Height) / 2
            );

            LoadVaccines();
            BindDanhSachChon();
        }

        private void LoadVaccines()
        {
            dtVaccines = vaccineBLL.GetDataVaccineDetail();
            BindVaccinesToGrid(dtVaccines);
        }

        private void BindVaccinesToGrid(DataTable dt)
        {
            dgvVaccine.AutoGenerateColumns = false;
            colMaVC.DataPropertyName = "MaVC";
            colTenVC.DataPropertyName = "TenVC";
            colLoaiBenh.DataPropertyName = "CacBenhPhongNgua";
            colGiaBan.DataPropertyName = "GiaBan";
            colSoMuiToiDa.DataPropertyName = "SoMuiToiDa";
            dgvVaccine.DataSource = dt;

            dgvVaccine.Columns["colGiaBan"].DefaultCellStyle.Format = "N0";
            dgvVaccine.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
        }

        private void BindDanhSachChon()
        {
            dgvDanhSachChon.AutoGenerateColumns = false;
            colChonMaVC.DataPropertyName = "MaVC";
            colChonTenVC.DataPropertyName = "TenVC";
            colChonGiaBan.DataPropertyName = "GiaBan";
            colChonSoMui.DataPropertyName = "SoMui";
            colChonGhiChu.DataPropertyName = "GhiChu";
            dgvDanhSachChon.DataSource = dtDanhSachChon;

            dgvDanhSachChon.Columns["colChonGiaBan"].DefaultCellStyle.Format = "N0";
            dgvDanhSachChon.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            UpdateTongGia();
        }

        private void UpdateTongGia()
        {
            decimal tongGia = 0;
            foreach (DataRow row in dtDanhSachChon.Rows)
            {
                decimal gia = Convert.ToDecimal(row["GiaBan"]);
                int soMui = Convert.ToInt32(row["SoMui"]);
                tongGia += gia * soMui;
            }
            lblTongGiaValue.Text = tongGia.ToString("N0") + " đ";
        }

        private void ApplyFilter()
        {
            if (dtVaccines == null) return;

            DataView dv = dtVaccines.DefaultView;

            if (!string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                string searchText = txtTimKiem.Text.Trim().Replace("'", "''");
                dv.RowFilter = $"TenVC LIKE '%{searchText}%' OR MaVC LIKE '%{searchText}%'";
            }
            else
            {
                dv.RowFilter = "";
            }

            dgvVaccine.DataSource = dv.ToTable();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnThemVaoDS_Click(object sender, EventArgs e)
        {
            if (dgvVaccine.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn vaccine để thêm vào danh sách.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow row in dgvVaccine.SelectedRows)
            {
                string maVC = row.Cells["colMaVC"].Value?.ToString();
                string tenVC = row.Cells["colTenVC"].Value?.ToString();
                decimal giaBan = Convert.ToDecimal(row.Cells["colGiaBan"].Value ?? 0);
                int soMuiToiDa = Convert.ToInt32(row.Cells["colSoMuiToiDa"].Value ?? 1);

                // Kiểm tra đã tồn tại chưa
                DataRow existingRow = dtDanhSachChon.Rows.Find(maVC);
                if (existingRow != null)
                {
                    MessageBox.Show($"Vaccine '{tenVC}' đã có trong danh sách.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    continue;
                }

                // Thêm vào danh sách
                DataRow newRow = dtDanhSachChon.NewRow();
                newRow["MaVC"] = maVC;
                newRow["TenVC"] = tenVC;
                newRow["GiaBan"] = giaBan;
                newRow["SoMui"] = soMuiToiDa > 0 ? soMuiToiDa : 1;
                newRow["GhiChu"] = "";
                dtDanhSachChon.Rows.Add(newRow);
            }

            UpdateTongGia();
        }

        private void btnXoaKhoiDS_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachChon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn vaccine để xóa khỏi danh sách.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow row in dgvDanhSachChon.SelectedRows)
            {
                string maVC = row.Cells["colChonMaVC"].Value?.ToString();
                DataRow existingRow = dtDanhSachChon.Rows.Find(maVC);
                if (existingRow != null)
                {
                    dtDanhSachChon.Rows.Remove(existingRow);
                }
            }

            UpdateTongGia();
        }

        private void btnLuuGoi_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtTenGoi.Text))
            {
                MessageBox.Show("Vui lòng nhập tên gói vaccine.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenGoi.Focus();
                return;
            }

            if (dtDanhSachChon.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một vaccine vào gói.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tạo mã gói mới từ BLL (format: GVAC000001)
                string maGoi = goiVaccineBLL.GenerateMaGoi();

                // Tính tổng giá gói
                decimal giaGoi = 0;
                foreach (DataRow row in dtDanhSachChon.Rows)
                {
                    decimal gia = Convert.ToDecimal(row["GiaBan"]);
                    int soMui = Convert.ToInt32(row["SoMui"]);
                    giaGoi += gia * soMui;
                }

                // Tạo gói vaccine
                GoiVaccineDTO goiVaccine = new GoiVaccineDTO
                {
                    MaGoi = maGoi,
                    TenGoi = txtTenGoi.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim(),
                    DoiTuongApDung = txtDoiTuong.Text.Trim(),
                    GiaGoi = giaGoi,
                    TrangThai = "Hoạt động"
                };

                goiVaccineBLL.Insert(goiVaccine);

                // Thêm chi tiết gói vaccine
                foreach (DataRow row in dtDanhSachChon.Rows)
                {
                    // Sinh mã chi tiết từ BLL (format: CTGV000001)
                    string maCTGoi = chiTietGoiVaccineBLL.GenerateMaCTGoi();
                    ChiTietGoiVaccineDTO chiTiet = new ChiTietGoiVaccineDTO
                    {
                        MaCTGoi = maCTGoi,
                        MaGoi = maGoi,
                        MaVC = row["MaVC"].ToString(),
                        SoMui = Convert.ToInt32(row["SoMui"]),
                        GhiChu = row["GhiChu"].ToString()
                    };
                    chiTietGoiVaccineBLL.Insert(chiTiet);
                }

                MessageBox.Show("Thêm gói vaccine thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm gói vaccine: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgvDanhSachChon_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Cập nhật tổng giá khi sửa số mũi
            if (dgvDanhSachChon.Columns[e.ColumnIndex].Name == "colChonSoMui")
            {
                UpdateTongGia();
            }
        }
    }
}
