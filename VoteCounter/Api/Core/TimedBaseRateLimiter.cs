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
        private readonly int maxRequest;

        public TimedBaseRateLimiter(ITimer timer, int maxRequest, ILogger logger)
        {
            this.timer = timer;
            this.maxRequest = maxRequest;
            this.logger = logger;

            customerIdRequestCounter = new Dictionary<int, int>();
        }

        public bool RateLimit(int customerId)
        {
            if (!timer.IsTimerStarted(customerId))
            {
                logger.LogTrace($"Timer started for Customer Id {customerId}");
                timer.StartTimer(customerId, 1, (id) => customerIdRequestCounter[id] = 0);
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
            return (customerIdRequestCounter[customerId] < maxRequest);
        }

    }
}
