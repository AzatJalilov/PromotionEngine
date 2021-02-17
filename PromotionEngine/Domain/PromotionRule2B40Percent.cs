namespace PromotionEngine.Domain
{
    using System.Collections.Generic;

    public class PromotionRule2B40Percent : IPromotionRule
    {
        private readonly ISingleItemPriceService singleItemPriceService;

        public PromotionRule2B40Percent(ISingleItemPriceService singleItemPriceService)
        {
            this.singleItemPriceService = singleItemPriceService;
        }

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
                    if (item.Value > 3)
                    {
                        newItems.Add(item.Key, item.Value - 2);
                    }
                }
                else
                {
                    newItems.Add(item.Key, item.Value);
                }
            }
            var bAmount = singleItemPriceService.GetPrice("B") * 2 * 0.4M;
            return ((int) bAmount, newItems);
        }
    }
}
