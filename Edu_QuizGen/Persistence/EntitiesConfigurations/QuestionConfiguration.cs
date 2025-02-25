
namespace Edu_QuizGen.Persistence.EntitiesConfigurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasMany(x => x.Options)
                .WithOne(x => x.Question)
                .HasForeignKey();//not completed
        }
    }
}
