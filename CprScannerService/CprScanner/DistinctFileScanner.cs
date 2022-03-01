namespace CprScanner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CprScanner.Business;

    class DistinctFileScanner : IFileScanner
	{
		private readonly IFileScanner fileScannerDecoratee;

		public DistinctFileScanner(IFileScanner fileScannerDecoratee)
		{
			this.fileScannerDecoratee = fileScannerDecoratee ?? throw new ArgumentNullException(nameof(fileScannerDecoratee));
		}

		public IEnumerable<Cpr> Scan(Stream fileStream, string fileName)
		{
			return this.fileScannerDecoratee.Scan(fileStream, fileName).Distinct();
		}
	}
}
