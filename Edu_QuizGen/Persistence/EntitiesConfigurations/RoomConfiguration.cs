
namespace Edu_QuizGen.Persistence.EntitiesConfigurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.Property(x=>x.Id)
             .HasDefaultValueSql("NEWID()");

        builder.HasOne(x => x.Teacher)
            .WithMany(x => x.Rooms)
            .HasForeignKey(x => x.TeacherId);

        builder.ToTable("Rooms");
    }
}
