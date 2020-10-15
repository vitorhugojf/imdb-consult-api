using Project.Domain.DTO.Actor;
using System.Collections.Generic;

namespace Project.Domain.DTO.Movie
{
    public class MovieForDetailedDto
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public virtual IEnumerable<ActorForDetailedDto> Actors { get; set; }
        public decimal AverageVote { get; set; }
    }
}
