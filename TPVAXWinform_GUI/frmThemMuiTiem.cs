using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using System.Globalization;
using TPVAXWinform_BLL;
using TPVAXWinform_DTO;

namespace TPVAXWinform_GUI
{
    public partial class frmThemMuiTiem : Form
    {
        private DataTable dtVC;
        private DataTable dtLT;
        private VaccineBLL vaccineBLL = new VaccineBLL();
        private LichTiemBLL lichTiemBLL = new LichTiemBLL();
        private string MaHSTC;
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
            MaHSTC = maHSTC;
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
            DataRowView drv;
            string maVC, tenVC, nuocSX, loaiBenh, loaiVC;
            decimal giaBan;
            int soMuiToiDa;

            try
            {
                // Lấy DataRowView (dòng dữ liệu gốc) từ hàng được chọn
                drv = (DataRowView)dgvVaccine.Rows[rowIndex].DataBoundItem;

                // Đọc dữ liệu bằng tên cột (Alias) của Stored Procedure
                maVC = drv["Mã Vaccine"].ToString();
                tenVC = drv["Tên Vaccine"].ToString();
                nuocSX = drv["Nước sản xuất"].ToString();
                loaiBenh = drv["Loại bệnh"].ToString();
                loaiVC = drv["Loại Vaccine"].ToString();
                giaBan = Convert.ToDecimal(drv["Giá bán"]);
                soMuiToiDa = Convert.ToInt32(drv["SoMuiToiDa"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc dữ liệu hàng được chọn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // --- BƯỚC 2: LẤY DỮ LIỆU NHẬP LIỆU ---
            string ngayTiem = dtpNgayTiem.Value.ToString();
            string ghiChu = txtGhiChu.Text.Trim();
            int soLuongMoi; // Số lượng mũi người dùng muốn thêm (từ textbox)

            if (!int.TryParse(txtSoLuong.Text, out soLuongMoi) || soLuongMoi <= 0)
            {
                soLuongMoi = 1; // Mặc định là 1 nếu nhập sai hoặc rỗng
            }

            // --- BƯỚC 3: KIỂM TRA TRÙNG LẶP TRONG BẢNG CHỜ ---
            foreach (DataGridViewRow row in dgvVaccineWait.Rows)
            {
                if (row.IsNewRow) continue;
                // (Giả sử cột mã VC trong bảng chờ là "colMaVCW")
                if (row.Cells["colMaVCW"].Value != null && row.Cells["colMaVCW"].Value.ToString() == maVC)
                {
                    MessageBox.Show($"Vaccine '{tenVC}' đã có trong danh sách chờ.", "Đã tồn tại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            // --- BƯỚC 4: VALIDATION PHÁC ĐỒ TIÊM (Logic đúng) ---

            // Bỏ qua nếu là loại tiêm nhắc lại (99)
            if (soMuiToiDa != 99)
            {
                // Đếm số mũi ĐÃ TIÊM (từ CSDL)
                // (Bạn cần có hàm DemSoMuiDaTiem trong LichTiemBLL)
                int soMuiDaTiem = lichTiemBLL.SoMuiDaTiem(MaHSTC, maVC); // Giả sử MaHSTC lưu ở Label

                // Đếm số mũi ĐANG CHỜ (trong Grid dgvVaccineWait)
                int soMuiDangCho = lichTiemBLL.SoMuiDangChoTiem(MaHSTC,maVC);
                
                // So sánh tổng
                int tongMuiSeTiem = soMuiDaTiem + soMuiDangCho + soLuongMoi;

                if (tongMuiSeTiem > soMuiToiDa)
                {
                    MessageBox.Show(
                        $"Số lượng mũi tiêm vượt quá phác đồ!\n\n" +
                        $"- Vaccine: {tenVC}\n" +
                        $"- Phác đồ tối đa: {soMuiToiDa} mũi\n" +
                        $"- Đã tiêm (CSDL): {soMuiDaTiem} mũi\n" +
                        $"- Đang chờ (Grid): {soMuiDangCho} mũi\n" +
                        $"-> Bạn chỉ có thể thêm tối đa: {soMuiToiDa - soMuiDaTiem - soMuiDangCho} mũi.",
                        "Lỗi Phác Đồ Tiêm",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }

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

                // CapNhatTongGia(); // Gọi hàm tính tổng giá
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
                            lichTiem.TrangThai = "0"; // 0 = Chưa tiêm
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
                        lichTiem.TrangThai = "0";
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
    }
}
