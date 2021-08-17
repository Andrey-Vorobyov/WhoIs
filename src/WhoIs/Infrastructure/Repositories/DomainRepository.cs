using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WhoIs.Common;
using WhoIs.Entities;

namespace WhoIs.Infrastructure.Repositories
{
    public class DomainRepository : IDomainRepository
    {

        private readonly DomainDbContext _dbContext;
        private readonly ILogger<DomainRepository> _logger;

        public DomainRepository(DomainDbContext dbContext, ILogger<DomainRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Domain>> GetDomainsAsync(string domainName)
        {
            _logger.LogInformation("Get domain by name {DomainName}", domainName);

            if (!await _dbContext.Database.CanConnectAsync())
            {
                _logger.LogError("Couldn't connect to Database");
                throw new ApplicationException();
            }

            var domains = _dbContext.Domains.Where(d => d.Name == domainName);

            if (!domains.Any())
            {
                _logger.LogInformation("No domain with name {DomainName}", domainName);

                throw new NotFoundException($"No domain with name {domainName}");
            }

            var result = domains
                .Include(d => d.Registrant)
                .Include(d => d.Admin)
                .Include(d => d.Tech)
                .ToList();

            return result;
        }
    }
}
