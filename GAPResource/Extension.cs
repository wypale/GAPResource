using Newtonsoft.Json.Linq;

namespace GAPResource
{
    public static class Extension
    {

        public static double GetDouble(this string doubleString)
        {
            string groupSep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            return Convert.ToDouble(doubleString.Replace(",", groupSep).Replace(".", groupSep));
        }

    }
}
