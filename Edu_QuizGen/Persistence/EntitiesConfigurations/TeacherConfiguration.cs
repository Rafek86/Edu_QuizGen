
namespace Edu_QuizGen.Persistence.EntitiesConfigurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {

        //builder.HasMany(ts => ts.TeacherStudents)
        //       .WithOne(s=>s.Teacher)
        //       .HasForeignKey(ts => ts.TeacherId);

        builder.ToTable("Teachers");

        string  Password =new PasswordHasher<Teacher>().HashPassword(null, "P@ss123456");
        builder.HasData(new Teacher { 
            Id= "11caefd4-1787-43cb-92b1-ec7a68d628c0",
            FirstName= "John",
            LastName = "Doe",
            Email="JohnDoe@gmail.com",
            UserName= "JohnDoe@gmail.com",
            EmailConfirmed = true,
            profilePicture="...",
            PasswordHash = Password,
        });
    }
}
