
namespace Edu_QuizGen.Persistence.EntitiesConfigurations;

public class StudentRoomConfiguration : IEntityTypeConfiguration<StudentRoom>
{
    public void Configure(EntityTypeBuilder<StudentRoom> builder)
    {
        builder.HasKey(x => new { x.StudentId, x.RoomId });

        builder.HasOne(x => x.Student)
            .WithMany(x => x.StudentRooms)
            .HasForeignKey(x => x.StudentId);

        builder.HasOne(x => x.Room)
            .WithMany(x => x.StudentRooms)
            .HasForeignKey(x => x.RoomId);

        builder.ToTable("StudentRooms");
    }
}
