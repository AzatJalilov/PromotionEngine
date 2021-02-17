namespace PromotionEngine.Tests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using PromotionEngine.Domain;
    using Xunit;

    public class PromotionRulesTests
    {
        [MemberData(nameof(ItemsMemberData))]
        [Theory]
        public void WhenRulesAreMatched_IsApplyingReturnsTrue(Dictionary<string, int> items, bool expected)
        {
            // Arrange
            var promotionRule = new PromotionRule
            {
            };

            // Act
            var result = promotionRule.IsApplying(items);

            // Assert
            result.Should().Be(expected);
        }

        [MemberData(nameof(ItemsWithPriceMemberData))]
        [Theory]
        public void WhenRulesAreMatched_ApplyReturnsCorrectResult(
            Dictionary<string, int> prices,  
            Dictionary<string, int> items, 
            int expectedAmount, 
            Dictionary<string, int> expectedItems)
        {
            // Arrange
            var promotionRule = new PromotionRule
            {
            };

            // Act
            var (resultAmount, resultItems) = promotionRule.Apply(prices, items);

            // Assert
            resultAmount.Should().Be(expectedAmount);
            resultItems.Should().BeEquivalentTo(expectedItems);
        }

        public static IEnumerable<object[]> ItemsMemberData =>
            new List<object[]>
            {
                new object[] { new Dictionary<string, int>{ {"A", 3} } , true },
                new object[] { new Dictionary<string, int> { { "A", 11 }, { "B", 10 } }, true },
                new object[] { new Dictionary<string, int>{ {"A", 2} } , false },
                new object[] { new Dictionary<string, int>{ {"B", 3} } , false },
            };

        public static IEnumerable<object[]> ItemsWithPriceMemberData =>
            new List<object[]>
            {
                new object[] {
                    new Dictionary<string, int> { { "A", 100 } }, new Dictionary<string, int>{ {"A", 3} } , 200, new Dictionary<string, int>()
                },
                new object[] {
                    new Dictionary<string, int> { { "A", 100 }, { "B", 200 } }, new Dictionary<string, int>{ {"A", 3}, { "B", 1 } } , 200, new Dictionary<string, int> { { "B", 1 } }
                },
                new object[] {
                    new Dictionary<string, int> { { "A", 100 }, { "B", 200 } }, new Dictionary<string, int>{ {"A", 4}, { "B", 1 } } , 200, new Dictionary<string, int> { { "A", 1 }, { "B", 1 } }
                }
            };
    }
}
