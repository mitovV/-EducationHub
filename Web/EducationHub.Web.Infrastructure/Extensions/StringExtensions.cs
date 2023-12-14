namespace EducationHub.Web.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToShortString(this string value, int count = 10)
        {
            if (value.Length > count)
            {
                return value[..count] + "...";
            }

            return value;
        }
    }
}
