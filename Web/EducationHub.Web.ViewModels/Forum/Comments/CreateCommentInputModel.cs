namespace EducationHub.Web.ViewModels.Forum.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCommentInputModel
    {
        public int PostId { get; set; }

        public int ParentId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
