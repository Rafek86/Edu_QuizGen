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

        public async Task<Result> AddQuestionAsync(IEnumerable<QuestionDTO> questionsDto)
        {
            foreach (var questionDto in questionsDto)
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
            }
            return Result.Success();
        }

        public async Task<Result> DeleteQuestion(int id)
        {
            var question = await _repository.GetQuestionByIdAsync(id);
            if (question == null)
                return Result.Failure(QuestionErrors.questionIsEmptyError);

            await _repository.Delete(question);
            return Result.Success();
        }

        public async Task<Result<IEnumerable<QuestionDTO>>> GetAllQuestionsAsync()
        {
            var questions = await _repository.GetAllAsync();
            if (questions == null)
                return Result.Failure<IEnumerable<QuestionDTO>>(QuestionErrors.questionlistIsEmptyError);

            var questionsDto = questions.Select(q => new QuestionDTO
            {
                Text = q.Text,
                Type = q.Type,
                CorrectAnswer = q.CorrectAnswer,
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

        public async Task<Result<QuestionDTO>> GetQuestionByIdAsync(int id)
        {
            var question = await _repository.GetQuestionByIdAsync(id);
            if (question == null)
                return Result.Failure<QuestionDTO>(QuestionErrors.questionIsEmptyError);

            var questionDto = new QuestionDTO
            {
                Text = question.Text,
                Type = question.Type,
                CorrectAnswer = question.CorrectAnswer,
                Options = question.Options?.Select(o => new OptionDTO { Text = o.Text }).ToList()
            };

            return Result.Success(questionDto);
        }

        public async Task<Result<IEnumerable<QuestionDTO>>> GetQuestionsByQuizId(int QuizId)
        {
            var questions = await _repository.GetQuestionsByQuizId(QuizId);
            if (questions == null)
                return Result.Failure<IEnumerable<QuestionDTO>>(QuestionErrors.questionlistIsEmptyError);

            var questionsDto = questions.Select(q => new QuestionDTO
            {
                Text = q.Text,
                Type = q.Type,
                CorrectAnswer = q.CorrectAnswer,
                Options = q.Options?.Select(o => new OptionDTO { Text = o.Text }).ToList()
            });

            return Result.Success(questionsDto);
        }

        public async Task<Result<IEnumerable<QuestionDTO>>> GetQuestionsByQuizTitle(string QuizTitle)
        {
            var questions = await _repository.GetQuestionsByQuizTitle(QuizTitle);
            if (questions == null)
                return Result.Failure<IEnumerable<QuestionDTO>>(QuestionErrors.questionlistIsEmptyError);

            var questionsDto = questions.Select(q => new QuestionDTO
            {
                Text = q.Text,
                Type = q.Type,
                CorrectAnswer = q.CorrectAnswer,
                Options = q.Options?.Select(o => new OptionDTO { Text = o.Text }).ToList()
            });

            return Result.Success(questionsDto);
        }

        public async Task<Result<IEnumerable<QuestionDTO>>> GetQuestionsByTypeAsync(QuestionType type)
        {
            var questions = await _repository.GetQuestionsByTypeAsync(type);
            if (questions == null)
                return Result.Failure<IEnumerable<QuestionDTO>>(QuestionErrors.questionlistIsEmptyError);

            var questionsDto = questions.Select(q => new QuestionDTO
            {
                Text = q.Text,
                Type = q.Type,
                CorrectAnswer = q.CorrectAnswer,
                Options = q.Options?.Select(o => new OptionDTO { Text = o.Text }).ToList()
            });

            return Result.Success(questionsDto);
        }

        //?! it doesn't work FIXXXXXXXXXX it
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
            question.QuizId=questionDto.QuizId;
            question.Options = questionDto.Options?.Select(o => new Option { Text = o.Text , QuestionId =question.Id }).ToList();
            await _repository.Update(question);
            return Result.Success(question);
        }
    }
}
