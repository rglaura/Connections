using Entities.Contracts;
using System.Collections.Generic;
using System.Diagnostics;

namespace Entities.Entities
{
    public partial class Emitter
    {
        private int _stock;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<IReceiver> _reachables = new List<IReceiver>();
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly List<Information> _initialInformations;

        public Emitter(string label, List<Information> informations)
            : base(label, informations)
        {
            _initialInformations = new List<Information>(informations);
            Initiate();
        }
    }
}