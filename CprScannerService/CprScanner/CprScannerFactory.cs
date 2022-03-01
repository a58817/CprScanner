namespace CprScanner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using CprScanner.Business;
    using CprScanner.Business.CprFinders;
    using CprScanner.Business.Ports;
    using CprScanner.Business.Strategies;
    using CprScanner.FileHandlers.Aspose;
    using CprScanner.FileHandlers.Aspose.Converters;
    using CprScanner.FileHandlers.Aspose.Extractors;

    /// <summary>The cpr scanner factory.</summary>
    public static class CprScannerFactory
    {
        /// <summary>The create.</summary>
        /// <returns>The <see cref="ICprScanner"/>.</returns>
        public static ICprScanner Create(CprScannerConfig config, ILogger externalLogger = null)
        {
            IBusinessLogger logger = new LogAdapter(externalLogger ?? new ConsoleLogger());

            SetupLicense(config.AsposeLicensePath, "Aspose.Total.lic");

            var fileExtractorStrategy = SetupExtractors(logger);
            var fileConverterStrategy = SetupConverts(logger);

            var cprFinders = new ICprFinder[] { new StandardCprFinder(), new MaxLengthCprFinder() };
            var cprFinderComposit = new CprFinderComposit(cprFinders);
            var filScanner = new FileScanner(fileExtractorStrategy, fileConverterStrategy, cprFinderComposit);
            var distinctFileScanner = new DistinctFileScanner(filScanner);
            var validCprFileScanner = new ValidCprFileScanner(distinctFileScanner);

            return new Scanner(validCprFileScanner);
        }

        private static FileConverterStrategy SetupConverts(IBusinessLogger logger)
        {
            var wordConverter = new WordToTextConverter();
            var converters = new Dictionary<string, ITextConverter>
            {
                {".msg", new MsgToTextConverter()},
                {".pdf", new PdfTextObjectsToTextConverter()},
                {".docx", wordConverter},
                {".doc", wordConverter},
                {".rtf", wordConverter},
                {".txt", wordConverter},
                {".html", wordConverter},
                {".htm", wordConverter}
            };

            var fileConverterStrategy = new FileConverterStrategy(converters, logger);
            return fileConverterStrategy;
        }

        private static FileExtractorStrategy SetupExtractors(IBusinessLogger logger)
        {
            var extractors = new Dictionary<string, IFileExtractor>
            {
                {".msg", new MsgExtractor(logger)}
            };

            var fileExtractorStrategy = new FileExtractorStrategy(extractors);
            return fileExtractorStrategy;
        }

        private static void SetupLicense(string licensePath, string licenseFile)
        {
            var path = licensePath ?? AppDomain.CurrentDomain.BaseDirectory;
            var file = $@"{path}\{licenseFile}";
            if (!File.Exists(file))
            {
                throw new Exception($"Kan ikke finde Aspose licensfil: {file}");
            }

            new TotalLicense().SetLicense(file);
        }
    }
}
