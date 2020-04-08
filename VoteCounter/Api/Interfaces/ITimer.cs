using System;

namespace Api.Interfaces
{
    public delegate void CallBack(int customerId);
    public interface ITimer
    {
        void StartTimer(int id, CallBack TimerExpiredCallback);
        bool IsTimerStarted(int id);
    }
}
