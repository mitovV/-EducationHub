namespace EducationHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using EducationHub.Services.Data.Categories;
    using EducationHub.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult All()
            => this.View(this.categoriesService.All());

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.categoriesService.GetById(id);

            return this.View(viewModel);
        }
    }
}
