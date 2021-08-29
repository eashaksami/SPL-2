using System.Threading.Tasks;
using EBET.Models;

namespace EBET.Data
{
    public interface IUserservice
    {
        Task<User> Register(User user, string password);
        Task<User> Authenticate(string username, string password);
        Task<bool> UserExist(string username);
    }
}
