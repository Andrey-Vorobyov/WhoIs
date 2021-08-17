using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using WhoIs.Common;
using WhoIs.Entities;
using WhoIs.Infrastructure;
using WhoIs.Infrastructure.Repositories;
using Xunit;

namespace WhoIs.UnitTests
{
    public class DomainRepositoryTests
    {
        // This is not working, mocking a DbContext is possible but bad practice and takes more efforts
        private readonly DomainDbContext _dbContextMock;
        private readonly DatabaseFacade _dbFacadeMock;

        private readonly IDomainRepository _repository;

        private string _domainName = "TestName";

        public DomainRepositoryTests()
        {
            _dbContextMock = Substitute.For<DomainDbContext>();
            _dbFacadeMock = Substitute.For<DatabaseFacade>();
            _dbContextMock.Domains.Returns(new DbSetClass
            {
                new()
                {
                    Name = _domainName
                }
            });
            _dbContextMock.Database.Returns(_dbFacadeMock);

            _repository = new DomainRepository(_dbContextMock, NullLogger<DomainRepository>.Instance);
        }

        [Fact]
        public async Task When_NoConnectionToDb_Should_ThrowApplicationException()
        {
            // Arrange
            _dbFacadeMock.CanConnectAsync().Returns(false);

            // Act
            Task GetDomainsAsync() => _repository.GetDomainsAsync(_domainName);

            // Assert
            await Assert.ThrowsAsync<ApplicationException>(GetDomainsAsync);
        }

        [Fact]
        public async Task When_NoDomainFound_Should_ThrowNotFoundException()
        {
            // Arrange
            _dbFacadeMock.CanConnectAsync().Returns(true);

            // Act
            Task GetDomainsAsync() => _repository.GetDomainsAsync("WrongName");

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(GetDomainsAsync);
        }

        [Fact]
        public async Task When_DomainFound_Should_ReturnIt()
        {
            // Arrange
            _dbFacadeMock.CanConnectAsync().Returns(false);

            // Act
            var result = await _repository.GetDomainsAsync(_domainName);

            // Assert
            Assert.Equal(_domainName, result.First().Name);
        }
    }

    class DbSetClass : DbSet<Domain>
    {

    }
}
