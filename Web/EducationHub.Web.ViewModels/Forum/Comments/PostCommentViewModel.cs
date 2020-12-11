namespace EducationHub.Web.ViewModels.Forum.Comments
{
    using System;

    using Data.Models;
    using Ganss.XSS;
    using Services.Mapping;

    public class PostCommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserUserName { get; set; }

        public string UserId { get; set; }

        public string UserPictureUrl { get; set; }
    }
}
