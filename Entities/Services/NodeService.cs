using Entities.Constants;
using Entities.Contracts;
using Entities.Loggers;
using System;

namespace Entities.Entities
{
    public abstract partial class Node : INode, IEquatable<INode>
    {
        public abstract void PrintStatus(ILogger logger);

        public abstract void Reset();

        public override string ToString()
        {
            return Label;
        }

        public bool Equals(INode node)
        {
            return Label.ToLower() == ((Node)node).Label.ToLower();
        }

        public bool Equals(string label)
        {
            return Label.ToLower() == label.ToLower();
        }

        public string FullToString(int amount)
        {
            return string.Join("", new String(TransmissionConstants.FullChar, amount));
        }
    }
}
