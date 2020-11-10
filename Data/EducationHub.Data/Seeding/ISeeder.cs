namespace EducationHub.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(EducationHubDbContext dbContext, IServiceProvider serviceProvider);
    }
}
