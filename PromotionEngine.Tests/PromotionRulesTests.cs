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

        public static IEnumerable<object[]> ItemsMemberData =>
            new List<object[]>
            {
                new object[] { new Dictionary<string, int>{ {"A", 3} } , true },
                new object[] { new Dictionary<string, int>{ {"B", 3} } , true }

            };

    }
}
