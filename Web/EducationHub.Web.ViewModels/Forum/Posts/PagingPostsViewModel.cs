namespace EducationHub.Web.ViewModels.Forum.Posts
{
    using System.Collections.Generic;

    using Home;

    public class PagingPostsViewModel : PagingViewModel
    {
        public IEnumerable<HomePagePostViewModel> Posts { get; set; }
    }
}
