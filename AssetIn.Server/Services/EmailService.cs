using System.Net;
using System.Net.Mail;

namespace AssetIn.Server.Services;

public class EmailService(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;
    public async Task<bool> SendEmailAsync(string targetEmail, string subject, string message)
    {
        MailMessage mail = CreateMailMessage(targetEmail, subject, message);
        var result = await SendAsync(mail);
        return result;
    }

    private MailMessage CreateMailMessage(string targetEmail, string subject, string message)
    {
        MailMessage mailMessage = new()
        {
            From = new MailAddress(_configuration["SmtpSettings:GMail:From"]!),
            To = { targetEmail },
            Subject = subject,
            IsBodyHtml = true,
            Body = $@"
         <!DOCTYPE html>
         <html lang=""en"">
         <head>
         <meta charset=""UTF-8"">
         <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
         <style>
         body {{
         font-family: 'Arial', sans-serif;
         margin: 0;
         padding: 0;
         background-color: #f4f4f4;
         }}
         .container {{
         width: 100%;
         max-width: 600px;
         margin: 0 auto;
         background-color: #ffffff;
         border-radius: 8px;
         box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
         overflow: hidden;
         }}
         .header {{
         background-color: #007BFF;
         color: white;
         padding: 30px;
         text-align: center;
         }}
         .header h1 {{
         margin: 0;
         font-size: 24px;
         letter-spacing: 1px;
         }}
         .content {{
         padding: 20px;
         line-height: 1.6;
         }}
         .content h2 {{
         font-size: 20px;
         color: #333;
         }}
         .content p {{
         font-size: 16px;
         color: #555;
         }}
         .button {{
         display: inline-block;
         background-color: #007BFF;
         color: white;
         padding: 10px 15px;
         text-decoration: none;
         border-radius: 5px;
         margin-top: 15px;
         transition: background-color 0.3s;
         }}
         .button:hover {{
         background-color: #0056b3;
         }}
         .footer {{
         text-align: center;
         padding: 15px;
         font-size: 12px;
         color: #777777;
         border-top: 1px solid #f0f0f0;
         }}
         .footer p {{
         margin: 5px 0;
         }}
         a {{
         color: #007BFF;
         text-decoration: none;
         }}
         a:hover {{
         text-decoration: underline;
         }}
         </style>
         </head>
         <body>
         <div class=""container"">
         <div class=""header"">
         <h1>AssetIn</h1>
         </div>
         <div class=""content"">
         <p>{message}</p>
         <p>Thank you for using AssetIn!</p>
         <a href=""https://www.AssetIn.com"" class=""button"">Visit Our Website</a>
         </div>
         <div class=""footer"">
         <p>&copy; 2025 AssetIn. All rights reserved.</p>
         </div>
         </div>
         </body>
         </html>"
        };
        return mailMessage;
    }

    private async Task<bool> SendAsync(MailMessage mailMessage)
    {
        SmtpClient smtpClient = new(_configuration["SmtpSettings:GMail:SmtpServer"])
        {
            Port = Convert.ToInt32(_configuration["SmtpSettings:GMail:Port"]),
            Credentials = new NetworkCredential(_configuration["SmtpSettings:GMail:From"], _configuration["SmtpSettings:GMail:FromPassword"]),
            Timeout = Convert.ToInt32(_configuration["SmtpSettings:GMail:Timeout"]),
            EnableSsl = Convert.ToBoolean(_configuration["SmtpSettings:GMail:EnableSsl"]),
        };
        bool result = false;
        try
        {
            await smtpClient.SendMailAsync(mailMessage);
            result = true;
        }
        catch
        {
            result = false;
        }
        finally
        {
            smtpClient.Dispose();
        }
        return result;
    }
}
