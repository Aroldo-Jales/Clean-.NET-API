using Microsoft.AspNetCore.Mvc;

namespace Prova1.Api.Controllers.Read
{
    [ApiController]
    [Route("readings")]
    public class ReadingsController : Controller
    {
        [HttpGet("all")]
        public IActionResult ListAllReadings()
        {
            return Ok();
        }
    }
}