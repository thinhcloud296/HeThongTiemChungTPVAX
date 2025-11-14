using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using TPVAXWinform_BLL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_GUI
{
    public partial class frmThemMuiTiem : Form
    {
        private DataTable dtVC;
        private DataTable dtGoiVC;
        private VaccineBLL vaccineBLL = new VaccineBLL();
        private LichTiemBLL lichTiemBLL = new LichTiemBLL();
        private GoiVaccineBLL goiVaccineBLL = new GoiVaccineBLL();
        private HoaDonBLL hoaDonBLL = new HoaDonBLL();
        private ChiTietHoaDonBLL chiTietHoaDonBLL = new ChiTietHoaDonBLL();
        private ChiTietGoiVaccineBLL chiTietGoiVaccineBLL = new ChiTietGoiVaccineBLL();
        private string MaHSTC;
        private string MaKHHSTC;
        public frmThemMuiTiem()
        {
            InitializeComponent();
            InitializeFormSize();
            InitializeDataGridViewStyling();
            SetupContextMenuVaccine();
            InitializeActionButtons();
            InitializeLoaiThem();
        }


        public frmThemMuiTiem(string maHSTC, string hoTen, string gioiTinh, string ngaySinh, string maKH, string tenKH, string quanHe, string soDTKH)
        {
            InitializeComponent();
            InitializeFormSize();
            InitializeDataGridViewStyling();
            SetupContextMenuVaccine();
            InitializeActionButtons();
            InitializeLoaiThem();
            lbMaHSTC.Text = maHSTC;
            lblTenNguoiTiemValue.Text = hoTen;
            lblGioiTinhValue.Text = gioiTinh;
            lblNgaySinhValue.Text = ngaySinh;

            lblTenKhachHangValue.Text = tenKH;
            lblQuanHeValue.Text = quanHe;
            lblSoDTValue.Text = soDTKH;

            MaKHHSTC = maKH;
            MaHSTC = maHSTC;
        }

        private void InitializeLoaiThem()
        {
            // Đặt giá trị mặc định là "Vaccine"
            cboLoaiThem.SelectedIndex = 0;

            // Gán event handler cho sự kiện SelectedIndexChanged
            cboLoaiThem.SelectedIndexChanged += cboLoaiThem_SelectedIndexChanged;

            // Ẩn dgvGoiVaccine mặc định (hiển thị dgvVaccine)
            dgvGoiVaccine.Visible = false;
            dgvVaccine.Visible = true;

            btnThemGoiVaccine.Visible = false;
            btnThemMuiTiem.Visible = true;
            btnLuuGoi.Visible = false;
        }

        private void cboLoaiThem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiThem.SelectedIndex == 0) // Vaccine
            {
                dgvVaccine.Visible = true;
                dgvGoiVaccine.Visible = false;

                btnThemGoiVaccine.Visible = false;
                btnThemMuiTiem.Visible = true;
                btnLuuGoi.Visible = false;
            }
            else if (cboLoaiThem.SelectedIndex == 1) // Gói Vaccine
            {
                dgvVaccine.Visible = false;
                dgvGoiVaccine.Visible = true;

                btnThemGoiVaccine.Visible = true;
                btnThemMuiTiem.Visible = false;
                btnLuuGoi.Visible = true;

            }
        }

        private void frmThemMuiTiem_Load(object sender, EventArgs e)
        {
            LoadDSGoiVC();
            LoadDSVC();
            LoadCboTimKiem();
            dgvVaccineWait.CellContentClick += dgvVaccineWait_CellContentClick;
        }
        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            dgvVaccineWait.Rows.Clear();
            LoadDSGoiVC();
            LoadDSVC();
            btnLuuGoi.Visible = false;
            btnLuuTatCa.Visible = true;
            // Reset các combobox bộ lọc
            if (cboLoaiBenh.Items.Count > 0)
            {
                cboLoaiBenh.SelectedIndex = 0;
            }

            if (cboLoaiVaccine.Items.Count > 0)
            {
                cboLoaiVaccine.SelectedIndex = 0;
            }

            txtGhiChu.Clear();
            txtSoLuong.Text = "1";
            dtpNgayTiem.Value = DateTime.Now;

            if (cboLoaiThem.SelectedIndex != 0)
            {
                cboLoaiThem.SelectedIndex = 0;
            }

            CapNhatTongGiaVaTongSoMui();
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
        public void InitializeActionButtons()
        {
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

            dgvGoiVaccine.ColumnHeadersDefaultCellStyle = headerStyle;
            dgvGoiVaccine.ColumnHeadersHeight = 45;
            dgvGoiVaccine.EnableHeadersVisualStyles = false;

            // Căn giữa các cột: Mã Vaccine, Nước sản xuất, Giá bán
            string[] centerColumns = { "colMaVC", "colNuocSX", "colGiaBan" };
            foreach (var name in centerColumns)
            {
                if (dgvVaccine.Columns[name] != null)
                    dgvVaccine.Columns[name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvGoiVaccine.Columns["MaGoi"] != null)
                dgvGoiVaccine.Columns["MaGoi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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

            dgvGoiVaccine.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgvGoiVaccine.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvGoiVaccine.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvGoiVaccine.RowTemplate.Height = 40;
            dgvGoiVaccine.BorderStyle = BorderStyle.None;
            dgvGoiVaccine.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvGoiVaccine.GridColor = Color.FromArgb(224, 224, 224);
            dgvGoiVaccine.RowHeadersVisible = false;
            dgvGoiVaccine.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgvGoiVaccine.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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

            // Căn giữa TẤT CẢ các cột trừ "Tên Vaccine" và "Giá bán"
            string[] centerColumns = { "colMaVCW", "colLoaiBenhW", "colLoaiVCW", "colNgayTiemW", "colSoLuongW", "colNuocSXW" };
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
            // --- BƯỚC 1: LẤY DỮ LIỆU TỪ NGUỒN (dgvVaccine) ---
            // (Giả sử bạn đã sửa proc để trả về SoMuiToiDa)
            DataRowView drv;
            string maVC, tenVC, nuocSX, loaiBenh, loaiVC;
            decimal giaBan;

            try
            {
                drv = (DataRowView)dgvVaccine.Rows[rowIndex].DataBoundItem;
                maVC = drv["Mã Vaccine"].ToString();
                tenVC = drv["Tên Vaccine"].ToString();
                nuocSX = drv["Nước sản xuất"].ToString();
                loaiBenh = drv["Loại bệnh"].ToString();
                loaiVC = drv["Loại Vaccine"].ToString();
                giaBan = Convert.ToDecimal(drv["Giá bán"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc dữ liệu hàng được chọn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // --- BƯỚC 2: LẤY DỮ LIỆU NHẬP LIỆU ---
            string ngayTiem = dtpNgayTiem.Value.ToString();
            string ghiChu = txtGhiChu.Text.Trim();
            int soLuongMoi;
            if (!int.TryParse(txtSoLuong.Text, out soLuongMoi) || soLuongMoi <= 0)
            {
                soLuongMoi = 1;
            }

            // --- BƯỚC 3: KIỂM TRA TRÙNG LẶP TRONG BẢNG CHỜ ---
            foreach (DataGridViewRow row in dgvVaccineWait.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["colMaVCW"].Value != null && row.Cells["colMaVCW"].Value.ToString() == maVC)
                {
                    MessageBox.Show($"Vaccine '{tenVC}' đã có trong danh sách chờ.", "Đã tồn tại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            // --- BƯỚC 4: (LOGIC VALIDATION ĐÃ BỊ XÓA THEO YÊU CẦU CỦA BẠN) ---
            // Khối code kiểm tra soMuiToiDa đã được xóa.

            // --- BƯỚC 5: THÊM VÀO BẢNG CHỜ (dgvVaccineWait) ---
            try
            {
                // (Đảm bảo dgvVaccineWait có đúng 9 cột và đúng thứ tự)
                dgvVaccineWait.Rows.Add(
                    maVC,
                    tenVC,
                    loaiBenh,
                    loaiVC,
                    ngayTiem,
                    soLuongMoi,
                    nuocSX,
                    giaBan.ToString("N0"), // Format giá
                    ghiChu
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm vào danh sách chờ: " + ex.Message +
                    "\nHãy đảm bảo số lượng cột trong dgvVaccineWait (Designer) khớp với 9 cột đang thêm.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // HÀM CLICK ĐÃ SỬA LẠI (GỌN HƠN)
        private void btnThemMuiTiem_Click(object sender, EventArgs e)
        {
            if (dgvVaccine.SelectedRows.Count > 0)
            {
                // Lấy index của dòng đang được chọn
                int rowIndex = dgvVaccine.SelectedRows[0].Index;

                // Gọi hàm chính (Không cần load lại dtVC, dtLT)
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

        private void CapNhatTongGiaVaTongSoMui()
        {
            decimal tongGia = 0;
            int soMui = 0;

            // Lặp qua tất cả các hàng trong bảng "chờ lưu"
            foreach (DataGridViewRow row in dgvVaccineWait.Rows)
            {
                // Kiểm tra xem hàng có dữ liệu không
                if (row.Cells["colGiaBanW"].Value != null &&
                    row.Cells["colSoLuongW"].Value != null)
                {
                    try
                    {
                        // Lấy giá và số lượng
                        decimal giaBan = Convert.ToDecimal(row.Cells["colGiaBanW"].Value);
                        int soLuong = Convert.ToInt32(row.Cells["colSoLuongW"].Value);

                        // Cộng dồn
                        tongGia += (giaBan * soLuong);
                        soMui += soLuong;
                    }
                    catch (Exception ex)
                    {
                        // Bỏ qua nếu có lỗi (ví dụ: hàng đang được thêm)
                        Console.WriteLine("Lỗi tính tổng: " + ex.Message);
                    }
                }
            }

            // Cập nhật Label Tổng giá
            // "N0" sẽ định dạng số (vd: 1,200,000)
            lbTongGia.Text = tongGia.ToString("N0") + " đ";
            lbTongSoMui.Text = soMui + " Mũi";
        }


        private void dgvVaccineWait_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CapNhatTongGiaVaTongSoMui();
        }

        private void dgvVaccineWait_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CapNhatTongGiaVaTongSoMui();
        }

        private void btnLuuTatCa_Click(object sender, EventArgs e)
        {
            if (dgvVaccineWait.Rows.Count <= 0)
            {
                MessageBox.Show("Không có mũi tiêm nào trong danh sách chờ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int countSuccess = 0;
            int countFailed = 0;

            foreach (DataGridViewRow row in dgvVaccineWait.Rows)
            {
                // Lấy thông tin chung từ hàng
                int soMui = Convert.ToInt32(row.Cells["colSoLuongW"].Value);
                DateTime ngayHen = Convert.ToDateTime(row.Cells["colNgayTiemW"].Value);
                string ghiChu = row.Cells["colGhiChuW"].Value.ToString();
                string maVC = row.Cells["colMaVCW"].Value.ToString();
                string maHSTC_HienTai = MaHSTC;

                try
                {
                    if (soMui > 1) // LỖI 2: Dùng if-else
                    {
                        // Xử lý phác đồ nhiều mũi (nhưng cùng 1 ngày hẹn)
                        for (int i = 0; i < soMui; i++)
                        {
                            // LỖI 1: Tạo DTO MỚI bên trong vòng lặp
                            LichTiemDTO lichTiem = new LichTiemDTO();
                            lichTiem.MaLT = lichTiemBLL.CreateNewMaLT();
                            lichTiem.NgayHenTiem = ngayHen;
                            lichTiem.NgayTiemThucTe = null;
                            lichTiem.SoMui = i + 1; // LỖI 3: Sửa thành i + 1
                            lichTiem.TrangThai = false; // 0 = Chưa tiêm
                            lichTiem.GhiChu = ghiChu;
                            lichTiem.MaHSTC = maHSTC_HienTai;
                            lichTiem.MaVC = maVC;

                            lichTiemBLL.Insert(lichTiem);
                            countSuccess++;
                        }
                    }
                    else // LỖI 2: Dùng if-else
                    {
                        // Xử lý 1 mũi
                        // LỖI 1: Tạo DTO MỚI
                        LichTiemDTO lichTiem = new LichTiemDTO();
                        lichTiem.MaLT = lichTiemBLL.CreateNewMaLT();
                        lichTiem.NgayHenTiem = ngayHen;
                        lichTiem.NgayTiemThucTe = null;
                        lichTiem.SoMui = 1;
                        lichTiem.TrangThai = false;
                        lichTiem.GhiChu = ghiChu;
                        lichTiem.MaHSTC = maHSTC_HienTai;
                        lichTiem.MaVC = maVC;

                        lichTiemBLL.Insert(lichTiem);
                        countSuccess++;
                    }
                }
                catch (Exception ex)
                {
                    countFailed++;
                    MessageBox.Show($"Lỗi khi thêm vaccine {maVC}: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } // Hết vòng lặp foreach

            // Thông báo kết quả
            if (countSuccess > 0)
            {
                MessageBox.Show($"Đã thêm thành công {countSuccess} lịch hẹn.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Báo cho form cha biết
                this.Close(); // Đóng form
            }
            else if (countFailed > 0)
            {
                MessageBox.Show($"Thêm thất bại {countFailed} lịch hẹn.", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // ========================================================== Gói Vaccine
        private void LoadDSGoiVC()
        {
            dtGoiVC = goiVaccineBLL.GetData();
            BindDataToGridGoiVaccine(dtGoiVC);
        }

        private void BindDataToGridGoiVaccine(DataTable dt)
        {
            dgvGoiVaccine.AutoGenerateColumns = false;
            colMaGoi.DataPropertyName = "MaGoi";
            colTenGoi.DataPropertyName = "TenGoi";
            colDoiTuongApDung.DataPropertyName = "DoiTuongApDung";
            colGiaGoi.DataPropertyName = "GiaGoi";

            dgvGoiVaccine.DataSource = dt;

            dgvGoiVaccine.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            dgvGoiVaccine.RowTemplate.Height = 36;
        }

        private void btnThemGoiVaccine_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có gói nào được chọn không
            if (dgvGoiVaccine.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một gói vaccine từ danh sách.", "Chưa chọn gói", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Lấy thông tin gói được chọn
                int selectedRowIndex = dgvGoiVaccine.SelectedRows[0].Index;
                string maGoi = dgvGoiVaccine.Rows[selectedRowIndex].Cells["colMaGoi"].Value?.ToString()?.Trim() ?? "";
                string tenGoi = dgvGoiVaccine.Rows[selectedRowIndex].Cells["colTenGoi"].Value?.ToString() ?? "";

                if (string.IsNullOrEmpty(maGoi))
                {
                    MessageBox.Show("Không thể lấy mã gói vaccine.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy danh sách vaccine trong gói
                DataTable dtVaccinesInPackage = chiTietGoiVaccineBLL.GetVaccinesByGoiVaccine(maGoi);

                if (dtVaccinesInPackage.Rows.Count == 0)
                {
                    MessageBox.Show($"Gói '{tenGoi}' không có vaccine nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Lấy thông tin nhập liệu
                string ngayTiem = dtpNgayTiem.Value.ToString();
                string ghiChuChung = txtGhiChu.Text.Trim();

                int countAdded = 0;
                int countSkipped = 0;

                // Thêm từng vaccine vào dgvVaccineWait
                foreach (DataRow row in dtVaccinesInPackage.Rows)
                {
                    string maVC = row["Mã Vaccine"].ToString();
                    string tenVC = row["Tên Vaccine"].ToString();
                    string loaiBenh = row["Loại bệnh"].ToString();
                    string loaiVC = row["Loại Vaccine"].ToString();
                    string nuocSX = row["Nước sản xuất"].ToString();
                    decimal giaBan = Convert.ToDecimal(row["Giá bán"]);
                    int soMui = row["Số mũi"] != DBNull.Value ? Convert.ToInt32(row["Số mũi"]) : 1;
                    string ghiChuVaccine = row["Ghi chú"] != DBNull.Value ? row["Ghi chú"].ToString() : "";

                    // Kết hợp ghi chú của gói và ghi chú vaccine
                    string ghiChuFinal = string.IsNullOrEmpty(ghiChuVaccine) ? ghiChuChung :
             (string.IsNullOrEmpty(ghiChuChung) ? ghiChuVaccine : $"{ghiChuChung} - {ghiChuVaccine}");

                    // Kiểm tra trùng lặp
                    bool isDuplicate = false;
                    foreach (DataGridViewRow waitRow in dgvVaccineWait.Rows)
                    {
                        if (waitRow.IsNewRow) continue;
                        if (waitRow.Cells["colMaVCW"].Value != null &&
                            waitRow.Cells["colMaVCW"].Value.ToString() == maVC)
                        {
                            isDuplicate = true;
                            countSkipped++;
                            break;
                        }
                    }

                    if (!isDuplicate)
                    {
                        // Thêm vào danh sách chờ
                        dgvVaccineWait.Rows.Add(
                            maVC,
                            tenVC,
                            loaiBenh,
                            loaiVC,
                            ngayTiem,
                            soMui,
                            nuocSX,
                            giaBan.ToString("N0"),
                            ghiChuFinal
                        );
                        countAdded++;
                    }
                }

                // Xóa tất cả các hàng khác trong dgvGoiVaccine, chỉ giữ lại hàng đã chọn
                for (int i = dgvGoiVaccine.Rows.Count - 1; i >= 0; i--)
                {
                    if (i != selectedRowIndex && !dgvGoiVaccine.Rows[i].IsNewRow)
                    {
                        dgvGoiVaccine.Rows.RemoveAt(i);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm gói vaccine: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuuGoi_Click(object sender, EventArgs e)
        {
            if (dgvGoiVaccine.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một gói vaccine từ danh sách.", "Chưa chọn gói", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int selectedRowIndex = dgvGoiVaccine.SelectedRows[0].Index;
            string maGoi = dgvGoiVaccine.Rows[selectedRowIndex].Cells["colMaGoi"].Value?.ToString()?.Trim() ?? "";

            HoaDonDTO newHD = new HoaDonDTO();
            newHD.MaHD = hoaDonBLL.CreateNewMaHD();
            newHD.NgayLap = DateTime.Now;
            newHD.TongTien = Convert.ToDecimal(dgvGoiVaccine.Rows[selectedRowIndex].Cells["colGiaGoi"].Value);
            newHD.TrangThai = false;
            newHD.MaKH = MaKHHSTC;
            newHD.MaNV = null;
            newHD.MaKM = null;

            ChiTietHoaDonDTO NewCTHD = new ChiTietHoaDonDTO();
            NewCTHD.MaCTHD = chiTietHoaDonBLL.CreateNewMaCTHD();
            NewCTHD.SoLuong = 1;
            NewCTHD.DonGia = newHD.TongTien;
            NewCTHD.MaSanPham = maGoi;
            NewCTHD.LoaiSanPham = "GOIVACCINE";
            NewCTHD.MaHD = newHD.MaHD;
            try
            {
                hoaDonBLL.Insert(newHD);
                chiTietHoaDonBLL.Insert(NewCTHD);
                MessageBox.Show("Lưu gói vaccine vào hóa đơn thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu gói vaccine vào hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
