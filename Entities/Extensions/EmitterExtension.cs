using Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    public static class EmitterExtension
    {
        public static void Reaches(this IEmitter origin, params IReceiver[] reachables)
        {
            origin.SetReachables(new List<IReceiver>(reachables));
        }
    }
}
