using Project.Domain.MovieAgg.Entities;
using Project.Domain.Shared.Entities;
using System.Threading.Tasks;

namespace Project.Application.Interfaces
{
    public interface IVoteService
    {
        Task<Result<Vote>> Vote(int value, int movieId, int userId);
    }
}
