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
using TPVAXWinform_GUI.Forms;

namespace TPVAXWinform_GUI.UserControls
{
    public partial class VaccineControl : UserControl
    {
        private VaccineBLL vaccineBLL = new VaccineBLL();
        private LoaiBenhBLL loaiBenhBLL = new LoaiBenhBLL();
        private LoaiVaccineBLL loaiVaccineBLL = new LoaiVaccineBLL();
        private DataTable dtVaccines;

        public VaccineControl()
        {
            InitializeComponent();
        }

        private void VaccineControl_Load(object sender, EventArgs e)
        {
            LoadVaccines();
            LoadFilters();
        }

        private void LoadVaccines()
        {
            dtVaccines = vaccineBLL.GetDataVaccineDetail();
            BindDataToGrid(dtVaccines);
        }

        private void BindDataToGrid(DataTable dt)
        {
            dgvVaccine.AutoGenerateColumns = false;
            colMaVC.DataPropertyName = "MaVC";
            colTenVC.DataPropertyName = "TenVC";
            colLoaiBenh.DataPropertyName = "CacBenhPhongNgua";
            colGiaBan.DataPropertyName = "GiaBan";
            colSoLuongTon.DataPropertyName = "SoLuongTonThucTe";
            colMaLoai.DataPropertyName = "TenLoaiVaccine";
            colSoMuiToiDa.DataPropertyName = "SoMuiToiDa";
            colMoTa.DataPropertyName = "MoTaVaccine";
            dgvVaccine.DataSource = dt;

            // Format giá bán
            dgvVaccine.Columns["colGiaBan"].DefaultCellStyle.Format = "N0";

            // Căn giữa các cột theo yêu cầu: Mã Vaccine, Giá bán, Số lượng tồn, Mã loại, Số mũi
            string[] centerColumns = { "colMaVC", "colGiaBan", "colSoLuongTon", "colTongSoLuongTon", "colMaLoai", "colSoMuiToiDa" };
            foreach (var name in centerColumns)
            {
                if (dgvVaccine.Columns[name] != null)
                    dgvVaccine.Columns[name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void LoadFilters()
        {
            // Load loại vaccine
            DataTable dtLoaiVaccine = loaiVaccineBLL.GetData();
            DataRow allVaccineRow = dtLoaiVaccine.NewRow();
            allVaccineRow["MaLoai"] = "";
            allVaccineRow["TenLoai"] = "-- Tất cả --";
            dtLoaiVaccine.Rows.InsertAt(allVaccineRow, 0);

            cboLoaiVaccine.DataSource = dtLoaiVaccine;
            cboLoaiVaccine.DisplayMember = "TenLoai";
            cboLoaiVaccine.ValueMember = "MaLoai";
            cboLoaiVaccine.SelectedIndex = 0;

            // Load loại bệnh
            DataTable dtLoaiBenh = loaiBenhBLL.GetData();
            DataRow allBenhRow = dtLoaiBenh.NewRow();
            allBenhRow["MaLoaiBenh"] = "";
            allBenhRow["TenBenh"] = "-- Tất cả --";
            dtLoaiBenh.Rows.InsertAt(allBenhRow, 0);

            cboLoaiBenh.DataSource = dtLoaiBenh;
            cboLoaiBenh.DisplayMember = "TenBenh";
            cboLoaiBenh.ValueMember = "MaLoaiBenh";
            cboLoaiBenh.SelectedIndex = 0;
        }

        private void ApplyFilters()
        {
            if (dtVaccines == null) return;

            DataView dv = dtVaccines.DefaultView;
            List<string> filters = new List<string>();

            // Filter by loại vaccine
            if (cboLoaiVaccine.SelectedValue != null && !string.IsNullOrEmpty(cboLoaiVaccine.SelectedValue.ToString()))
            {
                string maLoai = cboLoaiVaccine.SelectedValue.ToString();
                filters.Add($"MaLoai = '{maLoai}'");
            }

            // Filter by search text
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                string searchText = txtSearch.Text.Trim().Replace("'", "''");
                filters.Add($"(TenVC LIKE '%{searchText}%' OR MaVC LIKE '%{searchText}%')");
            }

            // Filter by price range
            if (numGiaMin.Value > 0 || numGiaMax.Value > 0)
            {
                if (numGiaMin.Value > 0 && numGiaMax.Value > 0)
                {
                    filters.Add($"GiaBan >= {numGiaMin.Value} AND GiaBan <= {numGiaMax.Value}");
                }
                else if (numGiaMin.Value > 0)
                {
                    filters.Add($"GiaBan >= {numGiaMin.Value}");
                }
                else if (numGiaMax.Value > 0)
                {
                    filters.Add($"GiaBan <= {numGiaMax.Value}");
                }
            }

            // Apply filter
            if (filters.Count > 0)
            {
                dv.RowFilter = string.Join(" AND ", filters);
            }
            else
            {
                dv.RowFilter = "";
            }

            dgvVaccine.DataSource = dv.ToTable();
        }

        private void dgvVaccine_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Tô màu cho cột TỒN THỰC TẾ
            if (dgvVaccine.Columns[e.ColumnIndex].Name == colSoLuongTon.Name) // Dùng .Name
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    int soLuongThucTe = Convert.ToInt32(e.Value);
                    if (soLuongThucTe == 0)
                    {
                        e.CellStyle.BackColor = Color.FromArgb(255, 224, 178); // Cam nhạt
                        e.CellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        // Reset lại màu nếu nó > 0 (tránh lỗi tô màu khi scroll)
                        e.CellStyle.BackColor = dgvVaccine.DefaultCellStyle.BackColor;
                        e.CellStyle.ForeColor = dgvVaccine.DefaultCellStyle.ForeColor;
                    }
                }
            }
        }

        private void btnQuanLyDanhMuc_Click(object sender, EventArgs e)
        {
            frmQuanLyDanhMuc frm = new frmQuanLyDanhMuc();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadFilters();
            }
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboLoaiVaccine.SelectedIndex = 0;
            cboLoaiBenh.SelectedIndex = 0;
            numGiaMin.Value = 0;
            numGiaMax.Value = 0;
            LoadVaccines();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cboLoaiVaccine_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cboLoaiBenh_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void numGia_ValueChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }
    }
}
