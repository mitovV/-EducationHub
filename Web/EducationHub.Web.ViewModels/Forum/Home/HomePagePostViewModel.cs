namespace EducationHub.Web.ViewModels.Forum.Home
{
    using System;

    using Data.Models;
    using Services.Mapping;

    public class HomePagePostViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public string CategoryName { get; set; }

        public string UserPictureUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
