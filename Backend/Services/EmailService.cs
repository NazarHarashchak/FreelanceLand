using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Backend.Interfaces.ServiceInterfaces;

namespace Backend.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmailAsync(string email, string subject, string messages)
        {
            var emailMessages = new MimeMessage();

            emailMessages.From.Add(new MailboxAddress("Administration", "stas.glazkov95@gmail.com"));
            emailMessages.To.Add(new MailboxAddress("", email));
            emailMessages.Subject = subject;
            emailMessages.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = messages
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("stas.glazkov95@gmail.com", "superman1996");
                client.Send(emailMessages);

                client.Disconnect(true);
            }
        }
    }
}

