namespace PromotionEngine.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using PromotionEngine.Domain;

    [ApiController]
    [Route("[controller]")]
    public class CalculateController : ControllerBase
    {
        private readonly IAmountCalculator amountCalculator;

        public CalculateController(IAmountCalculator amountCalculator)
        {
            this.amountCalculator = amountCalculator;
        }

        [HttpGet]
        public int Get([FromQuery] Dictionary<string, int> items)
        {
            return amountCalculator.Calculate(items);
        }
    }
}
