namespace EducationHub.Web.ViewModels.Forum
{
    public class CreateCommentInputModel
    {
        public int PostId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }
    }
}
