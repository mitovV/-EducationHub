namespace EducationHub.Web.Areas.Forum.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Services.Data;
    using Services.Data.Posts;
    using ViewModels.Forum.Home;

    public class HomeController : ForumController
    {
        private const int PostsCount = 4;
        private readonly IGetCountsService getCountsService;
        private readonly IPostsService postsService;

        public HomeController(IGetCountsService getCountsService, IPostsService postsService)
        {
            this.getCountsService = getCountsService;
            this.postsService = postsService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await this.getCountsService.GetForumPostsCountsAsync();
            viewModel.Posts = await this.postsService.GetPostsAsync<HomePagePostViewModel>(PostsCount);

            return this.View(viewModel);
        }
    }
}
