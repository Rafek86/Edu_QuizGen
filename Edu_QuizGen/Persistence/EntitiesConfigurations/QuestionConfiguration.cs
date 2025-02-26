
namespace Edu_QuizGen.Persistence.EntitiesConfigurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(e => e.Type)
                   .HasConversion(
            v => v.ToString(),
            v => (QuestionType)Enum.Parse(typeof(QuestionType), v));


            builder.HasMany(x => x.Options)
                .WithOne(x => x.Question)
                .HasForeignKey(x=> x.QuestionId);
        }
    }
}
