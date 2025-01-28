namespace Edu_QuizGen.Persistence.EntitiesConfigurations
{
    public class TeacherConfigurations : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasMany(t => t.Students)
                .WithMany(s => s.teachers);

            builder.HasMany(t => t.courses)
                .WithMany(c => c.Teachers);
        }
    }
}
