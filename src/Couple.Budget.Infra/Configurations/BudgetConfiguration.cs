using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Couple.Budget.Infra.Configurations
{
    public class BudgetConfiguration : IEntityTypeConfiguration<Domain.Budgets.Entities.Budget>
    {
        public void Configure(EntityTypeBuilder<Domain.Budgets.Entities.Budget> builder)
        {
            builder.ToTable("Budgets");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.OwnsOne(x => x.Month, b =>
            {
                b.Property(x => x.MonthNumber).IsRequired();
                b.Property(x => x.MonthName).HasMaxLength(30).IsRequired();
            });

            builder.Property(x => x.Value).HasPrecision(9,2).IsRequired();

            builder.HasMany(x => x.Suggests).WithOne(x => x.Budget);

            builder.HasMany(x => x.BudgetDays).WithOne(x => x.Budget);

            builder.HasOne(x => x.User).WithMany();
        }
    }
}