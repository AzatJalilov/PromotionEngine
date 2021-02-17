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
        public void WhenRulesAreMatched_ApplyReturnsCorrectResult(
            Dictionary<string, int> items, 
            bool expectedIsApplying,
            int expectedAmount, 
            Dictionary<string, int> expectedItems)
        {
            // Arrange
            var promotionRule = new PromotionRule();

            // Act
            var resultIsApplying = promotionRule.IsApplying(items);
            var (resultAmount, resultItems) = promotionRule.Apply(items);

            // Assert
            resultIsApplying.Should().Be(expectedIsApplying);
            resultAmount.Should().Be(expectedAmount);
            resultItems.Should().BeEquivalentTo(expectedItems);
        }

        public static IEnumerable<object[]> ItemsMemberData =>
            new List<object[]>
            {
                new object[] { new Dictionary<string, int>{ {"A", 3} }, true, 200, new Dictionary<string, int>() },
                new object[] { new Dictionary<string, int> { { "A", 11 }, { "B", 10 } }, true, 200, new Dictionary<string, int>{ { "A", 8 }, { "B", 10 } } },
                new object[] { new Dictionary<string, int>{ {"A", 2} }, false, 0, new Dictionary<string, int>{ {"A", 2} } },
                new object[] { new Dictionary<string, int>{ {"B", 3} }, false, 0, new Dictionary<string, int>{ {"B", 3} } },
            };
    }
}
