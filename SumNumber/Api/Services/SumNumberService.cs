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
        public int[] GetElementsThatHitTarget(int[] nums, int target)
        {
            if (nums.Length < 2)
            {
                logger.LogError("Not enough numbers");
                return null;
            }

            if (nums.Length == 2)
            {
                if (nums.Sum() == target)
                {
                    return new int[] { 0, 1 };
                }
                else
                {
                    logger.LogError("Only 2 numbers supplied but doesn't add to target");
                    return null;
                }
            }

            var complements = new Dictionary<int, int>();
            //foreach(var num in nums.Select((value, index) => (value, index)))
            // mid to first search
            var midIndex = nums.Length / 2;
            for (var index = midIndex; index >= 0 ; index-- )
            {
                logger.LogTrace($"{index} {nums[index]}");
                var diff = target - nums[index];
                logger.LogTrace($"{diff}");
                if (complements.ContainsKey(diff))
                {
                    return new int[] { index, complements[diff]};
                }
                complements[nums[index]] = index;
            }

            logger.LogTrace("Searching mid to last");
            // Not found, we do mid to last search
            for (var index = midIndex + 1; index < nums.Length; index++ )
            {
                logger.LogTrace($"{index} {nums[index]}");
                var diff = target - nums[index];
                logger.LogTrace($"{diff}");
                if (complements.ContainsKey(diff))
                {
                    return new int[] { index, complements[diff]};
                }
                complements[nums[index]] = index;
            }

            //logger.LogError("No addends found.");
            return null;


        }

    }
}
