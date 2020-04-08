using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Api.Interfaces;
using System;

namespace Api.Services
{
    public class VoteFinder
    {
        private ILogger logger;
        private Dictionary<string, int> votesCounter;
        private readonly bool doesFirstToReachWin;
        public VoteFinder(ILogger logger, bool doesFirstToReachWin)
        {
            this.logger = logger;
            this.doesFirstToReachWin = doesFirstToReachWin;
            votesCounter = new Dictionary<string, int>();
        }

        private bool DoesUpdateToNewWinner(int currentVoteCount, int maxCount)
        {
            if (doesFirstToReachWin)
            {
                return currentVoteCount > maxCount;
            }

            return currentVoteCount >= maxCount;
        }

        public string FindWinner(List<Vote> votes)
        {
            var winner = string.Empty;
            var maxCount = 0;

            foreach(var vote in votes)
            {
                if (votesCounter.ContainsKey(vote.Name))
                {
                    votesCounter[vote.Name] += vote.Count;
                }
                else
                {
                    votesCounter[vote.Name] = vote.Count;
                }

                if (DoesUpdateToNewWinner(votesCounter[vote.Name], maxCount))
                {
                    winner = vote.Name;
                    maxCount = votesCounter[vote.Name];
                }
            }

            return winner;
        }

        public string FindWinner(List<string> votes)
        {
            var listOfVote = new List<Vote>();
            foreach(var vote in votes)
            {
                listOfVote.Add(new Vote {Name = vote, Count = 1});
            }

            return FindWinner(listOfVote);
        }

    }
}
