namespace TripCoordination.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }
    }
}
