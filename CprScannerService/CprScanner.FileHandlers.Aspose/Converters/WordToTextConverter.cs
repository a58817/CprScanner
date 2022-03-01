namespace CprScanner.FileHandlers.Aspose.Converters
{
    using System.IO;
    using global::Aspose.Words;
    using CprScanner.Business.Ports;

    public class WordToTextConverter : ITextConverter
    {
        public string Convert(Stream filestream)
        {
            var plaintext = new PlainTextDocument(filestream);
            return plaintext.Text;
        }
    }
}
