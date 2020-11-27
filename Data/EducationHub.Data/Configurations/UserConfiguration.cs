namespace EducationHub.Data.Configurations
{
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Data.Common.Validations.DataValidation.User;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> appUser)
        {
            appUser.Property(x => x.PictureUrl)
                .HasMaxLength(PictureUrlMaxLength);

            appUser
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.VotedFor)
                .WithOne(v => v.Voted)
                .HasForeignKey(v => v.VotedId);

            appUser
               .HasMany(e => e.TheyVoted)
               .WithOne(v => v.VotedFor)
               .HasForeignKey(v => v.VotedForId);
        }
    }
}
