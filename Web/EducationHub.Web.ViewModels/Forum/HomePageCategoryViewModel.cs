namespace EducationHub.Web.ViewModels.Forum
{
    using Data.Models;
    using Services.Mapping;

    public class HomePageCategoryViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public int PostsCount { get; set; }
    }
}
