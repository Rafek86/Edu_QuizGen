using Edu_QuizGen.Contracts.Quiz;
using Edu_QuizGen.Service_Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Edu_QuizGen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {

        private readonly IQuizService _quizService;

        public ResultController(IQuizService quizResultService)
        {
            _quizService = quizResultService;
        }

        [HttpPost]
        public async Task<ActionResult<double>> AddQuizResult([FromBody] ResultRequest request)
        {
            var score = await _quizService.AddQuizResult(request);
            return Ok(score);
        }

        [HttpGet("{quizId}/{studentId}")]
        public async Task<ActionResult<double>> GetQuizResult(int quizId, string studentId)
        {
            var score = await _quizService.GetQuizResultById(quizId, studentId);
            if (score == null)
            {
                return NotFound("Quiz result not found.");
            }
            return Ok(score);
        }
    }
}
