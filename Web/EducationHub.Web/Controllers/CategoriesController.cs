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
            => this.View(await this.categoriesService.AllAsync<AllViewModel>());

        public IActionResult Details(int id)
        {
            return this.View();
        }
    }
}
