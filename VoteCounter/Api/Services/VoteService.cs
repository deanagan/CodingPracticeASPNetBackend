using Api.Interfaces;

namespace Api.Services
{
    public class VoteService : IVoteService
    {
        private IVoteRepository _voteRepository;
        public VoteService(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public string GetWinner()
        {
            return string.Empty;
        }

        public void AddVoteFor(string name)
        {
            _voteRepository.AddVoteFor(name);
        }

        public int TotalVotesProcessed()
        {
            return _voteRepository.TotalVotesProcessed();
        }
    }
}
