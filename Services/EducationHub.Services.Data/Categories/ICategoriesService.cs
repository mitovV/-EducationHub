namespace EducationHub.Services.Data.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task CreateAsync(string name, string pictureUrl, string userId);

        Task DeleteAsync(int id);

        Task<IEnumerable<T>> AllAsync<T>();

        Task<IEnumerable<T>> AllWithDeletedAsync<T>();

        Task<T> GetByIdAsync<T>(int id);
    }
}
