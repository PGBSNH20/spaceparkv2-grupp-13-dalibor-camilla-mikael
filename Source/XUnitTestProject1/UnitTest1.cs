using Xunit;
using WebApplication1.Controllers;
using Moq.EntityFrameworkCore;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var systemUnderTest = new ParkingController();

            // Act
            var result = systemUnderTest.Get();

            // Assert
            Assert.Equal("", result);
        }
    }
}
