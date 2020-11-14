namespace EducationHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using EducationHub.Services.Data.Categories;
    using EducationHub.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> All()
            => this.View(await this.categoriesService.AllAsync());

        public async Task<IActionResult> EditAsync(int id)
        {
            var viewModel = await this.categoriesService.GetByIdAsync(id);

            return this.View(viewModel);
        }
    }
}
