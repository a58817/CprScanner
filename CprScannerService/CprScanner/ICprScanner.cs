namespace CprScanner
{
    using System;
    using System.IO;

    public interface ICprScanner
	{
		void FindCpr(Stream fileStream, string fileName, Action<string> handleCpr);
	}
}