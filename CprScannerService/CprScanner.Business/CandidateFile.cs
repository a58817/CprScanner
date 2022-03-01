namespace CprScanner.Business
{
    using System.IO;

    public class CandidateFile : ICandidateFile
    {
        public Stream Stream { get; }

        public string Name { get; }

        public CandidateFile(Stream filestream, string fileName)
        {
            this.Stream = filestream;
            this.Name = fileName ?? string.Empty;
        }

        public string GetExtension()
        {
            return Path.GetExtension(this.Name)?.ToLower();
        }
    }
}
