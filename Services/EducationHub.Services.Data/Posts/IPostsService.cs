namespace EducationHub.Services.Data.Posts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostsService
    {
        Task<IEnumerable<T>> GetPostsAsync<T>(int count);

        Task<IEnumerable<T>> GetPostsByCategoryAsync<T>(int categoryId);

        Task<T> GetByIdAsync<T>(int id);

        Task<int> CratePostAsync(string title, string content, string userId, int categoryId);

        Task<IEnumerable<T>> GetByCategoryIdAsync<T>(int id, int page, int itemsPerPage);

        int GetCountByCategory(int id);
    }
}
