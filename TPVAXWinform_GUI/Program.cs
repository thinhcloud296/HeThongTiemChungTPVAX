using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPVAXWinform_GUI.Forms;

namespace TPVAXWinform
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            frmDangNhap loginForm = new frmDangNhap();

            // 1. Hiển thị form đăng nhập DƯỚI DẠNG DIALOG
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new frmMain());
            }
        }

        // Import hàm từ user32.dll để bật DPI awareness
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

    }
}
