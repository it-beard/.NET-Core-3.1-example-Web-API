using System;
using System.Threading.Tasks;
using Itbeard.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Itbeard.Web.Controllers
{
    [ApiController]
    [Route("api/shortener")]
    public class ShortenerControllers : ApiControllerBase
    {
        private readonly IUrlService urlService;

        public ShortenerControllers(IUrlService urlService)
        {
            this.urlService = urlService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(string url)
        {
            try
            {
                var result = await urlService.Reduce(url);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(string shortGuid)
        {
            try
            {
                var result = await urlService.Get(shortGuid);
                return Ok(result);
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }
    }
}