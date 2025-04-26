using Edu_QuizGen.Errors;
using Edu_QuizGen.Service_Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edu_QuizGen.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController(IQuestionSevice service) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Question>> Add([FromBody] Question question)
        {
            var result = await service.AddQuestionAsync(question);
            if (result.IsFailure)
                return result.ToProblem();

            return Ok("question is add successfully.");
        }

        [HttpPost("AddAll")]
        public async Task<ActionResult<IEnumerable<Question>>> AddAll([FromBody] IEnumerable<Question> questions)
        {
            var result = await service.AddQuestionAsync(questions);
            if (result.IsFailure)
                return result.ToProblem();

            return Ok("All questions is add successfully.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> Get()
        {
            var result = await service.GetAllQuestionsAsync();
            if (result.IsFailure)
                return result.ToProblem();

            return Ok(result.Value);
        }

        [HttpGet("{questionId}")]
        public async Task<ActionResult<Question>> GetById(int questionId)
        {
            var result = await service.GetQuestionByIdAsync(questionId);

            if (result.IsFailure)
                return result.ToProblem();
            return Ok(result.Value);
        }

        [HttpGet("quiz/{QuizId}")]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestionsByQuizId(int QuizId)
        {
            var result = await service.GetQuestionsByQuizId(QuizId);

            if (result.IsFailure)
                return result.ToProblem();

            return Ok(result.Value);
        }

        [HttpGet("quizTitle/{QuizTitle}")] // if there is more than one quiz with the same title
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestionsByQuizTitle(string QuizTitle)
        {
            var result = await service.GetQuestionsByQuizTitle(QuizTitle);

            if (result.IsFailure)
                return result.ToProblem();

            return Ok(result.Value);
        }

        [HttpGet("Type/{type}")]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestionsByTypeAsync(QuestionType type)
        {
            var result = await service.GetQuestionsByTypeAsync(type);

            if (result.IsFailure)
                return result.ToProblem();

            return Ok(result.Value);
        }


    }
}
