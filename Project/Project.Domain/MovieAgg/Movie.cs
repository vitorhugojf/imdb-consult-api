using Project.Domain.MovieAgg.Entities;
using Project.Domain.Shared.Entities;
using System.Collections.Generic;

namespace Project.Domain.MovieAgg
{
    public class Movie : Entity
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public virtual ICollection<MovieActor> MovieActors { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }

        public Movie() { }

        public Movie(int id, string name, string director, string genre)
        {
            Id = id;
            Name = name;
            Director = director;
            Genre = genre;
        }
    }
}
