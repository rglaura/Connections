using Entities.Contracts;
using Entities.Loggers;
using System.Collections.Generic;

namespace Application.Extensions
{
    public static class LevelExtensions
    {
        public static void Reset<T>(this List<T> components) where T : ITransmissionComponent
        {
            foreach (var info in components)
            {
                info.Reset();
            }
        }

        public static void PrintComponents<T>(this List<T> components, ILogger logger) where T : ITransmissionComponent
        {
            foreach (var component in components)
            {
                component.PrintStatus(logger);
            }
        }
    }
}
