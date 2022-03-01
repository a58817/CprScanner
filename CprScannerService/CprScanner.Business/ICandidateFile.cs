namespace CprScanner.Business
{
    using System.IO;

    public interface ICandidateFile
    {
        Stream Stream { get; }
        string Name { get; }
        string GetExtension();
    }
}
