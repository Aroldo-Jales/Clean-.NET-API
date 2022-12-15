using Microsoft.AspNetCore.Mvc;

namespace Prova1.Api.Controllers.Readings
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