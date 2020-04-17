using System.Collections.Generic;

using Api.Models;

namespace Api.Interfaces
{

    public interface IVoteRepository
    {
        void AddVoteFor(string name, int votes);
        void AddVoteFor(string name);

        void Reset();

        int TotalVotesProcessed();
    }
}
