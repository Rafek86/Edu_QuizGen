using Edu_QuizGen.Contracts.Feedback;
using Edu_QuizGen.Service_Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Edu_QuizGen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController(IFeedbackService feedbackService) : ControllerBase
    {
        private readonly IFeedbackService _feedbackService =feedbackService;

        [HttpPost]
        public async Task<IActionResult> AddFeedback([FromBody] FeedbackAddRequest feedbackDto)
        {
            var result = await _feedbackService.AddFeedbackAsync(feedbackDto);
            if (result.IsFailure)
                return result.ToProblem();

            return CreatedAtAction(nameof(GetFeedbackByStudent), new { studentId = feedbackDto.StudentId }, feedbackDto);
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetFeedbackByStudent(string studentId)
        {
            var result = await _feedbackService.GetFeedbackByStudentAsync(studentId);
            if (result.IsFailure)
               return result.ToProblem();

            return Ok(result.Value);
        }

        [HttpPut("{feedbackId}/{studentId}")]
        public async Task<IActionResult> UpdateFeedback(int feedbackId, string studentId, [FromBody] string newComment)
        {
            var result = await _feedbackService.UpdateFeedbackAsync(feedbackId, studentId, newComment);
            if (result.IsFailure)
               return result.ToProblem();

            return Ok(true);
        }

        [HttpDelete("{feedbackId}/{studentId}")]
        public async Task<IActionResult> DeleteFeedback(int feedbackId, string studentId)
        {
            var result = await _feedbackService.DeleteFeedbackAsync(feedbackId, studentId);
            if (result.IsFailure)
              return result.ToProblem();

            return Ok("Feedback deleted successfully.");
        }
    }
}
