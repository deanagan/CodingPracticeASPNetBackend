using Api.Interfaces;

namespace Api.Services
{
    public class VoteService : IVoteService
    {
        private IVoteRepository _voteRepository;
        private IVoteCounter _voteCounter;
        public VoteService(IVoteRepository voteRepository, IVoteCounter voteCounter)
        {
            _voteRepository = voteRepository;
            _voteCounter = voteCounter;
        }

        public string GetWinner()
        {
            return _voteCounter.FindWinner(_voteRepository.VotesCounted);
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
