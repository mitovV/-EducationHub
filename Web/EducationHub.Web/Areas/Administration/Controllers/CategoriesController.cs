namespace EducationHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Services.Data.Categories;
    using Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> All()
            => this.View(await this.categoriesService.AllAsync<CategoryAdminViewModel>());

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.categoriesService.GetByIdAsync<CategoryAdminViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CategoryAdminViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
