using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Api.Interfaces;

namespace Api.Services
{
    public class SumNumberService : ISumNumberService
    {
        private ILogger logger;
        public SumNumberService(ILogger logger)
        {
            this.logger = logger;
        }

        public List<int> GetElementsThatHitTarget()
        {

            return new List<int>();
        }

    }
}
