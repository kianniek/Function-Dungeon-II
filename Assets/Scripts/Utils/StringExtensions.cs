using System.Text.RegularExpressions;

namespace Utils
{
    public static class StringExtensions
    {
        /// <summary>
        /// Only leaves numbers, ',' and '.' in a string
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Input string with just the numbers and decimal indication left</returns>
        public static string CleanUpDecimalOnlyString(string input)
        {
            return Regex.Replace(input, "[^0-9.,]+", "");
        }
    }
}
