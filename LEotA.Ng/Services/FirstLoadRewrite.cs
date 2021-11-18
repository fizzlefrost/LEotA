using System.Globalization;
using Microsoft.AspNetCore.Rewrite;

namespace LEotA.Services
{
    public class FirstLoadRewriteRule : IRule
    {
        public void ApplyRule(RewriteContext context)
        {
            var culture = CultureInfo.CurrentCulture;
            var request = context.HttpContext.Request;
            if(request.Path == "/")  
            {
                request.Path = new Microsoft.AspNetCore.Http.PathString($"/{culture.Name}");
            }
        }
    }
}