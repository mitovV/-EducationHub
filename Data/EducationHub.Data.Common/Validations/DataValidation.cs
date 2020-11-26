namespace EducationHub.Data.Common.Validations
{
    public static class DataValidation
    {
        public class Category
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
            public const int PictureUrlMaxLength = 400;
        }

        public class Lesson
        {
            public const int TitleMaxLength = 50;
            public const int VideoUrlMinlength = 20;
            public const int VideoUrlMaxLength = 500;
        }

        public class User
        {
            public const int UsernameMinLenght = 4;
            public const int UsernameMaxLenght = 25;
            public const int PictureUrlMaxLength = 400;
        }

        public class Course
        {
            public const int TitleMinLength = 4;
            public const int TitleMaxLength = 50;
            public const int DescriptionMinLength = 100;
        }
    }
}
