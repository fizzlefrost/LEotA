using System.Collections.Generic;
using System.Globalization;
 
namespace LEotA.Models
{
    public class CultureSwitcherModel
    {
        public CultureInfo CurrentUiCulture { get; set; }
        public List<CultureInfo> SupportedCultures { get; set; }
    }
}