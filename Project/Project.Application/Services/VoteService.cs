using AutoMapper;
using Project.Application.Interfaces;
using Project.Domain.MovieAgg.Entities;
using Project.Domain.MovieAgg.Repositories;
using Project.Domain.Shared.Entities;
using Project.Domain.Shared.Interfaces.UoW;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Application.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IMapper _mapper;
        private readonly IUnityOfWork _unityOfWork;

        public VoteService(IMapper mapper, IUnityOfWork unityOfWork, IVoteRepository voteRepository)
        {
            _mapper = mapper;
            _unityOfWork = unityOfWork;
            _voteRepository = voteRepository;
        }

        public async Task<Result<Vote>> Vote(int value, int movieId, int userId)
        {
            var voteToCreate = new Vote(userId, movieId, value);

            if (value >= 0 && value <= 4)
            {
                voteToCreate = await _voteRepository.Create(voteToCreate);
                _unityOfWork.Commit();

                return Result<Vote>.CreateResult(voteToCreate);
            }

            return Result<Vote>.CreateResult(voteToCreate, new HashSet<string> { "Não foi possivel efetuar a votação." });
        }
    }
}
