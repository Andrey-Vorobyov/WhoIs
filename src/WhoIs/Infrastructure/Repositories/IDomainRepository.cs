using System.Collections.Generic;
using System.Threading.Tasks;
using WhoIs.Entities;

namespace WhoIs.Infrastructure.Repositories
{
    public interface IDomainRepository
    {
        Task<IEnumerable<Domain>> GetDomainsAsync(string domainName);
    }
}
