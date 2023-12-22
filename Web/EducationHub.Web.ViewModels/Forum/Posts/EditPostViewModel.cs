namespace EducationHub.Web.ViewModels.Forum.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EducationHub.Data.Models;
    using EducationHub.Services.Mapping;
    using EducationHub.Web.ViewModels.Categories;

    using static EducationHub.Data.Common.Validations.DataValidation.Post;

    public class EditPostViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ContentMinLength)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public IEnumerable<CategoriesItemsViewModel> CategoriesItems { get; set; }

    }
}
