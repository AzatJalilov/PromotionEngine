namespace PromotionEngine.Tests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using PromotionEngine.Domain;
    using Xunit;

    public class PromotionRuleCAndDTests
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
            var promotionRule = new PromotionRuleCAndD();

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
                new object[] { new Dictionary<string, int>{ {"C", 3} }, false, 0, new Dictionary<string, int>{ {"C", 3} } },
                new object[] { new Dictionary<string, int>{ {"D", 3} }, false, 0, new Dictionary<string, int>{ {"D", 3} } },
                new object[] { new Dictionary<string, int>{ {"C", 3}, { "D", 1 } }, true, 30, new Dictionary<string, int> { { "C", 2 } } },
                new object[] { new Dictionary<string, int>{ {"D", 3}, { "C", 1 } }, true, 30, new Dictionary<string, int> { { "D", 2 } } },
                new object[] { new Dictionary<string, int>{ {"D", 1}, { "C", 1 } }, true, 30, new Dictionary<string, int>() },
            };
    }
}
