using Api.Models;
using System.Collections.Generic;

namespace Api.Interfaces
{
    public interface IVoteCounter
    {
        string FindWinner(List<Vote> votes);
    }
}
