namespace EducationHub.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Categories;
    using ViewModels.Categories;
    using ViewModels.Courses;

    public class CoursesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public CoursesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult ByCategory()
        {
            return this.View();
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateCourseInputModel();
            viewModel.CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>();

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

            return this.Redirect("/");
        }
    }
}
