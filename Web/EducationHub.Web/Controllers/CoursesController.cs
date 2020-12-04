namespace EducationHub.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Categories;
    using Services.Data.Courses;
    using Services.Data.Lessons;
    using ViewModels.Categories;
    using ViewModels.Courses;
    using Web.ViewModels.Lessons;

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

        public async Task<IActionResult> ByCategory(int id)
        {
            var viewModel = await this.coursesService.GetByCategoryIdAsync<ByCategoryCourseViewModel>(id);

            return this.View(viewModel);
        }

        public IActionResult Details(string id)
        {
            return this.View();
        }

        public async Task<IActionResult> ByUser()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = await this.coursesService.GetByUserIdAsync<ListingCoursesViewModel>(userId);

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

            return this.RedirectToAction("MyResources", "Users");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.coursesService.GetByIdAsync<EditCourseViewModel>(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId != viewModel.User.Id)
            {
                this.TempData["Error"] = "You are not authorized for this operation!";
                return this.RedirectToAction(nameof(this.ByUser));
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> DeleteLesson(string id)
        {
            var viewModel = await this.lessonsService.ByIdAsync<EditLessonViewModel>(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId != viewModel.User.Id)
            {
                this.TempData["Error"] = "You are not authorized for this operation!";
                return this.RedirectToAction(nameof(this.ByUser));
            }

            await this.lessonsService.DeleteAsync(id);
            this.TempData["Message"] = "Successfully deleted resource.";
            return this.RedirectToAction(nameof(this.Edit), new { id = viewModel.CourseId });
        }

        public async Task<IActionResult> EditLesson(string id)
        {
            var viewModel = await this.lessonsService.ByIdAsync<EditLessonInCourseViewModel>(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId != viewModel.UserId)
            {
                this.TempData["Error"] = "You are not authorized for this operation!";
                return this.RedirectToAction(nameof(this.ByUser));
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
                this.TempData["Error"] = "You are not authorized for this operation!";
                return this.RedirectToAction(nameof(this.ByUser));
            }

            await this.lessonsService.EditAsync(model.Id, model.Title, model.Description, model.VideoUrl, model.CategoryId, model.CourseId);
            this.TempData["Message"] = "Successfully changed.";
            return this.RedirectToAction(nameof(this.Edit), new { id = model.CourseId });
        }

        public async Task<IActionResult> CreateLesson(string id)
        {
            var course = await this.coursesService.GetByIdAsync<CourseViewModel>(id);

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
    }
}
