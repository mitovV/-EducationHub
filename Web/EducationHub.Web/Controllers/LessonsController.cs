namespace EducationHub.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using EducationHub.Common;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Categories;
    using Services.Data.Lessons;
    using ViewModels.Categories;
    using ViewModels.Lessons;

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

        public async Task<IActionResult> ByCategory(int id, int page = 1)
        {
            const int ItemsPerPage = 4;

            if (page < 1)
            {
                page = 1;
            }

            var ifCategoryExist = this.categoriesService.IfExists(id);

            if (!ifCategoryExist)
            {
                return this.NotFound();
            }

            var viewModel = new PagingLessonsViewModel
            {
                CategoryId = id,
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ItemsCount = this.lessonsService.GetCountByCategory(id),
                Lessons = await this.lessonsService.GetByCategoryIdAsync<ByCategoryLessonViewModel>(id, page, ItemsPerPage),
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            if (page > viewModel.PagesCount)
            {
                page = viewModel.PagesCount;

                viewModel = new PagingLessonsViewModel
                {
                    CategoryId = id,
                    ItemsPerPage = ItemsPerPage,
                    PageNumber = page,
                    ItemsCount = this.lessonsService.GetCountByCategory(id),
                    Lessons = await this.lessonsService.GetByCategoryIdAsync<ByCategoryLessonViewModel>(id, page, ItemsPerPage),
                };
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.lessonsService.GetByIdAsync<DetailsLessonViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> ByUser()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = await this.lessonsService.GetByUserIdAsync<ListingLessonsViewModel>(userId);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.lessonsService.GetByIdAsync<EditLessonViewModel>(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (viewModel == null)
            {
                return this.NotFound();
            }

    

            if (userId != viewModel.User.Id)
            {
                this.TempData["Error"] = "You are not authorized for this operation!";
                return this.RedirectToAction(nameof(this.ByUser));
            }

            viewModel.CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditLessonViewModel model)
        {
            var viewModel = await this.lessonsService.GetByIdAsync<EditLessonViewModel>(model.Id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId != viewModel.User.Id)
            {
                this.TempData["Error"] = "You are not authorized for this operation!";
                return this.RedirectToAction(nameof(this.ByUser));
            }

            if (!this.ModelState.IsValid)
            {
                model.CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>();

                return this.View(model);
            }

            await this.lessonsService.EditAsync(model.Id, model.Title, model.Description, model.VideoUrl,model.CategoryId, false);

            this.TempData["Message"] = "Successfully changed.";
            return this.RedirectToAction(nameof(this.ByUser));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var viewModel = await this.lessonsService.GetByIdAsync<EditLessonViewModel>(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (viewModel == null)
            {
                return this.NotFound();
            }

            var userClaim = this.User.Claims.FirstOrDefault(c => c.Value == GlobalConstants.AdministratorRoleName).Value;

            if (userId != viewModel.User.Id && userClaim == null)
            {
                this.TempData["Error"] = "You are not authorized for this operation!";
                return this.RedirectToAction(nameof(this.ByUser));
            }

            this.TempData["Message"] = "Successfully deleted resource.";
            await this.lessonsService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.ByUser));
        }
    }
}
