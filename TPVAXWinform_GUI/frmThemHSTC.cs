using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPVAXWinform_GUI
{
    public partial class frmThemHSTC : Form
    {
        string[] quanHeOptions = {
        "Bản thân", "Cha", "Mẹ", "Con",
        "Anh ruột", "Chị ruột", "Em ruột",
        "Ông nội", "Bà nội", "Ông ngoại", "Bà ngoại",
        "Vợ", "Chồng",
        "Người giám hộ", "Người chăm sóc", "Đại diện theo pháp luật",
        "Khác"
        };
        public frmThemHSTC()
        {
            InitializeComponent();
            cboQuanHe.DataSource = quanHeOptions;
        }
    }
}
