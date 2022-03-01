namespace CprScanner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
	using System.Linq;

	using CprScanner.Business;

	class ValidCprFileScanner : IFileScanner
	{
		private readonly IFileScanner fileScannerDecoratee;

		public ValidCprFileScanner(IFileScanner fileScannerDecoratee)
		{
			this.fileScannerDecoratee = fileScannerDecoratee ?? throw new ArgumentNullException(nameof(fileScannerDecoratee));
		}
		public IEnumerable<Cpr> Scan(Stream fileStream, string fileName)
		{
			return this.fileScannerDecoratee.Scan(fileStream, fileName).Where(cpr => cpr.ErValid());
		}
	}
}
