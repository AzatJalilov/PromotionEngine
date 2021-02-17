namespace PromotionEngine.Domain
{
    public class SingleItemPriceService : ISingleItemPriceService
    {
        public int GetPrice(string id)
        {
            return id switch
            {
                "A" => 100,
                "B" => 200,
                "C" => 300,
                "D" => 400,
                _ => int.MaxValue
            };
        }
    }
}
