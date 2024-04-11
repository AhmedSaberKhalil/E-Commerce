using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using MimeKit;
using MailKit.Net.Smtp;
using E_CommerceWebApi.Models;
using E_CommerceWebApi.Repository;

namespace E_CommerceWebApi.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;

        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(toEmail));
            message.From = new MailAddress(_emailSettings.SmtpUsername);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtpClient = new System.Net.Mail.SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(message);
            }
        }

        //public async Task SendEmailAsync(string toEmail, string subject, string body)
        //{
        //	using (System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort))
        //	{
        //		smtpClient.UseDefaultCredentials = false;
        //		smtpClient.Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
        //		smtpClient.EnableSsl = true;

        //		using (MailMessage mailMessage = new MailMessage())
        //		{
        //			mailMessage.From = new MailAddress(_emailSettings.SenderEmail);
        //			mailMessage.To.Add(toEmail);
        //			mailMessage.Subject = subject;
        //			mailMessage.Body = body;
        //			mailMessage.IsBodyHtml = true;

        //			await smtpClient.SendMailAsync(mailMessage);
        //		}
        //	}
        //}
    }
}

