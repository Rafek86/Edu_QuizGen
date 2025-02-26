namespace Edu_QuizGen.Persistence.EntitiesConfigurations
{
    public class HashConfiguration : IEntityTypeConfiguration<Hash>
    {
        public void Configure(EntityTypeBuilder<Hash> builder)
        {
            builder.HasOne(x => x.Quiz)
                .WithOne(x => x.Hash)
                .HasForeignKey<Hash>(x => x.QuizId);
        }
    }
}
