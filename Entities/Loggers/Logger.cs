using System;

namespace Entities.Loggers
{
    public abstract class Logger : ILogger
    {
        public abstract void PrintStatus(string message);

        public abstract void PrintSeparator(char separator);
    }
}