namespace EducationHub.Web.ViewModels.Forum.Posts
{
    using System.Collections.Generic;

    using Home;

    public class PagingPostsViewModel : PagingViewModel
    {
        public int CategoryId { get; set; }

        public IEnumerable<HomePagePostViewModel> Posts { get; set; }
    }
}
