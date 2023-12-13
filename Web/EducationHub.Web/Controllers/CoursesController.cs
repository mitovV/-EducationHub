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

            var viewModel = new PagingCoursesViewModel
            {
                CategoryId = id,
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ItemsCount = this.coursesService.GetCountByCategory(id),
                Courses = await this.coursesService.GetByCategoryIdAsync<ByCategoryCourseViewModel>(id, page, ItemsPerPage),
            };

            if (page > viewModel.PagesCount)
            {
                page = viewModel.PagesCount;

                viewModel = new PagingCoursesViewModel
                {
                    CategoryId = id,
                    ItemsPerPage = ItemsPerPage,
                    PageNumber = page,
                    ItemsCount = this.coursesService.GetCountByCategory(id),
                    Courses = await this.coursesService.GetByCategoryIdAsync<ByCategoryCourseViewModel>(id, page, ItemsPerPage),
                };
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

            if (viewModel == null)
            {
                return this.RedirectWithErrMessage("There is no such course!", nameof(this.ByUser));
            }

            if (userId != viewModel.User.Id)
            {
                return this.RedirectWithErrMessage("You are not authorized for this operation!", nameof(this.ByUser));
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var viewModel = await this.coursesService.GetByIdAsync<CourseViewModel>(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (viewModel == null)
            {
                return this.RedirectWithErrMessage("There is no such course!", nameof(this.ByUser));
            }

            if (userId != viewModel.UserId)
            {
                return this.RedirectWithErrMessage("You are not authorized for this operation!", nameof(this.ByUser));
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
                return this.RedirectWithErrMessage("There is no such course!", nameof(this.ByUser));
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

            if (viewModel == null)
            {
                return this.RedirectWithErrMessage("There is no such lesson!", nameof(this.ByUser));
            }

            if (userId != viewModel.UserId)
            {
                return this.RedirectWithErrMessage("You are not authorized for this operation!", nameof(this.ByUser));
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

            var categoryExist = this.categoriesService.IfExists(model.CategoryId);

            if (!categoryExist)
            {
                return this.RedirectWithErrMessage("There is no such category!", nameof(this.ByUser));
            }

            var courseExist = this.coursesService.IfExist(model.CourseId);

            if (!courseExist)
            {
                return this.RedirectWithErrMessage("There is no such cource!", nameof(this.ByUser));
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId != model.UserId)
            {
                return this.RedirectWithErrMessage("You are not authorized for this operation!", nameof(this.ByUser));
            }

            await this.lessonsService.EditAsync(model.Id, model.Title, model.Description, model.VideoUrl, model.CategoryId, model.CourseId);
            this.TempData["Message"] = "Successfully changed.";
            return this.RedirectToAction(nameof(this.Edit), new { id = model.CourseId });
        }

        public async Task<IActionResult> DeleteLesson(string id)
        {
            var viewModel = await this.lessonsService.GetByIdAsync<EditLessonViewModel>(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (viewModel == null)
            {
                return this.RedirectWithErrMessage("There is no such lesson!", nameof(this.ByUser));
            }

            if (userId != viewModel.User.Id)
            {
                return this.RedirectWithErrMessage("You are not authorized for this operation!", nameof(this.ByUser));
            }

            await this.lessonsService.DeleteAsync(id);
            this.TempData["Message"] = "Successfully deleted resource.";

            return this.RedirectToAction(nameof(this.Edit), new { id = viewModel.CourseId });
        }

        private RedirectToActionResult RedirectWithErrMessage(string message, string destination)
        {
            this.TempData["Error"] = message;
            return this.RedirectToAction(destination);
        }
    }
}
