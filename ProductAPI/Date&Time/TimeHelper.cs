namespace ProductAPI.Date_Time
{
    public class TimeHelper
    {
        public static DateTime GetIndianTime()
        {
            TimeZoneInfo indiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indiaTimeZone);
        }
    }
}
