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
    public partial class frmChiTietGoiVaccine : Form
    {
        private string maGoi;
        private string tenGoi;
        private ChiTietGoiVaccineBLL chiTietGoiVaccineBLL = new ChiTietGoiVaccineBLL();

        public frmChiTietGoiVaccine(string maGoi, string tenGoi)
        {
            InitializeComponent();
            this.maGoi = maGoi;
            this.tenGoi = tenGoi;
        }

        private void frmChiTietGoiVaccine_Load(object sender, EventArgs e)
        {
            // Set form size to 80% of screen and center it
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = (int)(screenWidth * 0.8);
            this.Height = (int)(screenHeight * 0.8);
            this.Location = new Point(
                (screenWidth - this.Width) / 2,
                (screenHeight - this.Height) / 2
            );

            lblTenGoiValue.Text = tenGoi;
            lblMaGoiValue.Text = maGoi;

            LoadChiTietGoiVaccine();
        }

        private void LoadChiTietGoiVaccine()
        {
            try
            {
                DataTable dtChiTiet = chiTietGoiVaccineBLL.GetVaccinesByGoiVaccine(maGoi);
                BindDataToGrid(dtChiTiet);

                // Tính tổng giá
                decimal tongGia = 0;
                foreach (DataRow row in dtChiTiet.Rows)
                {
                    decimal giaBan = Convert.ToDecimal(row["GiaBan"]);
                    int soMui = Convert.ToInt32(row["SoMui"]);
                    tongGia += giaBan * soMui;
                }
                lblTongGiaValue.Text = tongGia.ToString("N0") + " đ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load chi tiết gói vaccine: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindDataToGrid(DataTable dt)
        {
            dgvChiTietGoi.AutoGenerateColumns = false;

            colMaVC.DataPropertyName = "MaVC";
            colTenVC.DataPropertyName = "TenVC";
            colLoaiBenh.DataPropertyName = "CacBenhPhongNgua";
            colLoaiVaccine.DataPropertyName = "TenLoaiVaccine";
            colNuocSX.DataPropertyName = "Nước sản xuất";
            colGiaBan.DataPropertyName = "GiaBan";
            colSoMui.DataPropertyName = "SoMui";
            colGhiChu.DataPropertyName = "GhiChu";

            dgvChiTietGoi.DataSource = dt;

            // Format giá bán
            dgvChiTietGoi.Columns["colGiaBan"].DefaultCellStyle.Format = "N0";
            dgvChiTietGoi.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgvChiTietGoi.RowTemplate.Height = 40;

            // Căn giữa một số cột
            string[] centerColumns = { "colMaVC", "colGiaBan", "colSoMui" };
            foreach (var name in centerColumns)
            {
                if (dgvChiTietGoi.Columns[name] != null)
                    dgvChiTietGoi.Columns[name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
