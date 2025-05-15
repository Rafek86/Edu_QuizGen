
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


            builder.HasData(new QuizRoom
            {
                QuizId = 1,
                RoomId = "41fa0ff2-2778-4018-8bca-438f8d3363b0",
                IsDisabled = false
            });
        }
    }
}
