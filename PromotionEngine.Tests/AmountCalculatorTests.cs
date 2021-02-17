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
                    new object[] { Array.Empty<IPromotionRule>(), new Dictionary<string, int>{ {"A", 3} }, 300 },
                    new object[] { new[] { new PromotionRule() }, new Dictionary<string, int>{ {"A", 3} }, 200 },
                    new object[] { new[] { new PromotionRule() }, new Dictionary<string, int>{ {"A", 3}, {"B", 10} }, 2200 },
            };
    }

    internal class MockSingleItemPriceService : ISingleItemPriceService
    {
        public int GetPrice(string id)
        {
            return id switch {
                "A" => 100,
                "B" => 200,
                "C" => 300,
                "D" => 400,
                _ => int.MaxValue
            };
        }
    }
}
