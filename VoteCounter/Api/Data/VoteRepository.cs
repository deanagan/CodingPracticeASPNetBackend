using System.Collections.Generic;
using System.Linq;

using Api.Interfaces;
using Api.Models;

namespace Api.Data
{
    public class VoteRepository : IVoteRepository
    {
        private readonly List<Vote> VotesCounted = new List<Vote>();

        public void Reset()
        {
            VotesCounted.Clear();
        }

        public void AddVoteFor(string name)
        {
            AddVoteFor(name, 1);
        }

        public void AddVoteFor(string name, int votes)
        {
            var candidate = VotesCounted.First(vote => vote.Name == name);
            if (candidate == null)
            {
                VotesCounted.Add(new Vote { Name = name, Count = votes});
            }
            else
            {
                candidate.Count += votes;
            }
        }
    }
}
