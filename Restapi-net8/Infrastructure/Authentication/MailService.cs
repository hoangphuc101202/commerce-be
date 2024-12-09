using System;
using System.Net;
using System.Net.Mail;
namespace Restapi_net8.Infrastructure.Authentication;

public class MailService
{   
    private readonly IConfiguration _configuration;
    
    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<string> SendMail(string email, string token)
    {
        string smtpServer = "smtp.gmail.com";
        int port = 587;
        string fromMail = _configuration["smtp:email"];
        string password = _configuration["smtp:password"];
        var client = new SmtpClient(smtpServer, port)
        {
            Credentials = new NetworkCredential(fromMail, password),
            EnableSsl = true
        };
        var mail = new MailMessage(fromMail, email)
        {
            Subject = "Hành động ngay: Đặt lại mật khẩu tài khoản của bạn",
            Body = $@"
            <div style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                <h1 style='text-align: center; color: #4CAF50;'>Đặt lại mật khẩu</h1>
                <p>Xin chào,</p>
                <p>
                    Chúng tôi nhận được yêu cầu đặt lại mật khẩu cho tài khoản của bạn. 
                    Để tiếp tục, vui lòng nhấn vào link bên dưới:
                </p>
                 <p style='text-align: center;'>
                    <a href='http://localhost:5173/reset-password?email={email}&token={token}' 
                    style='color: #4CAF50; text-decoration: none; font-size: 16px;'>
                    http://localhost:5173/reset-password?email={email}&token={token}
                    </a>
                </p>
                </div>
                <p style='font-size: 14px; color: #777;'>
                    Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này. Liên kết sẽ hết hạn sau 5 phút.
                </p>
                <p style='font-size: 14px; color: #777;'>
                    Trân trọng,<br>
                    Đội ngũ hỗ trợ khách hàng
                </p>
            </div>",
            IsBodyHtml = true
        };
        await client.SendMailAsync(mail);
        return "Mail to sent successfully";
    }
    public async Task<string> SendMailVerify(string email, string token){
        string smtpServer = "smtp.gmail.com";
        int port = 587;
        string fromMail = _configuration["smtp:email"];
        string password = _configuration["smtp:password"];
        var client = new SmtpClient(smtpServer, port)
        {
            Credentials = new NetworkCredential(fromMail, password),
            EnableSsl = true
        };
        var mail = new MailMessage(fromMail, email)
        {
            Subject = "Xác thực tài khoản",
            Body = $@"
            <div style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                <h1 style='text-align: center; color: #4CAF50;'>Xác thực tài khoản</h1>
                <p>Xin chào,</p>
                <p>
                    Chúng tôi nhận được yêu cầu xác thực email cho tài khoản của bạn. 
                    Để tiếp tục, vui lòng nhấn vào link bên dưới:
                </p>
                 <p style='text-align: center;'>
                    <a href='http://localhost:5173/verify-email?email={email}&token={token}' 
                    style='color: #4CAF50; text-decoration: none; font-size: 16px;'>
                    http://localhost:5173/verify-email?email={email}&token={token}
                    </a>
                </p>
                </div>
                <p style='font-size: 14px; color: #777;'>
                    Bạn vui lòng xác thực tài khoản để tiếp tục mua hàng. Liên kết sẽ hết hạn sau 5 phút.
                </p>
                <p style='font-size: 14px; color: #777;'>
                    Trân trọng,<br>
                    Đội ngũ hỗ trợ khách hàng
                </p>
            </div>",
            IsBodyHtml = true
        };
        await client.SendMailAsync(mail);
        return "Mail to sent successfully";
    }
}
