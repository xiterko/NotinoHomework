using MailKit.Net.Smtp;
using MimeKit;
using NotinoHomework.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NotinoHomework.Repositories
{
    public class EmailRepository : IEmailRepository
    {

        public void SendEmail(string recipient, Document attachement)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Notino", "info@notino.cz"));
            mailMessage.To.Add(new MailboxAddress("TestUser", recipient));
            mailMessage.Subject = "Notino";

            byte[] byteArray = Encoding.ASCII.GetBytes(attachement.Text);
            using MemoryStream stream = new MemoryStream(byteArray);

            var attachment = new MimePart
            {
                Content = new MimeContent(stream),
                FileName = attachement.Title
            };

            var multipart = new Multipart("mixed");
            multipart.Add(new TextPart("plain")
            {
                Text = "Check file in attachment"
            });
            multipart.Add(attachment);
            mailMessage.Body = multipart;

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtpClient.Authenticate("notinoworker@gmail.com", "workernotino");
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }

    }
}
