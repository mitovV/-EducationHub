namespace EducationHub.Services.Data.Users
{
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using EducationHub.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<User> userRepository;

        public UsersService(IDeletableEntityRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<T> GetResourcesInfoAsync<T>(string id)
            => await this.userRepository
                .AllAsNoTracking()
                .Where(u => u.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<T> GetUserAsync<T>(string id)
            => await this.userRepository
                .AllAsNoTracking()
                .Where(u => u.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
    }
}
