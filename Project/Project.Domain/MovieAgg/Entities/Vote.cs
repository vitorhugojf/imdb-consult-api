using Project.Domain.Shared;
using Project.Domain.Shared.Entities;
using Project.Domain.UserAgg;

namespace Project.Domain.MovieAgg.Entities
{
    public class Vote : Entity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public int Value { get; set; }

        public Vote() { }

        public Vote(int userId, int movieId, int value)
        {
            UserId = userId;
            MovieId = movieId;
            Value = value;
        }
    }
}
