namespace CprScanner.Business.Ports
{
    using System.Collections.Generic;
    using System.IO;

    public interface IFileExtractor
    {
        IEnumerable<ICandidateFile> ExtractFiles(Stream fileStream);
    }
}
