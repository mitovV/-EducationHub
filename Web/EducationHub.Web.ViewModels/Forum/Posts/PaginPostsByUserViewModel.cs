namespace EducationHub.Web.ViewModels.Forum.Posts
{
    using System.Collections.Generic;

    public class PaginPostsByUserViewModel : BasePagingViewModel
    {
        public IEnumerable<ByUserViewModel> Posts { get; set; }
    }
}
