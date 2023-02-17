using System.Threading.Tasks;

namespace Web_App.Services
{
    public interface IEmailService
    {
        Task Send(string from, string to, string subject, string body);
    }
}