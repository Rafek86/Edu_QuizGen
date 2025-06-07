using Edu_QuizGen.DTOs;

namespace Edu_QuizGen.Persistence.EntitiesConfigurations
{
    public class OptionDTOConfigurations : IEntityTypeConfiguration<OptionDTO>
    {
        public void Configure(EntityTypeBuilder<OptionDTO> builder)
        {
            builder.HasNoKey();
        }
    }
}
