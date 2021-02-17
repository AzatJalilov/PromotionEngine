namespace PromotionEngine.Domain
{
    public class SingleItemPriceService : ISingleItemPriceService
    {
        public int GetPrice(string id)
        {
            return id switch
            {
                "A" => 50,
                "B" => 30,
                "C" => 20,
                "D" => 15,
                _ => int.MaxValue
            };
        }
    }
}
