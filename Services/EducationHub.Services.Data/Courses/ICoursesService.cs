namespace EducationHub.Services.Data.Courses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICoursesService
    {
        Task<IEnumerable<T>> AllAsync<T>();

        Task<IEnumerable<T>> AllWithDeletedAsync<T>();

        Task<IEnumerable<T>> GetByUserIdAsync<T>(string userId);

        Task<T> GetByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetByCategoryIdAsync<T>(int categoryId, int page, int itemsPerPage);

        Task CreateAsync(string title, string description, string userId, int categoryId);

        Task DeleteAsync(string id);

        int GetCountByCategory(int id);

        Task EditAsync(string id, string title, string description, bool isDeleted, int categoryId);
    }
}
