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
    public partial class frmInHoaDon : Form
    {
        private string maHD;
        private TPVAXWinform_BLL.HoaDonInBLL hoaDonInBLL;

        // Constructor nhận tham số MaHD
        public frmInHoaDon(string maHD)
        {
            InitializeComponent();
            this.maHD = maHD;
            this.hoaDonInBLL = new TPVAXWinform_BLL.HoaDonInBLL();
        }

        private void frmInHoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy dữ liệu từ BLL
                DataTable dtHoaDon = hoaDonInBLL.GetHoaDonInData(maHD);

                if (dtHoaDon == null || dtHoaDon.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu hóa đơn.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                // 2. Xóa DataSource cũ
                reportViewer1.LocalReport.DataSources.Clear();

                // 3. Tạo ReportDataSource với tên "DataSet1" (tên mặc định trong RDLC)
                ReportDataSource rds = new ReportDataSource("DataSet1", dtHoaDon);

                // 4. Xác định đường dẫn file RDLC
                // Thử nhiều đường dẫn có thể có
                string reportPath = GetReportPath();

                // Kiểm tra file tồn tại
                if (!File.Exists(reportPath))
                {
                    MessageBox.Show(
                    $"Không tìm thấy file báo cáo.\n\n",
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
                this.Text = $"In Hóa Đơn - {maHD}";
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
                Path.Combine(Application.StartupPath, "rptHoaDon.rdlc"),

                // 2. Từ thư mục project (đi lên 2 cấp từ bin\Debug)
                Path.Combine(Application.StartupPath, @"..\..\rptHoaDon.rdlc"),

                // 3. Từ thư mục project với đường dẫn tuyệt đối
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\rptHoaDon.rdlc"),

                // 4. Đường dẫn trực tiếp trong project (fallback)
                Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\rptHoaDon.rdlc"))
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
