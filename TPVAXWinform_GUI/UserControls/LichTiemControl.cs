using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPVAXWinform_GUI.UserControls
{
    public partial class LichTiemControl : UserControl
    {
        public LichTiemControl()
        {
            InitializeComponent();
            InitializeEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            // Gán event handler để tô màu các dòng theo trạng thái
            dgvLichTiem.CellFormatting += dgvLichTiem_CellFormatting;
            
            // Thiết lập giá trị mặc định cho ComboBox
            cboTrangThai.SelectedIndex = 0; // "Tất cả"
        }

        private void dgvLichTiem_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra nếu có dữ liệu trong dòng
            if (e.RowIndex >= 0 && dgvLichTiem.Rows[e.RowIndex].Cells["colTrangThai"].Value != null)
            {
                string trangThai = dgvLichTiem.Rows[e.RowIndex].Cells["colTrangThai"].Value.ToString();
    
                // Tô màu theo trạng thái
                if (trangThai == "Chưa tiêm")
                {
                    // Màu cam nhạt
                    dgvLichTiem.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 178);
                }
                else if (trangThai == "Đã tiêm")
                {
                    // Màu xanh lá nhạt
                    dgvLichTiem.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);
                }
            }
        }
    }
}
