namespace CprScanner.Business.Ports
{
    public interface IBusinessLogger
    {
        void Error(string message);
        void Warning(string message);
        void Info(string message);
    }
}