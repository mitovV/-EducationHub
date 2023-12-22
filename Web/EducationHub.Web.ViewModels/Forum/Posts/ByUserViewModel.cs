namespace EducationHub.Web.ViewModels.Forum.Posts
{
    using Data.Models;
    using EducationHub.Data.Common.Models;
    using Services.Mapping;

    public class ByUserViewModel : BaseDeletableModel<string>, IMapFrom<Post>
    {
        public string Title { get; set; }

        public string UserId { get; set; }

        public string Content { get; set; }

        public virtual User User { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
