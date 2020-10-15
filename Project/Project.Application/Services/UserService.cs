using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Application.Interfaces;
using Project.Domain.DTO.User;
using Project.Domain.Shared.Entities;
using Project.Domain.UserAgg;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<User>> Register(UserForRegisterDto userRegisterDto)
        {
            var userToCreate = _mapper.Map<User>(userRegisterDto);

            var identityResult = await _userManager.CreateAsync(userToCreate, userRegisterDto.Password);
            if (identityResult.Succeeded)
            {
                _userManager.AddToRoleAsync(userToCreate, "Member").Wait();
            }

            return Result<User>.CreateResult(userToCreate, identityResult.GetErrorsDescription());
        }

        public async Task<Result<PaginatedList<User>>> AllUsers(int pageSize, int pageNumber, bool orderAsc)
        {
            var allUsers = _userManager.Users;
            if (orderAsc)
            {
                allUsers = allUsers.OrderBy(u => u.UserName);
            }

            var paginatedList = await new PaginatedList<User>().CreateAsync(allUsers, pageNumber, pageSize);

            return Result<PaginatedList<User>>.CreateResult(paginatedList);
        }

        public async Task<Result<UserForDetailedDto>> UserById(int id)
        {
            var userDetailed = _mapper.Map<UserForDetailedDto>(await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id.Equals(id)));
            return Result<UserForDetailedDto>.CreateResult(userDetailed);
        }

        public async Task<Result<User>> Update(int userRequestId, bool isAdmin, UserForUpdateDto userForUpdateDto)
        {
            if (isAdmin || userRequestId == userForUpdateDto.Id)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id.Equals(userForUpdateDto.Id));
                if (user != null)
                {
                    User.Update(user, userForUpdateDto.FirstName, userForUpdateDto.LastName, userForUpdateDto.Introduction);
                    var result = await _userManager.UpdateAsync(user);
                    Result<User>.CreateResult(user, result.GetErrorsDescription());
                }

                return Result<User>.CreateResult(user, new HashSet<string> { "Usuário não encontrado." });
            }

            return Result<User>.CreateResult(null, new HashSet<string> { "Permissão negada." });
        }

        public async Task<Result<User>> Disable(int userRequestId, bool isAdmin, int userId)
        {
            if (isAdmin || userRequestId == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));
                if (user != null)
                {
                    User.Disable(user);
                    var result = await _userManager.UpdateAsync(user);
                    Result<User>.CreateResult(user, result.GetErrorsDescription());
                }
                return Result<User>.CreateResult(user, new HashSet<string> { "Usuário não encontrado." });
            }

            return Result<User>.CreateResult(null, new HashSet<string> { "Permissão negada." });
        }
    }
}
