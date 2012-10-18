using System;

namespace App.Data.Helpers
{
    public static class DateTimeExtensions
    {
        public static DateTime ToCetTime(this DateTime dt)
        {

            return TimeZoneInfo.ConvertTime(dt,
                TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"))
                .AddHours(1);
        }
        public static string ToDateTimeString(this DateTime dt)
        {

            return dt.ToString("dd/MM/yyyy HH:mm");
        }
        public static string ToDateString(this DateTime dt)
        {

            return dt.ToString("dd/MM/yyyy");
        }

    }
}
