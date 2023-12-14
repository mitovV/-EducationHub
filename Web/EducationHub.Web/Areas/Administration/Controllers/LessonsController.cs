namespace EducationHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Services.Data.Lessons;
    using ViewModels.Administration.Lessons;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Categories;
    using Services.Data.Categories;
    using EducationHub.Data.Models;

    public class LessonsController : AdministrationController
    {
        private readonly ILessonsService lessonsService;
        private readonly ICategoriesService categoriesService;

        public LessonsController(ILessonsService lessonsService, ICategoriesService categoriesService)
        {
            this.lessonsService = lessonsService;
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> All()
         => this.View(await this.lessonsService.GetAllNotRelatedToCourseWithDeletedAsync<NotRelatedLessonAdminViewModel>());

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.lessonsService.GetByIdAsync<EditLessonAdminViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditLessonAdminViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.lessonsService.EditAsync(model.Id, model.Title, model.Description, model.VideoUrl, model.CategoryId, model.IsDeleted);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.lessonsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
