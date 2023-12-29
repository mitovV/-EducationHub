namespace EducationHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Services.Data.Lessons;
    using ViewModels.Administration.Lessons;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Categories;
    using Services.Data.Categories;

    public class LessonsController : AdministrationController
    {
        private readonly ILessonsService lessonsService;
        private readonly ICategoriesService categoriesService;

        public LessonsController(ILessonsService lessonsService, ICategoriesService categoriesService)
        {
            this.lessonsService = lessonsService;
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            const int ItemsPerPage = 5;

            if (page < 1)
            {
                page = 1;
            }

            var viewModel = new PagingLessonsAdminViewModel
            {
                Actoin = "All",
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ItemsCount = this.lessonsService.GetAllNotRelatedWithDelethedCount(),
                Lessons = await this.lessonsService.GetAllNotRelatedToCourseWithDeletedAsync<NotRelatedLessonAdminViewModel>(page, ItemsPerPage),
            };

            if (viewModel.PagesCount == 0)
            {
                return this.NotFound();
            }

            if (viewModel == null)
            {
                return this.NotFound();
            }

            if (page > viewModel.PagesCount)
            {
                page = viewModel.PagesCount;

                viewModel.PageNumber = page;
                viewModel.Lessons = await this.lessonsService
                    .GetAllNotRelatedToCourseWithDeletedAsync<NotRelatedLessonAdminViewModel>(page, ItemsPerPage);
            }

            return this.View(viewModel);
        }

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
