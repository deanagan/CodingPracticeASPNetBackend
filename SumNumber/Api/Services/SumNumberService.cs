using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Api.Interfaces;
using System;

namespace Api.Services
{
    public class SumNumberService : ISumNumberService
    {
        private ILogger logger;
        private Dictionary<Tuple<int,int>, int> solvedPairs;
        public SumNumberService(ILogger logger)
        {
            this.logger = logger;
            solvedPairs = new Dictionary<Tuple<int, int>, int>();
        }

        private int GetSum(int n1, int n2)
        {
            var key = Tuple.Create(n1, n2);
            if (solvedPairs.ContainsKey(key))
            {
                return solvedPairs[key];
            }
            var total = n1 + n2;
            solvedPairs.Add(key, total);

            return total;
        }
        public int[] GetElementsThatHitTarget(int[] numbers, int target)
        {
            if (numbers.Length < 2)
            {
                logger.LogError("Not enough numbers");
                return null;
            }

            if (numbers.Length == 2)
            {
                if (numbers.Sum() == target)
                {
                    return new int[] { 0, 1 };
                }
                else
                {
                    logger.LogError("Only 2 numbers supplied but doesn't add to target");
                    return null;
                }
            }


            foreach(var num in numbers.Select((value, index) => (value, index)))
            {
                var diff = target - num.value;
                logger.LogTrace($"{num.index} {num.value}");
                if (numbers.Any(n => n == diff) && (diff != num.value))
                {
                    return new int[] { num.index, Array.FindIndex(numbers, ni => ni == diff) };
                }
            }

            logger.LogError("No addends found.");
            return null;


        }

    }
}
