using Edu_QuizGen.Settings;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Security;

namespace Edu_QuizGen.Services;

public class EmailService(IOptions<MailSettings> mailSettings) : IEmailSender
{
    private readonly MailSettings _mailSettings = mailSettings.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage { 
          Sender=MailboxAddress.Parse(_mailSettings.SenderEmail),
          Subject=subject,    
        };

        message.To.Add(MailboxAddress.Parse(email));
    
        var builder =new BodyBuilder { 
          HtmlBody=htmlMessage
        };

        message.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();

        smtp.Connect(_mailSettings.Host,_mailSettings.Port,SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.Username, _mailSettings.Password);
        await smtp.SendAsync(message);
        smtp.Disconnect(true);
    }
}
