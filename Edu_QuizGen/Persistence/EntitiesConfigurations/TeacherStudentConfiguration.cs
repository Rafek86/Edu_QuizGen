
namespace Edu_QuizGen.Persistence.EntitiesConfigurations;

public class TeacherStudentConfiguration : IEntityTypeConfiguration<TeacherStudent>
{
    public void Configure(EntityTypeBuilder<TeacherStudent> builder)
    {
        builder.HasKey(ts => new { ts.StudentId, ts.TeacherId });

        //builder.HasOne(x => x.Student)
        //    .WithMany(x => x.TeacherStudents)
        //    .HasForeignKey(x => x.StudentId);


        //builder.HasOne(x => x.Teacher)
        //    .WithMany(x => x.TeacherStudents)
        //    .HasForeignKey(x => x.TeacherId);

        builder.ToTable("TeacherStudent");
    }
}
