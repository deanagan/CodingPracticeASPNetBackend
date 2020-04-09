using System;

namespace Api.Interfaces
{
    public delegate void CallBack(int customerId);
    public interface ITimer
    {
        void StartTimer(int id, int timeInMilliSeconds, CallBack TimerExpiredCallback);
        bool IsTimerStarted(int id);
    }
}
