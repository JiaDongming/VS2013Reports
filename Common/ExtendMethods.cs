using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    public static class ExtendMethods
    {
        public static bool IsNumber(this string str)
        {
            if (string.IsNullOrEmpty(str)) return false;

            return Regex.IsMatch(str, @"^-?\d+$");
        }

        public static int ToInt32(this string str)
        {
            if (!str.IsNumber())
            {
                return 0;
            }
            return Convert.ToInt32(str);
        }

        public static int WeekOfYear(this DateTime dt)
        {
            int firstWeekend = 7 - Convert.ToInt32(DateTime.Parse(dt.Year + "-1-1").DayOfWeek);
            int currentDay = dt.DayOfYear;
            return Convert.ToInt32(Math.Ceiling((currentDay - firstWeekend) / 7.0)) + 1;

        }

        public static DateTime NullDateTime = new DateTime(1970, 1, 1);
        public static bool IsNullDateTime(this DateTime dt)
        {
            return dt <= NullDateTime;
        }

        public static bool EqualByMonth(this DateTime d1, DateTime d2)
        {
            return d1.Year == d2.Year && d1.Month == d2.Month;
        }

        public static bool LessThanByMonth(this DateTime d1, DateTime d2)
        {
            if (d1.Year < d2.Year || d1.Year == d2.Year && d1.Month < d2.Month)
                return true;
            return false;
        }
    }
}
