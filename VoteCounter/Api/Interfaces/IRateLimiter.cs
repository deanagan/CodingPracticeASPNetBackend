using System.Collections.Generic;

namespace Api.Interfaces
{
    public interface IRateLimiter
    {
        bool RateLimit(int customerId);
    }
}
