namespace EducationHub.Web.Areas.Forum.Controllers
{
    using System.Threading.Tasks;

    using Services.Data;
    using Microsoft.AspNetCore.Mvc;
    using EducationHub.Services.Data.Posts;
    using EducationHub.Web.ViewModels.Forum;

    public class HomeController : ForumController
    {
        private const int PostsCount = 5;
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
            viewModel.Posts = await this.postsService.GetPosts<HomePagePostViewModel>(PostsCount);

            return this.View(viewModel);
        }
    }
}
