
namespace Edu_QuizGen.Persistence.EntitiesConfigurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.HasOne(x => x.Studnet)
            .WithMany(x => x.Feedbacks)
            .HasForeignKey(x => x.StudentId);


        builder.ToTable("Feedbacks");
    }
}
