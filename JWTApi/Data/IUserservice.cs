using System.Threading.Tasks;
using JWTApi.Models;

namespace JWTApi.Data
{
    public interface IUserservice
    {
        Task<Student> Register(Student user, string password);
        Task<Student> Authenticate(string username, string password);
        Task<bool> UserExist(string username);
    }
}