namespace TrainigSectorWebSite
{
    public static class LocalizationHelper
    {
        public static string GetLocalized(string ar, string en, string culture)
        {
            return culture.StartsWith("ar") ? ar : en;
        }
    }
}
