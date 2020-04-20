using System;

namespace Api.Interfaces
{
    public delegate void CallBack(string key);
    public interface ITimer
    {
        void StartTimer(string key, int timeInMilliSeconds, CallBack TimerExpiredCallback);
        bool IsTimerStarted(string key);
    }
}
