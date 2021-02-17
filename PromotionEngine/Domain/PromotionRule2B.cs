namespace PromotionEngine.Domain
{
    using System.Collections.Generic;

    public class PromotionRule2B : IPromotionRule
    {
        public bool IsApplying(Dictionary<string, int> items)
        {
            return items.ContainsKey("B") && items["B"] > 1;
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
                if (item.Key == "B")
                {
                    if (item.Value > 2)
                    {
                        newItems.Add(item.Key, item.Value - 2);
                    }
                }
                else
                {
                    newItems.Add(item.Key, item.Value);
                }
            }
            return (45, newItems);
        }
    }
}
