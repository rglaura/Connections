using Entities.Contracts;
using System.Collections.Generic;
using System.Diagnostics;

namespace Entities.Entities
{
    [DebuggerDisplay("Info {_label}")]
    public partial class Information : ITransmissionComponent
    {
        private readonly string _label;
        private List<INode> _status;

        public Information(string label)
        {
            _label = label;
            _status = new List<INode>();
        }
    }
}