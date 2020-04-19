using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Api.Interfaces;
using Api.Models;
using System;

namespace Api.Services
{
    public class VoteCounter : IVoteCounter
    {
        private ILogger<VoteCounter> logger;
        private Dictionary<string, int> votesCounter;

        public VoteCounter(ILogger<VoteCounter> logger)
        {
            this.logger = logger;
            votesCounter = new Dictionary<string, int>();
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

                if (votesCounter[vote.Name] > maxCount)
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
