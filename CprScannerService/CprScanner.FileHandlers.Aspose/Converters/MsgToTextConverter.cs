namespace CprScanner.FileHandlers.Aspose.Converters
{
    using System.IO;
    using System.Text;
    using global::Aspose.Email;
    using CprScanner.Business.Ports;

    public class MsgToTextConverter : ITextConverter
    {
        public string Convert(Stream filestream)
        {
            var email = MailMessage.Load(filestream);

            var output = new StringBuilder();
            output.Append(email.Subject ?? string.Empty);
            output.Append(email.Body ?? string.Empty);
            output.Append(email.HtmlBodyText ?? string.Empty);

            return output.ToString();
        }
    }
}
