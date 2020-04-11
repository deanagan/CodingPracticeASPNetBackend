using System.Collections.Generic;

using Api.Models;

namespace Api.Interfaces
{

    public interface IVoteRepository
    {
        IList<Vote> GetVotes();
    }
}
