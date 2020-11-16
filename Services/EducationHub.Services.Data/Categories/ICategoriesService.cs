namespace EducationHub.Services.Data.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task CreateAsync(string name, string pictureUrl, string userId);

        Task<IEnumerable<T>> AllAsync<T>();

        Task<T> GetByIdAsync<T>(int id);
    }
}
