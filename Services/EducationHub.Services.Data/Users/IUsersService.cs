namespace EducationHub.Services.Data.Users
{
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<T> GetResourcesInfoAsync<T>(string id);

        Task<T> GetUserAsync<T>(string id);
    }
}
