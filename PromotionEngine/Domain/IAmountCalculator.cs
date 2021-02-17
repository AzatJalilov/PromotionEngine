using System.Collections.Generic;

namespace PromotionEngine.Domain
{
    public interface IAmountCalculator
    {
        int Calculate(Dictionary<string, int> items);
    }
}