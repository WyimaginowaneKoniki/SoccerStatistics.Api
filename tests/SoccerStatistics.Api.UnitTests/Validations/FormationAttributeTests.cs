using FluentAssertions;
using SoccerStatistics.Api.Database.Validations;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Validations
{
    public class FormationAttributeTests
    {
        private readonly FormationAttribute _formationAttribute;

        public FormationAttributeTests()
        {
            _formationAttribute = new FormationAttribute();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ReturnFalseWhenFormationIsNullOrEmpty(object formation)
            => _formationAttribute.IsValid(formation).Should().BeFalse();

        [Theory]
        [InlineData("1")]
        [InlineData("1-1")]
        public void ReturnFalseWhenFormationHaveLessThanThreeLine(object formation)
            => _formationAttribute.IsValid(formation).Should().BeFalse();

        [Theory]
        [InlineData("9-1-1")]
        [InlineData("1-10-1")]
        [InlineData("1-1-0")]
        public void ReturnFalseWhenFormationHaveWrongNumberOfPlayersInLine(object formation)
            => _formationAttribute.IsValid(formation).Should().BeFalse();

        [Fact]
        public void ReturnFalseWhenFormationHaveUnexpectedSymbol()
            => _formationAttribute.IsValid("1e-1-1").Should().BeFalse();

        [Theory]
        [InlineData("8-7-1")]
        [InlineData("1-1-1")]
        public void ReturnFalseWhenFormationDifferentNumberOfPlayersThanTen(object formation)
            => _formationAttribute.IsValid(formation).Should().BeFalse();

        [Theory]
        [InlineData("2-3-5")]
        [InlineData("4-4-1-1")]
        [InlineData("4-1-2-1-2")]
        public void ReturnTrueWhenFormationIsCorrect(object formation)
            => _formationAttribute.IsValid(formation).Should().BeTrue();
    }
}
