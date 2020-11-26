namespace EducationHub.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Services.Data.Categories;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Categories;
    using ViewModels.Lessons;
    using EducationHub.Services.Data.Lessons;

    public class LessonsController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly ILessonsService lessonsService;

        public LessonsController(ICategoriesService categoriesService, ILessonsService lessonsService)
        {
            this.categoriesService = categoriesService;
            this.lessonsService = lessonsService;
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateLessonInputModel
            {
                CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLessonInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>();

                return this.View(model);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.lessonsService.CreateAsync(model.Title, model.Description, model.VideoUrl, userId, model.CategoryId);

            return this.RedirectToAction("MyResources", "Users");
        }

        public async Task<IActionResult> ByCategory(int id)
        {
            var viewModel = await this.lessonsService.GetByCategoryIdAsync<ByCategoryLessonViewModel>(id);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.lessonsService.ByIdAsync<DetailsLessonViewModel>(id);

            return this.View(viewModel);
        }
    }
}
