namespace EducationHub.Data.Common.Validations
{
    public static class DataValidation
    {
        public class Category
        {
            public const int NameMaxLength = 50;
            public const int PictureUrlMaxLength = 400;
        }

        public class Lesson
        {
            public const int TitleMaxLength = 50;
            public const int DescriptionMaxLength = 1000;
            public const int VideoUrlMaxLength = 500;
        }

        public class User
        {
            public const int PictureUrlMaxLength = 400;
        }

        public class Course
        {
            public const int TitleMaxLength = 50;
            public const int DescriptionMaxLength = 1000;
        }
    }
}
