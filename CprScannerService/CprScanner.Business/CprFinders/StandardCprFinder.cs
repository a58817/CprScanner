namespace CprScanner.Business.CprFinders
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class StandardCprFinder : ICprFinder
    {
        public IEnumerable<Cpr> Match(string text)
        {
            var format = @"(\d{6}[\\/-]\d{4})";
            var regex = new Regex(format);
            var matches = regex.Matches(text ?? string.Empty);
            foreach (Match match in matches)
            {
                yield return match.Value;
            }
        }
    }
}
