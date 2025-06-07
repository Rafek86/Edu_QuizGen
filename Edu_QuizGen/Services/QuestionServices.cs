using Edu_QuizGen.DTOs;
using Edu_QuizGen.Errors;
using Edu_QuizGen.Models;
using Edu_QuizGen.Repository_Abstraction;
using Edu_QuizGen.Service_Abstraction;

namespace Edu_QuizGen.Services
{
    public class QuestionServices(IQuestionRepository _repository) : IQuestionSevice
    {
        public async Task<Result> AddQuestionAsync(QuestionDTO questionDto)
        {
            // Validate question type and correct answer
            if (questionDto.Type == QuestionType.MCQ)
            {
                if (questionDto.Options == null || !questionDto.Options.Any())
                    return Result.Failure(QuestionErrors.MCQOptionsRequired);

                if (!questionDto.Options.Any(o => o.Text == questionDto.CorrectAnswer))
                    return Result.Failure(QuestionErrors.CorrectAnswerNotInOptions);
            }
            else if (questionDto.Type == QuestionType.TF)
            {
                if (questionDto.CorrectAnswer.ToLower() != "true" && questionDto.CorrectAnswer.ToLower() != "false")
                    return Result.Failure(QuestionErrors.InvalidTrueFalseAnswer);
            }

            var question = new Question
            {
                Text = questionDto.Text,
                Type = questionDto.Type,
                CorrectAnswer = questionDto.CorrectAnswer,
                QuizId = questionDto.QuizId,    
                Options = questionDto.Options?.Select(o => new Option { Text = o.Text }).ToList()
            };

            await _repository.AddAsync(question);
            return Result.Success();
        }

        public async Task<Result<IEnumerable<QuestionResponseDTO>>> AddQuestionAsync(IEnumerable<QuestionDTO> questionsDto)
        {
            var response = new List<QuestionResponseDTO>();
            foreach (var questionDto in questionsDto)
            {
                // Validate question type and correct answer
                if (questionDto.Type == QuestionType.MCQ)
                {
                    if (questionDto.Options == null || !questionDto.Options.Any())
                        return Result.Failure<IEnumerable<QuestionResponseDTO>>(QuestionErrors.MCQOptionsRequired);

                    if (!questionDto.Options.Any(o => o.Text == questionDto.CorrectAnswer))
                        return Result.Failure<IEnumerable<QuestionResponseDTO>>(QuestionErrors.CorrectAnswerNotInOptions);
                }
                else if (questionDto.Type == QuestionType.TF)
                {
                    if (questionDto.CorrectAnswer.ToLower() != "true" && questionDto.CorrectAnswer.ToLower() != "false")
                        return Result.Failure<IEnumerable<QuestionResponseDTO>>(QuestionErrors.InvalidTrueFalseAnswer);
                }

                var question = new Question
                {
                    Text = questionDto.Text,
                    Type = questionDto.Type,
                    CorrectAnswer = questionDto.CorrectAnswer,
                    QuizId = questionDto.QuizId,
                    Options = questionDto.Options?.Select(o => new Option { Text = o.Text }).ToList()
                };
                await _repository.AddAsync(question);

                var questionResponse = new QuestionResponseDTO
                {
                    Id = question.Id,
                    QuizId = question.QuizId,
                    Text = question.Text,
                    Type = question.Type,
                    CorrectAnswer = question.CorrectAnswer,
                    Options = questionDto.Options
                };
                response.Add(questionResponse);
            }
            return Result.Success<IEnumerable<QuestionResponseDTO>>(response);
        }

        public async Task<Result> DeleteQuestion(int id)
        {
            var question = await _repository.GetQuestionByIdAsync(id);
            if (question == null)
                return Result.Failure(QuestionErrors.questionIsEmptyError);

            await _repository.Delete(question);
            return Result.Success();
        }

        public async Task<Result<IEnumerable<QuestionResponseDTO>>> GetAllQuestionsAsync()
        {
            var questions = await _repository.GetAllAsync();
            if (questions == null)
                return Result.Failure<IEnumerable<QuestionResponseDTO>>(QuestionErrors.questionlistIsEmptyError);

            var questionsDto = questions.Select(q => new QuestionResponseDTO
            {
                Id = q.Id,
                Text = q.Text,
                Type = q.Type,
                CorrectAnswer = q.CorrectAnswer,
                QuizId = q.QuizId,
                Options = q.Options?.Select(o => new OptionDTO { Text = o.Text }).ToList()

            });

            return Result.Success(questionsDto);
        }

        public async Task<Result<PagedResult<QuestionDTO>>> GetPagedQuestionsAsync(int pageNumber, int pageSize)
        {
            var questionRepo =await  _repository.GetPagedQuestionsAsync(pageNumber, pageSize);

            if (questionRepo == null || !questionRepo.Items.Any())
                return Result.Failure<PagedResult<QuestionDTO>>(QuestionErrors.questionlistIsEmptyError);

            var questionsDto = questionRepo.Items.Select(q => new QuestionDTO
            {
                Text = q.Text,
                Type = q.Type,
                CorrectAnswer = q.CorrectAnswer,
                Options = q.Options?.Select(o => new OptionDTO { Text = o.Text }).ToList()
            });

            var pagedResult = new PagedResult<QuestionDTO>
            {
                Items = questionsDto,
                PageNumber = questionRepo.PageNumber,
                PageSize = questionRepo.PageSize,
                TotalItems = questionRepo.TotalItems
            };

            return Result.Success(pagedResult);
        }

        public async Task<Result<QuestionResponseDTO>> GetQuestionByIdAsync(int id)
        {
            var question = await _repository.GetQuestionByIdAsync(id);
            if (question == null)
                return Result.Failure<QuestionResponseDTO>(QuestionErrors.questionIsEmptyError);

            var questionDto = new QuestionResponseDTO
            {
                Id = question.Id,
                Text = question.Text,
                Type = question.Type,
                CorrectAnswer = question.CorrectAnswer,
                QuizId=question.QuizId,
                Options = question.Options?.Select(o => new OptionDTO { Text = o.Text }).ToList()
            };

            return Result.Success(questionDto);
        }

        public async Task<Result<IEnumerable<QuestionResponseDTO>>> GetQuestionsByQuizId(int QuizId)
        {
            var questions = await _repository.GetQuestionsByQuizId(QuizId);
            if (questions == null)
                return Result.Failure<IEnumerable<QuestionResponseDTO>>(QuestionErrors.questionlistIsEmptyError);

            var questionsDto = questions.Select(q => new QuestionResponseDTO
            {
                Id = q.Id,
                Text = q.Text,
                Type = q.Type,
                CorrectAnswer = q.CorrectAnswer,
                Options = q.Options?.Select(o => new OptionDTO { Text = o.Text }).ToList()
            });

            return Result.Success(questionsDto);
        }

        public async Task<Result<IEnumerable<QuestionResponseDTO>>> GetQuestionsByQuizTitle(string QuizTitle)
        {
            var questions = await _repository.GetQuestionsByQuizTitle(QuizTitle);
            if (questions == null)
                return Result.Failure<IEnumerable<QuestionResponseDTO>>(QuestionErrors.questionlistIsEmptyError);

            var questionsDto = questions.Select(q => new QuestionResponseDTO
            {
                Id = q.Id,
                Text = q.Text,
                Type = q.Type,
                CorrectAnswer = q.CorrectAnswer,
                Options = q.Options?.Select(o => new OptionDTO { Text = o.Text }).ToList()
            });

            return Result.Success(questionsDto);
        }

        public async Task<Result<IEnumerable<QuestionResponseDTO>>> GetQuestionsByTypeAsync(QuestionType type)
        {
            var questions = await _repository.GetQuestionsByTypeAsync(type);
            if (questions == null)
                return Result.Failure<IEnumerable<QuestionResponseDTO>>(QuestionErrors.questionlistIsEmptyError);

            var questionsDto = questions.Select(q => new QuestionResponseDTO
            {
                Id = q.Id,
                Text = q.Text,
                Type = q.Type,
                CorrectAnswer = q.CorrectAnswer,
                Options = q.Options?.Select(o => new OptionDTO { Text = o.Text }).ToList()
            });

            return Result.Success(questionsDto);
        }

        public async Task<Result> UpdateQuestion(int id, QuestionDTO questionDto)
        {
            var question = await _repository.GetQuestionByIdAsync(id);
            if (question == null)
                return Result.Failure(QuestionErrors.questionIsEmptyError);

            // Validate question type and correct answer
            if (questionDto.Type == QuestionType.MCQ)
            {
                if (questionDto.Options == null || !questionDto.Options.Any())
                    return Result.Failure(QuestionErrors.MCQOptionsRequired);

                if (!questionDto.Options.Any(o => o.Text == questionDto.CorrectAnswer))
                    return Result.Failure(QuestionErrors.CorrectAnswerNotInOptions);
            }
            else if (questionDto.Type == QuestionType.TF)
            {
                if (questionDto.CorrectAnswer.ToLower() != "true" && questionDto.CorrectAnswer.ToLower() != "false")
                    return Result.Failure(QuestionErrors.InvalidTrueFalseAnswer);
            }

            question.Text = questionDto.Text;
            question.Type = questionDto.Type;
            question.CorrectAnswer = questionDto.CorrectAnswer;
            question.Options = questionDto.Options?.Select(o => new Option
            {
                Text = o.Text,
                QuestionId = question.Id
            }).ToList();

            await _repository.UpdateQuestion(question);
            return Result.Success(question);
        }
    }
}
