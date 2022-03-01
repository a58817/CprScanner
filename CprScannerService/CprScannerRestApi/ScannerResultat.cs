namespace CprScannerRestApi
{
    using System.Collections.Generic;

    public class ScannerResultat
    {
        public string FilNavn { get; }
        public List<string> CprNumre { get; }

        public ScannerResultat(string filNavn, List<string> cprNumreFundet)
        {
            this.FilNavn = filNavn;
            this.CprNumre = cprNumreFundet;
        }
    }
}
