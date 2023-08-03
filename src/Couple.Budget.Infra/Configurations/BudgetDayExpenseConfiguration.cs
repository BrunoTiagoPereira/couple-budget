using Couple.Budget.Domain.Budgets.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Couple.Budget.Infra.Configurations
{
    public class BudgetDayExpenseConfiguration : IEntityTypeConfiguration<BudgetDayExpense>
    {
        public void Configure(EntityTypeBuilder<BudgetDayExpense> builder)
        {
            builder.ToTable("BudgetDayExpenses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Name).HasMaxLength(2048).IsRequired();

            builder.Property(x => x.Value).HasPrecision(9, 2).IsRequired();
        }
    }
}