using API_Project.Test;

namespace TestAPIProject
{
    [TestFixture]
    public class BinaryStringEvaluatorTests
    {
        [Test]
        public void IsGoodBinaryString_WithGoodBinaryString_ReturnsTrue()
        {
            // Arrange
            var goodBinaryStrings = new[] { "1100", "1010", "111000" }; 

            // Act & Assert
            foreach (var binaryString in goodBinaryStrings)
            {
                Assert.IsTrue(BinaryStringEvaluator.IsGoodBinaryString(binaryString), $"{binaryString} should be good");
            }
        }

        [Test]
        public void IsGoodBinaryString_WithBadBinaryString_ReturnsFalse()
        {
            // Arrange
            var badBinaryStrings = new[] { "1001", "0011", "1111", "0000" };

            // Act & Assert
            foreach (var binaryString in badBinaryStrings)
            {
                Assert.IsFalse(BinaryStringEvaluator.IsGoodBinaryString(binaryString), $"{binaryString} should not be good");
            }
        }

        [Test]
        public void IsGoodBinaryString_WithEmptyString_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => BinaryStringEvaluator.IsGoodBinaryString(string.Empty));
        }

        [Test]
        public void IsGoodBinaryString_WithNullString_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => BinaryStringEvaluator.IsGoodBinaryString(null));
        }

        [Test]
        public void IsGoodBinaryString_WithInvalidCharacter_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => BinaryStringEvaluator.IsGoodBinaryString("10102"));
        }
    }
}