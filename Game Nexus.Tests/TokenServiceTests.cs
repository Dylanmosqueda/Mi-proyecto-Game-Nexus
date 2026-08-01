using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Game_Nexus.API;

namespace Game_Nexus.Tests
{
    public class TokenServiceTests
    {
        [Fact]
        public void ValidateKeyLength_WithKeyTooShort_ReturnsFalse()
        {
            // Arrange
            var service = new TokenService();
            var shortKey = "clave_corta_123";

            // Act
            var result = service.ValidateKeyLength(shortKey);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateKeyLength_WithValidKeyLength_ReturnsTrue()
        {
            // Arrange
            var service = new TokenService();
            var secureKey = "esta_es_una_clave_super_segura_de_32_bytes";

            // Act
            var result = service.ValidateKeyLength(secureKey);

            // Assert
            Assert.True(result);
        }
    }
}
