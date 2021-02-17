namespace PromotionEngine.Tests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using PromotionEngine.Domain;
    using Xunit;

    public class AmountCalculatorTests
    {
        private readonly AmountCalculator sut;

        public AmountCalculatorTests()
        {
            sut = new AmountCalculator();
        }

        [MemberData(nameof(ItemsMemberData))]
        [Theory]
        public void WhenCalculateIsCalled_ItReturnsCorrectResult(Dictionary<string, int> items, int expectedPrice)
        {
            // Act
            var result = sut.Calculate(items);

            // Assert
            result.Should().Be(expectedPrice);
        }

        public static IEnumerable<object[]> ItemsMemberData =>
            new List<object[]>
            {
                    new object[] { new Dictionary<string, int>{ {"A", 3} }, 200 },
            };
    }
}
