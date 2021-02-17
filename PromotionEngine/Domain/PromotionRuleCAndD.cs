namespace PromotionEngine.Domain
{
    using System.Collections.Generic;

    public class PromotionRuleCAndD : IPromotionRule
    {
        public bool IsApplying(Dictionary<string, int> items)
        {
            return items.ContainsKey("C") && items.ContainsKey("D");
        }

        public (int amount, Dictionary<string, int> items) Apply(Dictionary<string, int> items)
        {
            if (!IsApplying(items))
            {
                return (0, items);
            }
            var newItems = new Dictionary<string, int>();
            foreach(var item in items)
            {
                if (item.Key == "C" || item.Key == "D")
                {
                    if (item.Value > 1)
                    {
                        newItems.Add(item.Key, item.Value - 1);
                    }
                }
                else
                {
                    newItems.Add(item.Key, item.Value);
                }
            }
            return (130, newItems);
        }
    }
}
