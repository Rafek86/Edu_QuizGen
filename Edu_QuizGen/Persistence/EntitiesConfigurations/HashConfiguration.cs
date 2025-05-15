namespace Edu_QuizGen.Persistence.EntitiesConfigurations
{
    public class HashConfiguration : IEntityTypeConfiguration<Hash>
    {
        public void Configure(EntityTypeBuilder<Hash> builder)
        {
            builder.HasOne(x => x.Quiz)
                .WithOne(x => x.Hash)
                .HasForeignKey<Hash>(x => x.QuizId);
           
            builder.Property(e => e.FileHash).IsRequired();
           
            builder.HasData(new Hash
            {
                Id = Guid.Parse("a3a7e3e1-48b6-4d9f-b77f-f3c7b6f18c61"),
                FileHash = "a3a7e3e1-48b6-4d9f-b77f-f3c7b6f18c61",
                QuizId =1
            });
        }
    }
}
