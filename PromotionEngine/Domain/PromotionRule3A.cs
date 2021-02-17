namespace PromotionEngine.Domain
{
    using System.Collections.Generic;

    public class PromotionRule3A : IPromotionRule
    {
        public bool IsApplying(Dictionary<string, int> items)
        {
            return items.ContainsKey("A") && items["A"] > 2;
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
                if (item.Key == "A")
                {
                    if (item.Value > 3)
                    {
                        newItems.Add(item.Key, item.Value - 3);
                    }
                }
                else
                {
                    newItems.Add(item.Key, item.Value);
                }
            }
            return (200, newItems);
        }
    }
}
