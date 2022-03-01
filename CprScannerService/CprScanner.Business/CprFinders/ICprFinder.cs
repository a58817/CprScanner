namespace CprScanner.Business
{
    using System.Collections.Generic;

    public interface ICprFinder
    {
        IEnumerable<Cpr> Match(string text);
    }
}
