namespace Entities.Loggers
{
    public interface ILogger
    {
        void PrintStatus(string message);
        void PrintSeparator(char separator);
    }
}
