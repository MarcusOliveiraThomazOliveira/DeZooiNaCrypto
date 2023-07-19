using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Util
{
    public static class ExtensionMethods
    {
        public static DateTime FirstDayOfWeek(this DateTime date)
        {
            return date.AddDays(((((int)date.DayOfWeek) - 1) + 1) * -1);
        }
        public static DateTime LastDayOfWeek(this DateTime date)
        {
            //(7 - DiaDaSemana) - 1
            return date.AddDays((7 - ((int)date.DayOfWeek)) - 1);
        }
        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
        public static DateTime LastDayOfMonth(this DateTime date)
        {
            return date.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }
        public static DateTime FirstDayOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }
        public static DateTime LastDayOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 12, 31);
        }
    }
}
