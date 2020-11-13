namespace EducationHub.Web.Areas.Administration.Controllers
{
    using EducationHub.Services.Data.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;

        public DashboardController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {

            return this.View(this.categoriesService.All());
        }
    }
}
