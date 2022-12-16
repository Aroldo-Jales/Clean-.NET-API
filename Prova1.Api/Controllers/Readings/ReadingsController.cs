using Microsoft.AspNetCore.Mvc;
using Prova1.Application.Common.Interfaces.Services.Readings.Commands;
using Prova1.Application.Common.Interfaces.Services.Readings.Queries;
using Prova1.Contracts.Readings.Request;
using Prova1.Contracts.Readings.Response;
using Prova1.Domain.Entities.Readings;
using System.Security.Claims;
using System.Text.Json;

namespace Prova1.Api.Controllers.Readings
{
    [ApiController]
    [Route("readings")]
    public class ReadingsController : Controller
    {
        private readonly IReadingsCommandService _readingsCommandService;
        private readonly IReadingsQueryService _readingsQueryService;

        private readonly IAnnotationsCommandService _annotationsCommandService;
        private readonly IAnnotationsQueryService _annotationsQueryService;

        public ReadingsController(
            IReadingsCommandService readingsCommandService, 
            IReadingsQueryService readingsQueryService,
            IAnnotationsCommandService annotationsCommandService,
            IAnnotationsQueryService annotationsQueryService)
        {            
            _readingsCommandService = readingsCommandService;
            _readingsQueryService = readingsQueryService;
            _annotationsCommandService = annotationsCommandService;
            _annotationsQueryService = annotationsQueryService;
        }

        [HttpPost("add-reading")]
        public async Task<IActionResult> AddReading(ReadingRequest request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Reading reading = new Reading(
                new Guid(userId),
                request.Title,
                request.SubTitle,
                request.Stopped,
                request.Completed,
                request.CurrentPage
            );

            await _readingsCommandService.AddReadingAsync(reading);

            return Ok(request);
        }

        [HttpGet("all")]
        public async Task<IActionResult> ListAllReadingsAsync()
        {
            var readings = await _readingsQueryService.AllReadingsAsync(10);

            return Ok(JsonSerializer.Serialize(readings));
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveReading(Guid id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (await _readingsQueryService.GetReadingById(id) is not Reading reading || reading.UserId != new Guid(userId))
            {
                return NotFound();
            }

            await _readingsCommandService.RemoveReadingAsync(id);

            return StatusCode(204);
        }

        [HttpPut("update-reading-page/{id}/{page}")]
        public async Task<IActionResult> UpdateReadingPage(Guid id, int page)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (await _readingsQueryService.GetReadingById(id) is not Reading reading || reading.UserId != new Guid(userId))
            {
                return NotFound();
            }
            
            reading.CurrentPage = page;

            await _readingsCommandService.UpdateReadingAsync(reading);

            ReadingResponse readingResponse = new ReadingResponse(
                reading.Title,
                reading.SubTitle,
                reading.Stopped,
                reading.Completed,
                reading.CurrentPage
            );

            return Ok(readingResponse);            
        }

        [HttpPost("add-annotation/{id}")]
        public async Task<IActionResult> AddAnnotation(Guid id, [FromBody] AnnotationRequest request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (await _readingsQueryService.GetReadingById(id) is not Reading reading || reading.UserId != new Guid(userId))
            {
                return NotFound();
            }

            Annotation annotation = new Annotation(
                id,
                request.Content,
                request.Page
            );

            var addedAnnotation = await _annotationsCommandService.AddAnnotationAsync(annotation);

            return Ok(JsonSerializer.Serialize(addedAnnotation));            
        }

        [HttpPost("update-annotation/{id}")]
        public async Task<IActionResult> UpdateAnnotationAsync(Guid id, [FromBody] AnnotationRequest request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (await _annotationsQueryService.GetAnnotationById(id) is not Annotation annotation || 
                _readingsQueryService.GetReadingById(annotation.ReadingId).Result.UserId != new Guid(userId))
            {
                return NotFound();
            }

            annotation.Content = request.Content;
            annotation.Page = request.Page;

            var updatedAnnotation = await _annotationsCommandService.UpdateAnnotationAsync(annotation); 

            return Ok(JsonSerializer.Serialize(updatedAnnotation));
        }   

        [HttpGet("annotations/{id}")]
        public async Task<IActionResult> AnnotationsByReadingAsync(Guid id)
        {
            if (await _readingsQueryService.GetReadingById(id) is not Reading reading)
            {
                return NotFound();
            }

            var annotations = await _annotationsQueryService.GetAllAnnotionsByReading(reading.Id);

            return Ok(JsonSerializer.Serialize(annotations));
        }

        [HttpPut("stopped-reading/{id}/{stopped}")]
        public async Task<IActionResult> StopReading(Guid id, bool stopped)
        {
            if (await _readingsQueryService.GetReadingById(id) is not Reading reading)
            {
                return NotFound();
            }

            reading.Stopped = stopped;
            var updatedReading = await _readingsCommandService.UpdateReadingAsync(reading);

            return Ok(JsonSerializer.Serialize(updatedReading));
        }

        [HttpPut("finish-reading/{id}/{finished}")]
        public async Task<IActionResult> FinishReading(Guid id, bool finished)
        {
            if (await _readingsQueryService.GetReadingById(id) is not Reading reading)
            {
                return NotFound();
            }

            reading.Completed = finished;
            var updatedReading = await _readingsCommandService.UpdateReadingAsync(reading);

            return Ok(JsonSerializer.Serialize(updatedReading));
        }
    }
}