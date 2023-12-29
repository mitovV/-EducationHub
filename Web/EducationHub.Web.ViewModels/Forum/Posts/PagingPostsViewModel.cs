namespace EducationHub.Web.ViewModels.Forum.Posts
{
    using System.Collections.Generic;

    using Home;

    public class PagingPostsViewModel : BasePagingViewModel
    {
        public IEnumerable<HomePagePostViewModel> Posts { get; set; }
    }
}
