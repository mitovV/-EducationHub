namespace EducationHub.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;

    using static Common.Validations.DataValidation.Category;

    public class Category : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PictureUrlMaxLength)]
        public string PictureUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
            = new HashSet<Course>();

        public virtual ICollection<Lesson> Lessons { get; set; }
            = new HashSet<Lesson>();

        public virtual ICollection<Post> Posts { get; set; }
            = new HashSet<Post>();
    }
}
