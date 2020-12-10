namespace EducationHub.Web.ViewModels.Forum
{
    using Data.Models;
    using Services.Mapping;

    public class HomePageCategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PostsCount { get; set; }

        public string PictureUrl { get; set; }
    }
}
