namespace EducationHub.Services.Data.Tests.Models
{
    using EducationHub.Data.Models;
    using EducationHub.Services.Mapping;

    public class CategoryModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public string UserId { get; set; }
    }
}
