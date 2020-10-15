using Project.Domain.DTO.Actor;
using System.Collections.Generic;

namespace Project.Domain.DTO.Movie
{
    public class MovieForRegisterDto
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public HashSet<ActorForRegisterDto> Actors { get; set; }
    }
}
