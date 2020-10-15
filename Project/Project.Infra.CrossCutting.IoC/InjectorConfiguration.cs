using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.AutoMapper;
using Project.Application.Interfaces;
using Project.Application.Services;
using Project.Domain.MovieAgg.Repositories;
using Project.Domain.Shared.Interfaces.Repositories;
using Project.Domain.Shared.Interfaces.UoW;
using Project.Infra.Data.Context;
using Project.Infra.Data.Repositories;
using Project.Infra.Data.UoW;
using MovieService = Project.Application.Services.MovieService;

namespace Project.Infra.CrossCutting.IoC
{
    public static class InjectorConfiguration
    {
        public static void RegisterDependencyInjector(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfiguration());
                mc.AllowNullCollections = true;
                mc.AllowNullDestinationValues = true;
            });

            services.AddScoped<IUnityOfWork, UnityOfWork>();

            services.AddSingleton(mappingConfig.CreateMapper());
            services.AddScoped<DataContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVoteService, VoteService>();

            //Repositories
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();
        }
    }
}
