using Edu_QuizGen.Abstractions;
using Edu_QuizGen.Repository_Abstraction;

namespace Edu_QuizGen.Repository;

public class QuizResultRepository : IQuizResultRepository
{
    public readonly ApplicationDbContext _context;
    public QuizResultRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<QuizResult> AddQuizResultAsync(QuizResult quizResult)
    {
        await _context.QuizResults.AddAsync(quizResult);
        await _context.SaveChangesAsync();
        return quizResult;
    }

    public async Task<QuizResult?> GetQuizResultByIdAsync(int id)
    {
        return await _context.QuizResults
            .Include(qr => qr.Student)
            .Include(qr => qr.Quiz)
            .FirstOrDefaultAsync(qr => qr.Id == id && !qr.IsDisabled);
    }

    public async Task<QuizResult> GetQuizResultsByStudentIdAsync(string studentId, int quizId)
    {
        return await _context.QuizResults.
             Include(qr => qr.Student)
            .Include(qr => qr.Quiz)
            .Where(qr => qr.StudentId == studentId && qr.QuizId ==quizId  && !qr.IsDisabled)
            .FirstOrDefaultAsync();
    }

    public async Task<QuizResult> UpdateQuizResultAsync(QuizResult quizResult)
    {
        _context.QuizResults.Update(quizResult);
        await _context.SaveChangesAsync();
        return quizResult;
    }

    public async Task<bool> DeleteQuizResultAsync(int id)
    {
        var quizResult = await _context.QuizResults.FindAsync(id);
        if (quizResult == null)
            return false;

        quizResult.IsDisabled = true;
        await _context.SaveChangesAsync();
        return true;
    }
}