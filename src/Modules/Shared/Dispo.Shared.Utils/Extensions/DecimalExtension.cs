using System.Globalization;

namespace Dispo.Shared.Utils.Extensions
{
    public static class DecimalExtension
    {
        public static string ConverterParaReal(this decimal valorDecimal)
        {
            int numeroCasasDecimais = 2;

            CultureInfo culturaBRL = new CultureInfo("pt-BR");

            NumberFormatInfo formatoMoedaBRL = culturaBRL.NumberFormat;
            formatoMoedaBRL.CurrencyDecimalDigits = numeroCasasDecimais;
            formatoMoedaBRL.CurrencyDecimalSeparator = ",";
            formatoMoedaBRL.CurrencyGroupSeparator = ".";
            formatoMoedaBRL.CurrencySymbol = "R$";

            string valorEmReal = valorDecimal.ToString("C", formatoMoedaBRL);

            return valorEmReal;
        }
    }
}