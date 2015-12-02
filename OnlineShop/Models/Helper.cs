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