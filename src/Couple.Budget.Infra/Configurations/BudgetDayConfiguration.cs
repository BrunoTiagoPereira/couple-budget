using Couple.Budget.Domain.Budgets.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Couple.Budget.Infra.Configurations
{
    public class BudgetDayConfiguration : IEntityTypeConfiguration<BudgetDay>
    {
        public void Configure(EntityTypeBuilder<BudgetDay> builder)
        {
            builder.ToTable("BudgetDays");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Name).HasMaxLength(2048).IsRequired();

            builder.Property(x => x.Value).HasPrecision(9, 2).IsRequired();

            builder.Property(x => x.Date).IsRequired();

            builder.HasMany(x => x.Expenses).WithOne(x => x.BudgetDay);
        }
    }
}