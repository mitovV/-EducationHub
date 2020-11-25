namespace EducationHub.Web.ViewModels.Categories
{
    using Data.Models;
    using Services.Mapping;

    public class CategoriesItemsViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }
    }
}
