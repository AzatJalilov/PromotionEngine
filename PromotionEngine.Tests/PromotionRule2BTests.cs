namespace PromotionEngine.Tests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using PromotionEngine.Domain;
    using Xunit;

    public class PromotionRule2BTests
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
            var promotionRule = new PromotionRule2B();

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
                new object[] { new Dictionary<string, int>{ {"B", 2} }, true, 45, new Dictionary<string, int>() },
                new object[] { new Dictionary<string, int> { { "B", 11 }, { "A", 10 } }, true, 45, new Dictionary<string, int>{ { "B", 9 }, { "A", 10 } } },
                new object[] { new Dictionary<string, int>{ {"B", 1} }, false, 0, new Dictionary<string, int>{ {"B", 1} } },
                new object[] { new Dictionary<string, int>{ {"A", 3} }, false, 0, new Dictionary<string, int>{ {"A", 3} } },
            };
    }
}
