using System.Threading.Tasks;
using Xunit;

namespace WhoIs.IntegrationTests
{
    public class DomainTcpListenerTests
    {
        // This is not working as TcpClient isn't working as expected
        [Fact]
        public async Task When_DomainNotFound_Should_ReturnText()
        {
            // Arrange
            var domainName = "Wrong name";

            // Act
            var result = await TestTcpClient.Connect(domainName);

            // Assert
            Assert.Equal("Domain not found", result);
        }
    }
}
