namespace EducationHub.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Services.Data.Categories;
    using Services.Data.Courses;
    using Services.Data.Lessons;
    using ViewModels.Categories;
    using ViewModels.Courses;
    using ViewModels.Lessons;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly ICoursesService coursesService;
        private readonly ILessonsService lessonsService;

        public CoursesController(ICategoriesService categoriesService, ICoursesService coursesService, ILessonsService lessonsService)
        {
            this.categoriesService = categoriesService;
            this.coursesService = coursesService;
            this.lessonsService = lessonsService;
        }

        public async Task<IActionResult> ByCategory(int id, int page = 1)
        {
            const int ItemsPerPage = 4;

            if (page < 1)
            {
                page = 1;
            }

            var viewModel = new PagingCoursesByCategoryViewModel
            {
                Actoin = "ByCategory",
                RouteId = id,
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ItemsCount = this.coursesService.GetCountByCategory(id),
                Courses = await this.coursesService.GetByCategoryIdAsync<ByCategoryCourseViewModel>(id, page, ItemsPerPage),
            };

            if (viewModel.PagesCount == 0)
            {
                return this.NotFound();
            }

            if (page > viewModel.PagesCount)
            {
                page = viewModel.PagesCount;

                viewModel.PageNumber = page;
                viewModel.Courses = await this
                .coursesService
                .GetByCategoryIdAsync<ByCategoryCourseViewModel>(id, page, ItemsPerPage);
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.coursesService.GetByIdAsync<DetailsCourseViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> ByUser(int page = 1)
        {
            const int ItemsPerPage = 6;

            if (page < 1)
            {
                page = 1;
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = new PagingCoursesByUserViewModel
            {
                Actoin = "ByUser",
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ItemsCount = this.coursesService.GetCountByUser(userId),
                Courses = await this.coursesService.GetByUserIdAsync<ListingCoursesViewModel>(userId, page, ItemsPerPage),
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
                viewModel.Courses = await this.coursesService.GetByUserIdAsync<ListingCoursesViewModel>(userId, page, ItemsPerPage);
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateCourseInputModel
            {
                CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>();

                return this.View(model);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.coursesService.CreateAsync(model.Title, model.Description, userId, model.CategoryId);

            this.TempData["Message"] = "Successfully created course.";
            return this.RedirectToAction("MyResources", "Users");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.coursesService.GetByIdAsync<EditCourseViewModel>(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (viewModel == null || userId != viewModel.User.Id)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var viewModel = await this.coursesService.GetByIdAsync<CourseViewModel>(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (viewModel == null || userId != viewModel.UserId)
            {
                return this.NotFound();
            }

            await this.coursesService.DeleteAsync(id);
            this.TempData["Message"] = "Successfully deleted resource.";

            return this.RedirectToAction(nameof(this.ByUser));
        }

        public async Task<IActionResult> CreateLesson(string id)
        {
            var course = await this.coursesService.GetByIdAsync<CourseViewModel>(id);

            if (course == null)
            {
                return this.NotFound();
            }

            var viewModel = new CreateLessonInCourseInputModel()
            {
                CourseId = id,
                CourseTitle = course.Title,
                CategoryName = course.CategoryName,
                CategoryId = course.CategoryId,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLesson(CreateLessonInCourseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var course = await this.coursesService.GetByIdAsync<CourseViewModel>(model.CourseId);
                model.CourseId = model.CourseId;
                model.CourseTitle = course.Title;
                model.CategoryName = course.CategoryName;
                model.CategoryId = course.CategoryId;

                return this.View(model);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.lessonsService.CreateAsync(model.Title, model.Description, model.VideoUrl, userId, model.CategoryId, model.CourseId);

            return this.RedirectToAction(nameof(this.Edit), new { id = model.CourseId });
        }

        public async Task<IActionResult> EditLesson(string id)
        {
            var viewModel = await this.lessonsService.GetByIdAsync<EditLessonInCourseViewModel>(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (viewModel == null || userId != viewModel.UserId)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditLesson(EditLessonInCourseViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId != model.UserId)
            {
                return this.NotFound();
            }

            await this.lessonsService.EditAsync(model.Id, model.Title, model.Description, model.VideoUrl, model.CategoryId, false);
            this.TempData["Message"] = "Successfully changed.";
            return this.RedirectToAction(nameof(this.Edit), new { id = model.CourseId });
        }

        public async Task<IActionResult> DeleteLesson(string id)
        {
            var viewModel = await this.lessonsService.GetByIdAsync<EditLessonViewModel>(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId != viewModel.User.Id)
            {
                return this.NotFound();
            }

            await this.lessonsService.DeleteAsync(id);
            this.TempData["Message"] = "Successfully deleted resource.";

            return this.RedirectToAction(nameof(this.Edit), new { id = viewModel.CourseId });
        }
    }
}
