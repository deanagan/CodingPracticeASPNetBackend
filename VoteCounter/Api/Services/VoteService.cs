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

        public void AddVoteFor(int id, int votes)
        {
            string name = "Donald";
            _voteRepository.AddVoteFor(name, votes);
        }

        public void AddVoteFor(int id)
        {
            string name ="Donald";
            _voteRepository.AddVoteFor(name);
        }
    }
}
