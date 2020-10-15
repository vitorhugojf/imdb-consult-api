using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using Project.Application.Interfaces;
using Project.Application.Services;
using Project.Domain.DTO.Movie;
using Project.Domain.MovieAgg;
using Project.Domain.MovieAgg.Repositories;
using Project.Domain.Shared.Entities;
using Project.Domain.Shared.Interfaces.UoW;
using Xunit;

namespace Project.Test
{
    public class MovieServiceTest
    {
        private readonly IMovieService _movieService;
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnityOfWork> _unityOfWorkMock;

        public MovieServiceTest()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _mapperMock = new Mock<IMapper>();
            _unityOfWorkMock = new Mock<IUnityOfWork>();
            _movieService = new MovieService(_movieRepositoryMock.Object, _mapperMock.Object, _unityOfWorkMock.Object);
        }

        [Fact]
        [Trait(nameof(IMovieService.Register), "Sucesso")]
        public async Task When_RegisterWithCommitTrue_Expected_MovieRegistered()
        {
            var movieForRegisterDto = new MovieForRegisterDto();

            _mapperMock.Setup(x => x.Map<Movie>(It.IsAny<MovieForRegisterDto>())).Returns(new Movie());
            _unityOfWorkMock.Setup(x => x.Commit()).Returns(true);

            var result = await _movieService.Register(movieForRegisterDto);

            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        [Trait(nameof(IMovieService.Register), "False")]
        public async Task When_RegisterWithCommitFalse_Expected_MovieNotRegistered()
        {
            var movieForRegisterDto = new MovieForRegisterDto();

            _mapperMock.Setup(x => x.Map<Movie>(It.IsAny<MovieForRegisterDto>())).Returns(new Movie());
            _unityOfWorkMock.Setup(x => x.Commit()).Returns(false);

            var result = await _movieService.Register(movieForRegisterDto);

            result.Succeeded.Should().BeFalse();
        }
    }
}
