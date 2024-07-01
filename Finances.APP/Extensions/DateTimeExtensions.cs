using System;

namespace Finances.APP.Extensions
{
    public static class DateTimeExtensions
    {
        static string[] monthNames = new string[]
            {
                "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho",
                "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"
            };

        public static string GetMonthNameAndYear(this DateTime date)
        {
            return $"{monthNames[date.Month - 1]} - {date.Year}";
        }

        public static string GetMonthName(this DateTime date)
        {
            return monthNames[date.Month - 1];
        }
    }
}
