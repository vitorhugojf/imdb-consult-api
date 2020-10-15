using System.Threading.Tasks;
using Project.Domain.MovieAgg.Entities;
using Project.Domain.Shared.Entities;
using Project.Domain.Shared.Interfaces.Repositories;

namespace Project.Domain.MovieAgg.Repositories
{
    public interface IVoteRepository : IRepository<Vote>
    {
    }
}
