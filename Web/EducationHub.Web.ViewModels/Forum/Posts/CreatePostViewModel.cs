namespace EducationHub.Web.ViewModels.Forum.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Categories;

    using static Data.Common.Validations.DataValidation.Post;

    public class CreatePostViewModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ContentMinLength)]
        public string Content { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoriesItemsViewModel> CategoriesItems { get; set; }
    }
}
