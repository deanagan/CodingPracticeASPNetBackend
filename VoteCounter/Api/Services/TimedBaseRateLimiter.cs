using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Api.Interfaces;
using System;

namespace Api.Services
{
    public class TimedBaseRateLimiter : IRateLimiter
    {
        private ILogger logger;
        private ITimer timer;
        private Dictionary<int, int> customerIdRequestCounter;
        private readonly int MAX_REQUESTS = 1000;

        public TimedBaseRateLimiter(ILogger logger, ITimer timer)
        {
            this.logger = logger;
            this.timer = timer;
            customerIdRequestCounter = new Dictionary<int, int>();
        }

        public bool RateLimit(int customerId)
        {
            if (!timer.IsTimerStarted(customerId))
            {
                timer.StartTimer(customerId, (id) => customerIdRequestCounter[id] = 0);
            }

            // Registration
            if (customerIdRequestCounter.ContainsKey(customerId))
            {
                customerIdRequestCounter[customerId]++;
            }
            else
            {
                customerIdRequestCounter[customerId] = 0;
            }

            // Check
            return (customerIdRequestCounter[customerId] < MAX_REQUESTS);
        }

    }
}
