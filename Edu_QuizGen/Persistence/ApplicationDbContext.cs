namespace Edu_QuizGen.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser,ApplicationRole,string>(options)
{


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);

    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> teachers { get; set; }
    public DbSet<Course> courses { get; set; }

}
