using Edu_QuizGen.Contracts.Rooms;
using Edu_QuizGen.Service_Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Edu_QuizGen.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController(IRoomService roomService) : ControllerBase
{
    private readonly IRoomService _roomService = roomService;

    [HttpPost]
    public async Task<IActionResult> AddRoom([FromBody] RoomAddRequest request)
    {
        var result = await _roomService.AddRoomAsync(request.Name, request.TeacherId);
        if (result.IsFailure)
            return result.ToProblem();

        return Ok("Room created successfully.");
    }

    [HttpGet("teacher/{teacherId}")]
    public async Task<IActionResult> GetRoomsByTeacher(string teacherId)
    {
        var result = await _roomService.GetRoomsByTeacherAsync(teacherId);
        if (result.IsFailure)
            return result.ToProblem();

        return Ok(result.Value);
    }

    [HttpPut("{roomId}")]
    public async Task<IActionResult> UpdateRoom(string roomId, [FromBody] RoomUpdateRequest request)
    {
        var result = await _roomService.UpdateRoomAsync(roomId, request.TeacherId, request.NewName);
        if (result.IsFailure)
            return result.ToProblem();

        return Ok("Room updated successfully.");
    }

    [HttpDelete("{roomId}/{teacherId}")]
    public async Task<IActionResult> DeleteRoom(string roomId, string teacherId)
    {
        var result = await _roomService.DeleteRoomAsync(roomId, teacherId);
        if (result.IsFailure)
            return result.ToProblem();

        return Ok("Room deleted successfully.");
    }

    [HttpPost("student/{roomId}/{studentId}")]
    public async Task<IActionResult> JoinRoom(string roomId, string studentId)
    {
        var result = await _roomService.JoinRoomAsync(roomId, studentId);
        if (result.IsFailure)
            return result.ToProblem();

        return Ok("Joined room successfully.");
    }

    [HttpDelete("student/{roomId}/{studentId}")]
    public async Task<IActionResult> LeaveRoom(string roomId, string studentId)
    {
        var result = await _roomService.LeaveRoomAsync(roomId, studentId);
        if (result.IsFailure)
            return result.ToProblem();
        return Ok("Left room successfully.");
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetAllStudentRooms(string studentId)
    {
        var result = await _roomService.GetAllStudentRoomsAsync(studentId);
        if (result.IsFailure)
            return result.ToProblem();
        return Ok(result.Value);
    }

}
