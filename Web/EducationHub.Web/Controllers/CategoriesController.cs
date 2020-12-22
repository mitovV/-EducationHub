namespace EducationHub.Web.Controllers
{
    using System.Threading.Tasks;

    using Common;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data;
    using Services.Data.Categories;
    using Web.ViewModels.Categories;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IGetCountsService getCountsService;

        public CategoriesController(ICategoriesService categoriesService, IGetCountsService getCountsService)
        {
            this.categoriesService = categoriesService;
            this.getCountsService = getCountsService;
        }

        public async Task<IActionResult> All()
            => this.View(await this.categoriesService.AllAsync<CategoriesAllViewModel>());

        public async Task<IActionResult> Details(int id)
        {
            var categoriesCount = this.getCountsService.GetCategoriesCount();

            if (categoriesCount < id || id < 1)
            {
               return this.NotFound();
            }

            var viewModel = await this.categoriesService.GetByIdAsync<CategoryDetailsViewModel>(id);

            return this.View(viewModel);
        }
    }
}
