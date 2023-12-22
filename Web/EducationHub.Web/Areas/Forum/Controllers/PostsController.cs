namespace EducationHub.Web.Areas.Forum.Controllers
{
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Categories;
    using Services.Data.Posts;
    using ViewModels.Categories;
    using ViewModels.Forum.Home;
    using ViewModels.Forum.Posts;

    public class PostsController : ForumController
    {
        private readonly IPostsService postsService;
        private readonly ICategoriesService categoriesService;

        public PostsController(IPostsService postsService, ICategoriesService categoriesService)
        {
            this.postsService = postsService;
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> ByCategory(int id, int page = 1)
        {
            const int ItemsPerPage = 4;

            if (page < 1)
            {
                page = 1;
            }

            var viewModel = new PagingPostsViewModel
            {
                CategoryId = id,
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ItemsCount = this.postsService.GetCountByCategory(id),
                Posts = await this.postsService.GetByCategoryIdAsync<HomePagePostViewModel>(id, page, ItemsPerPage),
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            if (page > viewModel.PagesCount)
            {
                page = viewModel.PagesCount;

                viewModel = new PagingPostsViewModel
                {
                    CategoryId = id,
                    ItemsPerPage = ItemsPerPage,
                    PageNumber = page,
                    ItemsCount = this.postsService.GetCountByCategory(id),
                    Posts = await this.postsService.GetByCategoryIdAsync<HomePagePostViewModel>(id, page, ItemsPerPage),
                };
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> ByUser()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = await this.postsService.GetByUserIdAsync<ByUserViewModel>(userId);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await this.postsService.GetByIdAsync<PostDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new CreatePostViewModel
            {
                CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>();
                return this.View(model);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var id = await this.postsService.CratePostAsync(model.Title, model.Content, userId, model.CategoryId);

            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.postsService.GetByIdAsync<EditPostViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.CategoriesItems = await this.categoriesService.AllAsync<CategoriesItemsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.postsService.EditAsync(model.Id, model.Title, model.Content, model.CategoryId);

            return this.RedirectToAction(nameof(this.ByUser));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.postsService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.ByUser));
        }
    }
}
