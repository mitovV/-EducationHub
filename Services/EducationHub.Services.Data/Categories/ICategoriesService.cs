namespace EducationHub.Services.Data.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EducationHub.Data.Models;
    using Web.ViewModels.Administration;

    public interface ICategoriesService
    {
        Task CreateAsync(string name, string pictureUrl, string userId);

        IEnumerable<CategoryAdminViewModel> All();

        Task<Category> GetById(int id);
    }
}
