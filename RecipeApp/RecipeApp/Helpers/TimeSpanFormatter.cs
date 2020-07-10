using System;

namespace RecipeApp.Helpers
{
    public static class TimeSpanFormatter
    {
        public static string Format(TimeSpan? nullableTimeSpan)
        {
            if (nullableTimeSpan == null)
                return string.Empty;

            var timeSpan = (TimeSpan)nullableTimeSpan;

            if (timeSpan.Hours > 0 && timeSpan.Minutes > 0)
                return $"{timeSpan.Hours}:{timeSpan.Minutes} h";

            if (timeSpan.Hours > 0)
                return $"{timeSpan.Hours} h";

            if (timeSpan.Minutes > 0)
                return $"{timeSpan.Minutes} min";

            return string.Empty;
        }
    }
}