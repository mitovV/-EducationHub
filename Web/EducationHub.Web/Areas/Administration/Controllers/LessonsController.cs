namespace EducationHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Services.Data.Lessons;
    using Web.ViewModels.Administration.Lessons;
    using Microsoft.AspNetCore.Mvc;
    using EducationHub.Web.ViewModels.Categories;
    using EducationHub.Services.Data.Categories;
    using Microsoft.CodeAnalysis.CSharp;

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
    }
}
