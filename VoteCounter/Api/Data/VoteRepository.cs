using System.Collections.Generic;

using Api.Interfaces;
using Api.Models;

namespace Api.Data
{
    public class VoteRepository : IVoteRepository
    {
        private readonly List<Vote> VotesCounted = new List<Vote>();

        public IList<Vote> GetVotes()
        {
            return VotesCounted;
        }
    }
}
