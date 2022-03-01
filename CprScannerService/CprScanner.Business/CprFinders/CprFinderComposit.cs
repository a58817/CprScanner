namespace CprScanner.Business.CprFinders
{
    using System.Collections.Generic;

    public class CprFinderComposit : ICprFinder
    {
        private readonly IEnumerable<ICprFinder> cprFinders;

        public CprFinderComposit(IEnumerable<ICprFinder> cprFinders)
        {
            this.cprFinders = cprFinders ?? throw new System.ArgumentNullException(nameof(cprFinders));
        }
        public IEnumerable<Cpr> Match(string text)
        {
            foreach (var cprFinder in this.cprFinders)
            {
                var matches = cprFinder.Match(text);
                foreach (var match in matches)
                {
                    yield return match;
                }
            }
        }
    }
}
