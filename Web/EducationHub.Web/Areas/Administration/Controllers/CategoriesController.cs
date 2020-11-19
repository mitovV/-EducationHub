namespace EducationHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Services.Data.Categories;
    using Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using EducationHub.Data.Models;

    public class CategoriesController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<User> userManager;

        public CategoriesController(ICategoriesService categoriesService, UserManager<User> userManager)
        {
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All()
            => this.View(await this.categoriesService.AllWithDeletedAsync<CategoryAdminViewModel>());

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.categoriesService.GetByIdAsync<CategoryAdminViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryAdminViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.categoriesService.EditAsync(model.Id, model.Name, model.PictureUrl, model.IsDeleted, user.Id);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Create()
            => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.categoriesService.CreateAsync(model.Name, model.PictureUrl, user.Id);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.categoriesService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
