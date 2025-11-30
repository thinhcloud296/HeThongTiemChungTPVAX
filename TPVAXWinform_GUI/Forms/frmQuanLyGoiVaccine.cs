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
    public partial class frmQuanLyGoiVaccine : Form
    {
        private GoiVaccineBLL goiVaccineBLL = new GoiVaccineBLL();
        private DataTable dtGoiVaccine;

        public frmQuanLyGoiVaccine()
        {
            InitializeComponent();
        }

        private void frmQuanLyGoiVaccine_Load(object sender, EventArgs e)
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

            LoadGoiVaccine();
        }

        private void LoadGoiVaccine()
        {
            dtGoiVaccine = goiVaccineBLL.GetData();
            BindDataToGrid(dtGoiVaccine);
        }

        private void BindDataToGrid(DataTable dt)
        {
            dgvGoiVaccine.AutoGenerateColumns = false;

            colMaGoi.DataPropertyName = "MaGoi";
            colTenGoi.DataPropertyName = "TenGoi";
            colMoTa.DataPropertyName = "MoTa";
            colDoiTuong.DataPropertyName = "DoiTuongApDung";
            colGiaGoi.DataPropertyName = "GiaGoi";
            colTrangThai.DataPropertyName = "TrangThai";

            dgvGoiVaccine.DataSource = dt;

            // Format giá gói
            dgvGoiVaccine.Columns["colGiaGoi"].DefaultCellStyle.Format = "N0";
            dgvGoiVaccine.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgvGoiVaccine.RowTemplate.Height = 40;
        }

        private void ApplyFilter()
        {
            if (dtGoiVaccine == null) return;

            DataView dv = dtGoiVaccine.DefaultView;

            if (!string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                string searchText = txtTimKiem.Text.Trim().Replace("'", "''");
                dv.RowFilter = $"TenGoi LIKE '%{searchText}%' OR MaGoi LIKE '%{searchText}%'";
            }
            else
            {
                dv.RowFilter = "";
            }

            dgvGoiVaccine.DataSource = dv.ToTable();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnThemGoi_Click(object sender, EventArgs e)
        {
            frmThemGoiVaccine frm = new frmThemGoiVaccine();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadGoiVaccine();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvGoiVaccine_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvGoiVaccine.ClearSelection();
                dgvGoiVaccine.Rows[e.RowIndex].Selected = true;
                dgvGoiVaccine.CurrentCell = dgvGoiVaccine.Rows[e.RowIndex].Cells[0];

                contextMenuGoi.Show(dgvGoiVaccine, dgvGoiVaccine.PointToClient(Cursor.Position));
            }
        }

        private void menuXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvGoiVaccine.SelectedRows.Count > 0)
            {
                string maGoi = dgvGoiVaccine.SelectedRows[0].Cells["colMaGoi"].Value?.ToString();
                string tenGoi = dgvGoiVaccine.SelectedRows[0].Cells["colTenGoi"].Value?.ToString();

                if (!string.IsNullOrEmpty(maGoi))
                {
                    frmChiTietGoiVaccine frm = new frmChiTietGoiVaccine(maGoi, tenGoi);
                    frm.ShowDialog();
                }
            }
        }
    }
}
