using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TPVAXWinform.UserControls
{
    public partial class HoSoTiemChungControl : UserControl
    {
        private DataTable dtRecords;

        public HoSoTiemChungControl()
        {
            InitializeComponent();
        }

        private void HoSoTiemChungControl_Load(object sender, EventArgs e)
        {
            InitializeFilters();
            LoadSampleData();
            SetupEventHandlers();
        }
        private void InitializeFilters()
        {
            // Set default values
            dtpFromDate.Value = DateTime.Now.AddMonths(-1);
            dtpToDate.Value = DateTime.Now;
            cboVaccine.SelectedIndex = 0;
            cboDoseNumber.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
        }

        private void SetupEventHandlers()
        {
            btnSearch.Click += BtnSearch_Click;
            btnReset.Click += BtnReset_Click;
            dgvRecords.CellContentClick += DgvRecords_CellContentClick;
        }

        private void LoadSampleData()
        {
            // Tạo DataTable với dữ liệu mẫu
            dtRecords = new DataTable();
            dtRecords.Columns.Add("RecordId", typeof(string));
            dtRecords.Columns.Add("CustomerId", typeof(string));
            dtRecords.Columns.Add("CustomerName", typeof(string));
            dtRecords.Columns.Add("VaccinationDate", typeof(DateTime));
            dtRecords.Columns.Add("VaccineName", typeof(string));
            dtRecords.Columns.Add("DoseNumber", typeof(string));
            dtRecords.Columns.Add("LotNumber", typeof(string));
            dtRecords.Columns.Add("Employee", typeof(string));
            dtRecords.Columns.Add("Status", typeof(string));

            // Thêm 20 bản ghi mẫu
            Random rnd = new Random();
            string[] statuses = { "Đã tiêm", "Chưa tiêm", "Đã hủy", "Đã tiêm", "Chưa tiêm" };
            string[] vaccines = { "Vaccine A", "Vaccine B", "Vaccine C", "Vaccine D" };
            string[] employees = { "Nguyễn Văn A", "Trần Thị B", "Lê Văn C", "Phạm Thị D" };

            for (int i = 1; i <= 20; i++)
            {
                dtRecords.Rows.Add(
                    $"HS{i:D4}",
                    $"KH{i:D4}",
                    $"Nguyễn Văn {(char)(64 + i)}",
                    DateTime.Now.AddDays(-rnd.Next(1, 60)),
                    vaccines[rnd.Next(vaccines.Length)],
                    $"Mũi {rnd.Next(1, 4)}",
                    $"LOT{DateTime.Now.Year}{rnd.Next(1000, 9999)}",
                    employees[rnd.Next(employees.Length)],
                    statuses[rnd.Next(statuses.Length)]
                );
            }

            BindDataToGrid(dtRecords);
        }

        private void BindDataToGrid(DataTable dt)
        {
            dgvRecords.Rows.Clear();

            foreach (DataRow row in dt.Rows)
            {
                int rowIndex = dgvRecords.Rows.Add();
                DataGridViewRow dgvRow = dgvRecords.Rows[rowIndex];

                dgvRow.Cells["colRecordId"].Value = row["RecordId"];
                dgvRow.Cells["colCustomerId"].Value = row["CustomerId"];
                dgvRow.Cells["colCustomerName"].Value = row["CustomerName"];
                dgvRow.Cells["colVaccinationDate"].Value = ((DateTime)row["VaccinationDate"]).ToString("dd/MM/yyyy");
                dgvRow.Cells["colVaccineName"].Value = row["VaccineName"];
                dgvRow.Cells["colDoseNumber"].Value = row["DoseNumber"];
                dgvRow.Cells["colLotNumber"].Value = row["LotNumber"];
                dgvRow.Cells["colEmployee"].Value = row["Employee"];
                dgvRow.Cells["colStatus"].Value = row["Status"];

                // Màu sắc theo trạng thái
                string status = row["Status"].ToString();
                if (status == "Đã tiêm")
                {
                    dgvRow.Cells["colStatus"].Style.BackColor = System.Drawing.Color.LightGreen;
                }
                else if (status == "Chưa tiêm")
                {
                    dgvRow.Cells["colStatus"].Style.BackColor = System.Drawing.Color.LightYellow;
                }
                else if (status == "Đã hủy")
                {
                    dgvRow.Cells["colStatus"].Style.BackColor = System.Drawing.Color.LightCoral;
                }
            }

            
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            // Lọc dữ liệu
            DataTable filteredData = dtRecords.Clone();

            foreach (DataRow row in dtRecords.Rows)
            {
                bool match = true;

                // Filter by date
                DateTime vaccinationDate = (DateTime)row["VaccinationDate"];
                if (vaccinationDate < dtpFromDate.Value.Date || vaccinationDate > dtpToDate.Value.Date)
                {
                    match = false;
                }

                // Filter by vaccine
                if (cboVaccine.SelectedIndex > 0 && row["VaccineName"].ToString() != cboVaccine.Text)
                {
                    match = false;
                }

                // Filter by dose number
                if (cboDoseNumber.SelectedIndex > 0 && row["DoseNumber"].ToString() != cboDoseNumber.Text)
                {
                    match = false;
                }

                // Filter by status
                if (cboStatus.SelectedIndex > 0 && row["Status"].ToString() != cboStatus.Text)
                {
                    match = false;
                }

                if (match)
                {
                    filteredData.ImportRow(row);
                }
            }

            BindDataToGrid(filteredData);

            if (filteredData.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy kết quả phù hợp!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            // Reset filters
            InitializeFilters();

            // Reload all data
            BindDataToGrid(dtRecords);
        }

        private void DgvRecords_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle button click for printing certificate
            if (e.ColumnIndex == dgvRecords.Columns["colPrintCert"].Index && e.RowIndex >= 0)
            {
                string recordId = dgvRecords.Rows[e.RowIndex].Cells["colRecordId"].Value.ToString();
                string customerName = dgvRecords.Rows[e.RowIndex].Cells["colCustomerName"].Value.ToString();
                string status = dgvRecords.Rows[e.RowIndex].Cells["colStatus"].Value.ToString();

                if (status != "Done")
                {
                    MessageBox.Show("Chỉ có thể in chứng nhận cho hồ sơ đã hoàn thành!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"In chứng nhận cho:\nMã HS: {recordId}\nKhách hàng: {customerName}",
                    "Xác nhận in",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // TODO: Implement print certificate
                    MessageBox.Show("Đang in chứng nhận...\n(Chức năng sẽ được phát triển sau)",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Public method để refresh data từ bên ngoài
        public void RefreshData()
        {
            LoadSampleData();
        }
    }
}