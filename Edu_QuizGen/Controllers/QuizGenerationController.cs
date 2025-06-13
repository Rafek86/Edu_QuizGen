using Edu_QuizGen.Service_Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Edu_QuizGen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizGenerationController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizGenerationController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost("generate/{quizId}")]
        public async Task<IActionResult> GenerateQuestions(int quizId, IFormFile pdfFile)
        {
            if (pdfFile == null || pdfFile.Length == 0)
                return BadRequest("PDF file is required");

            if (!pdfFile.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only PDF files are allowed");

            var result = await _quizService.GenerateQuestionsAsync(quizId, pdfFile);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(new { questions = result.Value, message = "Questions generated successfully" });
        }

        [HttpPost("generate-for-student/{quizId}")]
        public async Task<IActionResult> GenerateStudentQuestions(int quizId, IFormFile pdfFile)
        {
            if (pdfFile == null || pdfFile.Length == 0)
                return BadRequest("PDF file is required");

            if (!pdfFile.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only PDF files are allowed");

            var result = await _quizService.GenerateQuestionsAsync(quizId, pdfFile);

            if (result.IsFailure)
                return BadRequest(result.Error);


            var questions = result.Value.Take(10).ToList();
            var totalGenerated = result.Value.Count();

            return Ok(new
            {
                questions,
                message = totalGenerated switch
                {
                    0 => "No questions could be generated from the file.",
                    < 10 => $"Only {questions.Count} question(s) generated due to limited content.",
                    _ => "Questions generated successfully"
                }
            });
        }

        [HttpGet("generated-questions/{quizId}")]
        public async Task<IActionResult> GetGeneratedQuestions(int quizId)
        {
            var result = await _quizService.GetGeneratedQuestionsAsync(quizId);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("student-generated-questions/{quizId}")]
        public async Task<IActionResult> GetStudentGeneratedQuestions(int quizId)
        {
            var result = await _quizService.GetGeneratedQuestionsAsync(quizId);

            if (result.IsFailure)
                return BadRequest(result.Error);


            var questions = result.Value.Take(10).ToList();
            var totalGenerated = result.Value.Count();

            return Ok(new
            {
                questions,
                message = totalGenerated switch
                {
                    0 => "No questions could be generated from the file.",
                    < 10 => $"Only {questions.Count} question(s) generated due to limited content.",
                    _ => "Questions generated successfully"
                }
            });
        }

        [HttpPost("save-selected/{quizId}")]
        public async Task<IActionResult> SaveSelectedQuestions(int quizId, [FromBody] List<int> selectedQuestionIds)
        {
            if (selectedQuestionIds == null || !selectedQuestionIds.Any())
                return BadRequest("At least one question must be selected");

            var result = await _quizService.SaveSelectedQuestionsAsync(quizId, selectedQuestionIds);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(new { message = "Selected questions saved successfully" });
        }
    }
}
