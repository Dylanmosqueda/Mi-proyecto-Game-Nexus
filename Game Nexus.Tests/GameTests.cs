using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.OpenApi;
using Xunit;
using Game_Nexus.API;

namespace Game_Nexus.Tests
{
    public class GameTests
    {
        [Fact]
        public void IsValid_WithValidGameData_ReturnsTrue()
        {
            // Arrange
            var game = new Game { Title = "Hades II", ReleaseYear = 2024 };

            // Act
            var result = game.IsValid();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_WithEmptyTitle_ReturnsFalse()
        {
            // Arrange
            var game = new Game { Title = "", ReleaseYear = 2024 };

            // Act
            var result = game.IsValid();

            // Assert
            Assert.False(result);
        }
    }
}
