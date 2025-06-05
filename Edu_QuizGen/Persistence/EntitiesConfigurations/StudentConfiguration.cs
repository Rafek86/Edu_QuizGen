
namespace Edu_QuizGen.Persistence.EntitiesConfigurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {

        //builder.HasMany(ts => ts.TeacherStudents)
        //       .WithOne(s=>s.Student)
        //       .HasForeignKey(ts => ts.StudentId);

        builder.ToTable("Students");

        string Password = new PasswordHasher<Teacher>().HashPassword(null, "P@ss123456");
        builder.HasData(new Student
        {
            Id = "11caefd4-1787-43cb-92b1-ec7a68d628c9",
            FirstName = "John",
            LastName = "Doee",
            Email = "JohnDoee@gmail.com",
            UserName = "JohnDoee@gmail.com",
            EmailConfirmed = true,
            profilePicture = "...",
            PasswordHash = Password,
        });
    }
}
