using ARH.Front.Models;

namespace ARH.Front.Services
{
    public static class Converter
    {
        public static IEnumerable<DailyRecord> To(this IEnumerable<DayData> input, string userId)
        {
            IEnumerable<DailyRecord> result = input.To(To);
            foreach (DailyRecord item in result)
            {
                item.UserId = userId;
            }
            return result;
        }

        public static IEnumerable<DayData> To(this IEnumerable<DailyRecord> input) => input.To(To);

        public static DailyRecord To(this DayData input) => input.To(string.Empty);

        public static DailyRecord To(this DayData input, string userId)
        {
            DailyRecord result = new();
            result.AtHome = input.AtHome.ToString();
            result.Day = input.Day;
            result.Id = input.Id;
            result.OnSite = input.OnSite.ToString();
            result.PayedVacation = input.PayedVacation.ToString();
            result.Sickness = input.Sickness.ToString();
            result.UnpayedVacation = input.UnpayedVacation.ToString();
            result.UserId = userId;
            return result;
        }

        public static DayData To(this DailyRecord input)
        {
            DayData result = new();
            result.AtHome = Enum.Parse<Length>(input.AtHome);
            result.Day = input.Day;
            result.Id = input.Id;
            result.OnSite = Enum.Parse<Length>(input.OnSite);
            result.PayedVacation = Enum.Parse<Length>(input.PayedVacation);
            result.Sickness = Enum.Parse<Length>(input.Sickness);
            result.UnpayedVacation = Enum.Parse<Length>(input.UnpayedVacation);
            return result;
        }

        private static IEnumerable<T> To<T, U>(this IEnumerable<U> input, Func<U, T> convert)
        {
            List<T> result = new();
            if (input != null)
            {
                foreach (U item in input)
                {
                    T res = convert(item);
                    result.Add(res);
                }
            }
            return result;
        }
    }
}