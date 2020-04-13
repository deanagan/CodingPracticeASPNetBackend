using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using Api.Interfaces;


namespace Api.Services
{
    public class TimedBaseRateLimiter : IRateLimiter
    {
        private ILogger<TimedBaseRateLimiter> logger;
        private ITimer timer;
        private Dictionary<int, int> customerIdRequestCounter;
        private readonly int maxRequest;

        public TimedBaseRateLimiter(ITimer timer, IConfiguration config, ILogger<TimedBaseRateLimiter> logger)
        {
            this.timer = timer;
            this.maxRequest = Convert.ToInt32(config["MaxRequests"]);
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
