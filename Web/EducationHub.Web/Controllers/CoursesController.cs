namespace EducationHub.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Services.Data.Courses;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Categories;
    using ViewModels.Categories;
    using ViewModels.Courses;

    public class CoursesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly ICoursesService coursesService;

        public CoursesController(ICategoriesService categoriesService, ICoursesService coursesService)
        {
            this.categoriesService = categoriesService;
            this.coursesService = coursesService;
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

        public IActionResult ByUser()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = this.coursesService.GetByUserIdAsync<ByUserCourseViewModel>(userId);

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
    }
}
