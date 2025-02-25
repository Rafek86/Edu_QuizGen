namespace Edu_QuizGen.Persistence.EntitiesConfigurations;

public class QuizResultConfiguration : IEntityTypeConfiguration<QuizResult>
{
    public void Configure(EntityTypeBuilder<QuizResult> builder)
    {
        builder.HasOne(x => x.Student)
            .WithMany(x => x.QuizResults)
            .HasForeignKey(x => x.StduentId);

        builder.ToTable("QuizResults");
    }

}
