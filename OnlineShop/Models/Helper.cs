using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public static class Helper
    {
        public static bool IsNumeric(string number, out long outNumber)
        {
            return long.TryParse(number, out outNumber);
        }
    }
}