using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SocialClint._services.Interfaces;
using SocialClint.Dto;

namespace SocialClint._services.Classes
{
    public class MailService : ImailService
    {
        public MailService(IOptions<MailSetting> setting)
        {
            _setting = setting.Value;
        }

        public MailSetting _setting { get; }
        //public async Task SentEmailAsync(string MailTo, string subject, string body, IList<IFormFile> attachment = null)

        public async Task SentEmailAsync(string MailTo, string subject, string body)
        {
            var email = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_setting.Email),
                Subject=subject,

            };
            email.To.Add(MailboxAddress.Parse(MailTo));
            var builder = new BodyBuilder();
            //if (attachment != null)
            //{
            //    byte[] filebytes;
            //    foreach (var file in attachment) {
            //        if (file.Length > 0)
            //        {
            //            using (var ms = new MemoryStream())
            //            {
            //                file.CopyTo(ms);
            //                filebytes=ms.ToArray();
            //            }
            //            builder.Attachments.Add(file.FileName,filebytes,ContentType.Parse(file.ContentType));
            //        }
                
            //    }
            
            //}

            builder.HtmlBody= body;
            email.Body=builder.ToMessageBody();
            email.From.Add(new MailboxAddress(_setting.DisplayName, _setting.Email));



            var smtp = new SmtpClient();
            await  smtp.ConnectAsync(_setting.Host, _setting.Port,MailKit.Security.SecureSocketOptions.StartTls);
            await  smtp.AuthenticateAsync(_setting.Email, _setting.Password);
            await  smtp.SendAsync(email);
            await  smtp.DisconnectAsync(true);


        }
    }
}
