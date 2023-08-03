using Couple.Budget.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Couple.Budget.Infra.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.UserName).HasMaxLength(User.MAX_USER_NAME_LENGTH).IsRequired();

            builder.OwnsOne(x => x.Password).Property(x => x.Hash).HasMaxLength(150).IsRequired();

            builder.HasOne(x => x.UserPreference).WithOne(x => x.User);
        }
    }
}