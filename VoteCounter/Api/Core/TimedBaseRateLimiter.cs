using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Api.Interfaces;
using Api.Controllers;

namespace Api.Services
{
    public class TimedBaseRateLimiter : IRateLimiter
    {
        private ILogger<TimedBaseRateLimiter> _logger;
        private ITimer _timer;
        private Dictionary<int, int> _customerIdRequestCounter;
        private readonly int _maxRequest;
        private readonly int _maxTimeInMilliSeconds;

        public TimedBaseRateLimiter(ITimer timer, IOptions<VoteCounterControllerSettings> options, ILogger<TimedBaseRateLimiter> logger)
        {
            this._timer = timer;
            this._maxRequest = options.Value.MaxRequests;
            this._maxTimeInMilliSeconds = options.Value.MaxTimeInMilliSeconds;
            this._logger = logger;
            this._customerIdRequestCounter = new Dictionary<int, int>();
        }

        public bool RateLimit(int customerId)
        {
            if (!_timer.IsTimerStarted(customerId))
            {
                _logger.LogTrace($"Timer started for Customer Id {customerId}");
                _timer.StartTimer(customerId, _maxTimeInMilliSeconds, (id) => _customerIdRequestCounter[id] = 0);
            }

            // Registration
            if (_customerIdRequestCounter.ContainsKey(customerId))
            {
                _customerIdRequestCounter[customerId]++;
            }
            else
            {
                _customerIdRequestCounter[customerId] = 0;
            }

            // Check
            return (_customerIdRequestCounter[customerId] < _maxRequest);
        }

    }
}
