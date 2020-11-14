namespace EducationHub.Services.Data.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EducationHub.Data.Models;
    using Web.ViewModels.Administration;

    public interface ICategoriesService
    {
        Task CreateAsync(string name, string pictureUrl, string userId);

        Task<IEnumerable<CategoryAdminViewModel>> AllAsync();

        Task<Category> GetByIdAsync(int id);
    }
}
