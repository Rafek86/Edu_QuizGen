using System.ComponentModel.DataAnnotations;

namespace Edu_QuizGen.Contracts.Quiz;

public record AssignQuizToRoomRequest(
     [Required(ErrorMessage = "Quiz ID is required")]
        int QuizId,

     [Required(ErrorMessage = "Room ID is required")]
        string RoomId
 );