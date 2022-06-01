using System;
using System.Globalization;

namespace adminiotintel.Helpers
{
    public class ConvertTime
    {
        public static Int32 unixTimestamp()
        {
            return (Int32)DateTimeToUnixTimestamp(DateTime.Now);
        }

        ////public static Int32 GetHourStamp(string hourMinute)
        ////{
        ////    string[] hourMinuteArray = hourMinute.Split(':');
        ////    if (hourMinuteArray.Length >= 1)
        ////    {
        ////        DateTime dateFormat = new DateTime(1970, 1, 1, int.Parse(hourMinuteArray[0]), int.Parse(hourMinuteArray[1]), 0, 0, System.DateTimeKind.Utc);
        ////        ////return Math.Abs((Int32)DateTimeToUnixTimestamp(dateFormat));
        ////        return (Int32)(TimeZoneInfo.ConvertTimeToUtc(dateFormat) - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        ////    }
        ////    else
        ////        return (Int32)DateTimeToUnixTimestamp(new DateTime(1970, 1, 1, 0, 0, 0, 0));
        ////}
        public static DateTime StringtoDate(string dateString)
        {
            return DateTime.ParseExact(dateString, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static DateTime UnixTimeStampToDateTime1(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                     new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        public static double GetDateStamp(DateTime datetime)
        {
            var newdatetime = new DateTime(datetime.Year, datetime.Month, datetime.Day, 0, 0, 0);
            return (TimeZoneInfo.ConvertTimeToUtc(newdatetime) -
                     new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        public static double GetDateStamp(string datetime)
        {
            var tempDatetime = DateTime.ParseExact(datetime,
                   "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return (TimeZoneInfo.ConvertTimeToUtc(tempDatetime) -
                     new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

      
        public static DateTime StringToDateTime(string datetime)
        {
            return DateTime.ParseExact(datetime,
                  "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public static DateTime FormatMMDDYYYY(DateTime dateTime)
        {
            return (DateTime.ParseExact(dateTime.ToString("dd/MM/yyyy hh:mm:ss"),
                   "MM/dd/yyyy hh:mm:ss", CultureInfo.InvariantCulture));
        }

        //public static DateTime FormatDDMMYYYY(DateTime dateTime)
        //{
        //    return (DateTime.ParseExact(dateTime.ToString("MM/dd/yyyy hh:mm:ss"),
        //           "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture));
        //}
        public static string format = "dd/MM/yyyy HH:mm:ss";


        public static double LastDayMont(DateTime date)
        {
            DateTime lastDayOfMonth = UnixTimeStampToDateTime(FirstDayMont(date));
            return DateTimeToUnixTimestamp(lastDayOfMonth.AddMonths(1).AddDays(-1));
        }
        public static double FirstDayMont(DateTime date)
        {
            DateTime first = new DateTime(date.Year, date.Month, 1);
            return DateTimeToUnixTimestamp(first);
        }

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long GetCurrentUnixTimestampMillis()
        {
            return (long)(DateTime.UtcNow - UnixEpoch).TotalMilliseconds;
        }

        public static DateTime DateTimeFromUnixTimestampMillis(long millis)
        {
            return UnixEpoch.AddMilliseconds(millis);
        }

        public static long GetCurrentUnixTimestampSeconds()
        {
            return (long)(DateTime.UtcNow - UnixEpoch).TotalSeconds;
        }

        public static DateTime DateTimeFromUnixTimestampSeconds(long seconds)
        {
            return UnixEpoch.AddSeconds(seconds);
        }




    }
}