namespace PromotionEngine.Domain
{
    using System.Collections.Generic;

    public class PromotionRule : IPromotionRule
    {
        public bool IsApplying(Dictionary<string, int> items)
        {
            return true;
        }

        public int GetPrice(Dictionary<string, int> items)
        {
            return 0;
        }
    }
}
