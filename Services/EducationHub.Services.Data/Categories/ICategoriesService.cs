namespace EducationHub.Services.Data.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Web.ViewModels.Administration;

    public interface ICategoriesService
    {
        Task CreateAsync(string name, string pictureUrl, string userId);

        Task<IEnumerable<T>> AllAsync<T>();

        Task<CategoryAdminViewModel> GetByIdAsync(int id);
    }
}
