namespace EducationHub.Web.Areas.Forum.Controllers
{
    using System.Security.Claims;
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

        public async Task<IActionResult> ByCategory(int id)
        {
            var viewModel = await this.postsService.GetPostsByCategoryAsync<HomePagePostViewModel>(id);

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
    }
}
