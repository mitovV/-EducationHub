namespace EducationHub.Web.Infrastructure.Extensions
{
    using System;

    public static class DateTimeExtensions
    {
        public static string ToStringLocal(this DateTime? date)
            => date == null ? "No records" : ((DateTime)date).ToString("O");

        public static string ToStringLocal(this DateTime date)
           => date == null ? "No records" : date.ToString("O");
    }
}
