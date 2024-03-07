using JexAssessment.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JexAssessment.Infrastructure.Configurations
{
    public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder
                .Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
