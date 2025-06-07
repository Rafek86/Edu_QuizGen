
namespace Edu_QuizGen.Persistence.EntitiesConfigurations
{
    public class QuizQuestionConfiguration : IEntityTypeConfiguration<QuizQuestions>
    {
        public void Configure(EntityTypeBuilder<QuizQuestions> builder)
        {
            builder.HasKey(q => new { q.QuizId, q.QuistionId});

            builder.HasOne(q => q.quiz)
                .WithMany(q => q.quizQuestions)
                .HasForeignKey(q => q.QuizId);

            builder.HasOne(q => q.question)
                .WithMany(q => q.quizQuestions)
                .HasForeignKey(q => q.QuistionId);
        }
    }
}
