using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class DateTimeHelper
    {
        private static readonly TimeZoneInfo mexicoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");

        public static DateTime GetDateTimeFromDateTimeUTC(DateTime datetimeUTC)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(datetimeUTC, mexicoTimeZone);
        }
    }
}