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

namespace TPVAXWinform_GUI.UserControls
{
    public partial class KhachHangControl : UserControl
    {
        private DataTable dtRecords;
        private KhachHangBLL _bll = new KhachHangBLL();
        private int selectedRowIndex = -1;

        public KhachHangControl()
        {
            InitializeComponent();
            ConfigureDataGridViewStyling();
            SetupContextMenu();
            LoadDSKHHG();
        }

        private void ConfigureDataGridViewStyling()
        {
            // Header style
            var headerStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = System.Drawing.Color.FromArgb(41, 128, 185),
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Padding = new Padding(5)
            };
            dgvKhachHang.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvKhachHang.ColumnHeadersHeight = 45;
            dgvKhachHang.EnableHeadersVisualStyles = false;

            // Căn giữa các cột: Mã KH, Ngày sinh, Giới tính, Số ĐT
            string[] centerColumns = { "colMaKH", "colNgaySinh", "colGioiTinh", "colSoDT" };
            foreach (var name in centerColumns)
            {
                if (dgvKhachHang.Columns[name] != null)
                    dgvKhachHang.Columns[name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Đổi font chữ thành không in đậm
            dgvKhachHang.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            dgvKhachHang.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            dgvKhachHang.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvKhachHang.RowTemplate.Height = 40;
            dgvKhachHang.BorderStyle = BorderStyle.None;
            dgvKhachHang.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvKhachHang.GridColor = System.Drawing.Color.FromArgb(224, 224, 224);
            dgvKhachHang.RowHeadersVisible = false;
        }

        private void SetupContextMenu()
        {
            // Tạo Context Menu
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Menu item: Xem thông tin
            ToolStripMenuItem viewInfoItem = new ToolStripMenuItem("📄 Xem thông tin");
            viewInfoItem.Click += ViewInfo_Click;
            contextMenu.Items.Add(viewInfoItem);

            // Menu item: Sửa thông tin
            ToolStripMenuItem editInfoItem = new ToolStripMenuItem("✏️ Sửa thông tin");
            editInfoItem.Click += EditInfo_Click;
            contextMenu.Items.Add(editInfoItem);

            // Gán context menu cho DataGridView
            dgvKhachHang.ContextMenuStrip = contextMenu;

            // Xử lý sự kiện mở context menu để lấy thông tin dòng được chọn
            dgvKhachHang.MouseDown += DgvKhachHang_MouseDown;
        }

        private void DgvKhachHang_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dgvKhachHang.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    // Chọn dòng được click chuột phải
                    dgvKhachHang.ClearSelection();
                    dgvKhachHang.Rows[hitTest.RowIndex].Selected = true;
                    selectedRowIndex = hitTest.RowIndex;
                }
                else
                {
                    selectedRowIndex = -1;
                }
            }
        }

        private void ViewInfo_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0 && selectedRowIndex < dgvKhachHang.Rows.Count)
            {
                string maKH = dgvKhachHang.Rows[selectedRowIndex].Cells["colMaKH"].Value?.ToString() ?? "";
                if (dtRecords.PrimaryKey == null || dtRecords.PrimaryKey.Length == 0)
                    dtRecords.PrimaryKey = new[] { dtRecords.Columns["MaKH"] };
                DataRow dr = dtRecords.Rows.Find(maKH);
                string hoTen = dr["HoTen"]?.ToString() ?? "";
                string gioiTinh = dr["GioiTinh"]?.ToString() ?? "";
                string cccd = dr["CCCD"]?.ToString() ?? "";
                var valNgaySinh = dr["NgaySinh"];
                string ngaySinh =
              valNgaySinh is DateTime dt ? dt.ToString("dd/MM/yyyy") :
               DateTime.TryParse(valNgaySinh?.ToString(), out var d) ? d.ToString("dd/MM/yyyy") :
  "";

                string soDT = dgvKhachHang.Rows[selectedRowIndex].Cells["colSoDT"].Value?.ToString() ?? "";

                MessageBox.Show(
                    "📋 THÔNG TIN KHÁCH HÀNG\n\n" +
                       $"Mã khách hàng: {maKH}\n" +
                 $"Họ tên: {hoTen}\n" +
              $"Giới tính: {gioiTinh}\n" +
                   $"Ngày sinh: {ngaySinh}\n" +
                   $"CCCD: {cccd}\n" +
                      $"Số điện thoại: {soDT}\n",
                            "Thông tin khách hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditInfo_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0 && selectedRowIndex < dgvKhachHang.Rows.Count)
            {
                string maKH = dgvKhachHang.Rows[selectedRowIndex].Cells["colMaKH"].Value?.ToString() ?? "";
                string hoTen = dgvKhachHang.Rows[selectedRowIndex].Cells["colHoTen"].Value?.ToString() ?? "";

                MessageBox.Show(
                $"Chỉnh sửa thông tin khách hàng:\n\nMã KH: {maKH}\nKhách hàng: {hoTen}\n\n(Chức năng sẽ được phát triển sau)",
                    "Sửa thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BindDataToGrid(DataTable dt)
        {
            dgvKhachHang.AutoGenerateColumns = false;

            colMaKH.DataPropertyName = "MaKH";
            colHoTen.DataPropertyName = "HoTen";
            colGioiTinh.DataPropertyName = "GioiTinh";
            colNgaySinh.DataPropertyName = "NgaySinh";
            colSoDT.DataPropertyName = "SoDT";

            dgvKhachHang.DataSource = dt;
            dgvKhachHang.Columns["colNgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvKhachHang.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            dgvKhachHang.RowTemplate.Height = 36;
        }

        private void LoadDSKHHG()
        {
            // Tạo DataTable với dữ liệu mẫu
            dtRecords = new DataTable();
            dtRecords = _bll.GetData();

            BindDataToGrid(dtRecords);
        }
    }
}
