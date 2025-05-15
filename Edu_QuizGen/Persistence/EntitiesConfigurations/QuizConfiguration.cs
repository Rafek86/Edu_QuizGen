namespace Edu_QuizGen.Persistence.EntitiesConfigurations
{
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.ToTable("Quiz");


            builder.HasData(new Quiz
            {
                Id = 1,
                Title = "General Knowledge Quiz",
                Description = "A collection of general knowledge questions covering various topics",
                IsDisabled = false,
                TotalQuestions = 5
            });
        }
    }
}
