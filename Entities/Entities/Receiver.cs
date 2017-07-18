using System.Collections.Generic;
using System.Diagnostics;

namespace Entities.Entities
{
    public partial class Receiver
    {
        private int _capacity;
        private int _holes;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<Information> _visitBy;

        public Receiver(string label, int capacity, int holes)
            : base(label, new List<Information>())
        {
            _capacity = capacity;
            _holes = holes;
            _visitBy = new List<Information>();
        }
    }
}