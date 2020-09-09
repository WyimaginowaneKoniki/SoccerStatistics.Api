using FluentAssertions;
using SoccerStatistics.Api.Database.Validations;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Validations
{
    public class PositionInFormationAttributeTests
    {
        private readonly PositionInFormationAttribute _positionAttribute;

        public PositionInFormationAttributeTests()
        {
            _positionAttribute = new PositionInFormationAttribute();
        }

        [Theory]
        [InlineData(0u)]
        [InlineData(11u)]
        public void ReturnFalseWhenFormationNumberIsNotBetweenOneAndTen(object number)
        => _positionAttribute.IsValid(number).Should().BeFalse();

        [Fact]
        public void ReturnTrueWhenFormationNumberIsBetweenOneAndTen()
        {
            for(uint i = 1; i < 11; i++)
                _positionAttribute.IsValid(i).Should().BeTrue();
        }
    }
}
