using Edu_QuizGen.Repository_Abstraction;

namespace Edu_QuizGen.Repository;

public class StudentRepository : GenericRepository<Student>, IStudentRepository
{
    public StudentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}