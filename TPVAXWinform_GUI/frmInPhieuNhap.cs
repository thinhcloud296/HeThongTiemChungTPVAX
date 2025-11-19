using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace TPVAXWinform_GUI
{
    public partial class frmInPhieuNhap : Form
    {
        private string maPN;
        private TPVAXWinform_BLL.PhieuNhapInBLL phieuNhapInBLL;

        // Constructor nhận tham số MaPN
        public frmInPhieuNhap(string maPN)
        {
            InitializeComponent();
            this.maPN = maPN;
            this.phieuNhapInBLL = new TPVAXWinform_BLL.PhieuNhapInBLL();
        }

        private void frmInPhieuNhap_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy dữ liệu từ BLL
                DataTable dtPhieuNhap = phieuNhapInBLL.GetPhieuNhapInData(maPN);

                if (dtPhieuNhap == null || dtPhieuNhap.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu phiếu nhập.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                // 2. Xóa DataSource cũ
                reportViewer1.LocalReport.DataSources.Clear();

                // 3. Tạo ReportDataSource với tên "DataSet1" (tên trong RDLC)
                ReportDataSource rds = new ReportDataSource("DataSet1", dtPhieuNhap);

                // 4. Xác định đường dẫn file RDLC
                string reportPath = GetReportPath();

                // Kiểm tra file tồn tại
                if (!File.Exists(reportPath))
                {
                    MessageBox.Show(
                    $"Không tìm thấy file báo cáo.\n\n" +
                    $"Đường dẫn đã tìm: {reportPath}\n\n" +
                    $"Hướng dẫn khắc phục:\n" +
                    $"1. Nhấn chuột phải vào file 'rptPhieuNhap.rdlc' trong Solution Explorer\n" +
                    $"2. Chọn Properties\n" +
                    $"3. Set 'Copy to Output Directory' = 'Copy always'\n" +
                    $"4. Build lại project",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // 5. Set ReportPath
                reportViewer1.LocalReport.ReportPath = reportPath;

                // 6. Thêm DataSource
                reportViewer1.LocalReport.DataSources.Add(rds);

                // 7. Refresh Report
                reportViewer1.RefreshReport();

                // Thiết lập thêm cho form
                this.Text = $"In Phiếu Nhập - {maPN}";
                this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo:\n{ex.Message}",
                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        /// <summary>
        /// Tìm đường dẫn file RDLC từ nhiều vị trí có thể
        /// </summary>
        private string GetReportPath()
        {
            // Danh sách các đường dẫn có thể
            string[] possiblePaths = new string[]
            {
                // 1. Từ thư mục bin\Debug
                Path.Combine(Application.StartupPath, "rptPhieuNhap.rdlc"),

                // 2. Từ thư mục project (đi lên 2 cấp từ bin\Debug)
                Path.Combine(Application.StartupPath, @"..\..\rptPhieuNhap.rdlc"),

                // 3. Từ thư mục project với đường dẫn tuyệt đối
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\rptPhieuNhap.rdlc"),

                // 4. Đường dẫn trực tiếp trong project (fallback)
                Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\rptPhieuNhap.rdlc"))
            };

            // Tìm đường dẫn đầu tiên tồn tại
            foreach (string path in possiblePaths)
            {
                string fullPath = Path.GetFullPath(path);
                if (File.Exists(fullPath))
                {
                    return fullPath;
                }
            }

            // Nếu không tìm thấy, trả về đường dẫn mặc định
            return possiblePaths[0];
        }
    }
}
