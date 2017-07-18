using Entities.Loggers;

namespace Entities.Contracts
{
    public interface ITransmissionComponent
    {
        void PrintStatus(ILogger logger);
        string ToString();
        void Reset();
    }
}
