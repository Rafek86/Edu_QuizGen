
namespace Edu_QuizGen.Persistence.EntitiesConfigurations
{
    public class QuizRoomConfiguration : IEntityTypeConfiguration<QuizRoom>
    {
        public void Configure(EntityTypeBuilder<QuizRoom> builder)
        {
            builder.HasKey(x => new { x.QuizId, x.RoomId });
            
            builder.HasOne(x => x.Room)
                .WithMany(s => s.QuizRoom)
                .HasForeignKey(x => x.RoomId);

            builder.HasOne(x => x.Quiz)
                .WithMany(s => s.QuizRoom)
                .HasForeignKey(x => x.QuizId);
        }
    }
}
