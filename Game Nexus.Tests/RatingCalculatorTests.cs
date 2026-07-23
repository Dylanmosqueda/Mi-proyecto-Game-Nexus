using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Game_Nexus.API;

namespace Game_Nexus.Tests
{
    public class RatingCalculatorTests
    {
        [Fact]
        public void CalculateAverage_WithValidRatings_ReturnsCorrectAverage()
        {
            // Arrange (Preparar)
            var calculator = new RatingCalculator();
            var ratings = new List<int> { 4, 5, 3, 5 };

            // Act (Actuar)
            var result = calculator.CalculateAverage(ratings);

            // Assert (Afirmar)
            Assert.Equal(4.3, result);
        }

        [Fact]
        public void CalculateAverage_WithEmptyList_ReturnsZero()
        {
            // Arrange
            var calculator = new RatingCalculator();
            var ratings = new List<int>();

            // Act
            var result = calculator.CalculateAverage(ratings);

            // Assert
            Assert.Equal(0.0, result);
        }
    }
}
