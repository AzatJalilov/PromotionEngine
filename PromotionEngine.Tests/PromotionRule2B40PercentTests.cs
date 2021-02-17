namespace PromotionEngine.Tests
{
    using System.Collections.Generic;
    using FakeItEasy;
    using FluentAssertions;
    using PromotionEngine.Domain;
    using Xunit;

    public class PromotionRule2B40PercentTests
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
            var singlePriceService = A.Fake<ISingleItemPriceService>();
            A.CallTo(() => singlePriceService.GetPrice("B")).Returns(100);
            var promotionRule = new PromotionRule2B40Percent(singlePriceService);

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
                new object[] { new Dictionary<string, int>{ {"A", 3}, { "B", 1 } }, false, 0, new Dictionary<string, int>{ {"A", 3}, { "B", 1 } } },
                new object[] { new Dictionary<string, int> { { "A", 11 }, { "B", 10 } }, true, 80, new Dictionary<string, int>{ { "A", 11 }, { "B", 8 } } }
            };
    }
}
