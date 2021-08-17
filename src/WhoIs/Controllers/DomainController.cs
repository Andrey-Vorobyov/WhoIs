using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoIs.Common;
using WhoIs.Infrastructure.Repositories;

namespace WhoIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DomainController : Controller
    {
        private readonly IDomainRepository _domainRepository;

        public DomainController(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                var result = await _domainRepository.GetDomainsAsync(name);

                return Ok(result);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
