using Project.Application.ViewModels.User;
using System.Net;
using System.Threading.Tasks;

namespace Project.Application.Interfaces
{
    public interface IAccountService
    {
        Task<HttpStatusCode> AuthenticateUser(UserLoginVm userLoginVm);
        Task<string> GenerateToken(UserLoginVm userLoginVm);
    }
}
