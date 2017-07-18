using System.Collections.Generic;
using System.Diagnostics;

namespace Entities.Entities
{
    [DebuggerDisplay("Node {Label}")]
    public abstract partial class Node
    {
        protected readonly string Label;
        protected List<Information> Information;

        public Node(string label, List<Information> informations)
        {
            Label = label;
            Information = informations;
        }
    }
}