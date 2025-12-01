using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TPVAXWinform_BLL.Services
{
    /// <summary>
    /// Dịch vụ gửi email qua Gmail SMTP (Async)
    /// </summary>
    public class EmailService
    {
        // Gmail SMTP Configuration
        private const string SmtpHost = "smtp.gmail.com";
        private const int SmtpPort = 587;
        
        // TODO: Thay thế bằng email và App Password thực tế
        private const string SenderEmail = "tpvaxmanager@gmail.com";
        private const string SenderAppPassword = "kupg refb dvoz agys"; // Google App Password (16 ký tự)
        private const string SenderDisplayName = "Hệ Thống Tiêm Chủng TPVAX";

        /// <summary>
        /// Gửi email thông tin tài khoản cho nhân viên mới (Async)
        /// </summary>
        public async Task<bool> SendAccountInfoAsync(string toEmail, string hoTen, string password)
        {
            try
            {
                using (var smtpClient = new SmtpClient(SmtpHost, SmtpPort))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(SenderEmail, SenderAppPassword);
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    using (var mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(SenderEmail, SenderDisplayName);
                        mailMessage.To.Add(toEmail);
                        mailMessage.Subject = "Thông tin tài khoản hệ thống Tiêm Chủng";
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = BuildEmailBody(hoTen, password);

                        // Gửi email bất đồng bộ - KHÔNG block UI
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                System.Diagnostics.Debug.WriteLine($"Email Error: {ex.Message}");
                throw new Exception($"Lỗi gửi email: {ex.Message}");
            }
        }

        /// <summary>
        /// Tạo nội dung email HTML
        /// </summary>
        private string BuildEmailBody(string hoTen, string password)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
</head>
<body style='font-family: Arial, sans-serif; line-height: 1.6;'>
    <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 5px;'>
        <h2 style='color: #2c3e50;'>Chào mừng bạn đến với Hệ Thống Tiêm Chủng TPVAX!</h2>
        <p>Xin chào <strong>{hoTen}</strong>,</p>
        <p>Tài khoản của bạn đã được tạo thành công. Dưới đây là thông tin đăng nhập:</p>
        <div style='background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 15px 0;'>
            <p><strong>Mật khẩu:</strong> <code style='background-color: #e9ecef; padding: 2px 6px; border-radius: 3px;'>{password}</code></p>
        </div>
        <p style='color: #e74c3c;'><strong>Lưu ý:</strong> Vui lòng đổi mật khẩu sau khi đăng nhập lần đầu để đảm bảo an toàn.</p>
        <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;'>
        <p style='color: #7f8c8d; font-size: 12px;'>Email này được gửi tự động từ Hệ Thống Tiêm Chủng TPVAX. Vui lòng không trả lời email này.</p>
    </div>
</body>
</html>";
        }
    }
}
