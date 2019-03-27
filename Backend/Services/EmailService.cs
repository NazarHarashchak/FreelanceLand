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

            emailMessages.From.Add(new MailboxAddress("Freelance Land", "freelancelandservice@gmail.com"));
            emailMessages.To.Add(new MailboxAddress("", email));
            emailMessages.Subject = subject;
            emailMessages.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = messages
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("freelancelandservice@gmail.com", "qwerty123qwerty");
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Send(emailMessages);

                client.Disconnect(true);
            }
        }
    }
}

