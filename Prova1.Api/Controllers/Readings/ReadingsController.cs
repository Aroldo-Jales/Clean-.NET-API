using Microsoft.AspNetCore.Mvc;
using Prova1.Contracts.Readings.Request;
using System.Security.Claims;

namespace Prova1.Api.Controllers.Readings
{
    [ApiController]
    [Route("readings")]
    public class ReadingsController : Controller
    {
        
        [HttpPost("add-reading")]
        public IActionResult AddReading(ReadingRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpGet("all")]
        public IActionResult ListAllReadings()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult RemoveReading(Guid id)
        {
            return Ok();
        }


        [HttpPut("update-reading-page/{id}/{page}")]
        public IActionResult UpdateReadingPage(Guid id, int page)
        {
            throw new NotImplementedException();
        }

        [HttpPost("add-annotation/{id}")]
        public IActionResult AddAnnotation(AnnotationRequest request, Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("update-annotation/{id}")]
        public IActionResult UpdateAnnotation(AnnotationRequest request, Guid id)
        {
            throw new NotImplementedException();
        }   

        [HttpGet("annotations/{id}")]
        public IActionResult AnnotationsByReading(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPut("stop-reading/{id}")]
        public IActionResult StopReading(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPut("finish-reading/{id}")]
        public IActionResult FinishReading(Guid id)
        {
            throw new NotImplementedException();
        }        
    }
}