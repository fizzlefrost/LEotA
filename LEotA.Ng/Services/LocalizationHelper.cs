using LEotA.Models;

namespace LEotA.Services
{
    public class LocalizationHelper
    {
        public static string GetTextByCulture(CultureBase text, string currentCulture)
        {
            if (currentCulture == "en")
            {
                return text.English;
            }
            else
            {
                return text.Russian;
            }
        }
    }
}