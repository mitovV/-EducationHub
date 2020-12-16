namespace EducationHub.Services.Data.Tests.Models
{
    using EducationHub.Data.Models;
    using EducationHub.Services.Mapping;

    public class PostModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }
    }
}
