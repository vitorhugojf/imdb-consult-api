using Project.Domain.Shared;
using System.Collections.Generic;
using Project.Domain.Shared.Entities;

namespace Project.Domain.MovieAgg.Entities
{
    public class Actor : Entity
    {
        public string Name { get; set; }
        public virtual IEnumerable<MovieActor> ActorMovies { get; set; }

        public Actor() { }

        public Actor(string name)
        {
            Name = name;
        }
    }
}
