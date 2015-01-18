using System;
using System.Globalization;

namespace TrouveUnBand.Classes
{
    public static class DateHelper
    {
        public static string GetDayAndMonth(DateTime date)
        {
            int day = date.Day;
            int month = date.Month;
            CultureInfo culture = new CultureInfo("fr-CA");
            string abstractedMonth = culture.DateTimeFormat.GetMonthName(month);
            string dayAndMonth;

            abstractedMonth = abstractedMonth.Substring(0, 3) + ".";
            dayAndMonth = day + " " + abstractedMonth;

            return dayAndMonth;
        }

        public static string GetDayOfWeek(DateTime date)
        {
            CultureInfo culture = new CultureInfo("fr-CA");
            string dayOfWeek = culture.DateTimeFormat.GetDayName(date.DayOfWeek);
            dayOfWeek = char.ToUpper(dayOfWeek[0]) + dayOfWeek.Substring(1);

            return dayOfWeek;
        }

        public static string GetDay(DateTime date)
        {
            return date.Day.ToString();
        }
    }
}
