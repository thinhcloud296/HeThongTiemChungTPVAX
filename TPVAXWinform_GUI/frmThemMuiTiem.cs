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

namespace TPVAXWinform_GUI
{
    public partial class frmThemMuiTiem : Form
    {
        private DataTable dtVC;
        private VaccineBLL vaccineBLL = new VaccineBLL();

        public frmThemMuiTiem()
        {
            InitializeComponent();
            InitializeFormSize();
            InitializeDataGridViewStyling();
            SetupContextMenuVaccine();
            InitializeActionButtons();
        }


        public frmThemMuiTiem(string maHSTC, string hoTen, string gioiTinh, string ngaySinh, string tenKH, string quanHe, string soDTKH)
        {
            InitializeComponent();
            InitializeFormSize();
            InitializeDataGridViewStyling();
            SetupContextMenuVaccine();
            InitializeActionButtons();
            lbMaHSTC.Text = maHSTC; 
            lblTenNguoiTiemValue.Text = hoTen;
            lblGioiTinhValue.Text = gioiTinh;
            lblNgaySinhValue.Text = ngaySinh;

            lblTenKhachHangValue.Text = tenKH;
            lblQuanHeValue.Text = quanHe; 
            lblSoDTValue.Text = soDTKH;
        }
        private void frmThemMuiTiem_Load(object sender, EventArgs e)
        {
            LoadDSVC();
            LoadCboTimKiem();
            dgvVaccineWait.CellContentClick += dgvVaccineWait_CellContentClick;
        }
        private void LoadCboTimKiem()
        {
            cboLoaiBenh.DataSource = null;
            cboLoaiVaccine.DataSource = null;   

            cboLoaiBenh.DataSource = new LoaiBenhBLL().GetData();
            cboLoaiVaccine.DataSource = new LoaiVaccineBLL().GetData();

            cboLoaiBenh.DisplayMember = "TenBenh";
            cboLoaiBenh.ValueMember = "MaLoaiBenh";
            cboLoaiVaccine.DisplayMember = "TenLoai";
            cboLoaiVaccine.ValueMember = "MaLoai";
        }
        public void InitializeActionButtons() {
            if (dgvVaccineWait.Columns["colXoaMuiTiem"] == null)
            {
                var btnEditColumn = new DataGridViewButtonColumn
                {
                    Name = "colXoaMuiTiem",
                    HeaderText = "Xóa mũi tiêm",
                    Text = "Xóa",
                    UseColumnTextForButtonValue = true,
                    Width = 80,
                    FlatStyle = FlatStyle.Flat
                };
                dgvVaccineWait.Columns.Add(btnEditColumn);
            }
        } 
        private void InitializeFormSize()
        {
            // Đặt form ở 90% kích thước màn hình
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
            int formWidth = (int)(screenBounds.Width * 0.9);
            int formHeight = (int)(screenBounds.Height * 0.9);

            this.Width = formWidth;
            this.Height = formHeight;

            // Căn giữa form
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeDataGridViewStyling()
        {
            // Styling cho dgvVaccine
            ConfigureDataGridViewVaccineStyling();

            // Styling cho dgvMuiTiemCho
            ConfigureDataGridViewMuiTiemChoStyling();
        }

        private void ConfigureDataGridViewVaccineStyling()
        {
            // Header style
            var headerStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Padding = new Padding(5)
            };
            dgvVaccine.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvVaccine.ColumnHeadersHeight = 45;
            dgvVaccine.EnableHeadersVisualStyles = false;

            // Căn giữa các cột: Mã Vaccine, Nước sản xuất, Giá bán
            string[] centerColumns = { "colMaVC", "colNuocSX", "colGiaBan" };
            foreach (var name in centerColumns)
            {
                if (dgvVaccine.Columns[name] != null)
                    dgvVaccine.Columns[name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Cột Tên Vaccine cho phép xuống dòng (Word Wrap)
            if (dgvVaccine.Columns["colTenVC"] != null)
            {
                dgvVaccine.Columns["colTenVC"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }

            // Styling chung
            dgvVaccine.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgvVaccine.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvVaccine.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvVaccine.RowTemplate.Height = 40;
            dgvVaccine.BorderStyle = BorderStyle.None;
            dgvVaccine.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvVaccine.GridColor = Color.FromArgb(224, 224, 224);
            dgvVaccine.RowHeadersVisible = false;
            dgvVaccine.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgvVaccine.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void ConfigureDataGridViewMuiTiemChoStyling()
        {
            // Header style
            var headerStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Padding = new Padding(5)
            };
            dgvVaccineWait.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvVaccineWait.ColumnHeadersHeight = 45;
            dgvVaccineWait.EnableHeadersVisualStyles = false;

            // Căn giữa các cột: Mã PN, Ngày tiêm
            string[] centerColumns = { "colMaVaccineWait", "colSoLuongWait", "colNgayTiemWait", "colNuocSXWait" };
            foreach (var name in centerColumns)
            {
                if (dgvVaccineWait.Columns[name] != null)
                    dgvVaccineWait.Columns[name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Styling chung
            dgvVaccineWait.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgvVaccineWait.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvVaccineWait.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvVaccineWait.RowTemplate.Height = 40;
            dgvVaccineWait.BorderStyle = BorderStyle.None;
            dgvVaccineWait.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvVaccineWait.GridColor = Color.FromArgb(224, 224, 224);
            dgvVaccineWait.RowHeadersVisible = false;
            dgvVaccineWait.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
        }

        private int selectedVaccineRowIndex = -1;

        private void SetupContextMenuVaccine()
        {
            // Tạo Context Menu cho dgvVaccine
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Font = new Font("Segoe UI", 10F);

            // Menu item: Xem thông tin vaccine
            ToolStripMenuItem viewVaccineInfoItem = new ToolStripMenuItem("📄 Xem thông tin vaccine");
            viewVaccineInfoItem.Click += ViewVaccineInfo_Click;
            contextMenu.Items.Add(viewVaccineInfoItem);

            // Menu item: Thêm vào danh sách
            ToolStripMenuItem addToListItem = new ToolStripMenuItem("➕ Thêm vào danh sách");
            addToListItem.Click += AddToList_Click;
            contextMenu.Items.Add(addToListItem);

            // Gán context menu cho DataGridView
            dgvVaccine.ContextMenuStrip = contextMenu;

            // Xử lý sự kiện MouseDown để lấy thông tin dòng được chọn
            dgvVaccine.MouseDown += DgvVaccine_MouseDown;
        }

        private void DgvVaccine_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dgvVaccine.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    // Chọn dòng được click chuột phải
                    dgvVaccine.ClearSelection();
                    dgvVaccine.Rows[hitTest.RowIndex].Selected = true;
                    selectedVaccineRowIndex = hitTest.RowIndex;
                }
                else
                {
                    selectedVaccineRowIndex = -1;
                }
            }
        }

        private void ViewVaccineInfo_Click(object sender, EventArgs e)
        {
            if (selectedVaccineRowIndex >= 0 && selectedVaccineRowIndex < dgvVaccine.Rows.Count)
            {
                string maVaccine = dgvVaccine.Rows[selectedVaccineRowIndex].Cells["colMaVC"].Value?.ToString() ?? "";
                string tenVaccine = dgvVaccine.Rows[selectedVaccineRowIndex].Cells["colTenVC"].Value?.ToString() ?? "";
                string loaiBenh = dgvVaccine.Rows[selectedVaccineRowIndex].Cells["colLoaiBenh"].Value?.ToString() ?? "";
                string loaiVaccine = dgvVaccine.Rows[selectedVaccineRowIndex].Cells["colLoaiVC"].Value?.ToString() ?? "";
                string nuocSX = dgvVaccine.Rows[selectedVaccineRowIndex].Cells["colNuocSX"].Value?.ToString() ?? "";
                string giaBan = dgvVaccine.Rows[selectedVaccineRowIndex].Cells["colGiaBan"].Value?.ToString() ?? "";

                MessageBox.Show(
                    "💉 THÔNG TIN VACCINE\n\n" +
                    $"Mã Vaccine: {maVaccine}\n" +
                    $"Tên Vaccine: {tenVaccine}\n" +
                    $"Loại bệnh: {loaiBenh}\n" +
                    $"Loại Vaccine: {loaiVaccine}\n" +
                    $"Nước sản xuất: {nuocSX}\n" +
                    $"Giá bán: {giaBan}\n",
                    "Thông tin Vaccine",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void AddToList_Click(object sender, EventArgs e)
        {
            // Dùng biến 'selectedVaccineRowIndex' đã được gán ở sự kiện MouseDown
            if (selectedVaccineRowIndex >= 0 && selectedVaccineRowIndex < dgvVaccine.Rows.Count)
            {
                AddSelectedVaccineToWaitList(selectedVaccineRowIndex);
            }
        }
        // ======================================================================================

        private void LoadDSVC()
        {
            dtVC = vaccineBLL.GetDataVaccineDetail();
            BindDataToGridVaccine(dtVC);
        }

        private void BindDataToGridVaccine(DataTable dt)
        {
            dgvVaccine.Columns["colMaVC"].DataPropertyName = "Mã Vaccine";
            dgvVaccine.Columns["colTenVC"].DataPropertyName = "Tên Vaccine";
            dgvVaccine.Columns["colLoaiBenh"].DataPropertyName = "Loại bệnh";
            dgvVaccine.Columns["colLoaiVC"].DataPropertyName = "Loại Vaccine";
            dgvVaccine.Columns["colNuocSX"].DataPropertyName = "Nước sản xuất";
            dgvVaccine.Columns["colGiaBan"].DataPropertyName = "Giá bán";

            // 3. Gán DataSource (phải gán SAU KHI set DataPropertyName)
            dgvVaccine.DataSource = dt;

            // (Code styling cũ của bạn giữ nguyên)
            dgvVaccine.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            dgvVaccine.RowTemplate.Height = 36;
        }

        private void AddSelectedVaccineToWaitList(int rowIndex)
        {
            // 1. Lấy dòng đã chọn từ dgvVaccine
            DataGridViewRow selectedRow = dgvVaccine.Rows[rowIndex];

            // 2. Đọc dữ liệu từ các cell của dgvVaccine
            //    Chúng ta đọc theo TÊN CỘT (colMaVC, colTenVC...)
            string maVC = selectedRow.Cells["colMaVC"].Value?.ToString();
            string tenVC = selectedRow.Cells["colTenVC"].Value?.ToString();
            string nuocSX = selectedRow.Cells["colNuocSX"].Value?.ToString();
            string loaiBenh = selectedRow.Cells["colLoaiBenh"].Value?.ToString();
            string loaiVC = selectedRow.Cells["colLoaiVC"].Value?.ToString();
            string giaBan = selectedRow.Cells["colGiaBan"].Value?.ToString();
            string soluong = "1";
            string ngaytiem = dtpNgayTiem.Value.ToString("dd-MM-yyyy");
            // 3. Kiểm tra trùng lặp trong dgvVaccineWait

            //foreach (DataGridViewRow row in dgvVaccineWait.Rows)
            //{
            //    if (row.IsNewRow) continue;

            //    if (row.Cells["colMaVaccineW"].Value != null && row.Cells["colMaVaccineW"].Value.ToString() == maVC)
            //    {
            //        MessageBox.Show($"Vaccine '{tenVC}' đã có trong danh sách chờ.", "Đã tồn tại", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return; // Dừng lại vì đã tồn tại
            //    }
            //}

            // Thêm dữ liệu vào dgvVaccineWait
            try
            {
                dgvVaccineWait.Rows.Add(
                    maVC,
                    tenVC,
                    loaiBenh,
                    loaiVC,
                    ngaytiem,
                    soluong, 
                    nuocSX,
                    giaBan
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm mũi tiêm: " + ex.Message +
                    "\nHãy đảm bảo thứ tự cột và tên cột trong dgvVaccineWait đã chính xác.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnThemMuiTiem_Click(object sender, EventArgs e)
        {
            if (dgvVaccine.SelectedRows.Count > 0)
            {
                // Lấy index của dòng đang được chọn
                int rowIndex = dgvVaccine.SelectedRows[0].Index;
                AddSelectedVaccineToWaitList(rowIndex);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một vaccine từ danh sách.", "Chưa chọn vaccine", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void dgvVaccineWait_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Đảm bảo click không phải là header (e.RowIndex < 0)
            // 2. Đảm bảo click vào cột có tên "colXoaMuiTiem"
            if (e.RowIndex >= 0 && dgvVaccineWait.Columns[e.ColumnIndex].Name == "colXoaMuiTiem")
            {
                // 3. (Tùy chọn) Đảm bảo đó không phải là dòng "NewRow" (dòng trống ở cuối)
                if (dgvVaccineWait.Rows[e.RowIndex].IsNewRow)
                {
                    return;
                }

                // 4. Lấy tên vaccine để hỏi cho chắc chắn (giả sử tên vaccine ở cột 1)
                //    (Nếu thứ tự cột của bạn khác, hãy thay số 1 bằng index của cột Tên Vaccine)
                string tenVaccine = dgvVaccineWait.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "mũi tiêm này";

                // 5. Hỏi xác nhận
                DialogResult confirm = MessageBox.Show(
                    $"Bạn có chắc muốn xóa '{tenVaccine}' khỏi danh sách chờ?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                // 6. Nếu người dùng đồng ý, xóa dòng
                if (confirm == DialogResult.Yes)
                {
                    // Vì dgvVaccineWait đang ở chế độ Unbound (không có DataSource)
                    // chúng ta có thể xóa trực tiếp khỏi Rows collection
                    dgvVaccineWait.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
    }
}
