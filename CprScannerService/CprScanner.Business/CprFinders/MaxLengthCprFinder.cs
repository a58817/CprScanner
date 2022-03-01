namespace CprScanner.Business.CprFinders
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class MaxLengthCprFinder : ICprFinder
    {
        public IEnumerable<Cpr> Match(string text)
        {
            var formatMaxLength = @"(?<!\d)\d{10}(?!\d)";
            var regexMaxCprLength = new Regex(formatMaxLength);
            var maxLengthCprMatches = regexMaxCprLength.Matches(text ?? string.Empty);
            foreach (Match maxLengthCprMatch in maxLengthCprMatches)
            {
                yield return maxLengthCprMatch.Value;
            }
        }
    }
}
