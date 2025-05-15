using Edu_QuizGen.DTOs;
using Edu_QuizGen.Errors;
using Edu_QuizGen.Service_Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edu_QuizGen.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OptionController(IOptionService _optionService) : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] OptionCreateDTO optionDto)
        {

            var result = await _optionService.AddOptionAsync(optionDto);
            if (result.IsFailure)
                return result.Error.Code switch
                {
                    "Option.QuestionNotFound" => NotFound(result.Error),
                    "Option.DuplicateOption" => Conflict(result.Error),
                    _ => BadRequest(result.Error)
                };

            return Ok("Option added successfully.");
        }

        [HttpGet("question/{questionId}")]
        public async Task<ActionResult> GetByQuestionId(int questionId)
        {
            if (questionId <= 0)
                return BadRequest(OptionErrors.InvalidQuestionId);

            var result = await _optionService.GetOptionsByQuestionId(questionId);
            if (result.IsFailure)
                return result.ToProblem();

            return Ok(result.Value);
        }

        [HttpGet("question/text/{questionText}")]
        public async Task<ActionResult> GetByQuestionText(string questionText)
        {
            if (string.IsNullOrWhiteSpace(questionText))
                return BadRequest(OptionErrors.InvalidQuestionText);

            var result = await _optionService.GetOptionsByQuestionText(questionText);
            if (result.IsFailure)
                return result.ToProblem();

            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] OptionDTO optionDto)
        {
            if (id <= 0)
                return BadRequest(OptionErrors.InvalidOptionId);

            var result = await _optionService.UpdateOption(id, optionDto);
            if (result.IsFailure)
                return result.Error.Code switch
                {
                    "Option.NotFound" => NotFound(result.Error),
                    "Option.DuplicateOption" => Conflict(result.Error),
                    _ => BadRequest(result.Error)
                };

            return Ok("Option updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(OptionErrors.InvalidOptionId);

            var result = await _optionService.DeleteOption(id);
            if (result.IsFailure)
                return result.ToProblem();

            return Ok(new { message = "Option deleted successfully." });
        }
    }
}