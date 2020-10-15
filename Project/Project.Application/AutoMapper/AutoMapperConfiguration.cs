using AutoMapper;
using Project.Domain.DTO.Actor;
using Project.Domain.DTO.Movie;
using Project.Domain.DTO.User;
using Project.Domain.MovieAgg;
using Project.Domain.MovieAgg.Entities;
using Project.Domain.UserAgg;
using System.Collections.Generic;
using System.Linq;

namespace Project.Application.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<User, UserForRegisterDto>()
                .ForMember(u => u.Password, opt => opt.Ignore());

            CreateMap<UserForRegisterDto, User>()
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<User, UserForDetailedDto>();
            CreateMap<UserForDetailedDto, User>()
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<User, UserForDetailedDto>().ReverseMap();

            CreateMap<MovieForRegisterDto, Movie>()
                .ForMember(dest => dest.MovieActors, opt => opt.MapFrom<ActorsResolver>());
            CreateMap<Movie, MovieForRegisterDto>()
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<Movie, MovieForDetailedDto>().ReverseMap();

            CreateMap<Actor, ActorForRegisterDto>();
            CreateMap<Actor, ActorForDetailedDto>().ReverseMap();
        }

        internal class ActorsResolver :
            IValueResolver<MovieForRegisterDto, Movie, ICollection<MovieActor>>
        {
            public ICollection<MovieActor> Resolve(MovieForRegisterDto source,
                Movie destination,
                ICollection<MovieActor> destMember, ResolutionContext context)
            {
                return source.Actors.Select(actor => new MovieActor {Actor = new Actor(actor.Name), Movie = destination}).ToList();
            }
        }
    }
}
