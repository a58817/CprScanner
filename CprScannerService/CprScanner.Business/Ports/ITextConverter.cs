namespace CprScanner.Business.Ports
{
    using System.IO;

    public interface ITextConverter
    {
        string Convert(Stream fileStream);
    }
}
