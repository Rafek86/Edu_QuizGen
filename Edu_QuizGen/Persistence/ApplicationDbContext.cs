﻿namespace Edu_QuizGen.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<QuizQuestions> QuizQuestions { get; set; }
    public DbSet<QuizRoom> QuizRooms { get; set; }
    public DbSet<QuizResult> QuizResults { get; set; }  
    public DbSet<StudentRoom> StudentRooms { get; set; }
    public DbSet<Hash> Hashes { get; set; } 


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var cascadeFK = builder
            .Model
            .GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade && !fk.IsOwnership);

        foreach (var fk in cascadeFK)
        {
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
        base.OnModelCreating(builder);
    }

}
