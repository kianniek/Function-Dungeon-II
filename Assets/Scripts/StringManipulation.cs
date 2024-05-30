using System.Text.RegularExpressions;

public static class StringManipulation
{
    /// <summary>
    /// Only leaves numbers, ',' and '.' in a string
    /// </summary>
    /// <param name="input">Input string</param>
    /// <returns></returns>
    public static string CleanUpDecimalOnlyString(string input)
    {
        return Regex.Replace(input, @"[^0-9.,]+", "");
    }
}
