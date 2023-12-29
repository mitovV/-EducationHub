namespace EducationHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Services.Data.Lessons;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Courses;
    using ViewModels.Administration.Courses;
    using Web.ViewModels.Categories;
    using Services.Data.Categories;

    public class CoursesController : AdministrationController
    {
        private readonly ICoursesService coursesService;
        private readonly ILessonsService lessonsService;
        private readonly ICategoriesService categoriesService;

        public CoursesController(
            ICoursesService coursesService,
            ILessonsService lessonsService,
            ICategoriesService categoriesService)
        {
            this.coursesService = coursesService;
            this.lessonsService = lessonsService;
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> All(int page)
        {
            const int ItemsPerPage = 5;

            if (page < 1)
            {
                page = 1;
            }

            var viewModel = new PagingCoursesAdminViewModel
            {
                Actoin = "All",
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ItemsCount = this.coursesService.GetAllWithDelethedCount(),
                Courses = await this.coursesService.GetAllWithDeletedAsync<CourseAdminViewModel>(page, ItemsPerPage),
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
                viewModel.Courses = await this.coursesService
                    .GetAllWithDeletedAsync<CourseAdminViewModel>(page, ItemsPerPage);
            }

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.RedirectToActionPermanent("Create", "Courses", new { area = string.Empty });
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.coursesService.GetByIdWithDeletedAsync<EditCourseAdminViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCourseAdminViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.coursesService.EditAsync(model.Id, model.Title, model.Description, model.IsDeleted, model.CategoryId);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var lesoonsInCourse = this.lessonsService.DeleteAllInCourseAsync(id);
            await this.coursesService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
