namespace PromotionEngine.Tests
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using PromotionEngine.Domain;
    using Xunit;

    public class AmountCalculatorTests
    {
        [MemberData(nameof(ItemsMemberData))]
        [Theory]
        public void WhenCalculateIsCalled_ItReturnsCorrectResult(IEnumerable<IPromotionRule> promotionRules, Dictionary<string, int> items, int expectedPrice)
        {
            // Act
            var sut = new AmountCalculator(promotionRules, new MockSingleItemPriceService());

            var result = sut.Calculate(items);

            // Assert
            result.Should().Be(expectedPrice);
        }

        public static IEnumerable<object[]> ItemsMemberData =>
            new List<object[]>
            {
                    new object[] { Array.Empty<IPromotionRule>(), new Dictionary<string, int>{ {"A", 3} }, 150 },
                    new object[] { new[] { new PromotionRule3A() }, new Dictionary<string, int>{ {"A", 3} }, 130 },
                    new object[] { new IPromotionRule[] { new PromotionRule3A(), new PromotionRule2B() }, new Dictionary<string, int>{ {"A", 3}, {"B", 10} }, 355 },
                    new object[] { new[] { new PromotionRule3A() }, new Dictionary<string, int>{ {"A", 3}, {"B", 10}, {"C", 10}, {"D", 5} }, 705 },
                    new object[] { new IPromotionRule[] { new PromotionRule3A(), new PromotionRuleCAndD() }, new Dictionary<string, int>{ {"A", 3}, {"B", 10}, {"C", 10}, {"D", 5} }, 680 },
                    new object[] { new IPromotionRule[] { new PromotionRule3A(), new PromotionRuleCAndD(), new PromotionRule2B40Percent(new MockSingleItemPriceService()) }, new Dictionary<string, int>{ {"A", 3}, {"B", 10}, {"C", 10}, {"D", 5} }, 500 },
                    new object[] { new IPromotionRule[] { new PromotionRule3A(), new PromotionRuleCAndD(), new PromotionRule2B() }, new Dictionary<string, int>{ {"A", 1}, {"B", 1}, {"C", 1} }, 100 },
                    new object[] { new IPromotionRule[] { new PromotionRule3A(), new PromotionRuleCAndD(), new PromotionRule2B() }, new Dictionary<string, int>{ {"A", 5}, {"B", 5}, {"C", 1} }, 370 },
                    new object[] { new IPromotionRule[] { new PromotionRule3A(), new PromotionRuleCAndD(), new PromotionRule2B() }, new Dictionary<string, int>{ {"A", 3}, {"B", 5}, {"C", 1}, { "D", 1 } }, 280 }
            };
    }

    internal class MockSingleItemPriceService : ISingleItemPriceService
    {
        public int GetPrice(string id)
        {
            return id switch {
                "A" => 50,
                "B" => 30,
                "C" => 20,
                "D" => 15,
                _ => int.MaxValue
            };
        }
    }
}
