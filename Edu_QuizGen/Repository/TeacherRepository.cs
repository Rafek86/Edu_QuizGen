using Edu_QuizGen.Repository_Abstraction;

namespace Edu_QuizGen.Repository;

public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
{
    public TeacherRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
