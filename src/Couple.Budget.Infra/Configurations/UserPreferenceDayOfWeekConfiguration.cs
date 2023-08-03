using Couple.Budget.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Couple.Budget.Infra.Configurations
{
    public class UserPreferenceDayOfWeekConfiguration : IEntityTypeConfiguration<UserPreferenceDayOfWeek>
    {
        public void Configure(EntityTypeBuilder<UserPreferenceDayOfWeek> builder)
        {
            builder.ToTable("UserPreferenceDayOfWeeks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.DayOfWeek).IsRequired();
        }
    }
}