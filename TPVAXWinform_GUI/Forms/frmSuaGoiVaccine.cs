using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TPVAXWinform_BLL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_GUI.Forms
{
    public partial class frmSuaGoiVaccine : Form
    {
        private VaccineBLL vaccineBLL = new VaccineBLL();
        private GoiVaccineBLL goiVaccineBLL = new GoiVaccineBLL();
        private ChiTietGoiVaccineBLL chiTietGoiVaccineBLL = new ChiTietGoiVaccineBLL();

        private DataTable dtVaccines;
        private DataTable dtDanhSachChon;
        private string _maGoi;

        public frmSuaGoiVaccine(string maGoi)
        {
            InitializeComponent();
            _maGoi = maGoi;
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

        private void frmSuaGoiVaccine_Load(object sender, EventArgs e)
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = (int)(screenWidth * 0.9);
            this.Height = (int)(screenHeight * 0.9);
            this.Location = new Point((screenWidth - this.Width) / 2, (screenHeight - this.Height) / 2);

            LoadVaccines();
            LoadGoiVaccineInfo();
            BindDanhSachChon();
        }

        private void LoadGoiVaccineInfo()
        {
            DataTable dtGoi = goiVaccineBLL.GetData();
            DataRow[] rows = dtGoi.Select($"MaGoi = '{_maGoi}'");
            if (rows.Length > 0)
            {
                txtTenGoi.Text = rows[0]["TenGoi"]?.ToString() ?? "";
                txtMoTa.Text = rows[0]["MoTa"]?.ToString() ?? "";
                txtDoiTuong.Text = rows[0]["DoiTuongApDung"]?.ToString() ?? "";
            }

            // Load danh sách vaccine trong gói
            DataTable dtChiTiet = chiTietGoiVaccineBLL.GetVaccinesByGoiVaccine(_maGoi);
            foreach (DataRow row in dtChiTiet.Rows)
            {
                DataRow newRow = dtDanhSachChon.NewRow();
                newRow["MaVC"] = row["MaVC"];
                newRow["TenVC"] = row["TenVC"];
                newRow["GiaBan"] = row["GiaBan"];
                newRow["SoMui"] = row["SoMui"] != DBNull.Value ? row["SoMui"] : 1;
                newRow["GhiChu"] = row["GhiChu"]?.ToString() ?? "";
                dtDanhSachChon.Rows.Add(newRow);
            }
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
                MessageBox.Show("Vui lòng chọn vaccine để thêm vào danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow row in dgvVaccine.SelectedRows)
            {
                string maVC = row.Cells["colMaVC"].Value?.ToString();
                string tenVC = row.Cells["colTenVC"].Value?.ToString();
                decimal giaBan = Convert.ToDecimal(row.Cells["colGiaBan"].Value ?? 0);
                int soMuiToiDa = Convert.ToInt32(row.Cells["colSoMuiToiDa"].Value ?? 1);

                DataRow existingRow = dtDanhSachChon.Rows.Find(maVC);
                if (existingRow != null)
                {
                    MessageBox.Show($"Vaccine '{tenVC}' đã có trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    continue;
                }

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
                MessageBox.Show("Vui lòng chọn vaccine để xóa khỏi danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (string.IsNullOrWhiteSpace(txtTenGoi.Text))
            {
                MessageBox.Show("Vui lòng nhập tên gói vaccine.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenGoi.Focus();
                return;
            }

            if (dtDanhSachChon.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một vaccine vào gói.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                decimal giaGoi = 0;
                foreach (DataRow row in dtDanhSachChon.Rows)
                {
                    decimal gia = Convert.ToDecimal(row["GiaBan"]);
                    int soMui = Convert.ToInt32(row["SoMui"]);
                    giaGoi += gia * soMui;
                }

                GoiVaccineDTO goiVaccine = new GoiVaccineDTO
                {
                    MaGoi = _maGoi,
                    TenGoi = txtTenGoi.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim(),
                    DoiTuongApDung = txtDoiTuong.Text.Trim(),
                    GiaGoi = giaGoi,
                    TrangThai = "Hoạt động"
                };

                goiVaccineBLL.Edit(goiVaccine);

                // Xóa chi tiết cũ và thêm mới
                chiTietGoiVaccineBLL.DeleteByMaGoi(_maGoi);

                foreach (DataRow row in dtDanhSachChon.Rows)
                {
                    string maCTGoi = chiTietGoiVaccineBLL.GenerateMaCTGoi();
                    ChiTietGoiVaccineDTO chiTiet = new ChiTietGoiVaccineDTO
                    {
                        MaCTGoi = maCTGoi,
                        MaGoi = _maGoi,
                        MaVC = row["MaVC"].ToString(),
                        SoMui = Convert.ToInt32(row["SoMui"]),
                        GhiChu = row["GhiChu"].ToString()
                    };
                    chiTietGoiVaccineBLL.Insert(chiTiet);
                }

                MessageBox.Show("Cập nhật gói vaccine thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật gói vaccine: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgvDanhSachChon_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDanhSachChon.Columns[e.ColumnIndex].Name == "colChonSoMui")
            {
                UpdateTongGia();
            }
        }
    }
}
