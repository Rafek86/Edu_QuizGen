using Edu_QuizGen.Contracts.FastApi;
using Edu_QuizGen.Errors;
using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Edu_QuizGen.Services;

public class QuizGenerationService : IQuizGenerationService
{
    private readonly HttpClient _httpClient;
    private readonly IQuestionRepository _questionRepository;
    private readonly IQuizRepository _quizRepository;
    private readonly ILogger<QuizGenerationService> _logger;
    private readonly string _fastApiBaseUrl;

    public QuizGenerationService(
        HttpClient httpClient,
        IQuestionRepository questionRepository,
        IQuizRepository quizRepository,
        ILogger<QuizGenerationService> logger,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _questionRepository = questionRepository;
        _quizRepository = quizRepository;
        _logger = logger;
        _fastApiBaseUrl = configuration.GetValue<string>("FastApiSettings:BaseUrl") ?? "http://localhost:8000";
    }

    public async Task<Result<List<Question>>> GenerateQuestionsFromPdfAsync(int quizId, IFormFile pdfFile)
    {
        try
        {
            var quiz = await _quizRepository.GetByIdAsync(quizId);
            if (quiz == null)
                return Result.Failure<List<Question>>(QuizErrors.NotFound);

            using var content = new MultipartFormDataContent();
            using var fileStream = pdfFile.OpenReadStream();
            using var streamContent = new StreamContent(fileStream);

            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            content.Add(streamContent, "file", pdfFile.FileName);

            _httpClient.Timeout = Timeout.InfiniteTimeSpan;
            var response = await _httpClient.PostAsync($"{_fastApiBaseUrl}/generate-quiz", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"FastAPI returned error: {response.StatusCode}");
                return Result.Failure<List<Question>>(QuizErrors.GenerationFailed);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var fastApiResponse = JsonSerializer.Deserialize<FastApiQuizResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (fastApiResponse?.Questions == null || !fastApiResponse.Questions.Any())
                return Result.Failure<List<Question>>(QuizErrors.NoQuestionsGenerated);

            var questions = new List<Question>();
            foreach (var fastApiQuestion in fastApiResponse.Questions)
            {
                var question = new Question
                {
                    Text = fastApiQuestion.Text,
                    Type = (QuestionType)fastApiQuestion.Type,
                    CorrectAnswer = fastApiQuestion.CorrectAnswer,
                    Explanation = fastApiQuestion.Explanation,
                    QuizId = quizId,
                    IsDisabled = false,
                    Options = fastApiQuestion.Options?.Select(opt => new Option
                    {
                        Text = opt.Text,
                        IsDisabled = false
                    }).ToList() ?? new List<Option>()
                };

                questions.Add(question);
            }

            foreach (var question in questions)
            {
                await _questionRepository.AddAsync(question);
            }

            return Result.Success(questions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating questions from PDF");
            return Result.Failure<List<Question>>(QuizErrors.GenerationFailed);
        }
    }

    public async Task<Result<List<Question>>> GetGeneratedQuestionsAsync(int quizId)
    {
        try
        {
            var questions = await _questionRepository.GetGeneratedQuestionsByQuizId(quizId);
            var generatedQuestions = questions.Where(q => !q.IsDisabled && q.Quiz.Hash.FileHash !=null ).ToList();

            return Result.Success(generatedQuestions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving generated questions");
            return Result.Failure<List<Question>>(QuizErrors.RetrievalFailed);
        }
    }

    public async Task<Result> SaveSelectedQuestionsAsync(int quizId, List<int> selectedQuestionIds)
    {
        try
        {
            var quiz = await _quizRepository.GetByIdAsync(quizId);
            if (quiz == null)
                return Result.Failure(QuizErrors.NotFound);

            var existingRelations = await _questionRepository.GetQuizQuestionsByQuizIdAsync(quizId);
            foreach (var relation in existingRelations)
            {
                relation.IsDisabled = true;
            }

            foreach (var questionId in selectedQuestionIds)
            {
                var question = await _questionRepository.GetByIdAsync(questionId);
                if (question != null && question.QuizId == quizId)
                {
                    var quizQuestion = new QuizQuestions
                    {
                        QuizId = quizId,
                        QuistionId = questionId, 
                        IsDisabled = false
                    };

                    await _questionRepository.AddQuizQuestionAsync(quizQuestion);
                }
            }

            quiz.TotalQuestions = selectedQuestionIds.Count;
            await _quizRepository.Update(quiz);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving selected questions");
            return Result.Failure(QuizErrors.SaveFailed);
        }
    }
}



