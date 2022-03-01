namespace CprScanner.FileHandlers.Aspose.Converters
{
    using System.IO;
    using global::Aspose.Pdf;
    using global::Aspose.Pdf.Text;
    using CprScanner.Business.Ports;

    public class PdfTextObjectsToTextConverter : ITextConverter
    {
        public string Convert(Stream fileStream)
        {
            var pdfDocument = new Document(fileStream);
            var textAbsorber = new TextAbsorber();
            pdfDocument.Pages.Accept(textAbsorber);
            var plainText = textAbsorber.Text;
            return plainText;
        }
    }
}
