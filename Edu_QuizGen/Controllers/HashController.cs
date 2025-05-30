using Edu_QuizGen.DTOs;
using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Edu_QuizGen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HashController : ControllerBase
    {
        private readonly IHashService _hashService;
        private readonly IHashRepository _repository;

        public HashController(IHashService hashService, IHashRepository repository)
        {
            _hashService = hashService;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            try
            {
                var documents = await _repository.GetAllHashResponsesAsync();
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving documents: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(Guid id)
        {
            try
            {
                var document = await _repository.GetHashResponseByIdAsync(id.ToString());

                if (document == null)
                {
                    return NotFound($"Document with ID {id} not found");
                }

                return Ok(document);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving document: {ex.Message}");
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPdf( IFormFile file, [FromForm] int quizId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded or file is empty");
            }

            if (!file.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Only PDF files are accepted");
            }

            // First check if it's a duplicate
            var duplicateCheckResult = await _hashService.IsDuplicatePdfAsync(file);
            if (duplicateCheckResult.IsFailure)
            {
                return duplicateCheckResult.ToProblem();
            }

            if (duplicateCheckResult.Value.Exists)
            {
                return Conflict(new HashDuplicateResponse
                {
                    Message = "This PDF has already been uploaded",
                    Document = duplicateCheckResult.Value.Document
                });
            }

            // Save the PDF
            var saveResult = await _hashService.SavePdfAsync(file, quizId);
            if (saveResult.IsFailure)
            {
                return saveResult.ToProblem();
            }

            return CreatedAtAction(nameof(GetDocument),
                new { id = saveResult.Value.Id },
                new HashUploadResponse
                {
                    Message = "PDF uploaded successfully",
                    Document = saveResult.Value
                });
        }

        [HttpPost("check")]
        public async Task<IActionResult> CheckPdfExists( IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded or file is empty");
            }

            if (!file.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Only PDF files are accepted");
            }

            var result = await _hashService.IsDuplicatePdfAsync(file);
            if (result.IsFailure)
            {
                return result.ToProblem();
            }

            return Ok(result.Value);
        }
    }
}
