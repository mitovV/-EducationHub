// ReSharper disable VirtualMemberCallInConstructor
namespace EducationHub.Data.Models
{
    using System;

    using Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class Role : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public Role()
            : this(null)
        {
        }

        public Role(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
