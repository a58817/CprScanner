namespace CprScanner.Business
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using CprScanner.Business.Strategies;

    public class FileScanner : IFileScanner
    {
        private readonly FileExtractorStrategy fileExtractorStrategy;

        private readonly FileConverterStrategy fileConverterStrategy;

        private readonly ICprFinder cprFinder;

        public FileScanner(FileExtractorStrategy fileExtractorStrategy, FileConverterStrategy fileConverterStrategy, ICprFinder patternFinder)
        {
            this.fileExtractorStrategy = fileExtractorStrategy ?? throw new ArgumentNullException(nameof(fileExtractorStrategy));
            this.fileConverterStrategy = fileConverterStrategy ?? throw new ArgumentNullException(nameof(fileConverterStrategy));
            this.cprFinder = patternFinder ?? throw new ArgumentNullException(nameof(patternFinder));
        }

        public IEnumerable<Cpr> Scan(Stream fileStream, string fileName)
        {
            var fileStreamWithExtension = new CandidateFile(fileStream, fileName);
            var filer = this.fileExtractorStrategy.ExtractFiles(fileStreamWithExtension);
            foreach (var streamWithExtension in filer)
            {
                var text = this.fileConverterStrategy.Convert(streamWithExtension);
                foreach (var cpr in this.cprFinder.Match(text))
                {
                    yield return cpr;
                }
            }
        }
    }
}
