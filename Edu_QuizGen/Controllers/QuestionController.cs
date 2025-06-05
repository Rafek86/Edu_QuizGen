using Edu_QuizGen.DTOs;
using Edu_QuizGen.Errors;
using Edu_QuizGen.Service_Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edu_QuizGen.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController(IQuestionSevice service) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] QuestionDTO questionDto)
        {
            var result = await service.AddQuestionAsync(questionDto);
            if (result.IsFailure)
                return result.ToProblem();

            return Ok("Question added successfully.");
        }

        [HttpPost("AddAll")]
        public async Task<ActionResult> AddAll([FromBody] IEnumerable<QuestionDTO> questionsDto)
        {
            var result = await service.AddQuestionAsync(questionsDto);
            if (result.IsFailure)
                return result.ToProblem();

            return Ok("All questions added successfully.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDTO>>> Get()
        {
            var result = await service.GetAllQuestionsAsync();
            if (result.IsFailure)
                return result.ToProblem();

            return Ok(result.Value);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<QuestionDTO>>> GetPaged(int pageNumber = 1, int pageSize = 10)
        {
            var result = await service.GetPagedQuestionsAsync(pageNumber, pageSize);
            if (result.IsFailure)
                return result.ToProblem();


            return Ok(result.Value);
        }

        [HttpGet("{questionId}")]
        public async Task<ActionResult<QuestionDTO>> GetById(int questionId)
        {
            var result = await service.GetQuestionByIdAsync(questionId);

            if (result.IsFailure)
                return result.ToProblem();
            return Ok(result.Value);
        }

        [HttpGet("quiz/{QuizId}")]
        public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetQuestionsByQuizId(int QuizId)
        {
            var result = await service.GetQuestionsByQuizId(QuizId);

            if (result.IsFailure)
                return result.ToProblem();

            return Ok(result.Value);
        }

        [HttpGet("quiz/title/{QuizTitle}")] // if there is more than one quiz with the same title
        public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetQuestionsByQuizTitle(string QuizTitle)
        {
            var result = await service.GetQuestionsByQuizTitle(QuizTitle);

            if (result.IsFailure)
                return result.ToProblem();

            return Ok(result.Value);
        }

        [HttpGet("Type/{type}")]
        public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetQuestionsByType(QuestionType type)
        {
            var result = await service.GetQuestionsByTypeAsync(type);

            if (result.IsFailure)
                return result.ToProblem();

            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] QuestionDTO questionDto)
        {
            var result = await service.UpdateQuestion(id, questionDto);
            if (result.IsFailure)
                return result.ToProblem();

            return Ok("Question updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await service.DeleteQuestion(id);
            if (result.IsFailure)
                return result.ToProblem();

            return Ok("Question deleted successfully.");
        }

    }
}
