namespace EducationHub.Services.Data.Courses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICoursesService
    {
        Task<IEnumerable<T>> AllAsync<T>();

        Task<IEnumerable<T>> GetByUserId<T>(string userId);

        Task<IEnumerable<T>> GetCategoryId<T>(int categoryId);

        Task Create(string title, string description, string userId, int categoryId);
    }
}
