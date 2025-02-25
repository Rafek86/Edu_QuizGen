namespace Edu_QuizGen.Models;

public class Room
{
    public string Id { get; set; } 

    public string Name { get; set; } = string.Empty;

    public bool Status { get; set; }  //Or Make it Enum (Drafted , Processing ,Stoped)

    public string TeacherId { get; set; }
    public Teacher Teacher { get; set; }

    public ICollection<StudentRoom> StudentRooms { get; set; } = new List<StudentRoom>();
    public ICollection<QuizRoom> QuizRoom { get; set; }=new HashSet<QuizRoom>();
}
