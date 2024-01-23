using System.Text.RegularExpressions;

namespace Dispo.Shared.Utils.Extensions
{
    public static class StringExtension
    {
        public static string RemovePhoneCharacters(this string phone)
        {
            Regex regex = new Regex("[^0-9]");
            return regex.Replace(phone, "");
        }
    }
}
