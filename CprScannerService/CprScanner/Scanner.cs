namespace CprScanner
{
    using System;
    using System.IO;
    using System.Linq;

    using CprScanner.Business;

    class Scanner : ICprScanner
	{
		private readonly IFileScanner filScanner;

		public Scanner(IFileScanner filScanner)
		{
			this.filScanner = filScanner ?? throw new ArgumentNullException(nameof(filScanner));
		}

		public void FindCpr(Stream fileStream, string fileName, Action<string> handleCpr)
		{
            foreach (var cpr in this.filScanner.Scan(fileStream, fileName).Select(cpr => cpr.ToString()))
			{
				handleCpr(cpr);
			}
        }
    }
}