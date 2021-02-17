namespace PromotionEngine.Domain
{
    using System.Collections.Generic;

    public interface IPromotionRule
    {
        (int amount, Dictionary<string, int> items) Apply(Dictionary<string, int> items);

        bool IsApplying(Dictionary<string, int> items);
    }
}