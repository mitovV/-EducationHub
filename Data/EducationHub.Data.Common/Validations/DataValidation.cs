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
            public const int TitleMinLenth = 5;
            public const int TitleMaxLength = 70;

            public const int VideoUrlMinlength = 20;
            public const int VideoUrlMaxLength = 500;

            public const int DescriptionMinlength = 50;
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
            public const int TitleMaxLength = 70;
            public const int DescriptionMinLength = 100;
        }

        public class Vote
        {
            public const int MinValue = 1;
            public const int MaxValue = 5;
        }

        public class Post
        {
            public const int TitleMaxLength = 50;
        }
    }
}
