namespace PromotionEngine.Domain
{
    using System.Collections.Generic;

    public class PromotionRule : IPromotionRule
    {
        public bool IsApplying(Dictionary<string, int> items)
        {
            return items.ContainsKey("A") && items["A"] > 2;
        }

        public (int amount, Dictionary<string, int> items) Apply(Dictionary<string, int> prices, Dictionary<string, int> items)
        {
            if (!IsApplying(items))
            {
                return (0, items);
            }

            return (200, new Dictionary<string, int>());
        }
    }
}
