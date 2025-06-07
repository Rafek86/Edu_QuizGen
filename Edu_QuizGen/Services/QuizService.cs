using AutoMapper.Configuration.Annotations;
using Edu_QuizGen.Contracts.Quiz;
using Edu_QuizGen.Errors;
using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;

namespace Edu_QuizGen.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IHashRepository _hashRepository;

    public QuizService(IQuizRepository quizRepository, IHashRepository hashRepository)
    {
        _quizRepository = quizRepository;
        _hashRepository = hashRepository;
    }

    public async Task<Result<IEnumerable<QuizResponse>>> GetAllQuizzesAsync()
    {
        var quizzes = await _quizRepository.GetAllAsync();
        var activeQuizzes = quizzes.Where(q => !q.IsDisabled)
            .Select(q => new QuizResponse(
                q.Id,
                q.Title,
                q.Description,
                q.IsDisabled,
                q.TotalQuestions,
                q.Hash?.FileHash
            ));

        return Result.Success(activeQuizzes);
    }

    public async Task<Result<QuizResponse>> GetQuizByIdAsync(int id)
    {
        var quiz = await _quizRepository.GetByIdAsync(id);
        if (quiz == null || quiz.IsDisabled)
            return Result.Failure<QuizResponse>(QuizErrors.NotFound);

        var response = new QuizResponse(
            quiz.Id,
            quiz.Title,
            quiz.Description,
            quiz.IsDisabled,
            quiz.TotalQuestions,
            quiz.Hash?.FileHash
        );

        return Result.Success(response);
    }

    public async Task<Result<QuizResponse>> CreateQuizAsync(CreateQuizRequest request)
    {
        var quiz = new Quiz
        {
            Title = request.Title,
            Description = request.Description,
            TotalQuestions = request.TotalQuestions,
            IsDisabled = false
        };


        await _quizRepository.AddAsync(quiz);

        var response = new QuizResponse(
            quiz.Id,
            quiz.Title,
            quiz.Description,
            quiz.IsDisabled,
            quiz.TotalQuestions,
            quiz.Hash.FileHash
        );

        return Result.Success(response);
    }

    public async Task<Result<QuizResponse>> UpdateQuizAsync(int id, UpdateQuizRequest request)
    {
        var quiz = await _quizRepository.GetByIdAsync(id);
        if (quiz == null || quiz.IsDisabled)
            return Result.Failure<QuizResponse>(QuizErrors.NotFound);

        // Update only provided fields
        if (!string.IsNullOrEmpty(request.Title))
            quiz.Title = request.Title;

        if (request.Description != null)
            quiz.Description = request.Description;

        if (request.TotalQuestions.HasValue)
            quiz.TotalQuestions = request.TotalQuestions.Value;

        if (request.IsDisabled.HasValue)
            quiz.IsDisabled = request.IsDisabled.Value;

        await _quizRepository.Update(quiz);

        var response = new QuizResponse(
            quiz.Id,
            quiz.Title,
            quiz.Description,
            quiz.IsDisabled,
            quiz.TotalQuestions,
            quiz.Hash?.FileHash
        );

        return Result.Success(response);
    }

    public async Task<Result> DeleteQuizAsync(int id)
    {
        var quiz = await _quizRepository.GetByIdAsync(id);
        if (quiz == null)
            return Result.Failure(QuizErrors.NotFound);

        if (quiz.IsDisabled)
            return Result.Failure(QuizErrors.AlreadyDisabled);

        quiz.IsDisabled = true;
        await _quizRepository.Update(quiz);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<QuizResponse>>> GetQuizzesByRoomIdAsync(string roomId)
    {
        var quizzes = await _quizRepository.GetAllQuizzesByRoomId(roomId);
        var response = quizzes.Select(q => new QuizResponse(
            q.Id,
            q.Title,
            q.Description,
            q.IsDisabled,
            q.TotalQuestions,
            q.Hash?.FileHash
        ));

        return Result.Success(response);
    }

    public async Task<Result<IEnumerable<QuizResponse>>> GetQuizzesByTeacherIdAsync(string teacherId)
    {
        var quizzes = await _quizRepository.GetQuizzesByTeacherIdAsync(teacherId);
        var response = quizzes.Select(q => new QuizResponse(
            q.Id,
            q.Title,
            q.Description,
            q.IsDisabled,
            q.TotalQuestions,
            q.Hash?.FileHash
        ));

        return Result.Success(response);
    }

    public async Task<Result> AssignQuizToRoomAsync(int quizId, string roomId)
    {
        var quiz = await _quizRepository.GetByIdAsync(quizId);
        if (quiz == null || quiz.IsDisabled)
            return Result.Failure(QuizErrors.NotFound);

        var isAssigned = await _quizRepository.IsQuizAssignedToRoomAsync(quizId, roomId);
        if (isAssigned)
            return Result.Failure(QuizErrors.AlreadyAssigned);

        var success = await _quizRepository.AssignQuizToRoomAsync(quizId, roomId);
        if (!success)
            return Result.Failure(QuizErrors.AlreadyAssigned);

        return Result.Success();
    }

    public async Task<Result> RemoveQuizFromRoomAsync(int quizId, string roomId)
    {
        var success = await _quizRepository.RemoveQuizFromRoomAsync(quizId, roomId);
        if (!success)
            return Result.Failure(QuizErrors.AssignmentNotFound);

        return Result.Success();
    }

    public async Task<Result<QuizDetailResponse>> GetQuizWithDetailsAsync(int id)
    {
        var quiz = await _quizRepository.GetQuizWithDetailsAsync(id);
        if (quiz == null || quiz.IsDisabled)
            return Result.Failure<QuizDetailResponse>(QuizErrors.NotFound);

        var assignedRooms = quiz.QuizRoom
            .Where(qr => !qr.IsDisabled)
            .Select(qr => new QuizRoomResponse(
                qr.RoomId,
                qr.Room.Name,
                qr.IsDisabled
            ));

        var response = new QuizDetailResponse(
            quiz.Id,
            quiz.Title,
            quiz.Description,
            quiz.IsDisabled,
            quiz.TotalQuestions,
            quiz.Hash?.FileHash,
            assignedRooms
        );

        return Result.Success(response);
    }

    public async Task<Result<IEnumerable<QuizResponse>>> GetActiveQuizzesAsync()
    {
        var quizzes = await _quizRepository.GetActiveQuizzesAsync();
        var response = quizzes.Select(q => new QuizResponse(
            q.Id,
            q.Title,
            q.Description,
            q.IsDisabled,
            q.TotalQuestions,
            q.Hash?.FileHash
        ));

        return Result.Success(response);
    }

    public async Task<Result<QuizResponse>> GetQuizByHashAsync(string hashValue)
    {
        var quiz = await _quizRepository.GetQuizByHashAsync(hashValue);
        if (quiz == null)
            return Result.Failure<QuizResponse>(QuizErrors.InvalidHash);

        var response = new QuizResponse(
            quiz.Id,
            quiz.Title,
            quiz.Description,
            quiz.IsDisabled,
            quiz.TotalQuestions,
            quiz.Hash?.FileHash
        );

        return Result.Success(response);
    }
}