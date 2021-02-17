using System.Collections.Generic;

namespace PromotionEngine.Domain
{
    public interface IPromotionRule
    {
        int GetPrice(Dictionary<string, int> items);
        bool IsApplying(Dictionary<string, int> items);
    }
}