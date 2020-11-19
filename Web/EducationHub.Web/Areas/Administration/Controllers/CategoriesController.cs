namespace EducationHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Services.Data.Categories;
    using Web.ViewModels.Administration;

    public class CategoriesController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<User> userManager;
        private readonly ICloudinaryService cloudinary;

        public CategoriesController(ICategoriesService categoriesService, UserManager<User> userManager, ICloudinaryService cloudinary)
        {
            this.categoriesService = categoriesService;
            this.userManager = userManager;
            this.cloudinary = cloudinary;
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
            if (model.Image.Length > 0)
            {
                var pictureUrl = await this.cloudinary.ImageUploadAsync(model.Image);
                model.PictureUrl = pictureUrl;
            }

            // TODO: Check this!
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
