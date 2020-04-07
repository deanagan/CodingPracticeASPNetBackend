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
        public SumNumberService(ILogger logger)
        {
            this.logger = logger;
        }

        public int AddNumbers(int[] nums)
        {
           return nums.Sum();
        }

    }
}
