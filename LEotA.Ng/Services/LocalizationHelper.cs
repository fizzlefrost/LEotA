using LEotA.Models;
using Microsoft.AspNetCore.Localization;
using LEotA.Services;
using Microsoft.AspNetCore.Localization;

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