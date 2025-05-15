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
    public async Task<ActionResult<IEnumerable<Hash>>> GetAllDocuments()
    {
        var documents = await _repository.GetAllAsync();
        return Ok(documents);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Hash>> GetDocument(Guid id)
    {
        var document = await _repository.GetByIdAsync(id.ToString());
        if (document == null)
        {
            return NotFound();
        }
        return Ok(document);
    }

    [HttpPost("upload")]
    public async Task<ActionResult<Hash>> UploadPdf(IFormFile file,int quizId)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded or file is empty");
        }

        if (!file.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("Only PDF files are accepted");
        }

        try
        {
            var (isDuplicate, existingDocument) = await _hashService.IsDuplicatePdfAsync(file);

            if (isDuplicate)
            {
                return Conflict(new
                {
                    message = "This PDF has already been uploaded",
                    document = existingDocument
                });
            }

            // Save the PDF (both metadata to DB and file to storage)
            var document = await _hashService.SavePdfAsync(file,quizId);

            return CreatedAtAction(nameof(GetDocument), new { id = document.Id }, document);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("check")]
    public async Task<ActionResult> CheckPdfExists(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded or file is empty");
        }

        if (!file.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("Only PDF files are accepted");
        }

        try
        {
            var (isDuplicate, existingDocument) = await _hashService.IsDuplicatePdfAsync(file);

            if (isDuplicate)
            {
                return Ok(new
                {
                    exists = true,
                    message = "This PDF has already been uploaded",
                    document = existingDocument
                });
            }

            return Ok(new { exists = false, message = "This PDF has not been uploaded before" });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
}
