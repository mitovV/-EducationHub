﻿namespace EducationHub.Web.ViewModels.Categories
{
    using Data.Models;
    using Services.Mapping;

    public class AllViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }
    }
}
