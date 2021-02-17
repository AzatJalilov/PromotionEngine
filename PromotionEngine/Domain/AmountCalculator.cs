namespace PromotionEngine.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    public class AmountCalculator
    {
        private readonly IEnumerable<IPromotionRule> promotionRules;
        private readonly ISingleItemPriceService singleItemPriceService;

        public AmountCalculator(IEnumerable<IPromotionRule> promotionRules, ISingleItemPriceService singleItemPriceService)
        {
            this.promotionRules = promotionRules;
            this.singleItemPriceService = singleItemPriceService;
        }

       public int Calculate(Dictionary<string, int> items)
        {
            if (items.Count == 0)
            {
                return 0;
            }

            var currentBestAmount = int.MaxValue;

            foreach(var promotionRule in promotionRules)
            {
                if (promotionRule.IsApplying(items))
                {
                    var (ruleAmount, newItems) = promotionRule.Apply(items);
                    var amount = ruleAmount + Calculate(newItems);
                    if (amount < currentBestAmount)
                    {
                        currentBestAmount = amount;
                    }
                }
            }

            foreach(var item in items)
            {
                var newItems = items.Where(x => x.Key != item.Key).ToDictionary(s => s.Key, s => s.Value);
                var amount = item.Value * singleItemPriceService.GetPrice(item.Key) + Calculate(newItems); 
                if (amount < currentBestAmount)
                {
                    currentBestAmount = amount;
                }
            }
            return currentBestAmount;
        }
    }
}
