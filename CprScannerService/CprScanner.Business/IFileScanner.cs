namespace CprScanner.Business
{
	using System.Collections.Generic;
	using System.IO;

	public interface IFileScanner
	{
		IEnumerable<Cpr> Scan(Stream fileStream, string fileName);
	}
}