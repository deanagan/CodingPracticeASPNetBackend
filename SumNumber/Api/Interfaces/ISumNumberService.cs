using System.Collections.Generic;

namespace Api.Interfaces
{
    public interface ISumNumberService
    {
        List<int> GetElementsThatHitTarget(List<int> numbers, int target);
    }
}
