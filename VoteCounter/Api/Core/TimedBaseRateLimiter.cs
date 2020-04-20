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
        private Dictionary<string, int> _customerIdRequestCounter;
        private readonly int _maxRequest;
        private readonly int _maxTimeInMilliSeconds;

        public TimedBaseRateLimiter(ITimer timer, IOptions<VoteCounterControllerSettings> options, ILogger<TimedBaseRateLimiter> logger)
        {
            this._timer = timer;
            this._maxRequest = options.Value.MaxRequests;
            this._maxTimeInMilliSeconds = options.Value.MaxTimeInMilliSeconds;
            this._logger = logger;
            this._customerIdRequestCounter = new Dictionary<string, int>();
        }

        public bool RateLimit(string candidate)
        {
            if (!_timer.IsTimerStarted(candidate))
            {
                _logger.LogTrace($"Timer started for Customer Id {candidate}");
                _timer.StartTimer(candidate, _maxTimeInMilliSeconds, (id) => _customerIdRequestCounter[id] = 0);
            }

            // Registration
            if (_customerIdRequestCounter.ContainsKey(candidate))
            {
                _customerIdRequestCounter[candidate]++;
            }
            else
            {
                _customerIdRequestCounter[candidate] = 0;
            }

            // Check
            return (_customerIdRequestCounter[candidate] < _maxRequest);
        }

    }
}
