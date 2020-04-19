using System.Collections.Generic;

using Api.Models;

namespace Api.Interfaces
{

    public interface IVoteRepository
    {
        List<Vote> VotesCounted { get; }
        void AddVoteFor(string name);
        void Reset();
        int TotalVotesProcessed();
    }
}
