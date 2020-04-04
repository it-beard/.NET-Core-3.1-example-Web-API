using System.Net;
using System.Threading.Tasks;
using Itbeard.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Itbeard.Web.Controllers
{
    [ApiController]
    [Route("api/shortener")]
    public class ShortenerControllers : ControllerBase
    {
        private readonly IUrlService urlService;
        
        public ShortenerControllers(IUrlService urlService)
        {
            this.urlService = urlService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(string url)
        {
            var result = await urlService.Reduce(url);

            return StatusCode((int)result.StatusCode, result);
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(string shortGuid)
        {
            var result = await urlService.Get(shortGuid);
            return Ok(result);
        }
    }
}