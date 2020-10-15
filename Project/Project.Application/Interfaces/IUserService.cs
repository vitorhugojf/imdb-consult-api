using Project.Domain.DTO.User;
using Project.Domain.Shared.Entities;
using Project.Domain.UserAgg;
using System.Threading.Tasks;

namespace Project.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<User>> Register(UserForRegisterDto userLoginVm);
        Task<Result<PaginatedList<User>>> AllUsers(int pageSize = 10, int pageNumber = 1, bool orderAsc = true);
        Task<Result<UserForDetailedDto>> UserById(int id);
        Task<Result<User>> Update(int userRequestId, bool isAdmin, UserForUpdateDto userForUpdateDto);
        Task<Result<User>> Disable(int userRequestId, bool isAdmin, int userId);
    }
}
