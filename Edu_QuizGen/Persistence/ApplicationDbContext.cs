using Edu_QuizGen.Models;
using Microsoft.EntityFrameworkCore;

namespace Edu_QuizGen.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser,ApplicationRole,string>(options)
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }

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
