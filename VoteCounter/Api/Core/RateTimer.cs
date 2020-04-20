using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Api.Interfaces;
using System;

using System.Timers;

namespace Api.Services
{
    public class RateTimer : ITimer
    {
        private ILogger<RateTimer> logger;

        private Dictionary<string, Timer> customerIdTimers;

        public RateTimer(ILogger<RateTimer> logger)
        {
            this.logger = logger;

            customerIdTimers = new Dictionary<string, Timer>();
        }

        public void StartTimer(string key, int timeInMilliSeconds, CallBack TimerExpiredCallback)
        {
            customerIdTimers[key] = new Timer(timeInMilliSeconds);
            customerIdTimers[key].Elapsed += (sender, e) => TimerExpiredCallback(key);
            customerIdTimers[key].AutoReset = false;
            customerIdTimers[key].Enabled = true;
            customerIdTimers[key].Start();
        }

        public bool IsTimerStarted(string key)
        {
            return customerIdTimers.ContainsKey(key) && customerIdTimers[key].Enabled;
        }
    }
}
