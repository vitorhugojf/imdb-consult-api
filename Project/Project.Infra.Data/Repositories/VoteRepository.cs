using System.Threading.Tasks;
using Project.Domain.MovieAgg.Entities;
using Project.Domain.MovieAgg.Repositories;
using Project.Infra.Data.Context;

namespace Project.Infra.Data.Repositories
{
    public class VoteRepository : Repository<Vote>, IVoteRepository
    {
        public VoteRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
