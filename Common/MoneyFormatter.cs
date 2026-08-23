using System.Globalization;

namespace CapEx.Common;

public static class MoneyFormatter
{
    private static readonly NumberFormatInfo RandFormat = new()
    {
        NumberGroupSeparator = " ",
        NumberDecimalSeparator = ".",
        NumberDecimalDigits = 2
    };

    public static string ToRands(this decimal amount) => "R " + amount.ToString("N", RandFormat);
}
