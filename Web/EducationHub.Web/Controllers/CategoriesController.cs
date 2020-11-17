namespace EducationHub.Web.Controllers
{
    using System.Threading.Tasks;

    using Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Categories;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> All()
            => this.View(await this.categoriesService.AllAsync<CategoriesAllViewModel>());

        public async Task<IActionResult> Details(int id)
            => this.View(await this.categoriesService.GetByIdAsync<CategoryDetailsViewModel>(id));
    }
}
