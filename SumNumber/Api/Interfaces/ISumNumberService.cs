using System.Collections.Generic;

namespace Api.Interfaces
{
    public interface ISumNumberService
    {
        int[] GetElementsThatHitTarget(int[] numbers, int target);
    }
}
