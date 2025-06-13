using AutoMapper.Configuration.Annotations;
using Edu_QuizGen.Contracts.FastApi;
using Edu_QuizGen.Contracts.Quiz;
using Edu_QuizGen.Errors;
using Edu_QuizGen.Repository;
using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using TimeZoneConverter;

namespace Edu_QuizGen.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IHashService _hashService;
    private readonly IQuizGenerationService _quizGenerationService;
    private readonly IQuestionRepository _questionRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IQuizResultRepository _quizResult;

    public QuizService(IQuizRepository quizRepository, IHashService hashService, IRoomRepository roomRepository, IQuizGenerationService quizGenerationService, IQuestionRepository questionRepository,IStudentRepository studentRepository, IQuizResultRepository quizResult)
    {
        _quizRepository = quizRepository;
        _hashService = hashService;
        _roomRepository = roomRepository;
        _quizGenerationService = quizGenerationService;
        _questionRepository = questionRepository;
        _studentRepository = studentRepository;
        _quizResult = quizResult;
    }

    public async Task<Result<IEnumerable<QuizResponse>>> GetAllQuizzesAsync()
    {
        var quizzes = await _quizRepository.GetActiveQuizzesAsync();
        var activeQuizzes = quizzes.Where(q => !q.IsDisabled)
            .Select(q => new QuizResponse(
                q.Id,
                q.Title,
                q.Description,
                q.IsDisabled,
                q.quizQuestions.Count(),
                q.StartAt ?? DateTimeOffset.UtcNow,
                q.EndAt ?? DateTimeOffset.UtcNow,
                q.Duration ?? 0,
                q.AI ?? false,
                q.Hash?.FileHash ?? "manual"
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
            quiz.quizQuestions.Count(),
            quiz.StartAt ?? DateTimeOffset.UtcNow,
            quiz.EndAt ?? DateTimeOffset.UtcNow,
            quiz.Duration ?? 0,
            quiz.AI ?? false,
            quiz.Hash?.FileHash ?? "manual"
        );

        return Result.Success(response);
    }
    public async Task<Result<QuizResponse>> CreateQuizAsync(string roomId, CreateQuizRequest request)
    {
        // Get Cairo time zone (works cross-platform)
        var cairoTimeZone = TZConvert.GetTimeZoneInfo("Africa/Cairo");

        // Convert input times to Cairo time
        var startAtCairo = TimeZoneInfo.ConvertTime(request.StartAt.UtcDateTime, TimeZoneInfo.Utc, cairoTimeZone);
        var endAtCairo = TimeZoneInfo.ConvertTime(request.EndAt.UtcDateTime, TimeZoneInfo.Utc, cairoTimeZone);

        var quiz = new Quiz
        {
            Title = request.Title,
            Description = request.Description,
            TotalQuestions = request.TotalQuestions,
            IsDisabled = false,
            StartAt = new DateTimeOffset(startAtCairo, cairoTimeZone.GetUtcOffset(startAtCairo)),
            EndAt = new DateTimeOffset(endAtCairo, cairoTimeZone.GetUtcOffset(endAtCairo)),
            Duration = (int)(endAtCairo - startAtCairo).TotalMinutes,
            AI = request.AI
        };

        var room = await _roomRepository.GetByIdAsync(roomId);

        if (room is null || room.IsDisabled)
            return Result.Failure<QuizResponse>(QuizErrors.NotFound);

        await _quizRepository.AddAsync(quiz);
        var assign = await _quizRepository.AssignQuizToRoomAsync(quiz.Id, roomId);

        if (assign)
        {
            var response = new QuizResponse(
                quiz.Id,
                quiz.Title,
                quiz.Description,
                quiz.IsDisabled,
                quiz.quizQuestions.Count(),
                quiz.StartAt ?? DateTimeOffset.UtcNow,
                quiz.EndAt ?? DateTimeOffset.UtcNow,
                quiz.Duration ?? 0,
                quiz.AI ?? false,
                quiz.Hash?.FileHash ?? "manual"
            );

            return Result.Success(response);
        }

        return Result.Failure<QuizResponse>(QuizErrors.AssignmentNotFound);
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
            quiz.quizQuestions.Count(),
            quiz.StartAt ?? DateTimeOffset.UtcNow,
            quiz.EndAt ?? DateTimeOffset.UtcNow,
            quiz.Duration ?? 0,
            quiz.AI ?? false,
            quiz.Hash?.FileHash ?? "manual"
        );

        return Result.Success(response);
    }

    public async Task<Result<IEnumerable<QuizResponse>>> GetQuizzesByStudentIdAsync(string studentId)
    {
        try
        {
            var quizzes = await _quizRepository.GetQuizzesByStudentIdAsync(studentId);

            var response = quizzes.Select(q => new QuizResponse(
                q.Id,
                q.Title,
                q.Description,
                q.IsDisabled,
                q.quizQuestions.Count(),
                q.StartAt ?? DateTimeOffset.UtcNow,
                q.EndAt ?? DateTimeOffset.UtcNow,
                q.Duration ?? 0,
                q.AI ?? false,
                q.Hash?.FileHash ?? "manual"
            ));

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            return Result.Failure<IEnumerable<QuizResponse>>(QuizErrors.NotFound);
        }
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
            q.StartAt ?? DateTimeOffset.UtcNow,
            q.EndAt ?? DateTimeOffset.UtcNow,
            q.Duration ?? 0,
            q.AI ?? false,
            q.Hash?.FileHash ?? "manual"
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
            q.StartAt ?? DateTimeOffset.UtcNow,
            q.EndAt ?? DateTimeOffset.UtcNow,
            q.Duration ?? 0,
            q.AI ?? false,
            q.Hash?.FileHash ?? "manual"
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
            quiz.Hash?.FileHash ?? "manual",
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
            q.StartAt ?? DateTimeOffset.UtcNow,
            q.EndAt ?? DateTimeOffset.UtcNow,
            q.Duration ?? 0,
            q.AI ?? false,
            q.Hash?.FileHash ?? "manual"
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
            quiz.StartAt ?? DateTimeOffset.UtcNow,
            quiz.EndAt ?? DateTimeOffset.UtcNow,
            quiz.Duration ?? 0,
            quiz.AI ?? false,
            quiz.Hash?.FileHash ?? "manual"
        );

        return Result.Success(response);
    }
    public async Task<Result<List<QuestionResponse>>> GenerateQuestionsAsync(int quizId, IFormFile pdfFile)
    {
        try
        {
            var duplicateCheckResult = await _hashService.IsDuplicatePdfAsync(pdfFile);
            if (duplicateCheckResult.IsFailure)
                return Result.Failure<List<QuestionResponse>>(duplicateCheckResult.Error);

            if (duplicateCheckResult.Value.Exists)
            {
                var existingQuizId = duplicateCheckResult.Value.Document.QuizId;

                if (existingQuizId != quizId)
                {
                    var existingQuestions = await _questionRepository.GetQuestionsByQuizId(existingQuizId);

                    if (existingQuestions.Any())
                    {
                        var questionResponse = new List<QuestionResponse>();

                        foreach (var existingQuestion in existingQuestions.Where(q => !q.IsDisabled))
                        {
                            var question = new Question
                            {
                                Text = existingQuestion.Text,
                                Type = existingQuestion.Type,
                                CorrectAnswer = existingQuestion.CorrectAnswer,
                                Explanation = existingQuestion.Explanation,
                                QuizId = quizId,
                                IsDisabled = false,
                                Options = existingQuestion.Options?.Select(opt => new Option
                                {
                                    Text = opt.Text,
                                    IsDisabled = false
                                }).ToList() ?? new List<Option>()
                            };

                            await _questionRepository.AddAsync(question);

                            questionResponse.Add(new QuestionResponse
                            {
                                Id = existingQuestion.Id,
                                Text = existingQuestion.Text,
                                Type = existingQuestion.Type,
                                CorrectAnswer = existingQuestion.CorrectAnswer,
                                Explanation = existingQuestion.Explanation,
                                Options = existingQuestion.Options?.Where(opt => !opt.IsDisabled)
                                    .Select(opt => new OptionResponse
                                    {
                                        Id = opt.Id,
                                        Text = opt.Text
                                    }).ToList()
                            });
                        }

                        var currentQuiz = await _quizRepository.GetByIdAsync(quizId);
                        if (currentQuiz != null)
                        {
                            currentQuiz.TotalQuestions = questionResponse.Count;
                            await _quizRepository.Update(currentQuiz);
                        }

                        var saveHashResults = await _hashService.SavePdfAsync(pdfFile, quizId);
                        if (saveHashResults.IsFailure)
                        {
                        
                        }

                        return Result.Success(questionResponse);
                    }
                }
                else
                {
                    var existingQuestions = await _questionRepository.GetQuestionsByQuizId(existingQuizId);
                    return Result.Success(existingQuestions
                        .Where(q => !q.IsDisabled)
                        .Select(q => new QuestionResponse
                        {
                            Id = q.Id,
                            Text = q.Text,
                            Type = q.Type,
                            CorrectAnswer = q.CorrectAnswer,
                            Explanation = q.Explanation,
                            Options = q.Options?.Where(opt => !opt.IsDisabled)
                                .Select(opt => new OptionResponse
                                {
                                    Id = opt.Id,
                                    Text = opt.Text
                                }).ToList()
                        }).ToList());
                }
            }

            // If no duplicate hash found, generate new questions
            var result = await _quizGenerationService.GenerateQuestionsFromPdfAsync(quizId, pdfFile);
            if (result.IsFailure)
                return Result.Failure<List<QuestionResponse>>(result.Error);

            var saveHashResult = await _hashService.SavePdfAsync(pdfFile, quizId);
            if (saveHashResult.IsFailure)
            {
                return Result.Failure<List<QuestionResponse>>(saveHashResult.Error);
            }

            var questionResponses = result.Value.Select(q => new QuestionResponse
            {
                Id = q.Id,
                Text = q.Text,
                Type = q.Type,
                CorrectAnswer = q.CorrectAnswer,
                Explanation = q.Explanation,
                Options = q.Options?.Select(opt => new OptionResponse
                {
                    Id = opt.Id,
                    Text = opt.Text
                }).ToList()
            }).ToList();

            return Result.Success(questionResponses);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<QuestionResponse>>(QuizErrors.GenerationFailed);
        }
    }
    public async Task<Result<List<QuestionResponse>>> GetGeneratedQuestionsAsync(int quizId)
    {
        var result = await _quizGenerationService.GetGeneratedQuestionsAsync(quizId);
        if (result.IsFailure)
            return Result.Failure<List<QuestionResponse>>(result.Error);

        var questionResponses = result.Value.Select(q => new QuestionResponse
        {
            Id = q.Id,
            Text = q.Text,
            Type = q.Type,
            CorrectAnswer = q.CorrectAnswer,
            Explanation = q.Explanation,
            Options = q.Options?.Select(opt => new OptionResponse
            {
                Id = opt.Id,
                Text = opt.Text
            }).ToList()  
        }).ToList();

        return Result.Success(questionResponses);
    }

    public async Task<Result> SaveSelectedQuestionsAsync(int quizId, List<int> selectedQuestionIds)
    {
        return await _quizGenerationService.SaveSelectedQuestionsAsync(quizId, selectedQuestionIds);
    }

    public async Task<Result<QuizResponse>> ToggleStatus(int quizId)
    {
        var quiz = await _quizRepository.GetByIdAsync(quizId);
        if (quiz == null)
            return Result.Failure<QuizResponse>(QuizErrors.NotFound);
        quiz.AI = !quiz.AI;

        await _quizRepository.Update(quiz);

        return Result.Success(new QuizResponse(
            quiz.Id,
            quiz.Title,
            quiz.Description,
            quiz.IsDisabled,
            quiz.TotalQuestions,
            quiz.StartAt ?? DateTimeOffset.UtcNow,
            quiz.EndAt ?? DateTimeOffset.UtcNow,
            quiz.Duration ?? 0,
            quiz.AI ?? false,
            quiz.Hash?.FileHash ?? "manual"
            ));
    }

    public async Task<Result<ResultResponse>> AddQuizResult(ResultRequest request)
    {
     
        var student = await _studentRepository.GetByIdAsync(request.StudentId);

        if (student == null || student.IsDisabled)
            return Result.Failure<ResultResponse>(StudentErrors.NotFound);

        var quiz = await _quizRepository.GetByIdAsync(request.QuizId);

        if (quiz == null || quiz.IsDisabled)
            return Result.Failure<ResultResponse>(QuizErrors.NotFound);



        var cairoTimeZone = TZConvert.GetTimeZoneInfo("Africa/Cairo");
        var currentTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.Utc, cairoTimeZone);

        var quizResult = new QuizResult
        {
            StudentId = request.StudentId,
            Score = request.Score,
            QuizId = request.QuizId,
            CompletedAt = currentTime,
        };
        await _quizResult.AddQuizResultAsync(quizResult);


        return Result.Success(new ResultResponse(request.Score ,request.StudentId,request.QuizId,currentTime));
    }

    public async Task<Result<ResultResponse>> GetQuizResultById(int quizId, string studentId)
    {
        var quizResults = await _quizResult.GetQuizResultsByStudentIdAsync(studentId, quizId);

        if (quizResults == null || quizResults.IsDisabled)
            return Result.Failure<ResultResponse>(QuizErrors.NotFound);

        var response = new ResultResponse(
            quizResults.Score,
            quizResults.StudentId,
            quizResults.QuizId,
            quizResults.CompletedAt.UtcDateTime
        );

        return Result.Success(response);
    }
}
