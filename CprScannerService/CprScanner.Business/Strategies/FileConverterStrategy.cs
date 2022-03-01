namespace CprScanner.Business.Strategies
{
    using System;
    using System.Collections.Generic;
    using CprScanner.Business.Ports;

    public class FileConverterStrategy
    {
        private readonly IDictionary<string, ITextConverter> converters;
        private readonly IBusinessLogger logger;

        public FileConverterStrategy(IDictionary<string, ITextConverter> converters, IBusinessLogger logger)
        {
            this.converters = converters ?? throw new ArgumentNullException(nameof(converters));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string Convert(ICandidateFile candidateFileStream)
        {
            if (!this.converters.ContainsKey(candidateFileStream.GetExtension()))
            {
                this.logger.Error($"Ukendt filformat, kan ikke skannes: {candidateFileStream.Name}");
                return string.Empty;
            }

            var plaintext = this.converters[candidateFileStream.GetExtension()].Convert(candidateFileStream.Stream);

            if (string.IsNullOrWhiteSpace(plaintext))
            {
                this.logger.Error($"Kunne ikke skanne: {candidateFileStream.Name}. Var ikke i stand til at udvinde tekst.");
            }

            return plaintext;
        }
    }
}
