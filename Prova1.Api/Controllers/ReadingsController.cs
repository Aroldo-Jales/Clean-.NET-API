using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Prova1.Api.Controllers
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