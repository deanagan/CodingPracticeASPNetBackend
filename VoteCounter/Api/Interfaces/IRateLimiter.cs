using System.Collections.Generic;

namespace Api.Interfaces
{
    public interface IRateLimiter
    {
        bool RateLimit(string candidate);
    }
}
