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
        private ILogger logger;

        private Dictionary<int, Timer> customerIdTimers;

        public RateTimer(ILogger logger)
        {
            this.logger = logger;

            customerIdTimers = new Dictionary<int, Timer>();
        }

        public void StartTimer(int id, int timeInMilliSeconds, CallBack TimerExpiredCallback)
        {
            customerIdTimers[id] = new Timer(timeInMilliSeconds);
            customerIdTimers[id].Elapsed += (sender, e) => TimerExpiredCallback(id);
            customerIdTimers[id].AutoReset = false;
            customerIdTimers[id].Enabled = true;
            customerIdTimers[id].Start();
        }

        public bool IsTimerStarted(int id)
        {
            return customerIdTimers[id].Enabled;
        }
    }
}
