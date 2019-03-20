using System.Threading.Tasks;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface IEmailService
    {
        void SendEmailAsync(string email, string subject, string messages);
    }
}
