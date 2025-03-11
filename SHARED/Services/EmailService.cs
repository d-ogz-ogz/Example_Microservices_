using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Net;
using System.Net.Mail;

namespace SHARED.Services
{
    public class EmailService
    {

        private readonly string _smtpUser;
        private readonly string _smtpPassword;
        public EmailService(IConfiguration configuration)
        {
            _smtpUser = configuration["EmailConstants:_smtpUser"] ?? "derya.oguz2024@gmail.com";
            _smtpPassword = configuration["EmailConstants:_smtpPassword"] ?? "123456";

        }

        public async Task SendEmailAsync(string toEmail,string body,string mailSubject)
        {
            try
            {
               using SmtpClient smtpClient = new("smtp.gmail",587);
                smtpClient.EnableSsl = true;
                NetworkCredential credential = new(_smtpUser,_smtpPassword);
                smtpClient.Credentials = credential;
                MailAddress sender = new(_smtpUser, "Derya Oğuz");
                MailAddress receiver = new(toEmail);
                MailMessage mail= new(sender, receiver);
                mail.Subject = mailSubject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(toEmail));
                await smtpClient.SendMailAsync(mail);
                Task.CompletedTask.Wait();

            }
            catch (SmtpException smtpEx)
            {

                throw new SmtpException(body, smtpEx);  
            }
        }
    }
}  

