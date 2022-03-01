namespace CprScanner.FileHandlers.Aspose.Extractors
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using global::Aspose.Email;
    using global::Aspose.Email.Mime;
    using CprScanner.Business;
    using CprScanner.Business.Ports;

    public class MsgExtractor : IFileExtractor
    {
        private readonly IBusinessLogger logger;

        public MsgExtractor(IBusinessLogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<ICandidateFile> ExtractFiles(Stream fileStream)
        {
            var email = MailMessage.Load(fileStream);
            foreach (var attachment in email.Attachments)
            {
                var fileName = this.GetFileWithExtension(attachment.ContentType);
                this.logger.Info($"Vedhæftet fil fundet med navn: {fileName}.");
                yield return new CandidateFile(attachment.ContentStream, fileName);
            }
        }

        private string GetFileWithExtension(ContentType file)
        {
            var fileName = string.IsNullOrWhiteSpace(file.Name) ? "unavngiven" : file.Name;

            if (fileName.Contains("."))
            {
                return fileName;
            }

            return $"{fileName}{this.GuessExtension(file.MediaType)}";
        }

        private string GuessExtension(string mediaType)
        {
            var guess = string.Empty;
            switch (mediaType.ToLower())
            {
                case "application/pdf":
                    guess = ".pdf";
                    break;
                case "text/html":
                    guess = ".html";
                    break;
                case "text/plain":
                    guess = ".txt";
                    break;
                case "message/rfc822":
                case "application/vnd.ms-outlook":
                    guess = ".msg";
                    break;
                case "application/msword":
                    guess = ".doc";
                    break;
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    guess = ".docx";
                    break;
            }

            if (string.IsNullOrEmpty(guess))
            {
                this.logger.Warning($"Kunne ikke identificere filtypen: \"{mediaType}\"");
            }
            else
            {
                this.logger.Info($"Identificeret filtypen \"{mediaType}\" til at være \"{guess}\".");
            }

            return guess;
        }
    }
}
