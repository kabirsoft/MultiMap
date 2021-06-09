using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiMap.Helpers
{
    public static class HtmlHelpers
    {
        public static string Truncate(this IHtmlHelper helper, string input, int length)
        {
            if (string.IsNullOrEmpty(input) || input.Length < length)
            {
                return input;
            }
            else
            {
                return input.Substring(0, length) + "...";
            }
        }
    }
}
