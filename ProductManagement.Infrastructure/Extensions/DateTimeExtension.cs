using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Infrastructure.Extensions
{
  public static class DateTimeExtension
    {
        public static string ToShortDateTimeString(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd HH:mm");
        }

        public static string ToShortDateStringExtend(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd");
        }
        
        public static string ToShortTimeString(this TimeSpan value)
        {
            return value.ToString("hh\\:mm\\:ss");
        }

        public static bool IsValidDateTime(this DateTime value)
        {
            return value > DateTime.MinValue;
        }

        public static bool IsValidDateTime(this DateTime? value)
        {
            return value!=null && value > DateTime.MinValue;
        }

        public static string ToDateStringTry(this DateTime? dateTime)
        {
            return dateTime !=null ? dateTime.GetValueOrDefault().ToString(Utilities.Date.FormatDate.Date) :null;
        }

        public static string ToDateStringForSlashTry(this DateTime? dateTime)
        {
            return dateTime != null ? dateTime.GetValueOrDefault().ToString(Utilities.Date.FormatDate.DateForSlash) : "";
        }

        public static string ToDateTimeStringTry(this DateTime? dateTime)
        {
            return dateTime != null ? dateTime.GetValueOrDefault().ToString(Utilities.
                
                Date.FormatDate.DateTime) : "";
        }

        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime != null ? dateTime.ToString(Utilities.Date.FormatDate.Date) : "";
        }

        public static string ToDateTimeString(this DateTime dateTime)
        {
            return dateTime != null ? dateTime.ToString(Utilities.Date.FormatDate.DateTime) : "";
        }

        public static int CompareInMinutes(this DateTime value, DateTime secondDateTime)
        {
            return Convert.ToInt32(secondDateTime.Subtract(value).TotalMinutes);
        }

       
    }
}
