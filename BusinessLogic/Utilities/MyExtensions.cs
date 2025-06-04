namespace BusinessLogic.Utilities
{
    public static class MyExtensions
    {
        public static DateTime SaDateTime(this DateTime date)
        {
            return TimeZoneInfo.ConvertTime(date, TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time"));
        }
    }
}
