using Edu_QuizGen.Contracts.Quiz;
using Edu_QuizGen.Service_Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Edu_QuizGen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuizzes()
        {
            var result = await _quizService.GetAllQuizzesAsync();
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuiz(int id)
        {
            var result = await _quizService.GetQuizByIdAsync(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetQuizWithDetails(int id)
        {
            var result = await _quizService.GetQuizWithDetailsAsync(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpGet("by-room/{roomId}")]
        public async Task<IActionResult> GetQuizzesByRoom(string roomId)
        {
            var result = await _quizService.GetQuizzesByRoomIdAsync(roomId);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("by-hidden-room/{roomId}")]
        public async Task<IActionResult> GetQuizzesByHiddenRoom(string roomId= "fb13d1e7-e9af-4704-8544-e01cc0140d6c")
        {
            var result = await _quizService.GetQuizzesByRoomIdAsync(roomId);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("by-teacher/{teacherId}")]
        public async Task<IActionResult> GetQuizzesByTeacher(string teacherId)
        {
            var result = await _quizService.GetQuizzesByTeacherIdAsync(teacherId);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        /*
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveQuizzes()
        {
            var result = await _quizService.GetActiveQuizzesAsync();
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        */
        [HttpGet("by-hash/{hashValue}")]
        public async Task<IActionResult> GetQuizByHash(string hashValue)
        {
            var result = await _quizService.GetQuizByHashAsync(hashValue);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpPost("{roomId}")]
        public async Task<IActionResult> CreateQuiz([FromRoute]string roomId,[FromBody] CreateQuizRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _quizService.CreateQuizAsync(roomId,request);
            return result.IsSuccess
                ? CreatedAtAction(nameof(GetQuiz), new { id = result.Value.Id }, result.Value)
                : BadRequest(result.Error);
        }


        [HttpPost("create-quiz-with-student{roomId}")]
        public async Task<IActionResult> CreateStudentQuiz([FromBody] CreateQuizRequest request, [FromRoute] string roomId = "fb13d1e7-e9af-4704-8544-e01cc0140d6c")
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _quizService.CreateQuizAsync(roomId,request);
            return result.IsSuccess
                ? CreatedAtAction(nameof(GetQuiz), new { id = result.Value.Id }, result.Value)
                : BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(int id, [FromBody] UpdateQuizRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _quizService.UpdateQuizAsync(id, request);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpGet("by-student")]
        public async Task<IActionResult> GetQuizzesByStudentIdQuery([FromQuery] string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId))
                return BadRequest("Student ID is required");

            var result = await _quizService.GetQuizzesByStudentIdAsync(studentId);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }


        [HttpPut("toggle/{quizId}")]
        public async Task<IActionResult> ToggleStatus(int quizId)
        {
            var result = await _quizService.ToggleStatus(quizId);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var result = await _quizService.DeleteQuizAsync(id);
            return result.IsSuccess ? NoContent() : NotFound(result.Error);
        }

        [HttpPost("assign-to-room")]
        public async Task<IActionResult> AssignQuizToRoom([FromBody] AssignQuizToRoomRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _quizService.AssignQuizToRoomAsync(request.QuizId, request.RoomId);
            return result.IsSuccess ? Ok("Quiz successfully assigned to room.") : BadRequest(result.Error);
        }

        [HttpDelete("{quizId}/remove-from-room/{roomId}")]
        public async Task<IActionResult> RemoveQuizFromRoom(int quizId, string roomId)
        {
            var result = await _quizService.RemoveQuizFromRoomAsync(quizId, roomId);
            return result.IsSuccess ? Ok("Quiz successfully removed from room.") : NotFound(result.Error);
        }
    }
}