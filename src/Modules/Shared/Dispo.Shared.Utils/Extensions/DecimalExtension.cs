using System.Globalization;

namespace Dispo.Shared.Utils.Extensions
{
    public static class DecimalExtension
    {
        public static string ConvertToCurrency(this decimal value)
        {
            int decimalPlaces = 2;

            CultureInfo culturaBRL = new CultureInfo("pt-BR");

            NumberFormatInfo valueBRL = culturaBRL.NumberFormat;
            valueBRL.CurrencyDecimalDigits = decimalPlaces;
            valueBRL.CurrencyDecimalSeparator = ",";
            valueBRL.CurrencyGroupSeparator = ".";
            valueBRL.CurrencySymbol = "R$";

            string valorInBRL = value.ToString("C", valueBRL);

            return valorInBRL;
        }
    }
}