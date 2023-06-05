using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core.Helper
{
    public class UnixTime
    {
        private static DateTime BaseTime = new DateTime(1970, 1, 1);

        /// <summary>   
        /// 将unixtime(UTC)转换为.NET的DateTime(UTC+8)
        /// </summary>   
        /// <param name="timeStamp">秒数</param>   
        /// <returns>转换后的时间</returns>   
        public static DateTime FromTimeStamp(int timeStamp)
        {
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            var ticks = (timeStamp) * (long)10000000 + BaseTime.Ticks;
            var utcTime = new DateTime(ticks, DateTimeKind.Utc);
            var gtmTime = TimeZoneInfo.ConvertTime(utcTime, TimeZoneInfo.Utc, zone);
            return gtmTime;
        }

        /// <summary>   
        /// 将.NET的DateTime转换为unix time   
        /// </summary>   
        /// <param name="dateTime">待转换的时间</param>   
        /// <returns>转换后的unix time</returns>   
        public static int ToTimeStamp(DateTime dateTime)
        {
            var time = dateTime;
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            TimeZoneInfo utcZone = TimeZoneInfo.FindSystemTimeZoneById("UTC");
            if (dateTime.Kind == DateTimeKind.Unspecified)//中国时间
            {
                time = TimeZoneInfo.ConvertTime(time, zone, utcZone);
            }
            else if (time.Kind == DateTimeKind.Local)
            {
                time = TimeZoneInfo.ConvertTime(time, TimeZoneInfo.Local, utcZone);
            }

            return (int)((time.Ticks - BaseTime.Ticks) / (long)10000000);
        }

        /// <summary>   
        /// 将unixtime(UTC)转换为.NET的DateTime(UTC+8)
        /// </summary>   
        /// <param name="timeStamp">秒数</param>   
        /// <returns>转换后的时间</returns>   
        public static DateTime FromTimeString(string timeStr)
        {
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime utcTime = DateTime.MinValue;
            if (DateTime.TryParse(timeStr, out var d))
            {
                utcTime = d;
            }
            var gtmTime = TimeZoneInfo.ConvertTime(utcTime, TimeZoneInfo.Utc, zone);
            return gtmTime;
        }

        /// <summary>   
        /// 将.NET的DateTime转换为unix time   
        /// </summary>   
        /// <param name="dateTime">待转换的时间</param>   
        /// <returns>转换后的unix time</returns>   
        public static string ToTimeString(DateTime dateTime)
        {
            var time = dateTime;
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            TimeZoneInfo utcZone = TimeZoneInfo.FindSystemTimeZoneById("UTC");
            if (dateTime.Kind == DateTimeKind.Unspecified)//中国时间
            {
                time = TimeZoneInfo.ConvertTime(time, zone, utcZone);
            }
            else if (time.Kind == DateTimeKind.Local)
            {
                time = TimeZoneInfo.ConvertTime(time, TimeZoneInfo.Local, utcZone);
            }

            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static int ToTimeStamp(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return ToTimeStamp(dateTime.Value);
            }
            else
            {
                return 0;
            }
        }


    }
}
