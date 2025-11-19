using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Transactions; // Đảm bảo bạn đã thêm using này
using System.Windows.Forms;
using TPVAXWinform_BLL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_GUI
{
    public partial class frmThemMuiTiem : Form
    {
        // ... (Các biến BLL của bạn giữ nguyên) ...
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
        private decimal TongGia = 0;

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
            cboLoaiThem.SelectedIndex = 0;
            cboLoaiThem.SelectedIndexChanged += cboLoaiThem_SelectedIndexChanged;

            // --- SỬA: GỌI HÀM CẬP NHẬT UI ---
            UpdateUIVisibility(true); // Hiển thị UI cho Mũi Lẻ (Vaccine)
        }

        // --- SỬA: TÁCH LOGIC HIỂN THỊ UI RA HÀM RIÊNG ---
        private void UpdateUIVisibility(bool isVaccineMode)
        {
            // Hiển thị/ẩn các DataGridView
            dgvVaccine.Visible = isVaccineMode;
            dgvGoiVaccine.Visible = !isVaccineMode;

            // Hiển thị/ẩn các nút thêm vào danh sách chờ
            // (Nút "Thêm gói" bị loại bỏ vì gây nhầm lẫn, người dùng sẽ nhấn "Lưu Gói")
            btnThemGoiVaccine.Visible = false; // <-- LUÔN ẨN
            btnThemMuiTiem.Visible = isVaccineMode;

            // Hiển thị/ẩn các nút LƯU (Quan trọng nhất)
            // Hai nút này không bao giờ được xuất hiện cùng lúc
            btnLuuGoi.Visible = !isVaccineMode;
            btnLuuTatCa.Visible = isVaccineMode;
        }

        private void cboLoaiThem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiThem.SelectedIndex == 0) // Vaccine (Mũi Lẻ)
            {
                UpdateUIVisibility(true);
            }
            else if (cboLoaiThem.SelectedIndex == 1) // Gói Vaccine
            {
                UpdateUIVisibility(false);
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

            if (cboLoaiBenh.Items.Count > 0) cboLoaiBenh.SelectedIndex = 0;
            if (cboLoaiVaccine.Items.Count > 0) cboLoaiVaccine.SelectedIndex = 0;

            txtGhiChu.Clear();
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

        // ... (Các hàm InitializeFormSize, InitializeDataGridViewStyling, Configure...Styling giữ nguyên) ...
        // ... (Các hàm SetupContextMenuVaccine, DgvVaccine_MouseDown, ViewVaccineInfo_Click, AddToList_Click giữ nguyên) ...

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

            // Căn giữa TẤT CẢ các cột trừ "Tên Vaccine"
            string[] centerColumns = { "colMaVCW", "colLoaiBenhW", "colLoaiVCW", "colNgayTiemW", "colSoLuongW", "colNuocSXW" };
            foreach (var name in centerColumns)
            {
                if (dgvVaccineWait.Columns[name] != null)
                    dgvVaccineWait.Columns[name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // --- SỬA: Thêm định dạng cho cột Giá Bán W ---
            if (dgvVaccineWait.Columns["colGiaBanW"] != null)
            {
                dgvVaccineWait.Columns["colGiaBanW"].DefaultCellStyle.Format = "N0";
                dgvVaccineWait.Columns["colGiaBanW"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

                // --- SỬA: Đọc giá trị decimal và format ---
                decimal giaBanDecimal = 0;
                var giaBanObj = dgvVaccine.Rows[selectedVaccineRowIndex].Cells["colGiaBan"].Value;
                if (giaBanObj != null && giaBanObj != DBNull.Value)
                {
                    giaBanDecimal = Convert.ToDecimal(giaBanObj);
                }
                string giaBan = giaBanDecimal.ToString("N0") + " VNĐ";
                // --- KẾT THÚC SỬA ---

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
            if (selectedVaccineRowIndex >= 0 && selectedVaccineRowIndex < dgvVaccine.Rows.Count)
            {
                AddSelectedVaccineToWaitList(selectedVaccineRowIndex);
            }
        }

        // ======================================================================================

        private void LoadDSVC()
        {
            dtVC = vaccineBLL.GetDataVaccine_SingleDose();
            BindDataToGridVaccine(dtVC);
        }

        private void BindDataToGridVaccine(DataTable dt)
        {
            dgvVaccine.Columns["colMaVC"].DataPropertyName = "MaVC";
            dgvVaccine.Columns["colTenVC"].DataPropertyName = "TenVC";
            dgvVaccine.Columns["colLoaiBenh"].DataPropertyName = "CacBenhPhongNgua";
            dgvVaccine.Columns["colLoaiVC"].DataPropertyName = "TenLoaiVaccine";
            dgvVaccine.Columns["colNuocSX"].DataPropertyName = "Nước sản xuất";
            dgvVaccine.Columns["colGiaBan"].DataPropertyName = "GiaBan";

            // --- SỬA: Thêm 2 cột tồn kho ---
            // (Bạn cần thêm 2 cột 'colSoLuongTonThucTe' và 'colTongSoLuongTon' vào dgvVaccine trong Designer)
            if (dgvVaccine.Columns.Contains("colSoLuongTonThucTe"))
                dgvVaccine.Columns["colSoLuongTonThucTe"].DataPropertyName = "SoLuongTonThucTe";
            // --- KẾT THÚC SỬA ---

            dgvVaccine.DataSource = dt;

            dgvVaccine.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            dgvVaccine.RowTemplate.Height = 36;
        }

        private void AddSelectedVaccineToWaitList(int rowIndex)
        {
            DataRowView drv;
            string maVC, tenVC, nuocSX, loaiBenh, loaiVC;
            decimal giaBan;

            try
            {
                drv = (DataRowView)dgvVaccine.Rows[rowIndex].DataBoundItem;
                maVC = drv["MaVC"].ToString();
                tenVC = drv["TenVC"].ToString();
                nuocSX = drv["Nước sản xuất"].ToString();
                loaiBenh = drv["CacBenhPhongNgua"].ToString();
                loaiVC = drv["TenLoaiVaccine"].ToString();
                giaBan = Convert.ToDecimal(drv["GiaBan"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc dữ liệu hàng được chọn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string ngayTiem = dtpNgayTiem.Value.ToString("dd/MM/yyyy"); // --- SỬA: Định dạng ngày tháng
            string ghiChu = txtGhiChu.Text.Trim();

            foreach (DataGridViewRow row in dgvVaccineWait.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["colMaVCW"].Value != null && row.Cells["colMaVCW"].Value.ToString() == maVC)
                {
                    MessageBox.Show($"Vaccine '{tenVC}' đã có trong danh sách chờ.", "Đã tồn tại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            try
            {
                // --- SỬA: Lỗi Crash ToString("N0") ---
                dgvVaccineWait.Rows.Add(
                    maVC,
                    tenVC,
                    loaiBenh,
                    loaiVC,
                    ngayTiem,
                    1, // Mũi lẻ luôn là 1
                    nuocSX,
                    giaBan, // <-- SỬA: Lưu giá trị decimal thô
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


        private void btnThemMuiTiem_Click(object sender, EventArgs e)
        {
            if (dtpNgayTiem.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Ngày tiêm không được nhỏ hơn ngày hiện tại.", "Ngày tiêm không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvVaccine.SelectedRows.Count > 0)
            {
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
            if (e.RowIndex >= 0 && dgvVaccineWait.Columns[e.ColumnIndex].Name == "colXoaMuiTiem")
            {
                if (dgvVaccineWait.Rows[e.RowIndex].IsNewRow) return;

                // --- SỬA: Lấy tên bằng 'Name' thay vì index ---
                string tenVaccine = dgvVaccineWait.Rows[e.RowIndex].Cells["colTenVCW"].Value?.ToString() ?? "mũi tiêm này";

                DialogResult confirm = MessageBox.Show(
                    $"Bạn có chắc muốn xóa '{tenVaccine}' khỏi danh sách chờ?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.Yes)
                {
                    dgvVaccineWait.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void CapNhatTongGiaVaTongSoMui()
        {
            decimal tongGia = 0;
            int soMui = 0;

            foreach (DataGridViewRow row in dgvVaccineWait.Rows)
            {
                if (row.IsNewRow) continue; // Bỏ qua hàng mới

                if (row.Cells["colGiaBanW"].Value != null &&
                    row.Cells["colSoLuongW"].Value != null)
                {
                    try
                    {
                        // --- SỬA: Lỗi crash đã được sửa ở AddSelectedVaccineToWaitList ---
                        decimal giaBan = Convert.ToDecimal(row.Cells["colGiaBanW"].Value);
                        int soLuong = Convert.ToInt32(row.Cells["colSoLuongW"].Value);

                        tongGia += (giaBan * soLuong);
                        soMui += soLuong;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi tính tổng: " + ex.Message);
                    }
                }
            }

            TongGia = tongGia;
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
            if (dgvVaccineWait.Rows.Count == 0 || (dgvVaccineWait.Rows.Count == 1 && dgvVaccineWait.Rows[0].IsNewRow))
            {
                MessageBox.Show("Không có mũi tiêm nào trong danh sách chờ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maVC = dgvVaccineWait.Rows[0].Cells["colMaVCW"].Value.ToString();
            int soMui = Convert.ToInt32(dgvVaccineWait.Rows[0].Cells["colSoLuongW"].Value);
            DateTime ngayHen = DateTime.ParseExact(dgvVaccineWait.Rows[0].Cells["colNgayTiemW"].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string ghiChu = dgvVaccineWait.Rows[0].Cells["colGhiChuW"].Value.ToString();
            string maHSTC_HienTai = MaHSTC;
            decimal giaBan = Convert.ToDecimal(dgvVaccineWait.Rows[0].Cells["colGiaBanW"].Value);
            int soLuongTonThucTe = vaccineBLL.GetSoLuongTonThucTe(maVC);
            // Check tồn kho
            if (soLuongTonThucTe <= 0)
            {
                MessageBox.Show(
                    $"Vaccine {maVC} đã hết hàng (hoặc đã hết hạn)!\n" +
                    $"Số lượng có thể tiêm: {soLuongTonThucTe}",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    HoaDonDTO newHD = new HoaDonDTO();
                    newHD.MaHD = hoaDonBLL.CreateNewMaHD();
                    newHD.NgayLap = DateTime.Now;
                    newHD.TongTien = TongGia;
                    newHD.TrangThai = true;
                    newHD.MaKH = MaKHHSTC;
                    newHD.MaNV = null;
                    newHD.MaKM = null;
                    hoaDonBLL.Insert(newHD);

                    LichTiemDTO lichTiem = new LichTiemDTO();
                    lichTiem.MaLT = lichTiemBLL.CreateNewMaLT();
                    lichTiem.NgayHenTiem = ngayHen;
                    lichTiem.NgayTiemThucTe = DateTime.Now;
                    lichTiem.SoMui = soMui;
                    lichTiem.TrangThai = "Đã tiêm";
                    lichTiem.GhiChu = ghiChu;
                    lichTiem.MaHSTC = maHSTC_HienTai;
                    lichTiem.MaVC = maVC;

                    ChiTietHoaDonDTO NewCTHD = new ChiTietHoaDonDTO();
                    NewCTHD.MaCTHD = chiTietHoaDonBLL.CreateNewMaCTHD();
                    NewCTHD.SoLuong = soMui;
                    NewCTHD.DonGia = giaBan;
                    NewCTHD.MaSanPham = maVC;
                    NewCTHD.LoaiSanPham = "VACCINE";
                    NewCTHD.MaHD = newHD.MaHD;


                    vaccineBLL.UpdateSoLuongTon(maVC, -1);

                    lichTiemBLL.Insert(lichTiem);


                    chiTietHoaDonBLL.Insert(NewCTHD);


                    scope.Complete();

                    MessageBox.Show($"Đã thêm thành công lịch hẹn.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu. Toàn bộ thao tác đã được hủy.\nChi tiết: {ex.Message}",
                    "Lỗi Giao Dịch", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // --- SỬA: HÀM NÀY ĐÃ BỊ TẮT VISIBLE. LOGIC NÀY GÂY RA LỖI Ở ẢNH CHỤP MÀN HÌNH ---
        // (Tôi vẫn sửa lỗi 'giaBan' bên trong, phòng trường hợp bạn bật lại)
        private void btnThemGoiVaccine_Click(object sender, EventArgs e)
        {
            if (dgvGoiVaccine.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một gói vaccine từ danh sách.", "Chưa chọn gói", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int selectedRowIndex = dgvGoiVaccine.SelectedRows[0].Index;
                string maGoi = dgvGoiVaccine.Rows[selectedRowIndex].Cells["colMaGoi"].Value?.ToString()?.Trim() ?? "";
                string tenGoi = dgvGoiVaccine.Rows[selectedRowIndex].Cells["colTenGoi"].Value?.ToString() ?? "";

                if (string.IsNullOrEmpty(maGoi))
                {
                    MessageBox.Show("Không thể lấy mã gói vaccine.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataTable dtVaccinesInPackage = chiTietGoiVaccineBLL.GetVaccinesByGoiVaccine(maGoi);

                if (dtVaccinesInPackage.Rows.Count == 0)
                {
                    MessageBox.Show($"Gói '{tenGoi}' không có vaccine nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string ngayTiem = dtpNgayTiem.Value.ToString("dd/MM/yyyy"); // --- SỬA: Định dạng ngày tháng
                string ghiChuChung = txtGhiChu.Text.Trim();

                int countAdded = 0;
                int countSkipped = 0;

                foreach (DataRow row in dtVaccinesInPackage.Rows)
                {
                    // (Giả sử bạn đã sửa proc 'usp_GetVaccinesByGoiVaccine' như lần trước)
                    string maVC = row["MaVC"].ToString();
                    string tenVC = row["TenVC"].ToString();
                    string loaiBenh = row["CacBenhPhongNgua"].ToString();
                    string loaiVC = row["TenLoaiVaccine"].ToString();
                    string nuocSX = row["Nước sản xuất"].ToString();
                    decimal giaBan = Convert.ToDecimal(row["GiaBan"]);
                    int soMui = row["SoMui"] != DBNull.Value ? Convert.ToInt32(row["SoMui"]) : 1;
                    string ghiChuVaccine = row["GhiChu"] != DBNull.Value ? row["GhiChu"].ToString() : "";

                    string ghiChuFinal = string.IsNullOrEmpty(ghiChuVaccine) ? ghiChuChung :
                        (string.IsNullOrEmpty(ghiChuChung) ? ghiChuVaccine : $"{ghiChuChung} - {ghiChuVaccine}");

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
                        // --- SỬA: Lỗi Crash ToString("N0") ---
                        dgvVaccineWait.Rows.Add(
                            maVC,
                            tenVC,
                            loaiBenh,
                            loaiVC,
                            ngayTiem,
                            soMui,
                            nuocSX,
                            giaBan, // <-- SỬA: Lưu giá trị decimal thô
                            ghiChuFinal
                        );
                        countAdded++;
                    }
                }

                if (countAdded > 0)
                {
                    MessageBox.Show($"Đã thêm {countAdded} mũi tiêm từ gói '{tenGoi}'.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (countSkipped > 0)
                {
                    MessageBox.Show($"Đã bỏ qua {countSkipped} mũi tiêm (đã tồn tại trong danh sách chờ).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        // --- HÀM NÀY LÀ WORKFLOW ĐÚNG CHO GÓI VACCINE ---
        private void btnLuuGoi_Click(object sender, EventArgs e)
        {
            if (dgvGoiVaccine.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một gói vaccine từ danh sách.", "Chưa chọn gói", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- SỬA: Thêm kiểm tra ngày tiêm ---
            if (dtpNgayTiem.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Ngày tiêm không được nhỏ hơn ngày hiện tại.", "Ngày tiêm không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedRowIndex = dgvGoiVaccine.SelectedRows[0].Index;
            string maGoi = dgvGoiVaccine.Rows[selectedRowIndex].Cells["colMaGoi"].Value?.ToString()?.Trim() ?? "";

            // --- SỬA: Bọc trong TransactionScope ---
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    HoaDonDTO newHD = new HoaDonDTO();
                    newHD.MaHD = hoaDonBLL.CreateNewMaHD();
                    newHD.NgayLap = DateTime.Now;
                    newHD.TongTien = Convert.ToDecimal(dgvGoiVaccine.Rows[selectedRowIndex].Cells["colGiaGoi"].Value);
                    newHD.TrangThai = true;
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

                    hoaDonBLL.Insert(newHD);
                    chiTietHoaDonBLL.Insert(NewCTHD);

                    DateTime ngayHenChon = dtpNgayTiem.Value;
                    // (Hàm BLL này phải được sửa để nhận 'ngayHenChon' như lần trước)
                    int soLichHen = lichTiemBLL.TaoLichHenDauTienChoGoi(maGoi, MaHSTC, ngayHenChon);

                    scope.Complete(); // Hoàn tất giao dịch

                    MessageBox.Show($"Lưu gói vaccine và tạo {soLichHen} lịch hẹn đầu tiên thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                // Tự động rollback nếu lỗi
                MessageBox.Show("Lỗi khi lưu gói vaccine vào hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}