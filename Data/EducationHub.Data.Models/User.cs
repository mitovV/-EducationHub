﻿// ReSharper disable VirtualMemberCallInConstructor
namespace EducationHub.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Categories = new HashSet<Category>();
            this.Courses = new HashSet<Course>();
            this.Lessons = new HashSet<Lesson>();
        }

        public string PictureUrl { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
