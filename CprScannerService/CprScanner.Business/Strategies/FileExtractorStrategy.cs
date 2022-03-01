namespace CprScanner.Business.Strategies
{
    using System;
    using System.Collections.Generic;
    using CprScanner.Business.Ports;

    public class FileExtractorStrategy
    {
        private readonly IDictionary<string, IFileExtractor> extractors;

        public FileExtractorStrategy(IDictionary<string, IFileExtractor> extractors)
        {
            this.extractors = extractors ?? throw new ArgumentNullException(nameof(extractors));
        }

        public IEnumerable<ICandidateFile> ExtractFiles(ICandidateFile candidateFile)
        {
            yield return candidateFile;
            foreach (var subFile in this.ExtractSubFiles(candidateFile))
            {
                yield return subFile;
            }
        }

        private IEnumerable<ICandidateFile> ExtractSubFiles(ICandidateFile candidateFile)
        {
            if (!this.extractors.ContainsKey(candidateFile.GetExtension()))
            {
                yield break;
            }

            foreach (var extractedFile in this.extractors[candidateFile.GetExtension()].ExtractFiles(candidateFile.Stream))
            {
                yield return extractedFile;
                foreach (var subFile in this.ExtractSubFiles(extractedFile))
                {
                    yield return subFile;
                }
            }
        }
    }
}
